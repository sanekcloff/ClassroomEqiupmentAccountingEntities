using FormatConverterLib.Core;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FormatConverterLib.Formats
{
    public class PDFDoc<T> : DocumentBase<T>
    {
        private Document _document;
         
        public PDFDoc(string title) : base(title)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            _document = null!;
        }

        public override void Generate(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath)) _filePath = filePath;
            else throw new InvalidOperationException("Filepath не установлен");

            _document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    if (!string.IsNullOrWhiteSpace(Title))
                    {
                        page.Header().Text(Title).SemiBold().FontSize(16).FontColor(Colors.Blue.Medium);
                    }

                    page.Content().PaddingVertical(1, QuestPDF.Infrastructure.Unit.Centimetre).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            for (var i = 0; i < Headers.Count; i++)
                            {
                                columns.RelativeColumn();
                            }
                        });

                        foreach (var header in Headers)
                        {
                            table.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text(header).SemiBold();
                        }

                        foreach (var item in Data)
                        {
                            var props = typeof(T).GetProperties();
                            foreach (var property in props)
                            {
                                table.Cell()
                                .Border(1)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(5)
                                .Text(property.GetValue(item)?.ToString() ?? string.Empty);
                            }
                        }
                    });
                    page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Страница");
                        x.CurrentPageNumber();
                        x.Span(" из ");
                        x.TotalPages();
                    });
                });
            });
        }

        public override byte[] GetBytes()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            _document.GeneratePdfAndShow();
        }
    }
}
