
using System.Collections.Generic;
using Godot;

namespace Framework;

public class InputManager : Singleton<InputManager>
{
    private Dictionary<string, Key> _inputDict;

    public InputManager()
    {
        _inputDict = new();
    }

    //public InputEvent OnMove { get; private set; }

    public void Reset()
    {
        //InputMap.ActionAddEvent("move", OnMove);
    }
}