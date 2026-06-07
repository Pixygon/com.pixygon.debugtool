# Pixygon — Debug Tool

In-game debugging: a logger, a debug console with commands, debug profiles, and an
analyzer window. Dev-time tooling other packages can log through.

## Key types

| Type | What it is |
|---|---|
| **`Debugger` / `DebuggerWindow`** | The debug overlay/window. |
| **`Log`** | Logging entry point. |
| **`DebugProfile` / `DebugProfiles` / `DebugProfileCreator`** | Named debug configs. |
| **`DebugGroups`** | Toggleable log/feature groups. |
| **`Analyzer`** | Runtime analysis/diagnostics. |
| **`Console/ConsoleController` + `ConsoleCommandBase`** | In-game command console; subclass `ConsoleCommandBase` to add commands. |

## Dependencies

`com.unity.inputsystem`.

## Usage

```csharp
Log.Info("…");                       // routed through the debug system
// subclass ConsoleCommandBase to register a console command
```

## Status

`0.5.x`. Dev tooling; depended on by `effects` / `addressables`.
