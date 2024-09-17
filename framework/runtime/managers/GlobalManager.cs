using Godot;

namespace Framework;

public class GlobalManager : Singleton<GlobalManager>
{
    public readonly PropertyManager PropertyManager;

    public GlobalManager()
    {
        PropertyManager = new();
    }
}