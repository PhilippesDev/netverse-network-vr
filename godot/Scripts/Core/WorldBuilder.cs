using Godot;

namespace NetverseVR;

/// <summary>
/// Orchestrateur de la génération : l'ordre est significatif. La lumière et
/// l'environnement sont posés en premier, puis la coque du bâtiment, puis les
/// salles qui y ajoutent leur mobilier et leurs équipements.
/// </summary>
public static class WorldBuilder
{
    public static void Build(Node3D root)
    {
        Lighting.Build(root);

        var building = MeshKit.Group(root, "Building");
        BuildingShell.Build(building);

        Corridor.Build(building);
        ComputerLab.Build(building);
        ServerRoom.Build(building);
        AdminOffice.Build(building);
        MeetingRoom.Build(building);
        EntranceHall.Build(building);
    }
}
