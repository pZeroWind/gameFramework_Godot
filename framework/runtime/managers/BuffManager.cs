using System.Collections.Generic;
using System.Linq;

namespace Framework;

public class BuffManager(UnitNode unit)
{
    private readonly List<BaseBuff> _buffs = [];

    private readonly UnitNode _owner = unit;

    /// <summary>
    /// 添加Buff
    /// </summary>
    public void AddBuff<T>(UnitNode caster, int buffID) where T : BaseBuff, new()
    {   
        // 检查Buff是否已经存在
        var buff = _buffs.Where(buff => buff.BuffID == buffID && buff.Caster == caster).FirstOrDefault();
        if (buff != null && buff.EnableLayer)
        {   
            // 若已存在且允许层数 Buff层数+1
            buff.Layer += 1;
            return;
        }
        // 读取json文件中存储的Buff数据
        var json = JsonHelper.LoadJson($"buffs/buff_{buffID}");
        // 创建新Buff
        T newbuff = new();
        newbuff.LoadBuffData(caster, _owner, json);
        _buffs.Add(buff);
        newbuff.OnMounted();
    }

    /// <summary>
    /// 每帧执行Buff
    /// </summary>
    public void OnUpdate(double tick)
    {
        foreach (BaseBuff buff in _buffs)
        {
            buff.OnUpdate(tick);
        }
    }

    /// <summary>
    /// 移除Buff
    /// </summary>
    public void RemoveBuff(BaseBuff buff)
    {
        buff.OnDestory();
        _buffs.Remove(buff);
    }

    /// <summary>
    /// 获取该类型Buff
    /// </summary>
    public IEnumerable<T> GetBuff<T>() where T : BaseBuff
    {
        return _buffs.Where(buff => buff is T).Select(b => b as T);
    }
}