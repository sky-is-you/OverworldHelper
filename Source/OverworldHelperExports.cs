using System;
using System.Reflection;
using Celeste.Mod.Meta;
using MonoMod.ModInterop;
using MonoMod.Utils;

namespace Celeste.Mod.OverworldHelper;

[ModExportName("OverworldHelper")]
public static class OverworldHelperExports
{
    public static void SubscribeToAreaChanged(Action<AreaKey> callback)
    {
        OverworldTracker.AreaChanged += callback;
    }
    public static void UnsubscribeFromAreaChanged(Action<AreaKey> callback)
    {
        OverworldTracker.AreaChanged -= callback;
    }
    public static void SubscribeToOverworldCreated(Action<Overworld> callback)
    {
        OverworldTracker.OverworldCreated += callback;
    }
    public static void UnsubscribeFromOverworldCreated(Action<Overworld> callback)
    {
        OverworldTracker.OverworldCreated -= callback;
    }
    public static Overworld GetOverworld()
    {
        return OverworldHelperModule.Tracker.currentOverworld;
    }
    public static MapMeta GetConfig(AreaKey area, Type type)
    {
        return CustomConfig.GetConfig(area, type);
    }
}