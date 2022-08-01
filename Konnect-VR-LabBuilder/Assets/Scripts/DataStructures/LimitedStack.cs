using System;
using System.Collections.Generic;

public class LimitedStack<T>
{
    private int _maxSize;
    private LinkedList<T> _list;

    public LimitedStack(int maxSize)
    {
        _maxSize = maxSize;
        _list = new LinkedList<T>();
    }

    //add new value, remove over limit
    public void Push(T value)
    {
        if(_list.Count >= _maxSize)
        {
            _list.RemoveLast();
        }
        _list.AddFirst(value);
    }

    //grab value from top and remove it.
    public T Pop()
    {
        if (_list.Count > 0)
        {
            T value = _list.First.Value;
            _list.RemoveFirst();
            return value;
        }
        else
        {
            throw new InvalidOperationException("The Stack is empty");
        }
    }

    //look at top value without removing.
    public T Peek()
    {
        if (_list.Count > 0)
        {
            T value = _list.First.Value;
            return value;
        }
        else
        {
            throw new InvalidOperationException("The Stack is empty");
        }

    }

    //clear the stack
    public void Clear()
    {
        _list.Clear();

    }

    //get a count of the stack.
    public int Count
    {
        get { return _list.Count; }
    }

}
