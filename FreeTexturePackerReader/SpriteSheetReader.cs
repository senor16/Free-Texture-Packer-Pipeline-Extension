using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FreeTexturePackerReader;

public class SpriteSheetReader : ContentTypeReader<SpriteSheet>
{
    protected override SpriteSheet Read(ContentReader input, SpriteSheet existingInstance)
    {
        var texture = input.ReadExternalReference<Texture2D>();
        var name = input.ReadString();
        var spriteSheet = new SpriteSheet(
            name,
            texture
        );

        var json = input.ReadString();
        var sheet = JsonSerializer.Deserialize<Sheet>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (sheet != null)
        {
            foreach (SpriteFrame s in sheet.frames)
            {
                var sprite = new SpriteFrame();
                sprite.filename = s.filename;
                sprite.rotated = s.rotated;
                sprite.trimmed = s.trimmed;
                sprite.texture = texture;
                // frame
                sprite.frame = s.frame;
                // spriteSourceSize
                sprite.spriteSourceSize = s.spriteSourceSize;
                // sourceSize
                sprite.sourceSize = s.sourceSize;
                // pivot
                sprite.pivot = s.pivot;

                if (sprite.rotated)
                    sprite.sourceRect = new Rectangle(sprite.frame.X, sprite.frame.Y, sprite.frame.Height, sprite.frame.Width);
                else
                    sprite.sourceRect = new Rectangle(sprite.frame.X, sprite.frame.Y, sprite.frame.Width, sprite.frame.Height);
                sprite.origin = new Vector2(sprite.pivot.X * sprite.frame.Width, sprite.pivot.Y * sprite.frame.Width);

                spriteSheet.AddSprite(sprite.filename, sprite);
            }
        }

        return spriteSheet;
    }
}