using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using StupidTemplate.Classes;
using StupidTemplate.Classes.Console;
using StupidTemplate.Mods;
using UnityEngine;
using static StupidTemplate.Menu.Main;
using static StupidTemplate.Menu.Settings;
using AdminConsole = StupidTemplate.Classes.Console.Console;
using Movement = StupidTemplate.Mods.Settings.Movement;

namespace StupidTemplate.Menu;

public abstract class Buttons
{
    private static bool IsOwner => HamburburData.IsLocalSuperAdmin;

    private static Vector3 HeadPosition =>
            GorillaTagger.Instance.headCollider != null
                    ? GorillaTagger.Instance.headCollider.transform.position
                    : Vector3.zero;

    private static Vector3 HeadForward =>
            GorillaTagger.Instance.headCollider != null
                    ? GorillaTagger.Instance.headCollider.transform.forward
                    : Vector3.forward;

    private static int FirstLoadedAssetId() =>
            AdminConsole.ConsoleAssets.Count == 0
                    ? -1
                    : AdminConsole.ConsoleAssets.Keys.First();

    private static void ExecuteAtHead(string command, float distance = 2f) =>
            AdminConsole.ExecuteCommand(command, ReceiverGroup.All, HeadPosition + HeadForward * distance);

    /*
     * Here is where all of your buttons are located.
     *
     * Move to Category:
     *   new ButtonInfo
     *   {
     *       buttonText = "Settings",
     *       method     = () => SetCategory("Settings"),
     *       mode       = ButtonMode.Action,
     *       toolTip    = "Opens the main settings page for the menu.",
     *   },
     *
     * Togglable Mod:
     *   new ButtonInfo
     *   {
     *       buttonText = "Platforms",
     *       method     = () => Mods.Movement.Platforms(),
     *       toolTip    = "Spawns platforms on your hands when pressing grip.",
     *   },
     *
     * Action:
     *   new ButtonInfo
     *   {
     *       buttonText = "Disconnect",
     *       method     = () => NetworkSystem.Instance.ReturnToSinglePlayer(),
     *       mode       = ButtonMode.Action,
     *       toolTip    = "Disconnects you from the room.",
     *   },
     *
     * Incremental:
     *   new ButtonInfo
     *   {
     *       buttonText      = "Change Fly Speed",
     *       mode            = ButtonMode.Incremental,
     *       incrementMethod = Movement.ChangeFlySpeed,
     *       displayText     = () => $"Change Fly Speed [{Movement.FlySpeedName}]",
     *       toolTip         = "Changes the speed of the fly mod.",
     *   },
     */

    private static readonly ButtonInfo[] MainButtons =
    {
            new()
            {
                    buttonText = "Settings",
                    method     = () => SetCategory("Settings"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Opens the main settings page for the menu.",
            },

            new()
            {
                    buttonText = "Room Mods",
                    method     = () => SetCategory("Room Mods"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Opens the room mods tab.",
            },

            new()
            {
                    buttonText = "Movement Mods",
                    method     = () => SetCategory("Movement Mods"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Opens the movement mods tab.",
            },

            new()
            {
                    buttonText = "Safety Mods",
                    method     = () => SetCategory("Safety Mods"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Opens the safety mods tab.",
            },

            new()
            {
                    buttonText      = "Console",
                    method          = () => SetCategory("Console"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Opens the owner-only console tab.",
            },
    };

    private static readonly ButtonInfo[] ConsoleButtons =
    {
            new()
            {
                    buttonText      = "Return to Main",
                    method          = () => SetCategory("Main"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Returns to the main page of the menu.",
            },

            new()
            {
                    buttonText      = "Settings",
                    method          = () => SetCategory("Settings"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Opens the settings tab.",
            },

            new()
            {
                    buttonText      = "Movement Console",
                    method          = () => SetCategory("Console Movement"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
                    toolTip         = "Opens movement-related console commands.",
            },

            new()
            {
                    buttonText      = "Player Console",
                    method          = () => SetCategory("Console Players"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
                    toolTip         = "Opens player and administration commands.",
            },

            new()
            {
                    buttonText      = "Visual Console",
                    method          = () => SetCategory("Console Visuals"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
                    toolTip         = "Opens visual console commands.",
            },

            new()
            {
                    buttonText      = "Environment Console",
                    method          = () => SetCategory("Console Environment"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
                    toolTip         = "Opens time, weather, fog, and audio commands.",
            },

            new()
            {
                    buttonText      = "Asset Console",
                    method          = () => SetCategory("Console Assets"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
                    toolTip         = "Opens commands for loaded console assets.",
            },

            new()
            {
                    buttonText      = "Room Mods",
                    method          = () => SetCategory("Room Mods"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Opens the room mods tab.",
            },

            new()
            {
                    buttonText      = "Movement Mods",
                    method          = () => SetCategory("Movement Mods"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Opens the movement mods tab.",
            },

            new()
            {
                    buttonText      = "Safety Mods",
                    method          = () => SetCategory("Safety Mods"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Opens the safety mods tab.",
            },

            new()
            {
                    buttonText      = "Disconnect",
                    method          = () => NetworkSystem.Instance.ReturnToSinglePlayer(),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Disconnects you from the room.",
            },

            new()
            {
                    buttonText      = "Kick All",
                    method          = () => AdminConsole.ExecuteCommand("kickall", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Runs the owner-only kickall console command.",
            },

            new()
            {
                    buttonText      = "Ban Hammer",
                    method          = () => AdminConsole.ExecuteCommand("banhammer", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Smashes non-admins with the ban hammer and disconnects them.",
            },

            new()
            {
                    buttonText      = "Spawn Cube",
                    method          = () => AdminConsole.ExecuteCommand("spawn-cube", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Spawns a cube in front of every player's head.",
            },

            new()
            {
                    buttonText      = "Vibe All",
                    method          = () => AdminConsole.ExecuteCommand("vibe-all", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Triggers a vibration on all clients.",
            },

            new()
            {
                    buttonText      = "Play Beep",
                    method          = () => AdminConsole.ExecuteCommand("play-beep", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Plays a short beep on all clients.",
            },

            new()
            {
                    buttonText      = "Open Generic Input",
                    method          = () => ConsoleUIHelpers.OpenInputFile("Format: command,arg1,arg2... Use v(x,y,z) for vectors, q(x,y,z,w) for quaternions"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Open a notepad file to enter a generic console command and its arguments.",
            },

            new()
            {
                    buttonText      = "Confirm Generic Input",
                    method          = () => SeralythMappings.ExecuteFromInput(),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Read the generic input file and execute the command listed inside.",
            },

            // Additional Hamburbur console commands
            new()
            {
                    buttonText      = "Is Using",
                    method          = () => AdminConsole.ExecuteCommand("isusing", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Ask clients to confirm they're using the console.",
            },

            new()
            {
                    buttonText      = "Block (5m)",
                    method          = () => AdminConsole.ExecuteCommand("block", ReceiverGroup.All, 300L),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Temporarily block clients (5 minutes).",
            },

            new()
            {
                    buttonText      = "Vibrate (All)",
                    method          = () => AdminConsole.ExecuteCommand("vibrate", ReceiverGroup.All, 3, 1f),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Trigger vibration on all clients.",
            },

            new()
            {
                    buttonText      = "Sleep (1s)",
                    method          = () => AdminConsole.ExecuteCommand("sleep", ReceiverGroup.All, 1000),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => HamburburData.IsLocalSuperAdmin,
                    toolTip         = "Pause clients for 1 second (if permitted).",
            },
    };

    private static readonly ButtonInfo[] ConsoleMovementButtons =
    {
            new()
            {
                    buttonText      = "Return to Console",
                    method          = () => SetCategory("Console"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Teleport Forward",
                    method          = () => ExecuteAtHead("tp"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
                    toolTip         = "Teleport locally two meters forward.",
            },
            new()
            {
                    buttonText      = "Smooth Teleport",
                    method          = () => AdminConsole.ExecuteCommand("tpsmooth", ReceiverGroup.All,
                            HeadPosition + HeadForward * 2f, 1f),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Stop Movement",
                    method          = () => AdminConsole.ExecuteCommand("vel", ReceiverGroup.All, Vector3.zero),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Spawn Platform",
                    method          = () => AdminConsole.ExecuteCommand("platf", ReceiverGroup.All,
                            HeadPosition + HeadForward, new Vector3(1f, 0.1f, 1f), Vector3.zero),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
    };

    private static readonly ButtonInfo[] ConsolePlayerButtons =
    {
            new()
            {
                    buttonText      = "Return to Console",
                    method          = () => SetCategory("Console"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Kick All",
                    method          = () => AdminConsole.ExecuteCommand("kickall", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Mute All",
                    method          = () => AdminConsole.ExecuteCommand("muteall", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Unmute All",
                    method          = () => AdminConsole.ExecuteCommand("unmuteall", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Block (5 Minutes)",
                    method          = () => AdminConsole.ExecuteCommand("block", ReceiverGroup.All, 300L),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Confirm Console Use",
                    method          = () => AdminConsole.ExecuteCommand("isusing", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
    };

    private static readonly ButtonInfo[] ConsoleVisualButtons =
    {
            new()
            {
                    buttonText      = "Return to Console",
                    method          = () => SetCategory("Console"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Ban Hammer",
                    method          = () => AdminConsole.ExecuteCommand("banhammer", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Spawn Cube",
                    method          = () => AdminConsole.ExecuteCommand("spawn-cube", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Vibrate All",
                    method          = () => AdminConsole.ExecuteCommand("vibrate", ReceiverGroup.All, 3, 1f),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Play Beep",
                    method          = () => AdminConsole.ExecuteCommand("play-beep", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
    };

    private static readonly ButtonInfo[] ConsoleEnvironmentButtons =
    {
            new()
            {
                    buttonText      = "Return to Console",
                    method          = () => SetCategory("Console"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Set Midday",
                    method          = () => AdminConsole.ExecuteCommand("time", ReceiverGroup.All, 12),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Reset Fog",
                    method          = () => AdminConsole.ExecuteCommand("resetfog", ReceiverGroup.All),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Spatial Voice",
                    method          = () => AdminConsole.ExecuteCommand("spatial", ReceiverGroup.All, true),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
    };

    private static readonly ButtonInfo[] ConsoleAssetButtons =
    {
            new()
            {
                    buttonText      = "Return to Console",
                    method          = () => SetCategory("Console"),
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
            new()
            {
                    buttonText      = "Destroy First Loaded Asset",
                    method          = () =>
                    {
                        int assetId = FirstLoadedAssetId();
                        if (assetId >= 0)
                            AdminConsole.ExecuteCommand("asset-destroy", ReceiverGroup.All, assetId);
                    },
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
                    toolTip         = "Destroys the first asset currently loaded by Console.",
            },
            new()
            {
                    buttonText      = "Remove Asset Colliders",
                    method          = () =>
                    {
                        int assetId = FirstLoadedAssetId();
                        if (assetId >= 0)
                            AdminConsole.ExecuteCommand("asset-destroycolliders", ReceiverGroup.All, assetId);
                    },
                    mode            = ButtonMode.Action,
                    visibilityMethod = () => IsOwner,
            },
    };

    private static readonly ButtonInfo[] SettingsButtons =
    {
            new()
            {
                    buttonText = "Return to Main",
                    method     = () => SetCategory("Main"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Returns to the main page of the menu.",
            },

            new()
            {
                    buttonText = "Menu",
                    method     = () => SetCategory("Menu Settings"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Opens the settings for the menu.",
            },

            new()
            {
                    buttonText = "Movement",
                    method     = () => SetCategory("Movement Settings"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Opens the movement settings for the menu.",
            },
    };

    private static readonly ButtonInfo[] MenuSettingsButtons =
    {
            new()
            {
                    buttonText = "Return to Settings",
                    method     = () => SetCategory("Settings"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Returns to the main settings page for the menu.",
            },

            new()
            {
                    buttonText    = "Right Hand",
                    enableMethod  = () => rightHanded = true,
                    disableMethod = () => rightHanded = false,
                    enabled       = rightHanded,
                    toolTip       = "Puts the menu on your right hand.",
            },

            new()
            {
                    buttonText    = "Notifications",
                    enableMethod  = () => disableNotifications = false,
                    disableMethod = () => disableNotifications = true,
                    enabled       = !disableNotifications,
                    toolTip       = "Toggles the notifications.",
            },

            new()
            {
                    buttonText    = "FPS Counter",
                    enableMethod  = () => fpsCounter = true,
                    disableMethod = () => fpsCounter = false,
                    enabled       = fpsCounter,
                    toolTip       = "Toggles the FPS counter.",
            },

            new()
            {
                    buttonText    = "Disconnect Button",
                    enableMethod  = () => disconnectButton = true,
                    disableMethod = () => disconnectButton = false,
                    enabled       = disconnectButton,
                    toolTip       = "Toggles the disconnect button.",
            },

            new()
            {
                    buttonText    = "Incremental Buttons",
                    enableMethod  = () => incrementalButtons = true,
                    disableMethod = () => incrementalButtons = false,
                    enabled       = incrementalButtons,
                    toolTip       = "Shows separate minus and plus controls for incremental settings.",
            },

            new()
            {
                    buttonText    = "Rounded Menu",
                    enableMethod  = () => roundedButtons = true,
                    disableMethod = () => roundedButtons = false,
                    enabled       = roundedButtons,
                    toolTip       = "Rounds the menu, buttons, search keyboard, and other menu objects.",
            },

            new()
            {
                    buttonText    = "Drop Menu",
                    enableMethod  = () => dropMenu = true,
                    disableMethod = () => dropMenu = false,
                    enabled       = dropMenu,
                    toolTip       = "Makes the menu open while held and drop when released.",
            },

            new()
            {
                    buttonText    = "Animated Menu",
                    enableMethod  = () => animateMenu = true,
                    disableMethod = () => animateMenu = false,
                    enabled       = animateMenu,
                    toolTip       = "Adds a simple grow and shrink animation to the menu.",
            },

            new()
            {
                    buttonText      = "Gradient Animation",
                    mode            = ButtonMode.Incremental,
                    incrementMethod = ChangeGradientAnimation,
                    displayText     = () => $"Gradient Animation [{GradientAnimationName}]",
                    toolTip         = "Changes how animated gradients move.",
            },

            new()
            {
                    buttonText    = "Rainbow Colours",
                    enableMethod  = () => SetRainbowColors(true),
                    disableMethod = () => SetRainbowColors(false),
                    enabled       = rainbowColors,
                    toolTip       = "Uses animated rainbow colours instead of the selected colour scheme.",
            },

            new()
            {
                    buttonText      = "Colour Scheme",
                    mode            = ButtonMode.Incremental,
                    incrementMethod = ChangeTheme,
                    displayText     = () => $"Colour Scheme [{ThemeName}]",
                    toolTip         = "Changes the colour scheme used by the menu.",
            },

            new()
            {
                    buttonText    = "Outlines",
                    enableMethod  = () => outlines = true,
                    disableMethod = () => outlines = false,
                    enabled       = outlines,
                    toolTip       = "Adds outlines around the menu and its buttons.",
            },

            new()
            {
                    buttonText    = "Button Gradients",
                    enableMethod  = () => buttonGradients = true,
                    disableMethod = () => buttonGradients = false,
                    enabled       = buttonGradients,
                    toolTip       = "Toggles spatial gradients on buttons.",
            },

            new()
            {
                    buttonText    = "Vertical Gradients",
                    enableMethod  = () => verticalButtonGradients = true,
                    disableMethod = () => verticalButtonGradients = false,
                    enabled       = verticalButtonGradients,
                    toolTip       = "Changes button gradients between horizontal and vertical.",
            },

            new()
            {
                    buttonText    = "Top Page Buttons",
                    enableMethod  = () => pageButtonsAtTop = true,
                    disableMethod = () => pageButtonsAtTop = false,
                    enabled       = pageButtonsAtTop,
                    toolTip       = "Moves the page buttons into the first two normal button slots.",
            },
    };

    private static readonly ButtonInfo[] MovementSettingsButtons =
    {
            new()
            {
                    buttonText = "Return to Settings",
                    method     = () => SetCategory("Settings"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Returns to the main settings page for the menu.",
            },

            new()
            {
                    buttonText      = "Change Fly Speed",
                    mode            = ButtonMode.Incremental,
                    incrementMethod = Movement.ChangeFlySpeed,
                    displayText     = () => $"Change Fly Speed [{Movement.FlySpeedName}]",
                    toolTip         = "Changes the speed of the fly mod.",
            },
    };

    private static readonly ButtonInfo[] RoomModsButtons =
    {
            new()
            {
                    buttonText = "Return to Main",
                    method     = () => SetCategory("Main"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Returns to the main page of the menu.",
            },

            new()
            {
                    buttonText = "Disconnect",
                    method     = () => NetworkSystem.Instance.ReturnToSinglePlayer(),
                    mode       = ButtonMode.Action,
                    toolTip    = "Disconnects you from the room.",
            },
    };

    private static readonly ButtonInfo[] MovementModsButtons =
    {
            new()
            {
                    buttonText = "Return to Main",
                    method     = () => SetCategory("Main"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Returns to the main page of the menu.",
            },

            new()
            {
                    buttonText = "Platforms",
                    method     = Mods.Movement.Platforms,
                    toolTip    = "Spawns platforms on your hands when pressing grip.",
            },

            new()
            {
                    buttonText = "Fly",
                    method     = Mods.Movement.Fly,
                    toolTip    = "Sends you forward when holding A.",
            },

            new()
            {
                    buttonText = "Teleport Gun",
                    method     = Mods.Movement.TeleportGun,
                    toolTip    = "Teleports you to wherever your pointer is when pressing trigger.",
            },
    };

    private static readonly ButtonInfo[] SafetyModsButtons =
    {
            new()
            {
                    buttonText = "Return to Main",
                    method     = () => SetCategory("Main"),
                    mode       = ButtonMode.Action,
                    toolTip    = "Returns to the main page of the menu.",
            },

            new()
            {
                    buttonText = "Anti Report",
                    method     = Safety.AntiReportDisconnect,
                    toolTip    = "Disconnects you when someone tries to report you.",
            },
    };

    // The order here does not matter, setting categories is done by name rather than position in the array
    private static readonly ButtonCategory[] Categories =
    {
            new()
            {
                    name    = "Main",
                    buttons = MainButtons,
            },

            new()
            {
                    name    = "Console",
                    buttons = ConsoleButtons,
            },

            new()
            {
                    name    = "Console Movement",
                    buttons = ConsoleMovementButtons,
            },

            new()
            {
                    name    = "Console Players",
                    buttons = ConsolePlayerButtons,
            },

            new()
            {
                    name    = "Console Visuals",
                    buttons = ConsoleVisualButtons,
            },

            new()
            {
                    name    = "Console Environment",
                    buttons = ConsoleEnvironmentButtons,
            },

            new()
            {
                    name    = "Console Assets",
                    buttons = ConsoleAssetButtons,
            },

            new()
            {
                    name    = "Settings",
                    buttons = SettingsButtons,
            },

            new()
            {
                    name    = "Menu Settings",
                    buttons = MenuSettingsButtons,
            },

            new()
            {
                    name    = "Movement Settings",
                    buttons = MovementSettingsButtons,
            },

            new()
            {
                    name    = "Room Mods",
                    buttons = RoomModsButtons,
            },

            new()
            {
                    name    = "Movement Mods",
                    buttons = MovementModsButtons,
            },

            new()
            {
                    name    = "Safety Mods",
                    buttons = SafetyModsButtons,
            },
    };

    private static readonly Dictionary<string, ButtonCategory> CategoryLookup = BuildCategoryLookup();
    private static readonly Dictionary<string, ButtonInfo>     ButtonLookup   = BuildButtonLookup();
    
    private static readonly Dictionary<string, ButtonInfo> SavedToggleButtonLookup =
                    BuildSavedToggleButtonLookup();

    public static IReadOnlyDictionary<string, ButtonInfo> SavedToggleButtons =>
                    SavedToggleButtonLookup;

    public static ButtonInfo[] AllButtons { get; } = BuildButtonArray();

    public static ButtonCategory GetCategory(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
            return null;

        CategoryLookup.TryGetValue(categoryName, out ButtonCategory category);

        return category;
    }

    public static ButtonInfo GetIndex(string buttonText)
    {
        if (string.IsNullOrWhiteSpace(buttonText))
            return null;

        ButtonLookup.TryGetValue(buttonText, out ButtonInfo button);

        return button;
    }

    private static Dictionary<string, ButtonCategory> BuildCategoryLookup()
    {
        Dictionary<string, ButtonCategory> lookup = new(StringComparer.OrdinalIgnoreCase);

        foreach (ButtonCategory category in Categories)
        {
            if (string.IsNullOrWhiteSpace(category.name))
                throw new InvalidOperationException("A button category does not have a name.");

            if (!lookup.TryAdd(category.name, category))
                throw new InvalidOperationException($"Duplicate button category named {category.name}.");

        }

        return lookup;
    }

    private static Dictionary<string, ButtonInfo> BuildButtonLookup()
    {
        Dictionary<string, ButtonInfo> lookup = new(StringComparer.OrdinalIgnoreCase);

        foreach (ButtonCategory category in Categories)
        {
            foreach (ButtonInfo button in category.buttons)
            {
                if (string.IsNullOrWhiteSpace(button.buttonText))
                    continue;

                // Duplicate names such as "Return to Main" are allowed.
                // Actual menu presses reference their ButtonInfo directly,
                // so this lookup is only used when something explicitly requests a button by name.
                lookup.TryAdd(button.buttonText, button);
            }
        }

        return lookup;
    }

    private static ButtonInfo[] BuildButtonArray()
    {
        int buttonCount = Categories.Sum(category => category.buttons.Length);

        ButtonInfo[] result = new ButtonInfo[buttonCount];

        int index = 0;

        foreach (ButtonCategory category in Categories)
        {
            foreach (ButtonInfo button in category.buttons)
            {
                result[index] = button;
                index++;
            }
        }

        return result;
    }
    
    private static Dictionary<string, ButtonInfo> BuildSavedToggleButtonLookup()
    {
            Dictionary<string, ButtonInfo> result =
                            new(
                                            StringComparer.Ordinal);

            foreach (ButtonCategory category in Categories)
            {
                    foreach (ButtonInfo button in category.buttons)
                    {
                            if (button.mode != ButtonMode.Toggle)
                                    continue;

                            string key =
                                            category.name +
                                            "/"           +
                                            button.buttonText;

                            if (!result.TryAdd(key, button))
                            {
                                    throw new InvalidOperationException(
                                                    $"Duplicate saved toggle button key {key}.");
                            }

                    }
            }

            return result;
    }
}