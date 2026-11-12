using Godot;

namespace NetverseVR;

/// <summary>
/// Éclairage du monde. Une seule lumière projette des ombres (le soleil) afin
/// de rester fluide sur GPU intégré ; les éclairages intérieurs sont des
/// omnis sans ombre, complétées par des dalles lumineuses émissives.
/// </summary>
public static class Lighting
{
    public static void Build(Node3D parent)
    {
        var root = MeshKit.Group(parent, "Lighting");

        var skyMaterial = new ProceduralSkyMaterial
        {
            SkyTopColor = Color.FromHtml("#3C6E9E"),
            SkyHorizonColor = Color.FromHtml("#C9D7E2"),
            GroundBottomColor = Color.FromHtml("#5A5F55"),
            GroundHorizonColor = Color.FromHtml("#9DA79B"),
            SunAngleMax = 30f,
        };

        var environment = new Godot.Environment
        {
            BackgroundMode = Godot.Environment.BGMode.Sky,
            Sky = new Sky { SkyMaterial = skyMaterial },
            AmbientLightSource = Godot.Environment.AmbientSource.Sky,
            AmbientLightEnergy = 0.55f,
            TonemapMode = Godot.Environment.ToneMapper.Filmic,
            TonemapExposure = 1.05f,
        };

        root.AddChild(new WorldEnvironment { Name = "WorldEnvironment", Environment = environment });

        root.AddChild(new DirectionalLight3D
        {
            Name = "Sun",
            RotationDegrees = new Vector3(-52f, 34f, 0f),
            LightEnergy = 1.25f,
            LightColor = Color.FromHtml("#FFF3DC"),
            ShadowEnabled = true,
            DirectionalShadowMaxDistance = 60f,
        });
    }

    /// <summary>Dalle lumineuse au plafond + source omni associée.</summary>
    public static void CeilingFixture(Node3D parent, float x, float z, float ceilingHeight,
                                      float length = 1.2f, float width = 0.36f,
                                      bool alongZ = true, bool withLight = true,
                                      float range = 6.5f, float energy = 2.2f)
    {
        float sx = alongZ ? width : length;
        float sz = alongZ ? length : width;

        MeshKit.Box(parent, "Fixture", new Vector3(sx, 0.05f, sz),
            new Vector3(x, ceilingHeight - 0.03f, z), MaterialLibrary.LightPanel);

        if (!withLight) return;

        parent.AddChild(new OmniLight3D
        {
            Name = "Lamp",
            Position = new Vector3(x, ceilingHeight - 0.32f, z),
            OmniRange = range,
            LightEnergy = energy,
            LightColor = new Color(1f, 0.97f, 0.9f),
            ShadowEnabled = false,
        });
    }

    /// <summary>Applique un éclairage d'appoint coloré (accueil, salle serveur).</summary>
    public static void Accent(Node3D parent, Vector3 position, float range, Color color, float energy = 0.8f)
    {
        parent.AddChild(new OmniLight3D
        {
            Name = "Accent",
            Position = position,
            OmniRange = range,
            LightColor = color,
            LightEnergy = energy,
            ShadowEnabled = false,
        });
    }
}
