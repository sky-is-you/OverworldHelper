using MonoMod.ModInterop;

namespace Celeste.Mod.OverworldHelper;

public class OverworldHelperModule : EverestModule {
    public static OverworldHelperModule Instance { get; private set; }

    public OverworldHelperModule()
    {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(OverworldHelperModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(OverworldHelperModule), LogLevel.Info);
#endif
    }

    public override void Load()
    {
        OverworldTracker.Initialize();
        typeof(OverworldHelperExports).ModInterop();
    }

    public override void Unload()
    {
        OverworldTracker.Unload();
    }
}