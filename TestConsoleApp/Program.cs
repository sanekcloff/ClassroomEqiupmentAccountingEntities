using FormatConverterLib.Formats;

namespace TestConsoleApp
{
    internal class Program
    {
        class Product
        {
            public Product(string title, decimal price)
            {
                Title = title;
                Price = price;
            }

            public string Title { get; set; }
            public decimal Price { get; set; }
        }
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>()
            {
                new Product("Агурцы", 12),
                new Product("Бонаны",15),
            };
            var docPDF = new PDFDoc<Product>("EgorMaskimov");
            docPDF.SetData(["Название","Стоимость"], products);
            docPDF.Generate("C:\\Users\\master\\Documents");
            docPDF.Save();
        }
    }
}
