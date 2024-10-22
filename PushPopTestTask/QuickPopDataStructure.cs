namespace PushPopTestTask
{
    public class QuickPopDataStructure<T> where T : IComparable<T>
    {
        private T[] _data;
        private int _size;
        private readonly object _lockObj = new();

        public QuickPopDataStructure()
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

                int index = 0;
                while (index < _size && _data[index].CompareTo(item) < 0)
                {
                    index++;
                }

                for (int i = _size; i > index; i--)
                {
                    _data[i] = _data[i - 1];
                }

                _data[index] = item;
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

                T maxItem = _data[_size - 1];
                _size--;
                return maxItem;
            }
        }
    }
}
