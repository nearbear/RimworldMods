using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using RunescapeLevelUp.Core;

#pragma warning disable IDE0051 // Remove unused private members

namespace RunescapeLevelUp.Patch
{
    [HarmonyPatch(typeof(SkillRecord), "Learn")]
    internal static class Rimworld_SkillRecord_Learn
    {
        private static bool Prefix(SkillRecord __instance, float xp, bool direct = false)
        {
			float __xp = xp;

			// First we've got to calculate whether the XP would level us up
			if (__instance.levelInt == 20)
            {
				return true;
            }
			if (__instance.TotallyDisabled)
			{
				return true;
			}
			if (__xp < 0f && __instance.levelInt == 0)
			{
				return true;
			}
			if (__xp > 0f)
			{
				__xp *= __instance.LearnRateFactor(direct);
			}

			// On level up, this if block runs
			if (__instance.xpSinceLastLevel + __xp >= __instance.XpRequiredForLevelUp)
			{
				LevelUpTracker.PawnLeveledUp(__instance.Pawn, __instance);
			}
			return true;
		}
    }
}
