using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace FreeTexturePackerReader;

public class SpriteSheet
{
    public string Name { get; set; }
    public Dictionary<string, SpriteFrame> frames { get; set; }
    public Texture2D Texture { get; set; }
    public SpriteSheet(string pName, Texture2D pTexture)
    {
        frames = new Dictionary<string, SpriteFrame>();
        Name = pName;
        Texture = pTexture;
    }

    public void AddSprite(string spriteName, SpriteFrame sprite)
    {
        frames[spriteName] = sprite;
    }

    public SpriteFrame? GetSprite(string filename)
    {
        if (frames[filename] != null)
            return frames[filename];
        return null;
    }
}