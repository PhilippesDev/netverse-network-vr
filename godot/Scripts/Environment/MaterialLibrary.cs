using Godot;

namespace NetverseVR;

/// <summary>
/// Palette et matériaux partagés. Les ressources sont créées une seule fois et
/// réutilisées par tous les maillages : moins de variantes de shader, donc de
/// meilleures performances sur GPU intégré.
/// </summary>
public static class MaterialLibrary
{
    // --- Extérieurs -------------------------------------------------------
    public static readonly StandardMaterial3D Grass = Solid("#6E8A56", 1.0f);
    public static readonly StandardMaterial3D TreeTrunk = Solid("#5A4632", 0.95f);
    public static readonly StandardMaterial3D TreeLeaf = Solid("#4A7038", 0.95f);
    public static readonly StandardMaterial3D Concrete = Solid("#A9ACAF", 0.85f);

    // --- Sols -------------------------------------------------------------
    public static readonly StandardMaterial3D FloorCorridor = Solid("#C6CBD1", 0.26f, 0.05f);
    public static readonly StandardMaterial3D FloorHall = Solid("#C6CBD1", 0.26f, 0.05f);
    public static readonly StandardMaterial3D FloorLab = Solid("#98A2AB", 0.42f);
    public static readonly StandardMaterial3D FloorServer = Solid("#6C737A", 0.55f, 0.08f);
    public static readonly StandardMaterial3D FloorOffice = Solid("#3C454E", 0.95f);
    public static readonly StandardMaterial3D FloorMeeting = Solid("#46403A", 0.95f);
    public static readonly StandardMaterial3D Rug = Solid("#2E3944", 0.98f);

    // --- Murs et plafonds -------------------------------------------------
    public static readonly StandardMaterial3D Wall = Solid("#E9E7E2", 0.88f);
    public static readonly StandardMaterial3D WallAccent = Solid("#16324F", 0.72f);
    public static readonly StandardMaterial3D Ceiling = Solid("#F4F4F2", 0.98f);
    public static readonly StandardMaterial3D Trim = Solid("#3A4148", 0.45f, 0.35f);
    public static readonly StandardMaterial3D Door = Solid("#8A7458", 0.72f);

    // --- Matériaux techniques --------------------------------------------
    public static readonly StandardMaterial3D Metal = Solid("#2B3138", 0.34f, 0.78f);
    public static readonly StandardMaterial3D DarkMetal = Solid("#1B1F24", 0.45f, 0.65f);
    public static readonly StandardMaterial3D Plastic = Solid("#23272C", 0.58f);
    public static readonly StandardMaterial3D Wood = Solid("#8C6A4A", 0.74f);
    public static readonly StandardMaterial3D Fabric = Solid("#333B45", 0.96f);
    public static readonly StandardMaterial3D Whiteboard = Solid("#F7F9FA", 0.14f);
    public static readonly StandardMaterial3D Cable = Solid("#1E5FA8", 0.7f);

    public static readonly StandardMaterial3D Glass = new()
    {
        AlbedoColor = new Color(0.74f, 0.86f, 0.91f, 0.26f),
        Transparency = BaseMaterial3D.TransparencyEnum.Alpha,
        Metallic = 0.2f,
        Roughness = 0.05f,
        CullMode = BaseMaterial3D.CullModeEnum.Disabled,
    };

    // --- Surfaces lumineuses ---------------------------------------------
    public static readonly StandardMaterial3D LightPanel = Glow("#FFFDF2", 1.7f, "#FFFFFF");
    public static readonly StandardMaterial3D Screen = Glow("#2D6FA8", 1.25f, "#0B1620");
    public static readonly StandardMaterial3D ScreenCyan = Glow("#29B6C8", 1.15f, "#08161A");
    public static readonly StandardMaterial3D LedGreen = Glow("#48E07C", 2.4f, "#0A1A10");
    public static readonly StandardMaterial3D LedAmber = Glow("#F2A33A", 1.9f, "#1A1206");
    public static readonly StandardMaterial3D AccentCyan = Glow("#17B6C9", 0.85f, "#0C2B30");

    private static StandardMaterial3D Solid(string html, float roughness, float metallic = 0f)
    {
        return new StandardMaterial3D
        {
            AlbedoColor = Color.FromHtml(html),
            Roughness = roughness,
            Metallic = metallic,
        };
    }

    private static StandardMaterial3D Glow(string emissionHtml, float energy, string albedoHtml)
    {
        return new StandardMaterial3D
        {
            AlbedoColor = Color.FromHtml(albedoHtml),
            Roughness = 0.4f,
            EmissionEnabled = true,
            Emission = Color.FromHtml(emissionHtml),
            EmissionEnergyMultiplier = energy,
        };
    }
}
