using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace FreeTexturePackerPipeline;

[ContentImporter(".spritesheet", DisplayName = "Sprite Sheet Importer", DefaultProcessor = nameof(SpriteSheetProcessor))]
public class SpriteSheetImporter : ContentImporter<SpriteSheetContent>
{
    public override SpriteSheetContent Import(string filename, ContentImporterContext context)
    {
        context.Logger.LogMessage(filename);
        string getJson = File.ReadAllText(filename);
        
        var asset = JsonSerializer.Deserialize<SpriteSheetAsset>(
            getJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (asset == null)
        {
            throw new ArgumentException("Argument is null", nameof(filename));
        }
        context.Logger.LogMessage("Importing Dependency " + asset.texture);
        asset.texture = Path.Combine((Path.GetDirectoryName(filename) ?? string.Empty), asset.texture);
        context.AddDependency(asset.texture);
        context.Logger.LogMessage("Importing finished " + asset.texture);
        return new SpriteSheetContent() { Identity = new ContentIdentity(filename), Asset=asset, Json = getJson };
    }
}
