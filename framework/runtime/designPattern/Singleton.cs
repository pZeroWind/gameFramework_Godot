namespace Framework;

public abstract class Singleton<T> where T : class, new()
{
    /// <summary>
    /// 静态单例对象
    /// </summary>
    private static T _value;
    private static readonly object _lock = new();

    /// <summary>
    /// 实例化对象
    /// </summary>
    public static T Instance {
        get
        {
            if(_value == null)
            {
                lock(_lock)
                    _value ??= new T();
            }
            return _value;
        }
    }
}