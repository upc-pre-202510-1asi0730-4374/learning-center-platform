using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.ValueObjects;

namespace ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;

public class VideoAsset: Asset
{
    public VideoAsset() : base(EAssetsType.Video)
    {
        VideoUri = null;
    }

    public VideoAsset(string videoUri) : base(EAssetsType.Video)
    {
        VideoUri = new Uri(videoUri);
    }
    
    public Uri? VideoUri { get; }

    public override bool Readable => false;
    public override bool Viewable => true;

    public override string GetContent()
    {
        return VideoUri != null ? VideoUri.AbsoluteUri : string.Empty;
    }
}