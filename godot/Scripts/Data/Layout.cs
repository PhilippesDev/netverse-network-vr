using Godot;

namespace NetverseVR;

/// <summary>Plan du bâtiment : dimensions et zones, en mètres.</summary>
public readonly struct Zone
{
    public readonly string Id;
    public readonly string Label;
    public readonly float X0;
    public readonly float Z0;
    public readonly float X1;
    public readonly float Z1;
    public readonly float Height;

    public Zone(string id, string label, float x0, float z0, float x1, float z1, float height)
    {
        Id = id;
        Label = label;
        X0 = x0;
        Z0 = z0;
        X1 = x1;
        Z1 = z1;
        Height = height;
    }

    public float Width => X1 - X0;
    public float Depth => Z1 - Z0;
    public float CenterX => (X0 + X1) * 0.5f;
    public float CenterZ => (Z0 + Z1) * 0.5f;
    public Vector3 Center => new(CenterX, 0f, CenterZ);
}

/// <summary>
/// Plan directeur du bâtiment. Toutes les zones pavent le plan sans laisser
/// de vide, ce qui garantit un sol et un plafond continus.
/// </summary>
public static class Layout
{
    public const float WallHeight = 3.2f;
    public const float CorridorCeiling = 2.9f;
    public const float ExteriorWall = 0.35f;
    public const float InteriorWall = 0.2f;

    // Le couloir dessert les quatre salles et débouche sur le hall.
    public static readonly Zone Corridor = new("corridor", "Couloir principal", -2.2f, 0f, 2.2f, 21f, CorridorCeiling);
    public static readonly Zone Hall = new("hall", "Hall d'accueil", -9f, 21f, 9f, 24f, WallHeight);
    public static readonly Zone Lab = new("lab", "Salle informatique", -9f, 0f, -2.2f, 10.5f, WallHeight);
    public static readonly Zone Server = new("server", "Salle serveur", -9f, 10.5f, -2.2f, 21f, WallHeight);
    public static readonly Zone Admin = new("admin", "Poste d'administration", 2.2f, 0f, 9f, 9f, WallHeight);
    public static readonly Zone Meeting = new("meeting", "Salle de réunion", 2.2f, 9f, 9f, 21f, WallHeight);

    public static readonly Zone[] Rooms = { Lab, Server, Admin, Meeting, Hall };
}
