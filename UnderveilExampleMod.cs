using BepInEx.Logging;
using HarmonyLib;
using UnderveilModLoader;

namespace UnderveilExampleMod
{
    public class UnderveilExampleMod : IMod
    {
        internal static ManualLogSource Log;

        public void OnLoad(ModContext context)
        {
            Log = context.Logger;
            Log.LogInfo("Hello from a mod");
            context.Harmony.PatchAll(typeof(UnderveilExampleMod).Assembly);
        }
    }

    [HarmonyPatch(typeof(GameManager), "Start")]
    internal static class GameManagerStartPatch
    {
        private static void Postfix() => UnderveilExampleMod.Log.LogInfo("GameManager.Start patched by UnderveilExampleMod");
    }
}
