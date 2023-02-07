using FreeTexturePackerReader;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace FreeTexturePackerPipeline;
[ContentTypeWriter]
public class SpriteSheetWsriter : ContentTypeWriter<SpriteSheetContent>
{    
    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    => typeof(SpriteSheetReader).AssemblyQualifiedName ?? string.Empty;

    protected override void Write(ContentWriter output, SpriteSheetContent value)
    {
        SpriteSheetAsset asset = value.Asset;
        ExternalReference<Texture2DContent> textureReference 
            = value.GetReference<Texture2DContent>(asset.texture);
        
        output.WriteExternalReference(textureReference);
        output.Write(asset.name);
        output.Write(value.Json);
    }
}