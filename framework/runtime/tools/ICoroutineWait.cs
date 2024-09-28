using System;

namespace Framework.Runtime;

public interface ICorotineWait
{
    bool Wait(double delta);
}

public class WaitTimeBySecond(double seconds) : ICorotineWait
{
    private readonly double _seconds = seconds;

    private double _time;

    public bool Wait(double delta)
    {
        _time += delta;
        return _time >= _seconds;    
    }
}

public class WaitCondition(Func<bool> action) : ICorotineWait
{
    private readonly Func<bool> _action = action;

    public bool Wait(double delta = -1)
    {
        return _action.Invoke();
    }
}