using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;

public class ImageAsset: Asset
{
    public ImageAsset() : base(EAssetsType.Image)
    {
        ImageUri = null;
    }

    public ImageAsset(string imageUrl) : base(EAssetsType.Image)
    {
        ImageUri = new Uri(imageUrl);
    }
    
    public Uri? ImageUri { get; }

    public override bool Readable => false;
    public override bool Viewable => true;

    public override string GetContent()
    {
        return ImageUri != null ? ImageUri.AbsoluteUri : string.Empty;
    }
}