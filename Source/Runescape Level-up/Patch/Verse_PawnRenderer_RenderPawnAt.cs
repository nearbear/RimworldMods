using System;
using HarmonyLib;
using UnityEngine;
using Verse;

using RunescapeLevelUp.Animation;
using RunescapeLevelUp.Core;

namespace RunescapeLevelUp.Patch
{
	[HarmonyPatch(typeof(PawnRenderer), "RenderPawnAt")]
	public class Verse_PawnRenderer_RenderPawnAt
	{
		public static void Postfix(PawnRenderer __instance, Pawn ___pawn, ref Vector3 drawLoc, Rot4? rotOverride = null, bool neverAimWeapon = false)
		{
			Graphic_LevelUp graphic = LevelUpTracker.GetLevelUpGraphicForPawn(___pawn);
			if (graphic != null)
            {
				graphic.DrawWorker(drawLoc, ___pawn.Rotation, ___pawn.def, ___pawn, 0);
            }
		}
	}
}
