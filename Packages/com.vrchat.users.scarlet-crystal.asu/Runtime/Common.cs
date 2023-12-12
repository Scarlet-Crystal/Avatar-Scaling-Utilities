using VRC.SDKBase;
using UnityEngine;

namespace AvatarScalingUtilities
{
    static class Common
    {
        public static void ExecuteAction(VRCPlayerApi player, ScalingActions action, ScalingLimits limit, float minOrHeightOrMultiplier, float max, AnimationCurve curve)
        {
            if(!Utilities.IsValid(player) || !player.isLocal)
            {
                return;
            }

            float e;
            switch (action)
            {
                case ScalingActions.None:
                    break;

                case ScalingActions.LimitEyeHeight:
                    e = FilterEyeHeight(player.GetAvatarEyeHeightAsMeters(), minOrHeightOrMultiplier, max);
                    player.SetAvatarEyeHeightByMeters(e);
                    break;

                case ScalingActions.MultiplyPrefabHeight:
                    //Aug 9th 2023: If the player's eye height is set to exactly 1, then
                    // SetAvatarEyeHeightByMultiplier will fail.
                    player.SetAvatarEyeHeightByMultiplier(minOrHeightOrMultiplier);
                    break;

                case ScalingActions.SetEyeHeight:
                    player.SetAvatarEyeHeightByMeters(minOrHeightOrMultiplier);
                    break;

                case ScalingActions.RemapEyeHeight:
                    e = curve.Evaluate(player.GetAvatarEyeHeightAsMeters());
                    player.SetAvatarEyeHeightByMeters(e);
                    break;

                case ScalingActions.RemapAndLimitEyeHeight:
                    e = curve.Evaluate(player.GetAvatarEyeHeightAsMeters());
                    player.SetAvatarEyeHeightByMeters(FilterEyeHeight(e, minOrHeightOrMultiplier, max));
                    break;
            }

            switch (limit)
            {
                case ScalingLimits.Keep:
                    break;

                case ScalingLimits.Restrict:
                    ResetLimits(player);

                    if (action == ScalingActions.LimitEyeHeight || action == ScalingActions.RemapAndLimitEyeHeight)
                    {
                        player.SetAvatarEyeHeightMinimumByMeters(minOrHeightOrMultiplier);
                        player.SetAvatarEyeHeightMaximumByMeters(max);
                    }
                    else
                    {
                        player.SetManualAvatarScalingAllowed(false);
                    }

                    break;

                case ScalingLimits.Reset:
                    ResetLimits(player);
                    break;
            }
        }

        public static void HandleEyeHeightChanged(VRCPlayerApi player, bool avatarChanged, ScalingActions action, ScalingLimits limit, float minOrHeightOrMultiplier, float max, AnimationCurve curve)
        {
            if (limit == ScalingLimits.Restrict)
            {
                ScalingActions sa = avatarChanged ? action : FilterAction(action);
                ExecuteAction(player, sa, ScalingLimits.Keep, minOrHeightOrMultiplier, max, curve);
            }
        }

        private static ScalingActions FilterAction(ScalingActions action)
        {
            if (action == ScalingActions.RemapEyeHeight)
            {
                return ScalingActions.None;
            }

            if (action == ScalingActions.RemapAndLimitEyeHeight)
            {
                return ScalingActions.LimitEyeHeight;
            }

            return action;
        }

        private static void ResetLimits(VRCPlayerApi player)
        {
            player.SetManualAvatarScalingAllowed(true);
            player.SetAvatarEyeHeightMinimumByMeters(0f);
            player.SetAvatarEyeHeightMaximumByMeters(float.PositiveInfinity);
        }

        private static float FilterEyeHeight(float newEyeHeight, float min, float max)
        {
            if (newEyeHeight < min - 0.001f || newEyeHeight > max + 0.001f)
            {
                return Random.Range(Mathf.Max(min, 0.1f), Mathf.Min(max, 100f));
            }

            return newEyeHeight;
        }

        public static void UpdateCurve(AnimationCurve newCurve, ref AnimationCurve target)
        {
            target = newCurve ?? AnimationCurve.Constant(-1, 0, 0);
        }
    }
}