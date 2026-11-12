using Godot;

namespace NetverseVR;

/// <summary>
/// Hall d'accueil : banque d'accueil, assises, signalétique du projet et
/// entrée vitrée axée sur le couloir.
/// </summary>
public static class EntranceHall
{
    public static void Build(Node3D parent)
    {
        var hall = MeshKit.Group(parent, "EntranceHall");

        Entrance(hall);
        Reception(hall);

        Furniture.Bench(hall, "Bench", new Vector3(-5.4f, 0f, 22.7f), 0f, 2.2f);
        Furniture.Plant(hall, "PlantLeft", new Vector3(-1.85f, 0f, 23.45f), 1.1f);
        Furniture.Plant(hall, "PlantRight", new Vector3(1.85f, 0f, 23.45f), 1.1f);
        Furniture.Plant(hall, "PlantCorner", new Vector3(-8.3f, 0f, 21.6f), 1.0f);

        // Tapis d'accueil.
        MeshKit.Box(hall, "Rug", new Vector3(4.6f, 0.01f, 2.0f),
            new Vector3(0f, 0.022f, 22.5f), MaterialLibrary.Rug);

        // Identité du lieu.
        MeshKit.Text(hall, "Title", "NETVERSE VR", new Vector3(-7.4f, 2.15f, 23.96f),
            110, 0.0045f, Colors.White, new Vector3(0f, 180f, 0f));
        MeshKit.Text(hall, "Subtitle", "CENTRE DE FORMATION AUX RÉSEAUX",
            new Vector3(-7.4f, 1.72f, 23.96f), 40, 0.0045f,
            new Color(0.62f, 0.85f, 0.95f), new Vector3(0f, 180f, 0f));

        // Signalétique au-dessus du porche, lisible depuis l'accueil.
        MeshKit.Text(hall, "Wayfinding", "COULOIR PRINCIPAL  ·  SALLES DE FORMATION",
            new Vector3(0f, 2.86f, 21.14f), 40, 0.004f,
            new Color(0.75f, 0.88f, 0.95f), new Vector3(0f, 0f, 0f));

        Lighting.CeilingFixture(hall, -4.5f, 22.5f, Layout.WallHeight, 1.5f, 0.5f, false,
            true, 8f, 2.3f);
        Lighting.CeilingFixture(hall, 0f, 22.5f, Layout.WallHeight, 1.5f, 0.5f, false,
            false);
        Lighting.CeilingFixture(hall, 4.5f, 22.5f, Layout.WallHeight, 1.5f, 0.5f, false,
            true, 8f, 2.3f);

        Lighting.Accent(hall, new Vector3(0f, 2.6f, 23.4f), 6f,
            new Color(0.55f, 0.85f, 1f), 0.7f);
    }

    /// <summary>Paroi vitrée d'entrée : vitrage, montants et paillasson.</summary>
    private static void Entrance(Node3D hall)
    {
        MeshKit.Box(hall, "EntranceGlass", new Vector3(2.26f, 2.26f, 0.02f),
            new Vector3(0f, 1.2f, 24.17f), MaterialLibrary.Glass);
        MeshKit.Box(hall, "MullionCenter", new Vector3(0.05f, 2.3f, 0.06f),
            new Vector3(0f, 1.15f, 24.16f), MaterialLibrary.Trim);
        MeshKit.Box(hall, "HandleLeft", new Vector3(0.04f, 0.5f, 0.05f),
            new Vector3(-0.18f, 1.1f, 24.13f), MaterialLibrary.Metal);
        MeshKit.Box(hall, "HandleRight", new Vector3(0.04f, 0.5f, 0.05f),
            new Vector3(0.18f, 1.1f, 24.13f), MaterialLibrary.Metal);
        MeshKit.Box(hall, "Doormat", new Vector3(2.2f, 0.02f, 1.1f),
            new Vector3(0f, 0.03f, 23.4f), MaterialLibrary.Trim);
    }

    /// <summary>Banque d'accueil avec plan de travail, assise et écran.</summary>
    private static void Reception(Node3D hall)
    {
        MeshKit.Box(hall, "CounterBody", new Vector3(2.6f, 1.0f, 0.62f),
            new Vector3(6.1f, 0.5f, 22.5f), MaterialLibrary.Wood);
        MeshKit.Box(hall, "CounterTop", new Vector3(2.8f, 0.07f, 0.78f),
            new Vector3(6.1f, 1.03f, 22.5f), MaterialLibrary.DarkMetal);
        MeshKit.Box(hall, "CounterFront", new Vector3(2.6f, 0.55f, 0.03f),
            new Vector3(6.1f, 0.72f, 22.82f), MaterialLibrary.WallAccent);

        Computers.Monitor(hall, "CounterScreen", new Vector3(6.1f, 1.07f, 22.35f), 180f,
            0.5f, 0.3f, true);
        Furniture.Chair(hall, "CounterChair", new Vector3(6.1f, 0f, 21.95f), 0f);

        // Meuble bas derrière l'accueil, adossé à la cloison du fond.
        MeshKit.Box(hall, "Sideboard", new Vector3(2.2f, 0.9f, 0.4f),
            new Vector3(6.1f, 0.45f, 21.38f), MaterialLibrary.DarkMetal);
        MeshKit.Box(hall, "SideboardTop", new Vector3(2.3f, 0.05f, 0.46f),
            new Vector3(6.1f, 0.92f, 21.38f), MaterialLibrary.Wood);
    }
}
