using System.Collections;

namespace Helper;

public class DynamicList<T> : IEnumerable<T>
{
    private T[] _items;
    private int _capacity;
    private int _length;

    public DynamicList(int capacity = 16)
    {
        _items = new T[capacity];
        _capacity = capacity;
        _length = 0;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _length)
            {
                throw new IndexOutOfRangeException();
            }
            return _items[index];
        }
    }

    public void Add(T item)
    {
        if (_length < _capacity)
        {
            _items[_length++] = item;
            return;
        }

        var items = new T[2 * _length];
        _items.CopyTo(items, 0);
        _capacity = items.Length;
        items[_length++] = item;
        _items = items;
    }

    public bool Remove(T item)
    {
        var index = Array.IndexOf(_items, item, 0, _length);
        return index >= 0 && RemoveAt(index);
    }

    public bool RemoveAt(int index)
    {
        if (index < 0 || index >= _length)
        {
            throw new IndexOutOfRangeException();
        }
        _length--;
        if (index < _length)
        {
            Array.Copy(_items, index + 1, _items, index, _length - index);
        }
        _items[_length] = default!;
        return true;
    }

    public void Clear()
    {
        var length = _length;
        _length = 0;
        if (length > 0)
        {
            Array.Clear(_items, 0, length);
        }
    }

    public int Length => _length;
    public int Capacity => _capacity;
    
    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)_items).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}