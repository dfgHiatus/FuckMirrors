using HarmonyLib;
using NeosModLoader;
using UnityNeos;

namespace FuckMirrors
{
    public class FuckMirrors : NeosMod
    {
        public override string Name => "FuckMirrors";
        public override string Author => "dfgHiatus";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/dfgHiatus/FuckMirrors/";
        private static ModConfiguration config;

        [AutoRegisterConfigKey]
        public static ModConfigurationKey<bool> enabled = new ModConfigurationKey<bool>("enabled", "Enabled", () => true);

        public override void OnEngineInit()
        {
            config = GetConfiguration();
            new Harmony("net.dfgHiatus.FuckMirrors").PatchAll();
        }

        [HarmonyPatch(typeof(CameraConnector), "ApplyChanges")]
        public class CameraConnectorPatch
        {
            public static bool Prefix(CameraConnector __instance)
            {
                if (!config.GetValue(enabled))
                    return true;
                else
                    return __instance.Owner.World.Focus == FrooxEngine.World.WorldFocus.PrivateOverlay;
            }
        }

        [HarmonyPatch(typeof(CameraPortalConnector), "ApplyChanges")]
        public class CameraPortalPatch
        {
            public static bool Prefix(CameraPortalConnector __instance)
            {
                if (!config.GetValue(enabled))
                    return true;
                else
                    return __instance.Owner.World.Focus == FrooxEngine.World.WorldFocus.PrivateOverlay;
            }
        }
    }
}