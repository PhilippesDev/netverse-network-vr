using Godot;

namespace NetverseVR;

/// <summary>
/// Coque du bâtiment : dalle, revêtements de sol, plafonds, murs extérieurs
/// avec fenêtres, cloisons intérieures avec portes et paysage extérieur.
/// Le plan est volontairement simple et crédible : un couloir central dessert
/// quatre locaux et débouche sur un hall d'accueil.
/// </summary>
public static class BuildingShell
{
    public static void Build(Node3D parent)
    {
        var shell = MeshKit.Group(parent, "Shell");

        Slab(shell);
        Floors(shell);
        Ceilings(shell);
        ExteriorWalls(shell);
        InteriorWalls(shell);
        Accents(shell);
        Landscape(parent);
    }

    // --- Structure ------------------------------------------------------

    private static void Slab(Node3D shell)
    {
        var size = new Vector3(18.7f, 0.3f, 24.7f);
        var center = new Vector3(0f, -0.15f, 12f);

        MeshKit.Box(shell, "Slab", size, center, MaterialLibrary.Concrete);
        MeshKit.SolidBox(shell, "GroundCollision",
            new Vector3(18.7f, 0.3f, 24.7f), center, MaterialLibrary.Concrete);
    }

    private static void Floors(Node3D shell)
    {
        Floor(shell, Layout.Corridor, MaterialLibrary.FloorCorridor);
        Floor(shell, Layout.Hall, MaterialLibrary.FloorHall);
        Floor(shell, Layout.Lab, MaterialLibrary.FloorLab);
        Floor(shell, Layout.Server, MaterialLibrary.FloorServer);
        Floor(shell, Layout.Admin, MaterialLibrary.FloorOffice);
        Floor(shell, Layout.Meeting, MaterialLibrary.FloorMeeting);
    }

    private static void Floor(Node3D shell, Zone zone, Material material)
    {
        MeshKit.Box(shell, $"Floor_{zone.Id}",
            new Vector3(zone.Width, 0.02f, zone.Depth),
            new Vector3(zone.CenterX, 0.012f, zone.CenterZ), material);
    }

    private static void Ceilings(Node3D shell)
    {
        Ceiling(shell, Layout.Corridor, Layout.CorridorCeiling);
        Ceiling(shell, Layout.Hall, Layout.WallHeight);
        Ceiling(shell, Layout.Lab, Layout.WallHeight);
        Ceiling(shell, Layout.Server, Layout.WallHeight);
        Ceiling(shell, Layout.Admin, Layout.WallHeight);
        Ceiling(shell, Layout.Meeting, Layout.WallHeight);
    }

    private static void Ceiling(Node3D shell, Zone zone, float height)
    {
        MeshKit.Box(shell, $"Ceiling_{zone.Id}",
            new Vector3(zone.Width, 0.1f, zone.Depth),
            new Vector3(zone.CenterX, height + 0.05f, zone.CenterZ), MaterialLibrary.Ceiling);
    }

    // --- Enveloppe ------------------------------------------------------

    private static void ExteriorWalls(Node3D shell)
    {
        const float t = Layout.ExteriorWall;

        // Façade ouest : fenêtres de la salle informatique et du hall.
        WallBuilder.Build(shell, "WallWest", -9.175f, 0f, -9.175f, 24f, Layout.WallHeight, t,
            MaterialLibrary.Wall, false,
            Opening.Window(2.5f), Opening.Window(5.0f), Opening.Window(7.5f),
            Opening.Window(22.5f, 2.4f));

        // Façade est : fenêtres du bureau d'administration, de la réunion et du hall.
        WallBuilder.Build(shell, "WallEast", 9.175f, 0f, 9.175f, 24f, Layout.WallHeight, t,
            MaterialLibrary.Wall, false,
            Opening.Window(4.0f), Opening.Window(12.5f), Opening.Window(18.0f),
            Opening.Window(22.5f, 2.4f));

        // Pignon nord (arrière du bâtiment) : aveugle.
        WallBuilder.Build(shell, "WallNorth", -9.35f, -0.175f, 9.35f, -0.175f,
            Layout.WallHeight, t, MaterialLibrary.Wall);

        // Façade sud : entrée vitrée axée sur le couloir et deux fenêtres.
        WallBuilder.Build(shell, "WallSouth", -9.35f, 24.175f, 9.35f, 24.175f,
            Layout.WallHeight, t, MaterialLibrary.Wall, false,
            Opening.Window(4.35f), Opening.Arch(9.35f, 2.4f, 2.4f), Opening.Window(14.35f));
    }

    // --- Cloisons -------------------------------------------------------

    private static void InteriorWalls(Node3D shell)
    {
        const float h = Layout.WallHeight;
        const float t = Layout.InteriorWall;

        // Couloir / salle informatique + salle serveur (battants vers les salles).
        WallBuilder.Build(shell, "WallCorridorWest", -2.2f, 0f, -2.2f, 21f, h, t,
            MaterialLibrary.Wall, false,
            Opening.Door(5.2f), Opening.Door(15.5f));

        // Couloir / administration + réunion (battants vers les salles).
        WallBuilder.Build(shell, "WallCorridorEast", 2.2f, 0f, 2.2f, 21f, h, t,
            MaterialLibrary.Wall, true,
            Opening.Door(4.5f), Opening.Door(16.0f));

        // Cloison salle informatique / salle serveur.
        WallBuilder.Build(shell, "WallLabServer", -9f, 10.5f, -2.2f, 10.5f, h, t,
            MaterialLibrary.Wall);

        // Cloison administration / réunion.
        WallBuilder.Build(shell, "WallAdminMeeting", 2.2f, 9f, 9f, 9f, h, t,
            MaterialLibrary.Wall);

        // Séparations avec le hall.
        WallBuilder.Build(shell, "WallServerHall", -9f, 21f, -2.2f, 21f, h, t,
            MaterialLibrary.Wall);
        WallBuilder.Build(shell, "WallMeetingHall", 2.2f, 21f, 9f, 21f, h, t,
            MaterialLibrary.Wall);

        // Porche d'entrée du couloir dans le hall.
        WallBuilder.Build(shell, "WallPortal", -2.2f, 21f, 2.2f, 21f, h, t,
            MaterialLibrary.Wall, false, Opening.Arch(2.2f, 3.8f, 2.6f));
    }

    // --- Détails d'identité visuelle ------------------------------------

    private static void Accents(Node3D shell)
    {
        // Mur d'about bleu au fond du couloir.
        MeshKit.Box(shell, "CorridorFeatureWall",
            new Vector3(4.4f, 2.9f, 0.02f), new Vector3(0f, 1.45f, 0.03f),
            MaterialLibrary.WallAccent);

        // Bandeau lumineux au sol du couloir : guide le visiteur vers l'accueil.
        MeshKit.Box(shell, "CorridorGuide",
            new Vector3(0.06f, 0.004f, 20.9f), new Vector3(0f, 0.026f, 10.5f),
            MaterialLibrary.AccentCyan);

        // Liserés sombres de part et d'autre du couloir.
        MeshKit.Box(shell, "CorridorStripLeft",
            new Vector3(0.14f, 0.004f, 20.9f), new Vector3(-1.55f, 0.026f, 10.5f),
            MaterialLibrary.Trim);
        MeshKit.Box(shell, "CorridorStripRight",
            new Vector3(0.14f, 0.004f, 20.9f), new Vector3(1.55f, 0.026f, 10.5f),
            MaterialLibrary.Trim);
    }

    // --- Extérieur ------------------------------------------------------

    private static void Landscape(Node3D parent)
    {
        var exterior = MeshKit.Group(parent, "Exterior");

        MeshKit.Box(exterior, "Ground", new Vector3(70f, 0.1f, 90f),
            new Vector3(0f, -0.06f, 12f), MaterialLibrary.Grass);

        Tree(exterior, new Vector3(-13f, 0f, 2.5f));
        Tree(exterior, new Vector3(-14.5f, 0f, 9f));
        Tree(exterior, new Vector3(-13f, 0f, 16f));
        Tree(exterior, new Vector3(13.5f, 0f, 4f));
        Tree(exterior, new Vector3(14f, 0f, 12f));
        Tree(exterior, new Vector3(13f, 0f, 19f));
        Tree(exterior, new Vector3(-6f, 0f, 30f));
        Tree(exterior, new Vector3(5.5f, 0f, 31f));
    }

    private static void Tree(Node3D parent, Vector3 position)
    {
        var tree = MeshKit.Group(parent, "Tree", position);
        MeshKit.Cylinder(tree, "Trunk", 0.13f, 0.17f, 2.4f, new Vector3(0f, 1.2f, 0f),
            MaterialLibrary.TreeTrunk, 8);
        MeshKit.Sphere(tree, "Crown", 1.15f, new Vector3(0f, 3.2f, 0f),
            MaterialLibrary.TreeLeaf, 8, 5);
        MeshKit.Sphere(tree, "CrownTop", 0.75f, new Vector3(0.15f, 4.1f, 0.1f),
            MaterialLibrary.TreeLeaf, 8, 5);
    }
}
