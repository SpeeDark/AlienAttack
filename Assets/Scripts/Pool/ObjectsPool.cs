using System;
using System.Collections.Generic;

public class ObjectsPool<T>
{
    private Queue<T> pool = new Queue<T>();

    private readonly Func<T> preloadFunc;
    private readonly Action<T> getAction;
    private readonly Action<T> returnAction;

    public ObjectsPool(int preloadCount, Func<T> preloadFunc, Action<T> getAction = null, Action<T> returnAction = null)
    {
        this.preloadFunc = preloadFunc;
        this.getAction = getAction;
        this.returnAction = returnAction;

        for (var i = 0; i < preloadCount; i++)
            Return(preloadFunc());
    }

    public T Get()
    {
        T Item = pool.Count > 0 ? pool.Dequeue() : preloadFunc();

        if (getAction != null)
            getAction(Item);

        return Item;
    }

    public void Return(T Item)
    {
        if (returnAction != null)
            returnAction(Item);

        pool.Enqueue(Item);
    }
}