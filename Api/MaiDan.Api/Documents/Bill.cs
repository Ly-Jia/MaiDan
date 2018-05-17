using System.Drawing;
using System.Drawing.Printing;
using MaiDan.Api.DataContracts.Responses;
using MaiDan.Infrastructure;

namespace MaiDan.Api.Documents
{
    public class Bill : PrintDocument
    {
        private readonly DetailedBill bill;

        public Bill(Ordering.Domain.Order order, Billing.Domain.Bill bill)
        {
            this.bill = new DetailedBill(order, bill);

            PrintController = new StandardPrintController(); //To disable pop-up window that shows the printing progress
            PrintPage += billPrintDocument_PrintPage;
        }

        private void billPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            RectangleF bounds = new RectangleF(
                (float)5,
                (float)0,
                (float)e.PageBounds.Width - 30,
                (float)PrintingFormat.FONT.GetHeight(e.Graphics));


            bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics) * 2;
            
            if (!bill.NumberOfGuests.HasValue)
            {
                e.Graphics.DrawString("À emporter", PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.RIGHT_ALIGNMENT);
                bounds.Y = 10 + PrintingFormat.FONT.GetHeight(e.Graphics) * 10;
            }
            else
            {
                bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics);
                e.Graphics.DrawString("Table : " + bill.TableId, PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.LEFT_ALIGNMENT);
                e.Graphics.DrawString("Couverts : " + bill.NumberOfGuests, PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.RIGHT_ALIGNMENT);
                bounds.Y = 10 + PrintingFormat.FONT.GetHeight(e.Graphics) * 11;
            }

            foreach (var line in bill.Lines)
            {
                if (line.DishName.Length > 23)
                {
                    e.Graphics.DrawString(line.Quantity + " " + line.DishName.Substring(0, 23), PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.LEFT_ALIGNMENT);
                }
                else
                {
                    e.Graphics.DrawString(line.Quantity + " " + line.DishName, PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.LEFT_ALIGNMENT);
                }
                e.Graphics.DrawString(line.Amount.ToString("N2") + " €", PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.RIGHT_ALIGNMENT);
                bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics);               
            }

            foreach (var discount in bill.Discounts)
            {
                e.Graphics.DrawString($"{discount.Name} -{discount.Rate * 100}%", PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.LEFT_ALIGNMENT);
                e.Graphics.DrawString("-" + discount.Amount.ToString("N2") + " €", PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.RIGHT_ALIGNMENT);
                bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics);
            }
            
            e.Graphics.DrawLine(Pens.Black, bounds.X, bounds.Y + PrintingFormat.FONT.GetHeight(e.Graphics) / 2, bounds.Width, bounds.Y + PrintingFormat.FONT.GetHeight(e.Graphics) / 2);
            bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics) * 2;

            e.Graphics.DrawString("Total", PrintingFormat.BOLD_FONT, Brushes.Black, bounds, PrintingFormat.LEFT_ALIGNMENT);
            e.Graphics.DrawString(bill.Total.ToString("N2") + " €", PrintingFormat.BOLD_FONT, Brushes.Black, bounds, PrintingFormat.RIGHT_ALIGNMENT);
            bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics) * 2;
            
            foreach (var tax in bill.Taxes)
            {
                e.Graphics.DrawString($"TVA {tax.Rate * 100}%", PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.LEFT_ALIGNMENT);
                e.Graphics.DrawString(tax.Amount.ToString("N2") + " €", PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.RIGHT_ALIGNMENT);
                bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics);
            }
        }

    }
}
