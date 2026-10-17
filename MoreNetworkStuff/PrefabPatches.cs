using System.Reflection;
using ColossalFramework;
using HarmonyLib;
using ICities;
using UnityEngine;

namespace MoreNetworkStuff
{
    // 针对 NetInfo 的补丁
    [HarmonyPatch(typeof(NetInfo))]
    [HarmonyPatch("InitializePrefab")] // 拦截预制体初始化
    public static class NetInfo_InitializePrefab_Patch
    {
        [HarmonyPrefix]
        public static void Prefix(NetInfo __instance)
        {
            // 根据当前游戏模式，执行原来的逻辑
            var mode = LoadingExtension.CurrentMode;

            if (mode == AppMode.Game || mode == AppMode.MapEditor)
            {
                LoadingExtension.OnPreInitializationInGameAndMapEditor(__instance);
            }

            if (mode == AppMode.AssetEditor)
            {
                LoadingExtension.OnPreInitializationAssetEditor(__instance);
            }
        }
    }

    // 针对 TransportInfo 的补丁
    [HarmonyPatch(typeof(TransportInfo))]
    [HarmonyPatch("InitializePrefab")]
    public static class TransportInfo_InitializePrefab_Patch
    {
        [HarmonyPrefix]
        public static void Prefix(TransportInfo __instance)
        {
            if (LoadingExtension.CurrentMode == AppMode.Game)
            {
                LoadingExtension.OnPreInitializationTI(__instance);
            }
        }
    }
}