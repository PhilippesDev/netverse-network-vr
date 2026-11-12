using Godot;

namespace NetverseVR;

public enum OpeningKind
{
    Door,
    Window,
    Archway,
}

/// <summary>Une ouverture dans un mur, mesurée le long du mur depuis son origine.</summary>
public sealed class Opening
{
    public readonly float Center;
    public readonly float Width;
    public readonly float Bottom;
    public readonly float Top;
    public readonly OpeningKind Kind;

    public Opening(float center, float width, float bottom, float top, OpeningKind kind)
    {
        Center = center;
        Width = width;
        Bottom = bottom;
        Top = top;
        Kind = kind;
    }

    public static Opening Door(float center, float width = 1.1f, float height = 2.2f)
        => new(center, width, 0f, height, OpeningKind.Door);

    public static Opening Window(float center, float width = 1.8f, float bottom = 1.0f, float top = 2.4f)
        => new(center, width, bottom, top, OpeningKind.Window);

    public static Opening Arch(float center, float width, float top)
        => new(center, width, 0f, top, OpeningKind.Archway);
}

/// <summary>
/// Construit un mur à partir d'un segment au sol et d'une liste d'ouvertures.
/// Le mur est découpé en panneaux pleins, allèges et linteaux, chacun porteur
/// de sa propre collision : un joueur ne peut donc pas traverser un mur mais
/// peut franchir une porte.
/// </summary>
public static class WallBuilder
{
    public static void Build(Node3D parent, string name, float ax, float az, float bx, float bz,
                             float height, float thickness, Material material,
                             bool flipLeaf = false, params Opening[] openings)
    {
        float dx = bx - ax;
        float dz = bz - az;
        float length = Mathf.Sqrt(dx * dx + dz * dz);
        if (length < 0.01f) return;

        float nx = dx / length;
        float nz = dz / length;

        // Le +X local suit le mur : on aligne la rotation Y sur la direction.
        float rotY = Mathf.Atan2(-nz, nx);

        var body = new StaticBody3D
        {
            Name = name,
            Position = new Vector3(ax, 0f, az),
            Rotation = new Vector3(0f, rotY, 0f),
        };
        parent.AddChild(body);

        var ordered = new Opening[openings.Length];
        Array.Copy(openings, ordered, openings.Length);
        Array.Sort(ordered, (a, b) => a.Center.CompareTo(b.Center));

        float cursor = 0f;
        foreach (var op in ordered)
        {
            float u0 = op.Center - op.Width * 0.5f;
            float u1 = op.Center + op.Width * 0.5f;

            if (u0 > cursor) Panel(body, cursor, u0, 0f, height, thickness, material);
            if (op.Bottom > 0.01f) Panel(body, u0, u1, 0f, op.Bottom, thickness, material);
            if (op.Top < height - 0.01f) Panel(body, u0, u1, op.Top, height, thickness, material);

            cursor = Mathf.Max(cursor, u1);
        }

        if (cursor < length) Panel(body, cursor, length, 0f, height, thickness, material);

        foreach (var op in ordered) Trim(body, op, thickness, flipLeaf);
    }

    private static void Panel(Node3D body, float u0, float u1, float y0, float y1,
                              float thickness, Material material)
    {
        if (u1 - u0 < 0.005f || y1 - y0 < 0.005f) return;

        var size = new Vector3(u1 - u0, y1 - y0, thickness);
        var center = new Vector3((u0 + u1) * 0.5f, (y0 + y1) * 0.5f, 0f);

        MeshKit.Box(body, "Panel", size, center, material);
        body.AddChild(new CollisionShape3D { Shape = new BoxShape3D { Size = size }, Position = center });
    }

    private static void Trim(Node3D body, Opening op, float thickness, bool flipLeaf)
    {
        const float frame = 0.07f;
        float depth = thickness + 0.04f;
        float u0 = op.Center - op.Width * 0.5f;
        float u1 = op.Center + op.Width * 0.5f;

        switch (op.Kind)
        {
            case OpeningKind.Window:
            {
                float h = op.Top - op.Bottom;
                float mid = op.Bottom + h * 0.5f;

                MeshKit.Box(body, "Glass", new Vector3(op.Width - 0.06f, h - 0.06f, 0.02f),
                    new Vector3(op.Center, mid, 0f), MaterialLibrary.Glass);

                MeshKit.Box(body, "FrameTop", new Vector3(op.Width + frame, frame, depth),
                    new Vector3(op.Center, op.Top + frame * 0.5f, 0f), MaterialLibrary.Trim);
                MeshKit.Box(body, "FrameBottom", new Vector3(op.Width + frame, frame, depth),
                    new Vector3(op.Center, op.Bottom - frame * 0.5f, 0f), MaterialLibrary.Trim);
                MeshKit.Box(body, "FrameLeft", new Vector3(frame, h, depth),
                    new Vector3(u0 - frame * 0.5f, mid, 0f), MaterialLibrary.Trim);
                MeshKit.Box(body, "FrameRight", new Vector3(frame, h, depth),
                    new Vector3(u1 + frame * 0.5f, mid, 0f), MaterialLibrary.Trim);
                break;
            }

            case OpeningKind.Door:
            {
                float h = op.Top;

                MeshKit.Box(body, "FrameTop", new Vector3(op.Width + frame, frame, depth),
                    new Vector3(op.Center, h + frame * 0.5f, 0f), MaterialLibrary.Trim);
                MeshKit.Box(body, "FrameLeft", new Vector3(frame, h, depth),
                    new Vector3(u0 - frame * 0.5f, h * 0.5f, 0f), MaterialLibrary.Trim);
                MeshKit.Box(body, "FrameRight", new Vector3(frame, h, depth),
                    new Vector3(u1 + frame * 0.5f, h * 0.5f, 0f), MaterialLibrary.Trim);

                // Battant ouvert à ~100° : laisse le passage libre et signale la porte.
                float angle = flipLeaf ? 100f : -100f;
                var pivot = new Node3D
                {
                    Name = "Leaf",
                    Position = new Vector3(u0 + 0.03f, 0f, 0f),
                    RotationDegrees = new Vector3(0f, angle, 0f),
                };
                body.AddChild(pivot);

                float leafWidth = op.Width - 0.06f;
                float leafHeight = h - 0.05f;
                MeshKit.Box(pivot, "Panel", new Vector3(leafWidth, leafHeight, 0.045f),
                    new Vector3(leafWidth * 0.5f, leafHeight * 0.5f, 0f), MaterialLibrary.Door);
                MeshKit.Box(pivot, "Handle", new Vector3(0.03f, 0.03f, 0.11f),
                    new Vector3(leafWidth - 0.09f, 1.02f, 0.055f), MaterialLibrary.Metal);
                break;
            }

            case OpeningKind.Archway:
            {
                MeshKit.Box(body, "ArchTop", new Vector3(op.Width + 0.24f, 0.09f, depth + 0.08f),
                    new Vector3(op.Center, op.Top, 0f), MaterialLibrary.Trim);
                MeshKit.Box(body, "ArchLeft", new Vector3(0.1f, op.Top, depth + 0.06f),
                    new Vector3(u0, op.Top * 0.5f, 0f), MaterialLibrary.Trim);
                MeshKit.Box(body, "ArchRight", new Vector3(0.1f, op.Top, depth + 0.06f),
                    new Vector3(u1, op.Top * 0.5f, 0f), MaterialLibrary.Trim);
                break;
            }
        }
    }
}
