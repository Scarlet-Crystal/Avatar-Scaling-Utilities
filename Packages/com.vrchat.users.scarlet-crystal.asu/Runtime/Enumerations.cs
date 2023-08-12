namespace AvatarScalingUtilities
{
    public enum ScalingActions
    {
        None,
        SetEyeHeight,
        LimitEyeHeight,
        RemapEyeHeight,
        RemapAndLimitEyeHeight,
        MultiplyPrefabHeight
    }

    public enum ScalingLimits
    {
        Keep,
        Restrict,
        Reset
    }
}