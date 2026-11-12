using Godot;

namespace NetverseVR;

/// <summary>Mobilier low-poly : bureaux, sièges, bancs, plantes, tableaux.</summary>
public static class Furniture
{
    /// <summary>Bureau : plateau + joues latérales + traverse arrière.</summary>
    public static Node3D Desk(Node3D parent, string name, Vector3 position, float rotYDeg = 0f,
                              float width = 1.3f, float depth = 0.72f, float height = 0.74f)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Top", new Vector3(width, 0.04f, depth),
            new Vector3(0f, height - 0.02f, 0f), MaterialLibrary.Wood);
        MeshKit.Box(group, "SideLeft", new Vector3(0.05f, height - 0.04f, depth - 0.04f),
            new Vector3(-width * 0.5f + 0.025f, (height - 0.04f) * 0.5f, 0f), MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "SideRight", new Vector3(0.05f, height - 0.04f, depth - 0.04f),
            new Vector3(width * 0.5f - 0.025f, (height - 0.04f) * 0.5f, 0f), MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "Modesty", new Vector3(width - 0.12f, 0.32f, 0.03f),
            new Vector3(0f, height - 0.24f, -depth * 0.5f + 0.06f), MaterialLibrary.Plastic);

        return group;
    }

    /// <summary>Siège de bureau avec dossier légèrement incliné.</summary>
    public static Node3D Chair(Node3D parent, string name, Vector3 position, float rotYDeg = 0f)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Seat", new Vector3(0.46f, 0.06f, 0.46f),
            new Vector3(0f, 0.45f, 0f), MaterialLibrary.Fabric);
        MeshKit.Box(group, "Back", new Vector3(0.44f, 0.5f, 0.06f),
            new Vector3(0f, 0.72f, -0.21f), MaterialLibrary.Fabric,
            new Vector3(-8f, 0f, 0f));
        MeshKit.Cylinder(group, "Stem", 0.04f, 0.04f, 0.4f,
            new Vector3(0f, 0.24f, 0f), MaterialLibrary.DarkMetal, 8);
        MeshKit.Cylinder(group, "Base", 0.28f, 0.3f, 0.04f,
            new Vector3(0f, 0.03f, 0f), MaterialLibrary.DarkMetal, 10);

        return group;
    }

    /// <summary>Banc d'accueil.</summary>
    public static Node3D Bench(Node3D parent, string name, Vector3 position, float rotYDeg = 0f,
                               float width = 2.0f)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Seat", new Vector3(width, 0.08f, 0.48f),
            new Vector3(0f, 0.44f, 0f), MaterialLibrary.Wood);
        MeshKit.Box(group, "LegLeft", new Vector3(0.08f, 0.42f, 0.42f),
            new Vector3(-width * 0.5f + 0.18f, 0.21f, 0f), MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "LegRight", new Vector3(0.08f, 0.42f, 0.42f),
            new Vector3(width * 0.5f - 0.18f, 0.21f, 0f), MaterialLibrary.DarkMetal);

        return group;
    }

    /// <summary>Plante d'intérieur low-poly.</summary>
    public static Node3D Plant(Node3D parent, string name, Vector3 position, float scale = 1f)
    {
        var group = MeshKit.Group(parent, name, position);
        group.Scale = Vector3.One * scale;

        MeshKit.Cylinder(group, "Pot", 0.2f, 0.16f, 0.4f,
            new Vector3(0f, 0.2f, 0f), MaterialLibrary.Trim, 10);
        MeshKit.Cylinder(group, "Soil", 0.18f, 0.18f, 0.02f,
            new Vector3(0f, 0.4f, 0f), MaterialLibrary.Plastic, 10);

        Vector3[] offsets =
        {
            new(0f, 0.72f, 0f),
            new(0.22f, 0.62f, 0.1f),
            new(-0.2f, 0.66f, -0.08f),
            new(0.06f, 0.9f, -0.16f),
            new(-0.12f, 0.88f, 0.16f),
        };

        for (int i = 0; i < offsets.Length; i++)
        {
            MeshKit.Sphere(group, $"Leaf{i}", 0.28f, offsets[i], MaterialLibrary.TreeLeaf, 8, 5);
        }

        return group;
    }

    /// <summary>Tableau blanc mural (ou tableau d'affichage).</summary>
    public static Node3D Whiteboard(Node3D parent, string name, Vector3 position, float rotYDeg,
                                    float width = 2.4f, float height = 1.2f)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Frame", new Vector3(width + 0.08f, height + 0.08f, 0.03f),
            Vector3.Zero, MaterialLibrary.Trim);
        MeshKit.Box(group, "Board", new Vector3(width, height, 0.02f),
            new Vector3(0f, 0f, 0.025f), MaterialLibrary.Whiteboard);
        MeshKit.Box(group, "Tray", new Vector3(width * 0.6f, 0.03f, 0.06f),
            new Vector3(0f, -height * 0.5f - 0.03f, 0.045f), MaterialLibrary.Trim);

        return group;
    }

    /// <summary>Panneau de porte (blason de service) avec texte.</summary>
    public static void Sign(Node3D parent, string text, Vector3 position, float rotYDeg,
                            int fontSize = 56, Color? color = null)
    {
        MeshKit.Text(parent, "Sign", text, position, fontSize, 0.0032f,
            color ?? Colors.White, new Vector3(0f, rotYDeg, 0f));
    }
}
