using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content.Pipeline;

public abstract class ContentItem<T> : ContentItem
{
    private readonly Dictionary<string, ContentItem> _references = new();

    public T Asset{get;set;}

    protected ContentItem(){}
    public void AddReference<TContent>(ContentProcessorContext context,
        string filename,
        OpaqueDataDictionary processorParameters)
    {
        var sourceAsset = new ExternalReference<TContent>(filename);        
        var reference = context.BuildAsset<TContent, TContent>(sourceAsset, string.Empty, processorParameters, string.Empty, string.Empty);
        _references.Add(filename, reference);
    }

    public ExternalReference<TContent> GetReference<TContent>(string filename)
    {
        if (!_references.TryGetValue(filename, out ContentItem ContentItem))
        {
            throw new ArgumentException("No reference in content item", nameof(filename));
        }
        return (ExternalReference<TContent>)ContentItem;
    }
}