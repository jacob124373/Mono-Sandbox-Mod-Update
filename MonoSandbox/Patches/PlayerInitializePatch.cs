using HarmonyLib;
using UnityEngine;

namespace MonoSandbox.Patches
{
    [HarmonyPatch(typeof(GorillaTagger), "Start")]
    public class PlayerInitializePatch
    {
        public static void Postfix(GorillaTagger __instance)
        {
            RefCache.LHand = FindHand(__instance.offlineVRRig.leftHandTransform, "palm.01.L");
            RefCache.RHand = FindHand(__instance.offlineVRRig.rightHandTransform, "palm.01.R");
        }

        // The rig hierarchy has changed between game versions, so look for the palm bone
        // anywhere under the hand's parent and fall back to the hand transform itself.
        private static GameObject FindHand(Transform handTransform, string boneName)
        {
            Transform root = handTransform.parent != null ? handTransform.parent : handTransform;
            Transform bone = root.Find(boneName) ?? FindDeep(root, boneName);

            if (bone == null)
            {
                Debug.LogWarning($"[MonoSandbox] Couldn't find '{boneName}', attaching to '{handTransform.name}' instead");
                bone = handTransform;
            }

            return bone.gameObject;
        }

        private static Transform FindDeep(Transform parent, string name)
        {
            foreach (Transform child in parent)
            {
                if (child.name == name) return child;
                Transform found = FindDeep(child, name);
                if (found != null) return found;
            }
            return null;
        }
    }
}
