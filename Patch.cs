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
using EFT.Game.Spawning;
using EFT.Interactive;
using UnityEngine.AI;

//Swaps Bot Firemode based on distance
public class Firemode : ModulePatch
    {
        //static Dictionary<string, bool> botmap = new Dictionary<string, bool>();

        // Target the "Shoot" method of the class assigned to "ShootData" in the "BotOwner" class
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotOwner)?.GetProperty("ShootData")?.PropertyType?.GetMethod("Shoot");
        }

        // The parameter gives us access to the member variable "botOwner_0" in GClass541 that would normally be private
        [PatchPrefix]
        public static void PatchPrefix(ref BotOwner ___botOwner_0)
        {
            if (___botOwner_0.AimingData == null)
            {
                return;
            }

        Weapon weapon = ___botOwner_0.WeaponManager.CurrentWeapon;
            if (!weapon.WeapFireType.Contains(Weapon.EFireMode.single) || !weapon.WeapFireType.Contains(Weapon.EFireMode.fullauto))
            {
                return;
            }

            float semidist = SAIN.SAIN.SemiDistance.Value;
            float autodist = SAIN.SAIN.AutoDistance.Value;

            if (SAIN.SAIN.RecoilModifierFiremodeToggle.Value == true)
                    {
                        semidist = (SAIN.SAIN.SemiDistance.Value) / ((weapon.RecoilTotal / SAIN.SAIN.FiremodeRecoilBaseline.Value) * SAIN.SAIN.RecoilModifierFirerate.Value);
                        autodist = (SAIN.SAIN.AutoDistance.Value) / ((weapon.RecoilTotal / SAIN.SAIN.FiremodeRecoilBaseline.Value) * SAIN.SAIN.RecoilModifierFirerate.Value);
                    };

            if (___botOwner_0.AimingData.LastDist2Target > semidist && weapon.SelectedFireMode != Weapon.EFireMode.single)
            {
                Logger.LogDebug($"FireratePatch [{___botOwner_0.name}]: Target is > 20f away, switching to single fire");
                weapon.FireMode.SetFireMode(Weapon.EFireMode.single);
            }

            if (___botOwner_0.AimingData.LastDist2Target <= autodist && weapon.SelectedFireMode != Weapon.EFireMode.fullauto)
            {
                Logger.LogDebug($"FireratePatch [{___botOwner_0.name}]: Target is <= 20f away, switching to auto");
                weapon.FireMode.SetFireMode(Weapon.EFireMode.fullauto);
            }
        }
    }

//Allows all bots to use Sniper Scav Logic for scaling fire-rate at distance.
public class SemiROF : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
            {
                return typeof(BotOwner)?.GetProperty("ShootData")?.PropertyType?.GetMethod("method_5", BindingFlags.Instance | BindingFlags.NonPublic);
            }

        [PatchPrefix]
         public static bool PatchPrefix(ref BotOwner ___botOwner_0, ref float ___float_4)
            {
                Weapon weapon = ___botOwner_0.WeaponManager.CurrentWeapon;
                
                float recoil = 1;
                float maxwait = ___botOwner_0.Settings.FileSettings.Shoot.WAIT_NEXT_SINGLE_SHOT_LONG_MAX;
                float minwait = ___botOwner_0.Settings.FileSettings.Shoot.WAIT_NEXT_SINGLE_SHOT_LONG_MIN;
                float num = ___botOwner_0.WeaponManager.WeaponAIPreset.HoldTriggerUpTime();
                float permeter = ___botOwner_0.Settings.FileSettings.Shoot.MARKSMAN_DIST_SEK_COEF;
    
                if (SAIN.SAIN.RecoilModifierDistanceToggle.Value == true)
                    {
                        recoil = (weapon.RecoilTotal / SAIN.SAIN.FirerateRecoilBaseline.Value) * SAIN.SAIN.RecoilModifierFirerate.Value;
                    };
    
                if (SAIN.SAIN.MaxWaitOverride.Value == true)
                    {
                        maxwait = SAIN.SAIN.MaxWaitTime.Value;
                        minwait = SAIN.SAIN.MinWaitTime.Value;
                        num = SAIN.SAIN.BaseWaitTime.Value;
                    };
    
                if (SAIN.SAIN.DistancePerMeterOverride.Value == true)
                    {
                        permeter = SAIN.SAIN.WaitPerMeter.Value;
                    };
    
			    //Wait Time Logic
                if (___botOwner_0.AimingData.LastDist2Target > SAIN.SAIN.DistanceScalingStart.Value)
                    {
                        num = Mathf.Clamp((___botOwner_0.AimingData.RealTargetPoint - ___botOwner_0.Transform.position).magnitude / (permeter / recoil), minwait, maxwait);
                    }
                ___float_4 = Time.time + num * UnityEngine.Random.Range(0.5f, 1.5f);
                Logger.LogDebug($"Sniper Logic [{___botOwner_0.name}]: Enabling Sniper Logic for fire-rate");
                return false;
            }    
    }