using Godot;

namespace Framework;

public class GlobalManager : Singleton<GlobalManager>
{
    public PropertyManager PropertyManager;

    public GlobalManager()
    {
        PropertyManager = new();
    }
}