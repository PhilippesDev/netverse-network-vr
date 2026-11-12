using Godot;

namespace NetverseVR;

/// <summary>
/// Salle serveur : quatre baies 19", chemins de câbles, climatisation, poste de
/// supervision et coffret de brassage mural. Local technique crédible : sol
/// technique, murs clairs, éclairage froid.
/// </summary>
public static class ServerRoom
{
    public static void Build(Node3D parent)
    {
        var server = MeshKit.Group(parent, "ServerRoom");

        // Baies alignées contre la façade ouest, façade tournée vers la salle.
        float[] rackZ = { 12.3f, 14.0f, 15.7f, 17.4f };
        for (int i = 0; i < rackZ.Length; i++)
        {
            NetworkGear.Rack(server, $"Rack{i + 1}", new Vector3(-8.45f, 0f, rackZ[i]), 90f);
        }

        // Chemin de câbles horizontal au-dessus des baies.
        MeshKit.Box(server, "CableTray", new Vector3(0.34f, 0.1f, 6.4f),
            new Vector3(-7.3f, 2.78f, 14.9f), MaterialLibrary.Metal);
        for (int i = 0; i < 4; i++)
        {
            MeshKit.Cylinder(server, $"CableDrop{i}", 0.04f, 0.04f, 1.0f,
                new Vector3(-7.9f, 2.3f, rackZ[i]), MaterialLibrary.Cable, 6);
        }

        // Brassage et commutation muraux.
        NetworkGear.PatchPanel(server, "PatchPanel", new Vector3(-2.42f, 1.72f, 19.2f), -90f);
        NetworkGear.Switch(server, "SwitchA", new Vector3(-2.42f, 1.5f, 18.4f), -90f);
        NetworkGear.Switch(server, "SwitchB", new Vector3(-2.42f, 1.4f, 17.6f), -90f);
        NetworkGear.Router(server, "RouterCore", new Vector3(-2.52f, 1.95f, 16.6f), -90f);

        // Climatisation.
        MeshKit.Box(server, "AcUnit", new Vector3(0.3f, 0.58f, 0.95f),
            new Vector3(-2.45f, 2.45f, 12.4f), MaterialLibrary.Plastic);
        MeshKit.Box(server, "AcGrill", new Vector3(0.02f, 0.4f, 0.8f),
            new Vector3(-2.29f, 2.4f, 12.4f), MaterialLibrary.DarkMetal);

        // Poste de supervision.
        Furniture.Desk(server, "ConsoleDesk", new Vector3(-4.7f, 0f, 19.9f), 180f, 1.8f, 0.8f);
        Computers.Monitor(server, "ConsoleMonitor", new Vector3(-4.7f, 0.74f, 19.7f), 180f);
        Computers.Monitor(server, "ConsoleMonitor2", new Vector3(-5.4f, 0.74f, 19.7f), 180f,
            0.5f, 0.3f, true);
        Furniture.Chair(server, "ConsoleChair", new Vector3(-4.7f, 0f, 19.2f), 0f);
        Computers.Ups(server, "Ups", new Vector3(-6.0f, 0f, 20.4f));

        // Signalétique de sécurité.
        MeshKit.Text(server, "Warning", "ZONE TECHNIQUE — ACCÈS RÉSERVÉ",
            new Vector3(-5.0f, 2.6f, 10.68f), 40, 0.0035f,
            new Color(0.95f, 0.75f, 0.35f), Vector3.Zero);

        // Éclairage froid et régulier, deux sources seulement.
        Lighting.CeilingFixture(server, -7.4f, 13.0f, Layout.WallHeight, 1.3f, 0.4f, false,
            true, 7f, 2.1f);
        Lighting.CeilingFixture(server, -4.4f, 13.0f, Layout.WallHeight, 1.3f, 0.4f, false,
            false);
        Lighting.CeilingFixture(server, -7.4f, 18.0f, Layout.WallHeight, 1.3f, 0.4f, false,
            true, 7f, 2.1f);
        Lighting.CeilingFixture(server, -4.4f, 18.0f, Layout.WallHeight, 1.3f, 0.4f, false,
            false);

        Lighting.Accent(server, new Vector3(-4.0f, 2.7f, 15.6f), 8f,
            new Color(0.45f, 0.75f, 1f), 0.9f);
    }
}
