using Godot.Collections;

namespace Framework;

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
        string json = JsonHelper.ReadJsonFile(FileName);
        var data = json.Deserialize();

        // 生命值设置
        // Properties.Set<float>(UnitPropertyName.MaxHP, data[UnitPropertyName.MaxHP].As<float>());
        // Properties.Set<float>(UnitPropertyName.HP, data[UnitPropertyName.HP].As<float>());
        Properties[UnitPropertyName.MaxHP].Val(data[UnitPropertyName.MaxHP]);
        Properties[UnitPropertyName.HP].Val(data[UnitPropertyName.HP]);

        // 法力值设置
        // Properties.Set<float>(UnitPropertyName.MaxMP, data[UnitPropertyName.MaxHP].As<float>());
        // Properties.Set<float>(UnitPropertyName.MP, data[UnitPropertyName.MP].As<float>());
        Properties[UnitPropertyName.MaxMP].Val(data[UnitPropertyName.MaxMP]);
        Properties[UnitPropertyName.MP].Val(data[UnitPropertyName.MP]);

        // 体力值设置
        // Properties.Set<float>(UnitPropertyName.MaxSP, data[UnitPropertyName.MaxSP].As<float>());
        // Properties.Set<float>(UnitPropertyName.SP, data[UnitPropertyName.SP].As<float>());
        Properties[UnitPropertyName.MaxSP].Val(data[UnitPropertyName.MaxSP]);
        Properties[UnitPropertyName.SP].Val(data[UnitPropertyName.SP]);

        // 速度设置
        // Properties.Set<float>(UnitPropertyName.Speed, data[UnitPropertyName.Speed].As<float>());
        Properties[UnitPropertyName.Speed].Val(data[UnitPropertyName.Speed]);
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
            // 生命值保存
            .SetValue(UnitPropertyName.MaxHP, Properties[UnitPropertyName.MaxHP].As<float>())
            .SetValue(UnitPropertyName.HP, Properties[UnitPropertyName.MaxHP].As<float>())
            // 法力值保存
            .SetValue(UnitPropertyName.MaxMP, Properties[UnitPropertyName.MaxMP].As<float>())
            .SetValue(UnitPropertyName.MP, Properties[UnitPropertyName.MP].As<float>())
            // 体力值保存
            .SetValue(UnitPropertyName.MaxSP, Properties[UnitPropertyName.MaxSP].As<float>())
            .SetValue(UnitPropertyName.SP, Properties[UnitPropertyName.SP].As<float>())
            // 速度保存
            .SetValue(UnitPropertyName.Speed, Properties[UnitPropertyName.Speed].As<float>())
            .Serialize();
        
        JsonHelper.WriteJsonFile(UnitName, json);
    }
}