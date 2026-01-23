using FormatConverterLib.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatConverterLib.Formats
{
    public class ExcelDoc<T> : DocumentBase<T>
    {
        public ExcelDoc(string title) : base(title)
        {
        }

        public override void Generate(string filePath)
        {
            throw new NotImplementedException();
        }

        public override byte[] GetBytes()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
