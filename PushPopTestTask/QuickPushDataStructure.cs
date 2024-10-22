namespace PushPopTestTask
{
    public class QuickPushDataStructure<T> where T : IComparable<T>
    {
        private T[] _data;
        private int _size;
        private readonly object _lockObj = new();

        public QuickPushDataStructure()
        {
            _data = new T[2];
            _size = 0;
        }

        public void Push(T item)
        {
            lock (_lockObj)
            {
                if (_size == _data.Length)
                {
                    Array.Resize(ref _data, _data.Length * 2);
                }

                _data[_size] = item;
                _size++;
            }
        }

        public T Pop()
        {
            lock (_lockObj)
            {
                if (_size == 0)
                {
                    throw new InvalidOperationException("No elements to pop.");
                }

                int maxIndex = 0;
                for (int i = 1; i < _size; i++)
                {
                    if (_data[i].CompareTo(_data[maxIndex]) > 0)
                    {
                        maxIndex = i;
                    }
                }

                T maxItem = _data[maxIndex];

                for (int i = maxIndex; i < _size - 1; i++)
                {
                    _data[i] = _data[i + 1];
                }

                _size--;
                return maxItem;
            }
        }
    }
}
