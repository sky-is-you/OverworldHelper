using Celeste.Mod.CustomConfigExample;
using MonoMod.ModInterop;

namespace Celeste.Mod.CustomConfigExample;

public class CustomConfigExampleModule : EverestModule {
    public static CustomConfigExampleModule Instance { get; private set; }

    public CustomConfigExampleModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(CustomConfigExampleModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(CustomConfigExampleModule), LogLevel.Info);
#endif
    }
    
    private void OnAreaChanged(AreaKey area)
    {
        CustomConfig.OurMapMeta meta = OverworldHelperImports.ReadConfig<CustomConfig.OurMapMeta>(area);
        if (meta.OurMeta != null)
        {
            Logger.Info("CustomConfigExample", "bool: " + meta.OurMeta.BoolValue);
            Logger.Info("CustomConfigExample", "float: " + meta.OurMeta.FloatValue);
            Logger.Info("CustomConfigExample", "posX: " + meta.OurMeta.PositionValue[0]);
            Logger.Info("CustomConfigExample", "posY: " + meta.OurMeta.PositionValue[1]);
            Logger.Info("CustomConfigExample", "posZ: " + meta.OurMeta.PositionValue[2]);
        }
        else Logger.Warn("CustomConfigExample", "OurMeta not found in map config!");
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