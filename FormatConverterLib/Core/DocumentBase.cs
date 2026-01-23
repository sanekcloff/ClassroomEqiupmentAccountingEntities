using System;
using System.Collections.Generic;
using System.Text;

namespace FormatConverterLib.Core
{
    public abstract class DocumentBase<T>
    {
        #region Constructors
        protected DocumentBase(string title)
        {
            _createdDate = DateTime.Now;
            _headers = new List<string>();
            _data = new List<T>();
            _title = title;
            _filePath = string.Empty;
        }
        #endregion
        #region Fields & Properties
        protected string _filePath;
        protected string _title;
        protected DateTime _createdDate;
        protected List<string> _headers;
        protected List<T> _data;

        public string Title => _title;
        public string FilePath => _filePath;
        public DateTime CreatedDate => _createdDate;
        public List<string> Headers => _headers;
        public List<T> Data => _data;
        #endregion
        #region Methods
        public virtual void SetData(List<string> headers, List<T> data)
        {
            _headers = headers ?? throw new ArgumentNullException(nameof(headers));
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }
        public abstract void Generate(string filePath);
        public abstract void Save();
        public abstract byte[] GetBytes();
        #endregion
    }
}
