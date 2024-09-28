using Godot;
using Godot.Collections;

namespace Framework.Runtime;

public static class ObjectHelper
{
    /// <summary>
    /// 将对象转为Godot Dictionary
    /// </summary>
    public static Dictionary ToGodotDict(this object obj)
    {
        var result = new Dictionary();
        var type = obj.GetType();
        foreach (var key in type.GetProperties())
        {
            if(result.ContainsKey(key.Name)) continue;
            result.Add(key.Name, (Variant)key.GetValue(obj));
        }
        foreach (var key in type.GetFields())
        {
             if(result.ContainsKey(key.Name)) continue;
            result.Add(key.Name, (Variant)key.GetValue(obj));
        }
        return result;
    }
}