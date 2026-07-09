using System;
using System.Text;
using Celeste.Mod.Meta;
using YamlDotNet.Serialization;

namespace Celeste.Mod.OverworldHelper;

public class CustomConfig
{
    private static IDeserializer deserializer => YamlHelper.Deserializer;

    public static MapMeta GetConfig(int areaID, Type ty)
    {
        // arg checks
        if (areaID < 0 || areaID >= AreaData.Areas.Count)
            throw new ArgumentException("areaID must not be out of range");
        if (!ty.IsAssignableTo(typeof(MapMeta)))
            throw new ArgumentException("given config type must inherit from MapMeta class");

        AreaData area = AreaData.Areas[areaID]; // assume it exists if we're here :D
        // retrieve .meta.yaml file because everest doesn't parse our stuff
        ModAsset levelSetAsset = Everest.Content.Get("Maps/"+area.LevelSet);
        ModAsset areaMeta = levelSetAsset?.Children.Find(asset => asset.PathVirtual.EndsWith(area.SID+".meta"));
        if (areaMeta == null) return null;
        string confData = Encoding.UTF8.GetString(areaMeta.Data);
        return (MapMeta)deserializer.Deserialize(confData, ty); // parse and return as given type
    }
    public static int? FindAreaIDByName(string areaName) => FindAreaByName(areaName)?.ID;
    public static AreaData FindAreaByName(string areaName) => AreaData.Areas.Find(area => area.SID == areaName);
}