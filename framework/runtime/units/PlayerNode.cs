using Godot.Collections;

namespace Framework.Runtime;

public partial class PlayerNode : UnitNode, IJsonSerializable
{
    public string FileName => UnitName;

    public override void _Ready()
    {
        base._Ready();
        Deserialize();
    }

    public void Deserialize()
    {
#region  初始化角色
        var json = JsonHelper.LoadJson($"characters/{FileName}");

        // 生命值设置
        // Properties.Set<float>(UnitPropertyName.MaxHP, data[UnitPropertyName.MaxHP].As<float>());
        // Properties.Set<float>(UnitPropertyName.HP, data[UnitPropertyName.HP].As<float>());
        PropsMgr[UnitPropertyName.MaxHP].Val(json[UnitPropertyName.MaxHP]);
        PropsMgr[UnitPropertyName.HP].Val(PropsMgr[UnitPropertyName.MaxHP].As<float>());

        // 法力值设置
        // Properties.Set<float>(UnitPropertyName.MaxMP, data[UnitPropertyName.MaxHP].As<float>());
        // Properties.Set<float>(UnitPropertyName.MP, data[UnitPropertyName.MP].As<float>());
        PropsMgr[UnitPropertyName.MaxMP].Val(json[UnitPropertyName.MaxMP]);
        PropsMgr[UnitPropertyName.MP].Val(PropsMgr[UnitPropertyName.MaxMP].As<float>());

        // 体力值设置
        // Properties.Set<float>(UnitPropertyName.MaxSP, data[UnitPropertyName.MaxSP].As<float>());
        // Properties.Set<float>(UnitPropertyName.SP, data[UnitPropertyName.SP].As<float>());
        PropsMgr[UnitPropertyName.MaxSP].Val(json[UnitPropertyName.MaxSP]);
        PropsMgr[UnitPropertyName.SP].Val(PropsMgr[UnitPropertyName.MaxSP].As<float>());

        // 速度设置
        // Properties.Set<float>(UnitPropertyName.Speed, data[UnitPropertyName.Speed].As<float>());
        PropsMgr[UnitPropertyName.Speed].Val(json[UnitPropertyName.Speed]);
#endregion
    }

    public void Serialize()
    {
        var dict = new Dictionary();

        // string json = dict
        //     // 生命值保存
        //     .SetValue(UnitPropertyName.MaxHP, Properties.Get<float>(UnitPropertyName.MaxHP))
        //     .SetValue(UnitPropertyName.HP, Properties.Get<float>(UnitPropertyName.HP))
        //     // 法力值保存
        //     .SetValue(UnitPropertyName.MaxMP, Properties.Get<float>(UnitPropertyName.MaxMP))
        //     .SetValue(UnitPropertyName.MP, Properties.Get<float>(UnitPropertyName.MP))
        //     // 体力值保存
        //     .SetValue(UnitPropertyName.MaxSP, Properties.Get<float>(UnitPropertyName.MaxSP))
        //     .SetValue(UnitPropertyName.SP, Properties.Get<float>(UnitPropertyName.SP))
        //     // 速度保存
        //     .SetValue(UnitPropertyName.Speed, Properties.Get<float>(UnitPropertyName.Speed))
        //     .Serialize();

        string json = dict
            // 当前生命值保存
            .SetValue(UnitPropertyName.MaxHP, PropsMgr[UnitPropertyName.MaxHP].As<float>())
            // 当前法力值保存
            .SetValue(UnitPropertyName.MP, PropsMgr[UnitPropertyName.MP].As<float>())
            // 当前体力值保存
            .SetValue(UnitPropertyName.SP, PropsMgr[UnitPropertyName.SP].As<float>())
            .Serialize();
        
        JsonHelper.WriteJsonFile(UnitName, json);
    }
}