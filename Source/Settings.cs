namespace Celeste.Mod.OverworldHelper;

public class Settings : EverestModuleSettings
{
    [SettingNeedsRelaunch]
    [SettingName("OverworldHelper_Settings_Enabled")]
    public bool Enabled { get; set; } = true;
}