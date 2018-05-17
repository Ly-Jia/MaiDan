using System.Drawing.Printing;
namespace MaiDan.Infrastructure
{
    public class Printer
    {
        private PrintDocument document;

        private readonly string name;

        public Printer(string name)
        {
            this.name = name;
        }

        public void Print(PrintDocument document)
        {
            document.PrinterSettings.PrinterName = name;
            document.Print();
        }
    }
}
