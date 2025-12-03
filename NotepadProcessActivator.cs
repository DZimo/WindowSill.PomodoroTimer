using System.ComponentModel.Composition;
using System.Diagnostics;

using WindowSill.API;

namespace WindowSill.PomodoroTimer;

[Export(typeof(ISillProcessActivator))]
[ActivationType(InternalName)]
internal sealed class NotepadProcessActivator : ISillProcessActivator
{
    internal const string InternalName = "Notepad Activator";

    public ValueTask<bool> GetShouldBeActivatedAsync(string applicationIdentifier, Process process, Version? version, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(applicationIdentifier.EndsWith("Notepad.exe"));
    }
}

