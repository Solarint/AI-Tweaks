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
using EFT.InventoryLogic;

    public class FireratePatch : ModulePatch
    {
        // Temporary debug dictionary, delete it and its usage in a production delivery
        static Dictionary<string, bool> botmap = new Dictionary<string, bool>();

        // Target the "Shoot" method of the class assigned to "ShootData" in the "BotOwner" class
        // This should hopefully be future proof
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotOwner)?.GetProperty("ShootData")?.PropertyType?.GetMethod("Shoot");
        }

        // Apply our patch before the "Shoot" method executes
        // The parameter gives us access to the member variable "botOwner_0" in GClass541 that would normally be private
        [PatchPrefix]
        public static void PatchPrefix(ref BotOwner ___botOwner_0)
        {
            // If aimingData is null, don't do anything
            if (___botOwner_0.AimingData == null)
            {
                return;
            }

            // Buffer zone for Range to target decision. If aimingData is < 22 and > 18, do nothing.
            // Will add to this later to adjust firerate in a linear way when on single fire to avoid abrupt changes in fire-rate
            if (___botOwner_0.AimingData.LastDist2Target < 22f && ___botOwner_0.AimingData.LastDist2Target > 18f)
            {
                Logger.LogInfo($"FireratePatch [{___botOwner_0.name}]: Adjusting Single Fire Rate Based on Distance");
                return;
            }

        // If the target weapon doesn't support both semiauto and fullauto, don't bother trying to change it
        Weapon weapon = ___botOwner_0.WeaponManager.CurrentWeapon;
            if (!weapon.WeapFireType.Contains(Weapon.EFireMode.single) || !weapon.WeapFireType.Contains(Weapon.EFireMode.fullauto))
            {
                if (!botmap.ContainsKey(___botOwner_0.name))
                {
                    Logger.LogInfo($"FireratePatch [{___botOwner_0.name}]: Weapon {weapon.Name.Localized(null)} fire types [{weapon.WeapFireType.CastToStringValue(", ")}], ignoring");
                    botmap.Add(___botOwner_0.name, true);
                }
                return;
            }

            // If the target is far away, and we're not already set to semi auto, switch to semi auto
            if (___botOwner_0.AimingData.LastDist2Target > 20f && weapon.SelectedFireMode != Weapon.EFireMode.single)
            {
                Logger.LogInfo($"FireratePatch [{___botOwner_0.name}]: Target is > 20f away, switching to single fire");
                weapon.FireMode.SetFireMode(Weapon.EFireMode.single);
            }

            // If the target is close by, and we're not already set to full auto, switch to full auto
            if (___botOwner_0.AimingData.LastDist2Target <= 20f && weapon.SelectedFireMode != Weapon.EFireMode.fullauto)
            {
                Logger.LogInfo($"FireratePatch [{___botOwner_0.name}]: Target is <= 20f away, switching to auto");
                weapon.FireMode.SetFireMode(Weapon.EFireMode.fullauto);
            }
        }
    }