using ClassroomEquipmentAccountingEntities.Models;
using FormatConverterLib.Core;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
            if (string.IsNullOrWhiteSpace(filePath))
                throw new InvalidOperationException("Filepath не установлен");

            _filePath = filePath;

            _document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    if (!string.IsNullOrWhiteSpace(Title))
                    {
                        page.Header().Text(Title).SemiBold().FontSize(16).FontColor(Colors.Blue.Medium);
                    }

                    page.Content().PaddingVertical(1, Unit.Centimetre).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            foreach (var _ in Headers)
                            {
                                columns.RelativeColumn();
                            }
                        });

                        // Заголовки таблицы
                        foreach (var header in Headers)
                        {
                            table.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text(header).SemiBold();
                        }

                        // Данные таблицы
                        foreach (var item in Data)
                        {
                            if (item is RepairRequest request)
                            {
                                // Основные данные заявки
                                table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(request.Id.ToString());
                                table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(request.Description);
                                table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(request.StartDate.ToString("dd.MM.yyyy HH:mm:ss"));
                                table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(request.EndDate?.ToString("dd.MM.yyyy HH:mm:ss") ?? "Не завершена");

                                // Оборудование
                                if (request.RepairRequestEquipments.Any())
                                {
                                    foreach (var equipment in request.RepairRequestEquipments)
                                    {
                                        table.Cell().ColumnSpan(4).Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                                            .Text($"Оборудование: {equipment.Equipment?.ToString() ?? "Неизвестное оборудование"}");
                                    }
                                }
                                else
                                {
                                    table.Cell().ColumnSpan(4).Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                                        .Text("Оборудование: Отсутствует");
                                }
                            }
                            else
                            {
                                // Для других типов данных
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
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Страница ");
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
