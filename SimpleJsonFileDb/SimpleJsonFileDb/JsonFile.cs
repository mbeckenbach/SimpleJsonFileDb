using System.Collections;
using System.Collections.Generic;

namespace SimpleJsonFileDbContext
{
    /// <summary>
    /// Represents a json file, that contains an array of T returned as a list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonFile<T> : IList<T> where T : class
    {
        readonly IList<T> _list;
        readonly JsonFileHandler<T> _dataFile;

        /// <summary>
        /// Creates a new instance of this file from a given directory
        /// </summary>
        /// <param name="dataDirectory"></param>
        public JsonFile(string dataDirectory)
        {
            _dataFile = new JsonFileHandler<T>(dataDirectory);
            _list = _dataFile.Data;
        }

        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<T>

        public void Add(T item)
        {
            _list.Add(item);
            _dataFile.SaveChanges();
        }

        public void Clear()
        {
            _list.Clear();
            _dataFile.SaveChanges();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
            _dataFile.SaveChanges();
        }

        public bool Remove(T item)
        {
            var result = _list.Remove(item);
            _dataFile.SaveChanges();
            return result;
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return _list.IsReadOnly; }
        }

        #endregion

        #region Implementation of IList<T>

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            _dataFile.SaveChanges();
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
            _dataFile.SaveChanges();
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        #endregion

    }
}
