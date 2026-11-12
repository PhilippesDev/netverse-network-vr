using Godot;

namespace NetverseVR;

/// <summary>
/// Contrôleur de déplacement à la première personne, destiné à parcourir et
/// valider le monde. Il est volontairement simple et sera doublé, dans une
/// mission ultérieure, par le Navigation Engine piloté par gestes : la caméra
/// et le déplacement restent exposés ici de façon à pouvoir être réutilisés.
/// </summary>
public partial class PlayerController : CharacterBody3D
{
    [Export] public float WalkSpeed = 4.4f;
    [Export] public float SprintSpeed = 6.8f;
    [Export] public float JumpVelocity = 4.2f;
    [Export] public float Gravity = 22f;
    [Export] public float Acceleration = 42f;
    [Export] public float AirAcceleration = 8f;
    [Export] public float MouseSensitivity = 0.0022f;
    [Export] public float PitchLimitDegrees = 84f;

    private Node3D _head;
    private Camera3D _camera;
    private float _pitch;

    public override void _Ready()
    {
        InputActions.EnsureRegistered();

        _head = GetNode<Node3D>("Head");
        _camera = GetNode<Camera3D>("Head/Camera");

        Input.MouseMode = Input.MouseModeEnum.Captured;
        BuildHud();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion motion && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            RotateY(-motion.Relative.X * MouseSensitivity);

            float limit = Mathf.DegToRad(PitchLimitDegrees);
            _pitch = Mathf.Clamp(_pitch - motion.Relative.Y * MouseSensitivity, -limit, limit);
            _head.Rotation = new Vector3(_pitch, 0f, 0f);
        }

        if (@event.IsActionPressed("release_mouse"))
        {
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
        else if (@event is InputEventMouseButton { Pressed: true } button
                 && button.ButtonIndex == MouseButton.Left
                 && Input.MouseMode == Input.MouseModeEnum.Visible)
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity.Y -= Gravity * (float)delta;
        }
        else if (Input.IsActionJustPressed("jump"))
        {
            velocity.Y = JumpVelocity;
        }

        var input = Input.GetVector("move_left", "move_right", "move_forward", "move_back");
        var direction = Transform.Basis * new Vector3(input.X, 0f, input.Y);
        direction.Y = 0f;
        if (direction.LengthSquared() > 0.0001f) direction = direction.Normalized();

        float speed = Input.IsActionPressed("sprint") ? SprintSpeed : WalkSpeed;
        var target = direction * speed;
        float acceleration = IsOnFloor() ? Acceleration : AirAcceleration;

        velocity.X = Mathf.MoveToward(velocity.X, target.X, acceleration * (float)delta);
        velocity.Z = Mathf.MoveToward(velocity.Z, target.Z, acceleration * (float)delta);

        Velocity = velocity;
        MoveAndSlide();
    }

    // --- Interface minimale de test -------------------------------------

    private void BuildHud()
    {
        var layer = new CanvasLayer { Name = "HUD" };
        AddChild(layer);

        var root = new Control { Name = "Root", MouseFilter = Control.MouseFilterEnum.Ignore };
        root.SetAnchorsPreset(Control.LayoutPreset.FullRect);
        layer.AddChild(root);

        var dot = new ColorRect
        {
            Name = "Crosshair",
            Color = new Color(0.92f, 0.97f, 1f, 0.9f),
            MouseFilter = Control.MouseFilterEnum.Ignore,
        };
        dot.SetAnchorsPreset(Control.LayoutPreset.Center);
        dot.OffsetLeft = -2f;
        dot.OffsetTop = -2f;
        dot.OffsetRight = 2f;
        dot.OffsetBottom = 2f;
        root.AddChild(dot);

        var hint = new Label
        {
            Name = "Hint",
            Text = "ZQSD / WASD : se déplacer   ·   Souris : regarder   ·   Maj : courir   ·   Espace : sauter   ·   Échap : libérer la souris",
            MouseFilter = Control.MouseFilterEnum.Ignore,
        };
        hint.AddThemeFontSizeOverride("font_size", 14);
        hint.AddThemeColorOverride("font_color", new Color(1f, 1f, 1f, 0.72f));
        hint.AddThemeColorOverride("font_shadow_color", new Color(0f, 0f, 0f, 0.85f));
        hint.AddThemeConstantOverride("shadow_offset_x", 1);
        hint.AddThemeConstantOverride("shadow_offset_y", 1);
        hint.SetAnchorsPreset(Control.LayoutPreset.BottomLeft);
        hint.OffsetLeft = 18f;
        hint.OffsetTop = -38f;
        hint.OffsetRight = 900f;
        hint.OffsetBottom = -12f;
        root.AddChild(hint);
    }
}
