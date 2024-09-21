using Godot.Collections;

namespace Framework;

public abstract class BaseBuff
{
    /// <summary>
    /// 施法者
    /// </summary>
    private UnitNode _caster;

    /// <summary>
    /// 目标
    /// </summary>
    private UnitNode _target;

    /// <summary>
    /// 获取施法者
    /// </summary>
    public UnitNode Caster => _caster;

    /// <summary>
    /// 获取目标
    /// </summary>
    public UnitNode Target => _target;

    /// <summary>
    /// Buff编号
    /// </summary>
    public int BuffID { get; set; }

    /// <summary>
    /// Buff名称
    /// </summary>
    public string BuffName { get; set; }

    /// <summary>
    /// 层数
    /// </summary>
    public int Layer { get; set; } = 1;

    /// <summary>
    /// 是否存在层数
    /// </summary>
    public bool EnableLayer { get; set; }

    /// <summary>
    /// 加载Buff信息
    /// </summary>
    public virtual void LoadBuffData(UnitNode caster, UnitNode target, Dictionary dict)
    {
        _caster = caster;
        _target = target;
        BuffID = dict["BuffID"].AsInt32();
        BuffName = dict["BuffName"].AsString();
        EnableLayer = dict["EnableLayer"].AsBool();
    }

    /// <summary>
    /// buff装载后执行
    /// </summary>
    public void OnMounted()
    {
        OnMountedHandle(0, _caster, _target);
    }

    /// <summary>
    /// 每帧Buff执行
    /// </summary>
    public void OnUpdate(double tick)
    {
        OnUpdateHandle(tick,_caster, _target);
    }

    /// <summary>
    /// buff销毁时执行
    /// </summary>
    public void OnDestory()
    {
        OnDestoryHandle(0, _caster, _target);
    }
    

    /// <summary>
    /// 装载时执行Buff效果
    /// </summary>
    protected virtual void OnMountedHandle(double tick, UnitNode caster, UnitNode target) {}

    /// <summary>
    /// 每帧执行Buff效果
    /// </summary>
    protected virtual void OnUpdateHandle(double tick, UnitNode caster, UnitNode target) {}

    /// <summary>
    /// 销毁时执行Buff效果
    /// </summary>
    protected virtual void OnDestoryHandle(double tick, UnitNode caster, UnitNode target) {}
}