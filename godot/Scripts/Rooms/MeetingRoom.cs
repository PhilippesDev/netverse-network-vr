using Godot;

namespace NetverseVR;

/// <summary>
/// Salle de réunion : table centrale, huit sièges, écran de visioconférence,
/// tableau et éclairage chaleureux.
/// </summary>
public static class MeetingRoom
{
    public static void Build(Node3D parent)
    {
        var meeting = MeshKit.Group(parent, "MeetingRoom");

        // Table.
        MeshKit.Box(meeting, "TableTop", new Vector3(3.2f, 0.07f, 1.3f),
            new Vector3(5.6f, 0.73f, 15.2f), MaterialLibrary.Wood);
        MeshKit.Box(meeting, "TableLegA", new Vector3(0.12f, 0.7f, 0.9f),
            new Vector3(4.3f, 0.35f, 15.2f), MaterialLibrary.DarkMetal);
        MeshKit.Box(meeting, "TableLegB", new Vector3(0.12f, 0.7f, 0.9f),
            new Vector3(6.9f, 0.35f, 15.2f), MaterialLibrary.DarkMetal);

        // Sièges, trois par grand côté et un à chaque extrémité.
        float[] seatX = { 4.4f, 5.6f, 6.8f };
        foreach (float x in seatX)
        {
            Furniture.Chair(meeting, $"ChairNorth{x:0.0}", new Vector3(x, 0f, 14.25f), 0f);
            Furniture.Chair(meeting, $"ChairSouth{x:0.0}", new Vector3(x, 0f, 16.15f), 180f);
        }

        Furniture.Chair(meeting, "ChairWest", new Vector3(3.55f, 0f, 15.2f), 90f);
        Furniture.Chair(meeting, "ChairEast", new Vector3(7.65f, 0f, 15.2f), -90f);

        // Écran de visioconférence et tableau.
        Computers.WallDisplay(meeting, "VideoWall", new Vector3(5.6f, 1.9f, 9.2f), 0f,
            1.9f, 1.05f, true);
        Furniture.Whiteboard(meeting, "Whiteboard", new Vector3(2.34f, 1.7f, 18.6f), 90f,
            2.0f, 1.1f);

        Furniture.Plant(meeting, "PlantCorner", new Vector3(8.3f, 0f, 20.2f), 1.15f);
        Furniture.Plant(meeting, "PlantDoor", new Vector3(2.95f, 0f, 10.1f), 0.85f);

        Lighting.CeilingFixture(meeting, 5.6f, 12.2f, Layout.WallHeight, 1.6f, 0.45f, false,
            true, 8f, 2.2f);
        Lighting.CeilingFixture(meeting, 5.6f, 15.4f, Layout.WallHeight, 1.6f, 0.45f, false,
            true, 8f, 2.2f);
        Lighting.CeilingFixture(meeting, 5.6f, 18.6f, Layout.WallHeight, 1.6f, 0.45f, false,
            false);
    }
}
