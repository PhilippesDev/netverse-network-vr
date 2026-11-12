using Godot;

namespace NetverseVR;

/// <summary>Couloir central : éclairage rythmé, signalétique et guide au sol.</summary>
public static class Corridor
{
    public static void Build(Node3D parent)
    {
        var corridor = MeshKit.Group(parent, "Corridor");
        const float ceiling = Layout.CorridorCeiling;

        // Dalles lumineuses régulières.
        for (int i = 0; i < 5; i++)
        {
            float z = 3f + i * 4f;
            Lighting.CeilingFixture(corridor, 0f, z, ceiling, 1.15f, 0.34f, true,
                withLight: i % 2 == 0, range: 7.5f, energy: 2.4f);
        }

        // Signalétique au-dessus des portes.
        Sign(corridor, "SALLE INFORMATIQUE", -2.06f, 2.52f, 5.2f, 90f, 44);
        Sign(corridor, "SALLE SERVEUR", -2.06f, 2.52f, 15.5f, 90f, 44);
        Sign(corridor, "ADMINISTRATION RÉSEAU", 2.06f, 2.52f, 4.5f, -90f, 40);
        Sign(corridor, "SALLE DE RÉUNION", 2.06f, 2.52f, 16.0f, -90f, 44);

        // Identité du projet au fond du couloir.
        MeshKit.Text(corridor, "Title", "NETVERSE VR", new Vector3(0f, 2.05f, 0.06f),
            120, 0.0042f, Colors.White, Vector3.Zero);
        MeshKit.Text(corridor, "Subtitle", "CENTRE DE FORMATION AUX RÉSEAUX",
            new Vector3(0f, 1.62f, 0.06f), 46, 0.0042f,
            new Color(0.65f, 0.85f, 0.95f), Vector3.Zero);

        // Banc d'attente près du hall.
        Furniture.Bench(corridor, "Bench", new Vector3(-1.72f, 0f, 18.4f), 90f, 1.8f);
        Furniture.Plant(corridor, "PlantEndLeft", new Vector3(-1.72f, 0f, 0.55f), 0.9f);
        Furniture.Plant(corridor, "PlantEndRight", new Vector3(1.72f, 0f, 0.55f), 0.9f);

        // Panneau de direction vers l'accueil.
        MeshKit.Text(corridor, "ExitSign", "ACCUEIL  →", new Vector3(0f, 2.45f, 20.6f),
            52, 0.0042f, new Color(0.55f, 0.9f, 0.95f), new Vector3(0f, 180f, 0f));
    }

    private static void Sign(Node3D parent, string text, float x, float y, float z,
                             float rotYDeg, int fontSize)
    {
        var backing = MeshKit.Group(parent, "SignBacking", new Vector3(x, y, z), rotYDeg);
        MeshKit.Box(backing, "Plate", new Vector3(1.3f, 0.22f, 0.02f), Vector3.Zero,
            MaterialLibrary.WallAccent);
        MeshKit.Text(backing, "Text", text, new Vector3(0f, -0.02f, 0.02f),
            fontSize, 0.0032f, Colors.White, Vector3.Zero);
    }
}
