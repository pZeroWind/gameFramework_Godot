using System.Collections;
using System.Collections.Generic;

namespace Framework.Runtime;

public class CoroutineCore
{
    private readonly Queue<IEnumerator> _its = [];

    /// <summary>
    /// 开始协程
    /// </summary>
    /// <param name="coroutine"></param>
    public void StartCoroutine(IEnumerator routine)
    {
        _its.Enqueue(routine);
    }

    /// <summary>
    /// 执行协程
    /// </summary>
    public void OnUpdateCoroutine(double delta)
    {
        if (_its.Count <= 0) return;
        var it = _its.Dequeue();
        bool isNotOver = true;

        if (it.Current is ICorotineWait wait)
        {
            if (wait.Wait(delta)) 
                isNotOver = it.MoveNext();
        }
        else
        {
            isNotOver = it.MoveNext();
        }

        if (isNotOver)
        {
            _its.Enqueue(it);
        }
    }
}