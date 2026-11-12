using Godot;

namespace NetverseVR;

/// <summary>
/// Déclare les actions d'entrée au démarrage. Les touches sont enregistrées par
/// code (keycode logique) pour que ZQSD et WASD fonctionnent tous les deux sans
/// dépendre d'un clavier particulier.
/// </summary>
public static class InputActions
{
    private static bool _registered;

    public static void EnsureRegistered()
    {
        if (_registered) return;
        _registered = true;

        Register("move_forward", Key.W, Key.Z, Key.Up);
        Register("move_back", Key.S, Key.Down);
        Register("move_left", Key.A, Key.Q, Key.Left);
        Register("move_right", Key.D, Key.Right);
        Register("jump", Key.Space);
        Register("sprint", Key.Shift);
        Register("release_mouse", Key.Escape);
    }

    private static void Register(string action, params Key[] keys)
    {
        if (!InputMap.HasAction(action)) InputMap.AddAction(action);

        foreach (var key in keys)
        {
            var pressed = new InputEventKey { Keycode = key, PhysicalKeycode = key };
            InputMap.ActionAddEvent(action, pressed);
        }
    }
}
