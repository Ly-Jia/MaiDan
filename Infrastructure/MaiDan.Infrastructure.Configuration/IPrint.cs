using System.Drawing.Printing;

namespace MaiDan.Infrastructure
{
    public interface IPrint
    {
        void Print(PrintDocument document);
    }
}
