using Celeste.Mod.YourMod;
using MonoMod.ModInterop;

namespace Celeste.Mod.YourMod;

public class YourModModule : EverestModule {
    public static YourModModule Instance { get; private set; }

    public YourModModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(YourModModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(YourModModule), LogLevel.Info);
#endif
    }
    
    private void OnAreaChanged(AreaKey area)
    {
        CustomConfig.OurMapMeta meta = OverworldHelperImports.ReadConfig<CustomConfig.OurMapMeta>(area);
        if (meta.OurMeta != null)
        {
            Logger.Info("YourMod", "bool: " + meta.OurMeta.BoolValue);
            Logger.Info("YourMod", "float: " + meta.OurMeta.FloatValue);
            Logger.Info("YourMod", "posX: " + meta.OurMeta.PositionValue[0]);
            Logger.Info("YourMod", "posY: " + meta.OurMeta.PositionValue[1]);
            Logger.Info("YourMod", "posZ: " + meta.OurMeta.PositionValue[2]);
        }
        else Logger.Warn("YourMod", "OurMeta not found in map config!");
        // OR you can use meta however youd like...
    }

    public override void Load()
    {
        typeof(OverworldHelperImports).ModInterop();
        OverworldHelperImports.AreaChanged += OnAreaChanged;
    }

    public override void Unload()
    {
        OverworldHelperImports.AreaChanged -= OnAreaChanged;
    }
}