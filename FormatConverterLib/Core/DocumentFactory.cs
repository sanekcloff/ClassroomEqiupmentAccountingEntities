using FormatConverterLib.Formats;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatConverterLib.Core
{
    public enum DocumentType
    {
        Excel,
        Word,
        PDF
    }
    public class DocumentFactory
    {
        public static DocumentBase<T> CreateDocument<T>(DocumentType type, string title) => type switch
        {
            DocumentType.Excel => new ExcelDoc<T>(title),
            DocumentType.Word => new MSWordDoc<T>(title),
            DocumentType.PDF => new PDFDoc<T>(title),
            _ => throw new ArgumentException($"Не поддерживаемый тип документа: {type}")
        };
    }
}
