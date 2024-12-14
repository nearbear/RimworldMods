using HarmonyLib;
using System.Diagnostics;
using Verse;

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
            Harmony.DEBUG = true;
#endif
            harmony.PatchAll();

            Log("Initialized");

            settings = GetSettings<Settings>();
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

        public override void ExposeData()
        {
            base.ExposeData();
        }
    }
}