using System;
using System.Collections.Generic;
using RimWorld;
using RunescapeLevelUp.Animation;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace RunescapeLevelUp.Core
{
    class LevelUpTracker
    {
        private static Dictionary<Pawn, Graphic_LevelUp> tracker = new Dictionary<Pawn, Graphic_LevelUp>();

        private static string GetSkillNameForRecord(SkillRecord skillRecord)
        {
            string name = skillRecord.def.label;
            if (name == SkillDefOf.Animals.label) return "Animals";
            if (name == SkillDefOf.Artistic.label) return "Artistic";
            if (name == SkillDefOf.Construction.label) return "Construction";
            if (name == SkillDefOf.Cooking.label) return "Cooking";
            if (name == SkillDefOf.Crafting.label) return "Crafting";
            if (name == SkillDefOf.Intellectual.label) return "Intellectual";
            if (name == SkillDefOf.Medicine.label) return "Medicine";
            if (name == SkillDefOf.Melee.label) return "Melee";
            if (name == SkillDefOf.Mining.label) return "Mining";
            if (name == SkillDefOf.Plants.label) return "Plants";
            if (name == SkillDefOf.Shooting.label) return "Shooting";
            if (name == SkillDefOf.Social.label) return "Social";
            return "UnknownSkill";
        }

        private static bool ShouldPawnShowLevelUp(Pawn pawn)
        {
            return pawn.IsColonist && pawn.Spawned;
        }

        public static void PawnLeveledUp(Pawn pawn, SkillRecord skillRecord)
        {
            if (ShouldPawnShowLevelUp(pawn))
            {
                string skillName = GetSkillNameForRecord(skillRecord);

                Mod.Log(string.Format("Pawn leveled up {0}!", skillName));

                // LevelUp fireworks
                GetLevelUpGraphicForPawn(pawn).Play();

                // LevelUp Jingle
                SoundDef soundDef = DefDatabase<SoundDef>.GetNamed(skillName + "LevelUp");
                SoundInfo soundInfo = new TargetInfo(pawn.Position, pawn.Map, false);
                SoundStarter.PlayOneShot(soundDef, soundInfo);
            }
        }

        public static Graphic_LevelUp GetLevelUpGraphicForPawn(Pawn pawn)
        {
            if(!ShouldPawnShowLevelUp(pawn))
            {
                return null;
            }

            Graphic_LevelUp graphic;
            if (!tracker.ContainsKey(pawn))
            {
                graphic = new Graphic_LevelUp();
                graphic.Init();
                tracker.Add(pawn, graphic);
            }
            else
            {
                graphic = tracker[pawn];
            }

            return graphic;
        }
    }
}
