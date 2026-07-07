using System;
using Celeste.Mod.Meta;
using MonoMod.ModInterop;

namespace Celeste.Mod.CustomConfigExample;

[ModImportName("OverworldHelper")]
public static class OverworldHelperImports
{
    public static Action<Action<AreaKey>> SubscribeToAreaChanged;
    public static Action<Action<AreaKey>> UnsubscribeFromAreaChanged;
    public static Action<Action<Overworld>> SubscribeToOverworldCreated;
    public static Action<Action<Overworld>> UnsubscribeFromOverworldCreated;
    public static Func<Overworld> GetOverworld;

    [Obsolete("GetConfig is deprecated and will be removed in the next breaking update, use GetConfigFromArea or use GetConfigFromString")]    public static Func<AreaKey, Type, MapMeta> GetConfigFromArea;
    public static Func<string, Type, MapMeta> GetConfigFromString;

    public static event Action<AreaKey> AreaChanged
    {
        add => SubscribeToAreaChanged(value);
        remove => UnsubscribeFromAreaChanged(value);
    }
    public static T ReadConfig<T>(AreaKey area) where T : MapMeta
    {
        return (T)GetConfigFromArea(area, typeof(T));
    }
    
    public static T ReadConfig<T>(string area) where T : MapMeta
    {
        return (T)GetConfigFromString(area, typeof(T));
    }

    public static event Action<Overworld> OverworldLoaded
    {
        add => SubscribeToOverworldCreated(value);
        remove => UnsubscribeFromOverworldCreated(value);
    }
}