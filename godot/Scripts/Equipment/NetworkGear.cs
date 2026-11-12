using Godot;

namespace NetverseVR;

/// <summary>
/// Équipements réseau : baies 19", commutateurs, routeurs, panneaux de brassage
/// et coffrets muraux. Chaque appareil est identifiable à sa façade, à ses
/// ports et à ses voyants.
/// </summary>
public static class NetworkGear
{
    /// <summary>Baie 19" avec portes vitrées et unités en façade (+Z).</summary>
    public static Node3D Rack(Node3D parent, string name, Vector3 position, float rotYDeg = 0f,
                              float height = 2.0f, float width = 0.62f, float depth = 0.92f,
                              int units = 11)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Cabinet", new Vector3(width, height, depth),
            new Vector3(0f, height * 0.5f, 0f), MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "Door", new Vector3(width - 0.05f, height - 0.12f, 0.03f),
            new Vector3(0f, height * 0.5f, depth * 0.5f + 0.005f), MaterialLibrary.Metal);

        float spacing = (height - 0.4f) / units;
        for (int i = 0; i < units; i++)
        {
            float y = 0.28f + spacing * i;
            MeshKit.Box(group, $"Unit{i}", new Vector3(width - 0.14f, spacing * 0.62f, 0.02f),
                new Vector3(0f, y, depth * 0.5f + 0.025f), MaterialLibrary.Plastic);
            MeshKit.Box(group, $"Led{i}", new Vector3(0.014f, 0.014f, 0.006f),
                new Vector3(-width * 0.5f + 0.12f, y, depth * 0.5f + 0.038f),
                i % 4 == 0 ? MaterialLibrary.LedAmber : MaterialLibrary.LedGreen);
        }

        // Goulotte de câblage au-dessus de la baie.
        MeshKit.Box(group, "Tray", new Vector3(width + 0.06f, 0.07f, depth - 0.1f),
            new Vector3(0f, height + 0.06f, 0f), MaterialLibrary.Metal);

        return group;
    }

    /// <summary>Commutateur 8 ports avec voyants de liaison.</summary>
    public static Node3D Switch(Node3D parent, string name, Vector3 position, float rotYDeg = 0f,
                                int ports = 8)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Body", new Vector3(0.44f, 0.045f, 0.2f),
            new Vector3(0f, 0.0225f, 0f), MaterialLibrary.DarkMetal);

        float step = 0.44f / (ports + 1);
        for (int i = 0; i < ports; i++)
        {
            float x = -0.22f + step * (i + 1);
            MeshKit.Box(group, $"Port{i}", new Vector3(0.028f, 0.02f, 0.008f),
                new Vector3(x, 0.0225f, 0.104f), MaterialLibrary.Plastic);
            MeshKit.Box(group, $"Link{i}", new Vector3(0.008f, 0.008f, 0.004f),
                new Vector3(x + 0.012f, 0.036f, 0.104f),
                i % 3 == 0 ? MaterialLibrary.LedAmber : MaterialLibrary.LedGreen);
        }

        return group;
    }

    /// <summary>Routeur avec antennes et voyants d'alimentation.</summary>
    public static Node3D Router(Node3D parent, string name, Vector3 position, float rotYDeg = 0f)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Body", new Vector3(0.4f, 0.06f, 0.26f),
            new Vector3(0f, 0.03f, 0f), MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "Front", new Vector3(0.36f, 0.02f, 0.01f),
            new Vector3(0f, 0.03f, 0.132f), MaterialLibrary.Plastic);

        for (int i = 0; i < 4; i++)
        {
            MeshKit.Box(group, $"Port{i}", new Vector3(0.03f, 0.016f, 0.008f),
                new Vector3(-0.12f + i * 0.08f, 0.022f, 0.136f), MaterialLibrary.Plastic);
        }

        MeshKit.Box(group, "Power", new Vector3(0.01f, 0.01f, 0.004f),
            new Vector3(0.15f, 0.04f, 0.136f), MaterialLibrary.LedGreen);

        MeshKit.Cylinder(group, "AntennaA", 0.008f, 0.008f, 0.24f,
            new Vector3(-0.12f, 0.17f, -0.1f), MaterialLibrary.DarkMetal, 6);
        MeshKit.Cylinder(group, "AntennaB", 0.008f, 0.008f, 0.24f,
            new Vector3(0.12f, 0.17f, -0.1f), MaterialLibrary.DarkMetal, 6);

        return group;
    }

    /// <summary>Panneau de brassage 12 ports.</summary>
    public static Node3D PatchPanel(Node3D parent, string name, Vector3 position, float rotYDeg = 0f)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Body", new Vector3(0.48f, 0.09f, 0.05f),
            Vector3.Zero, MaterialLibrary.DarkMetal);

        for (int i = 0; i < 12; i++)
        {
            float x = -0.2f + i * 0.036f;
            MeshKit.Box(group, $"Port{i}", new Vector3(0.022f, 0.026f, 0.008f),
                new Vector3(x, 0f, 0.03f), MaterialLibrary.Plastic);
        }

        return group;
    }

    /// <summary>Coffret mural : brassage + commutateur, pour les petites salles.</summary>
    public static Node3D WallCabinet(Node3D parent, string name, Vector3 position, float rotYDeg = 0f)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Box", new Vector3(0.62f, 0.82f, 0.2f),
            Vector3.Zero, MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "Door", new Vector3(0.56f, 0.76f, 0.02f),
            new Vector3(0f, 0f, 0.108f), MaterialLibrary.Metal);
        MeshKit.Box(group, "Led", new Vector3(0.02f, 0.012f, 0.006f),
            new Vector3(0.2f, 0.3f, 0.122f), MaterialLibrary.LedGreen);

        return group;
    }
}
