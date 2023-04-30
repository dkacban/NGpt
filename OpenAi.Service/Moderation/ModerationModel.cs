namespace NGpt.Services.Moderation;

public enum ModerationModel
{
    [System.ComponentModel.Description("text-moderation-stable")]
    TextModerationStable,

    [System.ComponentModel.Description("text-moderation-latest")]
    TextModerationLatest,
}
