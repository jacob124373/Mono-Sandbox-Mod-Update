# Compatibility update for current Gorilla Tag

Verified against the current Assembly-CSharp.dll and Utilla 1.7.2 (metadata check, not a runtime test).

- `GorillaLocomotion.Player` -> `GorillaLocomotion.GTPlayer` (all uses, plus the Harmony patch on `GetSlidePercentage`, whose signature is unchanged: `float GetSlidePercentage(RaycastHit)`)
- `Player.rightControllerTransform` -> `GTPlayer.Instance.RightHand.controllerTransform`
- Utilla attributes moved to the `Utilla.Attributes` namespace (added `using Utilla.Attributes;`)
- `GorillaTagger.Start` patch: palm bone lookup is now null-safe with a recursive search and fallback
- Removed unused `BepInEx.Harmony` reference from the csproj
