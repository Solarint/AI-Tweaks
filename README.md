# AI-Tweaks
Adjustments to AI Behaviors for SPTarkov

Current Features: 
1. Swaps the rate of fire on a bot's gun depending on the distance to the target being shot.
By default, bots only fire in quick bursts even if at point blank range or at maximum range. Their recoil settings change how fast they fire those bursts depending on distance.
+Ignores guns that are not capable of select fire.
+Default threshold is 20 meters or less, swap to full-auto. If over 20 meters, swap to single fire.
+This prevent bots from rapid firing accurate bursts at long range, but lets them fire quickly at short range.
+Needs adjustments to bot behavior configs currently to achieve true full auto fire at close range, this will be controlled through this mod in the future.
+Implemented overly complicated buffer zone to build off of later.

Todo:
Configurable distance threshold in f12 menu.
Configurable buffer zone where it doesn't attempt to change rate of fire to avoid rapid switching between fire modes. Currently defaults to +- 2 meters at the distance threshold.
Adjust bot semi auto rate of fire depending on distance. This is also controlled through bot behavior files, but those things are a nightmare.
+ much much more
