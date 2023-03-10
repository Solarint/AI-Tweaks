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
using Aki.Reflection.Patching;

namespace SAIN
{
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    [BepInProcess("EscapeFromTarkov.exe")]
    public class SAIN : BaseUnityPlugin
    {
        private const String MOD_GUID = "me.sol.aitweaks";
        private const String MOD_NAME = "SAIN";
        private const String MOD_VERSION = "0.2";

        public static ConfigEntry<bool> Firemode { get; private set; }
        public static ConfigEntry<bool> SemiROF { get; private set; }
        public static ConfigEntry<float> SemiDistance { get; private set; }
        public static ConfigEntry<float> AutoDistance { get; private set; }
        public static ConfigEntry<float> DistanceScalingStart { get; private set; }

        public void Awake()
            {
                Firemode = Config.Bind("Firemode Swap Settings", "Firemode Toggle", true);               
                SemiDistance = Config.Bind("Firemode Swap Settings", "Maximum Distance where bots will swap to Semi-Auto", 35f);
                AutoDistance = Config.Bind("Firemode Swap Settings", "Minimum Distance where bots will swap back to Full-Auto", 30f);

                SemiROF = Config.Bind("Semiauto Distance Scaling Settings", "Distance Scaling Toggle", true);
                DistanceScalingStart = Config.Bind("Semiauto Distance Scaling Settings", "Distance to start slowing down Semi-Auto Firerate", 30f);
                
                new Firemode().Enable();
                new SemiROF().Enable();
            }
    }
}