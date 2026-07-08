using System;
using MonoMod.ModInterop;

namespace Celeste.Mod.OverworldHelper;

public class OverworldHelperModule : EverestModule {
    public static OverworldHelperModule Instance { get; private set; }

    public override Type SettingsType => typeof(Settings);
    public static Settings Settings => (Settings) Instance._Settings;
    public static bool Enabled => Settings.Enabled;
    
    public OverworldHelperModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(OverworldHelperModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(OverworldHelperModule), LogLevel.Info);
#endif
    }
    public static OverworldTracker Tracker;

    public override void Load() {
        if (Enabled) Tracker = new();
        typeof(OverworldHelperExports).ModInterop();
    }

    public override void Unload()
    {
        Tracker.Unload();
    }
}