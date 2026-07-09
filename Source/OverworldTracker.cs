using System;
using System.Collections.Generic;
using Monocle;

namespace Celeste.Mod.OverworldHelper;

public static class OverworldTracker
{
    public static event Action<AreaKey> AreaChanged;
    public static event Action<int> AreaChangedID;
    public static event Action<Overworld> VanillaOverworldCreated;
    public static event Action<Overworld> CustomOverworldCreated;
    public static event Action<Overworld> OverworldCreated;
    public static event Action TitleScreenTriggered;

    public static Overworld CurrentOverworld;
    public static bool OverworldIsVanilla;
    
    private static int? lastAreaID;
    
    private static void AttachToNewOverworld(OuiMainMenu menu, List<MenuButton> buttons)
    {
        CurrentOverworld = menu.Overworld;
        // overworld cores should use custom type
        OverworldIsVanilla = typeof(Overworld).IsAssignableTo(CurrentOverworld.GetType());
        lastAreaID = null; // invoke area change
        ( OverworldIsVanilla ? VanillaOverworldCreated : CustomOverworldCreated )?.Invoke(CurrentOverworld);
        OverworldCreated?.Invoke(CurrentOverworld);
        menu.PostUpdate += PollArea;
    }
    
    private static void PollArea(Entity entity)
    {
        OuiMainMenu menu = (OuiMainMenu)entity;
        if (menu.Overworld.Mountain.Area != lastAreaID)
        {
            if ((lastAreaID = menu.Overworld.Mountain.Area) >= 0)
                AreaChanged?.Invoke(AreaData.Areas[lastAreaID.Value].ToKey());
            else TitleScreenTriggered?.Invoke();
            AreaChangedID?.Invoke(lastAreaID.Value);
        }
    }

    public static void Initialize()
    {
        Everest.Events.MainMenu.OnCreateButtons += AttachToNewOverworld;
    }

    public static void Unload()
    {
        Everest.Events.MainMenu.OnCreateButtons -= AttachToNewOverworld;
    }
}