using System;
using Celeste.Mod.Meta;
using MonoMod.ModInterop;

namespace Celeste.Mod.OverworldHelper;

[ModExportName("OverworldHelper")]
public static class OverworldHelperExports
{
    // events
    public static void SubscribeToAreaChanged(Action<AreaKey> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.AreaChanged += callback;
    }

    public static void UnsubscribeFromAreaChanged(Action<AreaKey> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.AreaChanged -= callback;
    }

    public static void SubscribeToAreaChangedID(Action<int> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.AreaChangedID += callback;
    }

    public static void UnsubscribeFromAreaChangedID(Action<int> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.AreaChangedID -= callback;
    }

    public static void SubscribeToOverworldCreated(Action<Overworld> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.OverworldCreated += callback;
    }

    public static void UnsubscribeFromOverworldCreated(Action<Overworld> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.OverworldCreated -= callback;
    }

    public static void SubscribeToVanillaOverworldCreated(Action<Overworld> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.VanillaOverworldCreated += callback;
    }

    public static void UnsubscribeFromVanillaOverworldCreated(Action<Overworld> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.VanillaOverworldCreated -= callback;
    }

    public static void SubscribeToCustomOverworldCreated(Action<Overworld> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.CustomOverworldCreated += callback;
    }

    public static void UnsubscribeFromCustomOverworldCreated(Action<Overworld> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.CustomOverworldCreated -= callback;
    }

    public static void SubscribeToTitleScreenEntry(Action<OuiTitleScreen> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.TitleScreenEntry += callback;
    }

    public static void UnsubscribeFromTitleScreenEntry(Action<OuiTitleScreen> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.TitleScreenEntry -= callback;
    }

    public static void SubscribeToTitleScreenExit(Action<OuiTitleScreen> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.TitleScreenExit += callback;
    }

    public static void UnsubscribeFromTitleScreenExit(Action<OuiTitleScreen> callback)
    {
        ArgumentNullException.ThrowIfNull(callback);
        OverworldTracker.TitleScreenExit -= callback;
    }
    // shorthand stuff i'd like to not write a bunch
    public static Overworld GetOverworld() => OverworldTracker.CurrentOverworld;
    public static bool GetOverworldIsVanilla() => OverworldTracker.OverworldIsVanilla;
    public static AreaKey GetAreaKeyFromID(int id) => GetAreaDataFromID(id).ToKey();
    public static AreaData GetAreaDataFromID(int id) => AreaData.Areas[id];
    public static AreaKey? FindAreaKeyFromString(string area) => FindAreaDataFromString(area)?.ToKey();
    public static AreaData FindAreaDataFromString(string area)
    {
        int? areaID = CustomConfig.FindAreaIDByName(area);
        return (areaID != null) ? GetAreaDataFromID(areaID.Value) : null;
    }
    
    // get configs
    public static MapMeta GetConfigFromAreaID(int areaID, Type type) => CustomConfig.GetConfig(areaID, type);
    public static MapMeta GetConfigFromAreaKey(AreaKey area, Type type) => GetConfigFromAreaID(area.ID, type);
    public static MapMeta GetConfigFromAreaData(AreaData area, Type type) => GetConfigFromAreaID(area.ID, type);

    public static MapMeta FindConfigFromString(string area, Type type)
    {
        AreaData areaData = FindAreaDataFromString(area);
        return (areaData != null) ? GetConfigFromAreaData(areaData, type) : null;
    }
}