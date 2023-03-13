# AI-Tweaks

Fully configurable in the f12 menu while in-game. Settings take effect immediately
Bots will only fullauto/burst at close range
Distance based scaling for fire-rate.
Recoil Total on the gun a bot using affects fire-rate and the distance to swap to full-auto or semi-auto
Difficulty Overrides Enabled by default to circumvent default settings and any server mods. With this enabled, changing the semiauto firerate in POOP or Fin's AI Tweaks will have no effect.
!Overrides will only work if the distance scaling feature is enabled!

HUGE Thanks to DrakiaXYZ, gaylatea, kiobu-kouhai, JustNU, Props, SSH, Fontaine, and everyone else in the SPTarkov discord for their help in learning to put this together and their help with my many dumb questions. It would have taken ages to figure out on my own.

v0.3 WILL NOT WORK ON 3.5.0, use v0.1 instead for now.

Current Features:

1. Swaps the rate of fire on a bot's gun depending on the distance to the target being shot.
   By default, bots only fire in quick bursts even if at point blank range or at maximum range. Their recoil settings change how fast they fire those bursts depending on distance.
   Ignores guns that are not capable of select fire.
   Default threshold is 30 meters or less, swap to full-auto. If over 35 meters, swap to single fire. The gap between the two is to prevent the bots from rapidly swapping between if a target is right at the threshold.
   This prevent bots from rapid firing accurate bursts at long range, but lets them fire fullauto at short range.

2. Distance Scaling for Semi-auto fire-rate for all bots.
   Unlocks AI logic previously reserved for sniper scavs.
   Adds additional wait time per shot based on the distance to the target.

3. Recoil Scaling for both Firemode Swap Distance and Fire-rate Scaling features.
   The Recoil "Total" (horizontal + vertical) modifies the distance that bots will swap between fullauto and semiauto. So when using a gun such as an mp5, they will swap to semi auto at a further distance, and stay on full auto up to a longer distance.
   For distance scaling, the higher the recoil of their gun, the more time they will wait between shots, up to the maximum or minimum wait time as set in the difficulty overrides section.

4. Difficulty Overrides
   Added toggles to circumvent difficulty settings for all bots globally. This lets you configure their "wait time" while in-game and allows the use of AI server mods that change other behavior settings without conflicting.
   If you use Fin's AI Tweaks or POOP, I recommend setting their "recoil" difficulty setting in their config to something very low. This setting is unreliable at best of times, and I could never achieve good results from modifying it - hense the desire to create this feature.
   I will add more difficulty overrides in the future to circumvent the pseudo recoil settings that bots use.
   !Overrides will only work if the distance scaling feature is enabled!

Note: There needs to be adjustments to bot behavior configs currently to achieve true fullauto fire at close range, this will be controlled through this mod in the future.

To-do:
More Difficulty Overrides
Modifications to how bot's reaction time is calculated at close range to get them reacting faster in close quarters such as dorms.
and much more
