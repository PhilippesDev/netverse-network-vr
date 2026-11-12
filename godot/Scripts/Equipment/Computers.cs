using Godot;

namespace NetverseVR;

/// <summary>Postes de travail : écrans, unités centrales, imprimante, mur d'écrans.</summary>
public static class Computers
{
    /// <summary>Écran plat orienté vers +Z (l'utilisateur se tient devant).</summary>
    public static Node3D Monitor(Node3D parent, string name, Vector3 position, float rotYDeg = 0f,
                                 float width = 0.62f, float height = 0.36f, bool cyan = false)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Foot", new Vector3(0.24f, 0.02f, 0.16f),
            new Vector3(0f, 0.01f, 0f), MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "Stem", new Vector3(0.05f, 0.22f, 0.05f),
            new Vector3(0f, 0.12f, -0.02f), MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "Bezel", new Vector3(width, height, 0.035f),
            new Vector3(0f, 0.23f + height * 0.5f, 0f), MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "Screen", new Vector3(width - 0.045f, height - 0.045f, 0.006f),
            new Vector3(0f, 0.23f + height * 0.5f, 0.021f),
            cyan ? MaterialLibrary.ScreenCyan : MaterialLibrary.Screen);

        return group;
    }

    /// <summary>Poste complet : bureau, écran, clavier, souris, unité centrale, siège.</summary>
    public static Node3D Workstation(Node3D parent, string name, Vector3 position,
                                     float rotYDeg = 0f, bool withChair = true)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);
        Furniture.Desk(group, "Desk", Vector3.Zero, 0f);

        Monitor(group, "Monitor", new Vector3(0f, 0.74f, -0.2f));
        MeshKit.Box(group, "Keyboard", new Vector3(0.42f, 0.02f, 0.14f),
            new Vector3(0f, 0.755f, 0.1f), MaterialLibrary.Plastic);
        MeshKit.Box(group, "Mouse", new Vector3(0.06f, 0.025f, 0.1f),
            new Vector3(0.32f, 0.757f, 0.12f), MaterialLibrary.Plastic);

        // Unité centrale au sol, côté droit.
        MeshKit.Box(group, "Tower", new Vector3(0.19f, 0.42f, 0.44f),
            new Vector3(0.5f, 0.21f, -0.02f), MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "TowerLed", new Vector3(0.01f, 0.02f, 0.02f),
            new Vector3(0.595f, 0.34f, 0.14f), MaterialLibrary.LedGreen);

        if (withChair)
        {
            Furniture.Chair(group, "Chair", new Vector3(0f, 0f, 0.66f), 180f);
        }

        return group;
    }

    /// <summary>Imprimante multifonction sur meuble bas.</summary>
    public static Node3D Printer(Node3D parent, string name, Vector3 position, float rotYDeg = 0f)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Cabinet", new Vector3(0.62f, 0.5f, 0.5f),
            new Vector3(0f, 0.25f, 0f), MaterialLibrary.Wood);
        MeshKit.Box(group, "Body", new Vector3(0.58f, 0.34f, 0.46f),
            new Vector3(0f, 0.67f, 0f), MaterialLibrary.Plastic);
        MeshKit.Box(group, "Tray", new Vector3(0.44f, 0.02f, 0.2f),
            new Vector3(0f, 0.85f, 0.14f), MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "Panel", new Vector3(0.14f, 0.05f, 0.01f),
            new Vector3(-0.16f, 0.78f, 0.235f), MaterialLibrary.ScreenCyan);

        return group;
    }

    /// <summary>Mur d'écrans de supervision (poste d'administration).</summary>
    public static Node3D WallDisplay(Node3D parent, string name, Vector3 position, float rotYDeg,
                                     float width = 2.2f, float height = 1.2f, bool cyan = false)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Frame", new Vector3(width + 0.08f, height + 0.08f, 0.07f),
            Vector3.Zero, MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "Panel", new Vector3(width, height, 0.02f),
            new Vector3(0f, 0f, 0.045f),
            cyan ? MaterialLibrary.ScreenCyan : MaterialLibrary.Screen);

        return group;
    }

    /// <summary>Onduleur posé au sol.</summary>
    public static Node3D Ups(Node3D parent, string name, Vector3 position, float rotYDeg = 0f)
    {
        var group = MeshKit.Group(parent, name, position, rotYDeg);

        MeshKit.Box(group, "Body", new Vector3(0.44f, 0.18f, 0.34f),
            new Vector3(0f, 0.09f, 0f), MaterialLibrary.DarkMetal);
        MeshKit.Box(group, "Led", new Vector3(0.03f, 0.012f, 0.005f),
            new Vector3(0f, 0.14f, 0.172f), MaterialLibrary.LedGreen);

        return group;
    }
}
