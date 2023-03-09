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

namespace AI_Tweaks
{
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    [BepInProcess("EscapeFromTarkov.exe")]

    public class AI_Tweaks : BaseUnityPlugin
    {
        private const String MOD_GUID = "me.sol.aitweaks";
        private const String MOD_NAME = "Solarint's AI Tweaks";
        private const String MOD_VERSION = "0.1";

        public static AI_Tweaks Instance { get; private set; }

        public void Awake()
            {
                new FireratePatch().Enable();
               // new RoF().Enable();
        }
    }
}