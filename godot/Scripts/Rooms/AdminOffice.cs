using Godot;

namespace NetverseVR;

/// <summary>
/// Poste d'administration réseau : bureau de supervision avec triple écran,
/// mur d'affichage, baie 12U, documentation et coin détente.
/// </summary>
public static class AdminOffice
{
    public static void Build(Node3D parent)
    {
        var admin = MeshKit.Group(parent, "AdminOffice");

        // Bureau principal orienté vers le mur d'écrans (utilisateur face à -X).
        Furniture.Desk(admin, "Desk", new Vector3(7.15f, 0f, 3.0f), 90f, 1.9f, 0.8f);
        Computers.Monitor(admin, "MonitorLeft", new Vector3(7.3f, 0.74f, 2.45f), 90f,
            0.54f, 0.32f, true);
        Computers.Monitor(admin, "MonitorCenter", new Vector3(7.3f, 0.74f, 3.0f), 90f);
        Computers.Monitor(admin, "MonitorRight", new Vector3(7.3f, 0.74f, 3.55f), 90f,
            0.54f, 0.32f, true);
        MeshKit.Box(admin, "Keyboard", new Vector3(0.42f, 0.02f, 0.14f),
            new Vector3(7.55f, 0.755f, 3.0f), MaterialLibrary.Plastic);
        Furniture.Chair(admin, "Chair", new Vector3(7.95f, 0f, 3.0f), -90f);

        // Mur d'écrans de supervision sur la façade nord.
        Computers.WallDisplay(admin, "SupervisionWall", new Vector3(5.4f, 1.95f, 0.2f), 0f,
            2.4f, 1.25f, true);

        // Baie 12U.
        NetworkGear.Rack(admin, "MiniRack", new Vector3(8.35f, 0f, 7.4f), -90f, 1.25f, 0.6f,
            0.9f, 6);

        // Documentation et rangement.
        Furniture.Whiteboard(admin, "Whiteboard", new Vector3(2.34f, 1.75f, 2.4f), 90f,
            1.6f, 1.0f);
        MeshKit.Box(admin, "Shelf", new Vector3(0.3f, 0.04f, 1.3f),
            new Vector3(2.45f, 1.55f, 6.6f), MaterialLibrary.Wood);
        MeshKit.Box(admin, "ShelfBracketA", new Vector3(0.06f, 0.3f, 0.06f),
            new Vector3(2.32f, 1.4f, 6.1f), MaterialLibrary.Metal);
        MeshKit.Box(admin, "ShelfBracketB", new Vector3(0.06f, 0.3f, 0.06f),
            new Vector3(2.32f, 1.4f, 7.1f), MaterialLibrary.Metal);

        Furniture.Plant(admin, "Plant", new Vector3(2.95f, 0f, 8.35f), 1.0f);

        for (int i = 0; i < 2; i++)
        {
            Lighting.CeilingFixture(admin, 4.4f + i * 3f, 2.8f, Layout.WallHeight,
                1.2f, 0.4f, false, true, 7.5f, 2.2f);
            Lighting.CeilingFixture(admin, 4.4f + i * 3f, 6.6f, Layout.WallHeight,
                1.2f, 0.4f, false, false);
        }
    }
}
