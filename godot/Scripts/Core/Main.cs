using Godot;

namespace NetverseVR;

/// <summary>
/// Point d'entrée du monde. La scène principale ne contient que le joueur :
/// tout l'environnement est généré au démarrage par le WorldBuilder.
/// </summary>
public partial class Main : Node3D
{
    public override void _Ready()
    {
        InputActions.EnsureRegistered();

        var world = new Node3D { Name = "World" };
        AddChild(world);

        ulong start = Time.GetTicksMsec();
        WorldBuilder.Build(world);
        ulong elapsed = Time.GetTicksMsec() - start;

        GD.Print($"[NETVERSE] Monde généré en {elapsed} ms — " +
                 $"{Count(world)} nœuds, {Count<MeshInstance3D>(world)} maillages, " +
                 $"{Count<OmniLight3D>(world)} éclairages d'appoint.");
    }

    private static int Count(Node root)
    {
        int total = 0;
        foreach (var child in root.GetChildren())
        {
            total += 1 + Count(child);
        }

        return total;
    }

    private static int Count<T>(Node root) where T : Node
    {
        int total = root is T ? 1 : 0;
        foreach (var child in root.GetChildren())
        {
            total += Count<T>(child);
        }

        return total;
    }
}
