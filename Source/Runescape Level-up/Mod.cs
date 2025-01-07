using HarmonyLib;
using System.Diagnostics;
using Verse;
using UnityEngine;

namespace RunescapeLevelUp
{
    public class Mod : Verse.Mod
    {
        public const string Id = "RunescapeLevelUp";
        public const string Name = "Runescape Level-up";
        public const string Version = "1.1";

        public static Mod Instance { get; private set; }
        public static Settings settings { get; private set; }

        public Mod(ModContentPack content) : base(content)
        {
            Instance = this;

            Harmony harmony = new Harmony(Id);
#if DEBUG
            Log("Debug is enabled");
            Harmony.DEBUG = true;
#endif
            harmony.PatchAll();

            Log("Initialized");

            settings = GetSettings<Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("Volume Percent");
            settings.volumePercent = listingStandard.Slider(settings.volumePercent, 0f, 100f);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Runescape Level-Up";
        }

        public static void Log(string message)
        {
#if DEBUG
            Verse.Log.Message(PrefixMessage(message));
#endif
        }

        private static string PrefixMessage(string message) => $"[{Name} v{Version}] {message}";
    }

    public class Settings : ModSettings
    {
        public float volumePercent = 100f;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref volumePercent, "volumePercent", 100f);

            base.ExposeData();
        }
    }
}