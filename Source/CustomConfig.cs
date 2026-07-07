using System;
using System.Runtime.CompilerServices;
using System.Text;
using Celeste.Mod.Meta;
using YamlDotNet.Serialization;
using On.Celeste.Mod.Helpers.LegacyMonoMod;

namespace Celeste.Mod.OverworldHelper;

public class CustomConfig
{
    private static IDeserializer deserializer => YamlHelper.Deserializer;

    private static MapMeta DeserializeConfig(string yamlData, Type ty)
    {
        return (MapMeta)deserializer.Deserialize(yamlData, ty);
    }

    public static MapMeta GetConfig(AreaKey area, Type ty)
    {
        if (!(typeof(MapMeta).IsAssignableFrom(ty))) throw new ArgumentException("given config type must inherit from MapMeta class");
        string sid = area.GetSID();
        ModAsset levelSet = Everest.Content.Get("Maps/"+area.GetLevelSet());
        if (levelSet != null)
        {
            ModAsset MapMeta = levelSet.Children.Find((ModAsset asset) =>
            {
                return asset.PathVirtual.StartsWith("Maps/" + sid) && asset.PathVirtual.EndsWith(".meta");
            });
            if (MapMeta != null)
            {
                string confData = Encoding.UTF8.GetString(MapMeta.Data);
                return DeserializeConfig(confData, ty);
            }
        }
        return null;
    }

    public static T GetConfig<T>(AreaKey area) where T : MapMeta
    {
        Type ty = typeof(T);
        return (T)GetConfig(area, ty);
    }
}