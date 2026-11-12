using Godot;

namespace NetverseVR;

/// <summary>Outil de contrôle temporaire : parcourt le monde et fige des points de vue.</summary>
public partial class DevTour : Node3D
{
    private static readonly (Vector3 Pos, float Yaw)[] Views =
    {
        (new Vector3(0f, 1.62f, 22.4f), 0f),      // hall, vers le couloir
        (new Vector3(0f, 1.62f, 21.4f), 180f),    // hall, vers l'entrée
        (new Vector3(0f, 1.62f, 12.0f), 0f),      // couloir
        (new Vector3(-2.0f, 1.62f, 5.2f), 90f),   // entrée salle info
        (new Vector3(-6.0f, 1.62f, 6.5f), 0f),    // salle info, vers le tableau
        (new Vector3(-6.0f, 1.62f, 15.5f), 90f),  // salle serveur, vers les baies
        (new Vector3(2.0f, 1.62f, 4.5f), -90f),   // administration
        (new Vector3(3.0f, 1.62f, 15.5f), -90f),  // réunion
    };

    private Camera3D _camera;
    private int _index;

    public override void _Ready()
    {
        InputActions.EnsureRegistered();
        WorldBuilder.Build(this);

        _camera = new Camera3D { Name = "TourCamera", Fov = 75f, Near = 0.05f, Current = true };
        AddChild(_camera);
        Apply(0);
    }

    public override void _Process(double delta)
    {
        ulong frame = Engine.GetProcessFrames();
        if (frame % 25 != 0) return;

        Apply(_index % Views.Length);
        _index++;
    }

    private void Apply(int index)
    {
        var view = Views[index];
        _camera.Position = view.Pos;
        _camera.RotationDegrees = new Vector3(0f, view.Yaw, 0f);
        GD.Print($"[TOUR] vue {index} — {view.Pos} yaw={view.Yaw}");
    }
}
