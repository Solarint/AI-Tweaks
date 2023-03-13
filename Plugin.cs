using BepInEx;
using BepInEx.Configuration;
using Comfort.Common;
using EFT;
using EFT.MovingPlatforms;
using EFT.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using EFT.InventoryLogic;
using System.Reflection;
using HarmonyLib;
using Aki.Reflection.Patching;

namespace SAIN
{
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    [BepInProcess("EscapeFromTarkov.exe")]
    public class SAIN : BaseUnityPlugin
    {
        private const String MOD_GUID = "me.sol.aitweaks";
        private const String MOD_NAME = "SAIN";
        private const String MOD_VERSION = "3.5.2";

        public static ConfigEntry<bool> Firemode { get; private set; }
        public static ConfigEntry<bool> SemiROF { get; private set; }

        public static ConfigEntry<float> RecoilModifier { get; private set; }
        public static ConfigEntry<bool> RecoilModifierDistanceToggle { get; private set; }
        public static ConfigEntry<bool> RecoilModifierFiremodeToggle { get; private set; }
        public static ConfigEntry<float> RecoilModifierFiremode { get; private set; }
        public static ConfigEntry<float> FiremodeRecoilBaseline { get; private set; }

        public static ConfigEntry<float> SemiDistance { get; private set; }
        public static ConfigEntry<float> AutoDistance { get; private set; }
        public static ConfigEntry<float> DistanceScalingStart { get; private set; }

        public static ConfigEntry<bool> MaxWaitOverride { get; private set; }
        public static ConfigEntry<bool> DistancePerMeterOverride { get; private set; }
        public static ConfigEntry<float> MaxWaitTime { get; private set; }
        public static ConfigEntry<float> MinWaitTime { get; private set; }
        public static ConfigEntry<float> WaitPerMeter { get; private set; }
        public static ConfigEntry<float> BaseWaitTime { get; private set; }
        public static ConfigEntry<float> RecoilModifierFirerate { get; private set; }
        public static ConfigEntry<float> FirerateRecoilBaseline { get; private set; }

        private void Awake()
            {
                string modeswap = "1. Firemode Swap Settings";

                Firemode = Config.Bind(modeswap, "1 Firemode Toggle", true, "Enables bots swapping to SemiAuto when shooting distant targets");               
                SemiDistance = Config.Bind(modeswap, "2 Semi Firemode Max Distance", 35f, "Maximum Distance where bots will swap to SemiAuto");
                AutoDistance = Config.Bind(modeswap, "3 Auto Firemode Min Distance", 30f, "Minimum Distance where bots will swap back to FullAuto");
                RecoilModifierFiremodeToggle = Config.Bind(modeswap, "4 Recoil for Distance", true, "Enables Recoil Modifiers for Firemode Swap Distance");
                RecoilModifierFiremode = Config.Bind(modeswap, "5 Recoil Modifier", 1f, "How Much the recoil total of a bots weapon and mods affects fire-rate");
                FiremodeRecoilBaseline = Config.Bind<float>(modeswap, "6 Total Recoil Baseline", 300f, new ConfigDescription("The amount of total recoil that serves as a baseline for when to start increasing or decreasing Firemode Swap Distance"));
                //FiremodeRecoilBaseline = Config.Bind<float>("Firemode Swap Settings", "Global ADS FOV Multi", 1f, new ConfigDescription("Applies On Top Of All Other ADS FOV Change Multies. Multiplier For The FOV Change When ADSing. Lower Multi = Lower FOV So More Zoom.", new AcceptableValueRange<float>(0.4f, 1.3f), new ConfigurationManagerAttributes { Order = 10 }));
                
                string scaling = "2. Distance Scaling Settings";

                SemiROF = Config.Bind(scaling, "2 Distance Scaling", true, "Distance Scaling Additional Wait Time Toggle");
                DistanceScalingStart = Config.Bind(scaling, "3 Distance Scaling Start", 20f, "Distance to start slowing down SemiAuto Firerate");
                RecoilModifierDistanceToggle = Config.Bind(scaling, "4 Recoil for Distance", true, "Enables Recoil Modifiers for SemiAuto distance scaling");
                RecoilModifierFirerate = Config.Bind(scaling, "5 Recoil Modifier", 1f, "How Much the recoil total of a bots weapon and mods affects fire-rate");
                FirerateRecoilBaseline = Config.Bind(scaling, "6 Total Recoil Baseline", 300f, new ConfigDescription("The amount of total recoil that serves as a baseline for when to start increasing or decreasing Firerate Scaling"));
                
                string diffoverride = "3. Difficulty Overrides";

                MaxWaitOverride = Config.Bind(diffoverride, "1 Override Max and Min Wait Time", true, new ConfigDescription("Override Maximum and Minimum Wait Time for fire-rate Difficulty Settings"));
                DistancePerMeterOverride = Config.Bind(diffoverride, "2 Distance Per Second Override", true, new ConfigDescription("Override Distance per Seconds of Wait Time for fire-rate Difficulty Setting"));
                BaseWaitTime = Config.Bind(diffoverride, "3 Base Wait Time", 0.1f, new ConfigDescription("Base Wait Time between shots for all distances"));
                MaxWaitTime = Config.Bind(diffoverride, "4 Maximum Wait Time", 3f, new ConfigDescription("Max Additional Time to Wait Between Shots"));
                MinWaitTime = Config.Bind(diffoverride, "5 Minimum Wait Time", 0.01f, new ConfigDescription("Minimum Additional Time to Wait Between Shots"));
                WaitPerMeter = Config.Bind(diffoverride, "6 Seconds Per X Meter", 100f, new ConfigDescription("For every X meters, add around 1 second of wait time between shots"));
                
                new Firemode().Enable();
                new SemiROF().Enable();
            }
    }
}