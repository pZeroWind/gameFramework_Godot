using System;
using System.Collections.Generic;
using Godot;

namespace Framework.Runtime;

public class MessageData
{
    public string Name;

    public ITValue Value;
}

[SingletonNode]
public partial class MessageManager : Node
{
    private readonly Dictionary<string, Action<MessageData>> _events = [];

    private readonly Queue<MessageData> _messages = [];

    public MessageManager()
    {
        Register("test_evt", msg =>{
            var p = msg.Value.As<PlayerNode>();
            p.MoveAndCollide(Vector2.Right * 0.25f);
        });
    }

    /// <summary>
    /// 注册事件
    /// </summary>
    public void Register(string name, Action<MessageData> evt)
    {
        _events.Add(name, evt);
    }

    /// <summary>
    /// 移除事件
    /// </summary>
    public void Remove(string name, Action<MessageData> evt)
    {
        _events.Remove(name);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    public void Send(MessageData data)
    {
        _messages.Enqueue(data);
    }

    public override void _Process(double delta)
    {
        // 消息处理
        if(_messages.Count > 0)
        {
            var msg = _messages.Dequeue();
            if(_events.TryGetValue(msg.Name, out var value)) value.Invoke(msg);
        }
    }
}