using Godot;

namespace NetverseVR;

/// <summary>
/// Fabrique de primitives 3D. Toute la géométrie du monde est low-poly et
/// construite à partir de ces helpers, ce qui garantit une identité visuelle
/// homogène et un coût de rendu maîtrisé (renderer Compatibility).
/// </summary>
public static class MeshKit
{
    /// <summary>Nœud de regroupement, éventuellement positionné/orienté.</summary>
    public static Node3D Group(Node parent, string name, Vector3? position = null, float rotYDeg = 0f)
    {
        var node = new Node3D { Name = name };
        if (position.HasValue) node.Position = position.Value;
        if (!Mathf.IsZeroApprox(rotYDeg)) node.RotationDegrees = new Vector3(0f, rotYDeg, 0f);
        parent.AddChild(node);
        return node;
    }

    public static MeshInstance3D Box(Node parent, string name, Vector3 size, Vector3 center,
                                     Material material, Vector3? rotationDeg = null)
    {
        var mesh = new BoxMesh { Size = size };
        var instance = new MeshInstance3D
        {
            Name = name,
            Mesh = mesh,
            MaterialOverride = material,
            Position = center,
        };
        if (rotationDeg.HasValue) instance.RotationDegrees = rotationDeg.Value;
        parent.AddChild(instance);
        return instance;
    }

    public static MeshInstance3D Cylinder(Node parent, string name, float topRadius, float bottomRadius,
                                          float height, Vector3 center, Material material,
                                          int radialSegments = 12)
    {
        var mesh = new CylinderMesh
        {
            TopRadius = topRadius,
            BottomRadius = bottomRadius,
            Height = height,
            RadialSegments = radialSegments,
            Rings = 1,
        };
        var instance = new MeshInstance3D
        {
            Name = name,
            Mesh = mesh,
            MaterialOverride = material,
            Position = center,
        };
        parent.AddChild(instance);
        return instance;
    }

    public static MeshInstance3D Sphere(Node parent, string name, float radius, Vector3 center,
                                        Material material, int radialSegments = 10, int rings = 6)
    {
        var mesh = new SphereMesh
        {
            Radius = radius,
            Height = radius * 2f,
            RadialSegments = radialSegments,
            Rings = rings,
        };
        var instance = new MeshInstance3D
        {
            Name = name,
            Mesh = mesh,
            MaterialOverride = material,
            Position = center,
        };
        parent.AddChild(instance);
        return instance;
    }

    /// <summary>Boîte avec collision : murs, sols porteurs, mobilier massif.</summary>
    public static StaticBody3D SolidBox(Node parent, string name, Vector3 size, Vector3 center, Material material)
    {
        var body = new StaticBody3D { Name = name, Position = center };
        parent.AddChild(body);
        body.AddChild(new CollisionShape3D { Shape = new BoxShape3D { Size = size } });
        body.AddChild(new MeshInstance3D
        {
            Name = "Mesh",
            Mesh = new BoxMesh { Size = size },
            MaterialOverride = material,
        });
        return body;
    }

    /// <summary>Texte 3D lisible dans le monde (signalétique, écrans).</summary>
    public static Label3D Text(Node parent, string name, string text, Vector3 position,
                               int fontSize = 64, float pixelSize = 0.0035f,
                               Color? color = null, Vector3? rotationDeg = null,
                               bool noDepthTest = false)
    {
        var label = new Label3D
        {
            Name = name,
            Text = text,
            FontSize = fontSize,
            PixelSize = pixelSize,
            Modulate = color ?? Colors.White,
            NoDepthTest = noDepthTest,
            Position = position,
        };
        if (rotationDeg.HasValue) label.RotationDegrees = rotationDeg.Value;
        parent.AddChild(label);
        return label;
    }
}
