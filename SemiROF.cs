using System.Reflection;
using Aki.Reflection.Patching;
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

public class RoF : ModulePatch
{
    // Temporary debug dictionary, delete it and its usage in a production delivery
    static Dictionary<string, bool> botmap = new Dictionary<string, bool>();

    protected override MethodBase GetTargetMethod()
    {
        return typeof(BotOwner)?.GetProperty("ShootData")?.PropertyType?.GetMethod("method_1");
    }

    [PatchPrefix]
    public static void PatchPrefix(ref BotOwner ___botOwner_0)
    {
           // if (___botOwner_0.AimingData == null)
          //  {
          //      return;
          //  }
            if (___botOwner_0.AimingData.LastDist2Target > 10f)
            {
                return ___botOwner_0.AimingData.LastDist2Target * 0.01 + 0.5;
            }
            return;
    }
}