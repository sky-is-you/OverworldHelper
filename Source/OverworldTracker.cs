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
    public static event Action<OuiTitleScreen> TitleScreenEntry;
    public static event Action<OuiTitleScreen> TitleScreenExit;

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
        CurrentOverworld.OnEndOfFrame += () =>
        {
            CurrentOverworld.Entities.UpdateLists(); // register changes after OverworldCreated hooks
            OuiTitleScreen title = CurrentOverworld.Entities.FindFirst<OuiTitleScreen>();
            if (title != null) title.PostUpdate += PollTitle; // hook to title screen :D
        };
        menu.PostUpdate += PollArea;
    }

    private static float lastAlpha = -1f;
    private static Scene lastScene;
    private static bool lastEnterState = false;

    private static void PollTitle(Entity entity)
    {
        OuiTitleScreen title = (OuiTitleScreen)entity;
        if (title.alpha != lastAlpha)
        {
            bool enterstate = (title.alpha > lastAlpha);
            if (lastEnterState != enterstate)
                ( (lastEnterState = enterstate) ? TitleScreenEntry : TitleScreenExit )?.Invoke(title);
            lastAlpha = title.alpha;
        }
    }

    private static void PollArea(Entity entity)
    {
        OuiMainMenu menu = (OuiMainMenu)entity;
        if (menu.Overworld.Mountain.Area != lastAreaID)
        {
            if ((lastAreaID = menu.Overworld.Mountain.Area) >= 0)
                AreaChanged?.Invoke(AreaData.Areas[lastAreaID.Value].ToKey());
            AreaChangedID?.Invoke(lastAreaID.Value);
        }
    }

    private static void InvokeOverworldCreated(Overworld overworld) => OverworldCreated?.Invoke(overworld);
    
    public static void Initialize()
    {
        Everest.Events.MainMenu.OnCreateButtons += AttachToNewOverworld;
        VanillaOverworldCreated += InvokeOverworldCreated;
        CustomOverworldCreated += InvokeOverworldCreated;
    }

    public static void Unload()
    {
        VanillaOverworldCreated -= InvokeOverworldCreated;
        CustomOverworldCreated -= InvokeOverworldCreated;
        Everest.Events.MainMenu.OnCreateButtons += AttachToNewOverworld;
    }
}