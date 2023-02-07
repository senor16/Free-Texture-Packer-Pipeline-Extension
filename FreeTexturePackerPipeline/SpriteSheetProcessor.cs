using System;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace FreeTexturePackerPipeline;

[ContentProcessor(DisplayName = "Sprite Sheet Processor")]
class SpriteSheetProcessor : ContentProcessor<SpriteSheetContent, SpriteSheetContent>
{
    public override SpriteSheetContent Process(SpriteSheetContent input, ContentProcessorContext context)
    {        
        input.AddReference<Texture2DContent>(context,input.Asset.texture,new OpaqueDataDictionary());
        context.Logger.LogMessage("Processing finished "+ input.Identity.SourceFilename);
        return input;
    }

    public static void ValidateAsset(SpriteSheetAsset asset){
        if(asset.frames.Count<=0)
            throw new InvalidOperationException("Sheet has no frames");
    }
}
