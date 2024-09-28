using Godot.Collections;

namespace Framework.Runtime;

public class NumericBuff : BaseBuff
{
    public int Value { get; set; }

    public string PropertyName { get; set; }

    public override void LoadBuffData(UnitNode caster, UnitNode target, Dictionary dict)
    {
        base.LoadBuffData(caster, target, dict);
        Value = dict["Value"].AsInt32();
        PropertyName = dict["PropertyName"].AsString();
    }
}