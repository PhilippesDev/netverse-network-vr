using Godot;

namespace NetverseVR;

/// <summary>
/// Salle informatique : 12 postes élèves en quatre colonnes, poste formateur,
/// tableau, écran de projection et armoire réseau murale.
/// </summary>
public static class ComputerLab
{
    private static readonly float[] Columns = { -8.20f, -6.87f, -5.53f, -4.20f };
    private static readonly float[] Rows = { 2.4f, 5.4f, 8.4f };

    public static void Build(Node3D parent)
    {
        var lab = MeshKit.Group(parent, "ComputerLab");
        int index = 0;

        foreach (float z in Rows)
        {
            foreach (float x in Columns)
            {
                Computers.Workstation(lab, $"Poste{index:00}", new Vector3(x, 0f, z));
                index++;
            }
        }

        // Poste formateur face aux élèves.
        Computers.Workstation(lab, "PosteFormateur", new Vector3(-8.2f, 0f, 1.0f), 180f);

        // Mur pédagogique.
        Furniture.Whiteboard(lab, "Whiteboard", new Vector3(-6.3f, 1.85f, 0.16f), 0f, 2.6f, 1.2f);
        Computers.WallDisplay(lab, "ProjectionScreen", new Vector3(-3.9f, 1.95f, 0.2f), 0f,
            1.6f, 0.9f, true);

        // Armoire réseau murale de la salle.
        NetworkGear.WallCabinet(lab, "WallCabinet", new Vector3(-2.42f, 1.62f, 9.6f), -90f);

        // Périphériques et décor.
        Computers.Printer(lab, "Printer", new Vector3(-3.25f, 0f, 9.5f), 180f);
        Furniture.Plant(lab, "PlantWindow", new Vector3(-8.55f, 0f, 9.9f), 1.0f);
        Furniture.Plant(lab, "PlantDoor", new Vector3(-2.75f, 0f, 0.55f), 0.85f);

        // Éclairage : six dalles, quatre sources.
        float[][] fixtures =
        {
            new[] { -7.2f, 2.6f, 1f },
            new[] { -4.4f, 2.6f, 1f },
            new[] { -7.2f, 6.0f, 1f },
            new[] { -4.4f, 6.0f, 1f },
            new[] { -7.2f, 9.4f, 0f },
            new[] { -4.4f, 9.4f, 0f },
        };

        foreach (var fixture in fixtures)
        {
            Lighting.CeilingFixture(lab, fixture[0], fixture[1], Layout.WallHeight,
                1.25f, 0.42f, false, fixture[2] > 0.5f, 7.5f, 2.3f);
        }
    }
}
