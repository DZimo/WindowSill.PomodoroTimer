using CommunityToolkit.Diagnostics;

using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

using Windows.System;

using WindowSill.API;

namespace WindowSill.PomodoroTimer;

[Export(typeof(ISill))]                                     // Marks this class as a Sill to be discovered by MEF.
[Name("WindowSill.PomodoroTimer")]                              // A unique, internal name of the sill.
[Priority(Priority.Lowest)]                                 // Optional. The priority of this sill relative to others. Lowest means it will be after all other sills.
[Order(Before = "My other sill", After = "Another sill")]   // Optional. Helped break up ties with other sills by precising the ordering with other specific sill names.
public sealed class MySill
    : ISillActivatedByProcess,  // Indicates that this sill will be activated when a specific process or window gets the focus.
    ISillListView               // Indicates that this sill provides a list of commands (buttons, popup, etc.).
{
    private readonly IProcessInteractionService _processInteractionService;

    private WindowInfo? _activeProcessWindow;

    [ImportingConstructor]
    public MySill(IProcessInteractionService processInteractionService)
    {
        _processInteractionService = processInteractionService; // Import a MEF service allowing to interact with processes.
    }

    public string DisplayName => "/WindowSill.PomodoroTimer/Misc/DisplayName".GetLocalizedString(); // Get the `DisplayName` from the resources.

    public IconElement CreateIcon()
        => new FontIcon
        {
            Glyph = "\uED56" // Pizza icon
        };

    public ObservableCollection<SillListViewItem> ViewList
        => [
            // A button command with a '+' icon and a localized tooltip that triggers `OnCommandButtonClickAsync` when clicked.
            new SillListViewButtonItem(
                '\uE710', // '+' icon
                "/WindowSill.PomodoroTimer/Misc/CommandTitle".GetLocalizedString(),
                OnCommandButtonClickAsync),
        ];

    public SillView? PlaceholderView => throw new NotImplementedException();

    public SillSettingsView[]? SettingsViews => throw new NotImplementedException();

    // This sill will be activated when a process detected by `NotepadProcessActivator` gets the focus.
    public string[] ProcessActivatorTypeNames => [NotepadProcessActivator.InternalName];

    public ValueTask OnActivatedAsync(string processActivatorTypeName, WindowInfo currentProcessWindow)
    {
        _activeProcessWindow = currentProcessWindow;
        return ValueTask.CompletedTask;
    }

    public ValueTask OnDeactivatedAsync()
    {
        _activeProcessWindow = null;
        return ValueTask.CompletedTask;
    }

    private async Task OnCommandButtonClickAsync()
    {
        // Sends Ctrl+N to the foreground window, which we expect to be Notepad.
        Guard.IsNotNull(_activeProcessWindow);
        await _processInteractionService.SimulateKeysOnWindow(
            _activeProcessWindow,
            VirtualKey.LeftControl,
            VirtualKey.N);
    }
}
