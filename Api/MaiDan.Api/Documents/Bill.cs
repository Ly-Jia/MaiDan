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

            PrintLegalMentions(e, ref bounds);

            PrintOrderType(e, ref bounds);

            PrintLines(e, ref bounds);

            PrintDiscounts(e, ref bounds);
            
            PrintTotal(e, ref bounds);

            PrintTaxes(e, ref bounds);
        }

        private void PrintLegalMentions(PrintPageEventArgs e, ref RectangleF bounds)
        {
            foreach (var line in BillConfiguration.LEGAL_MENTIONS)
            {
                e.Graphics.DrawString(line, PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.CENTER_ALIGNMENT);
                bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics);
            }
            bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics) * 2;
        }

        private void PrintOrderType(PrintPageEventArgs e, ref RectangleF bounds)
        {
            e.Graphics.DrawString($"Ticket n°{bill.Id} - Edité le {bill.BillingDate:dd/MM/yyyy hh:mm}", PrintingFormat.FONT, Brushes.Black, bounds,PrintingFormat.LEFT_ALIGNMENT);

            if (!bill.NumberOfGuests.HasValue)
            {
                e.Graphics.DrawString("À emporter", PrintingFormat.FONT, Brushes.Black, bounds, PrintingFormat.RIGHT_ALIGNMENT);
                bounds.Y += 10 + PrintingFormat.FONT.GetHeight(e.Graphics);
            }
            else
            {
                bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics);
                e.Graphics.DrawString("Table : " + bill.TableId, PrintingFormat.FONT, Brushes.Black, bounds,
                    PrintingFormat.LEFT_ALIGNMENT);
                e.Graphics.DrawString("Couverts : " + bill.NumberOfGuests.Value, PrintingFormat.FONT, Brushes.Black, bounds,
                    PrintingFormat.RIGHT_ALIGNMENT);
                bounds.Y += 10 + PrintingFormat.FONT.GetHeight(e.Graphics) * 11;
            }
        }

        private void PrintLines(PrintPageEventArgs e, ref RectangleF bounds)
        {
            foreach (var line in bill.Lines)
            {
                if (line.DishName.Length > 23)
                {
                    e.Graphics.DrawString(line.Quantity + " " + line.DishName.Substring(0, 23), PrintingFormat.FONT,
                        Brushes.Black, bounds, PrintingFormat.LEFT_ALIGNMENT);
                }
                else
                {
                    e.Graphics.DrawString(line.Quantity + " " + line.DishName, PrintingFormat.FONT, Brushes.Black, bounds,
                        PrintingFormat.LEFT_ALIGNMENT);
                }
                e.Graphics.DrawString(line.Amount.ToString("N2") + " €", PrintingFormat.FONT, Brushes.Black, bounds,
                    PrintingFormat.RIGHT_ALIGNMENT);
                bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics);
            }
        }

        private void PrintDiscounts(PrintPageEventArgs e, ref RectangleF bounds)
        {
            foreach (var discount in bill.Discounts)
            {
                e.Graphics.DrawString($"{discount.Name} -{discount.Rate * 100}%", PrintingFormat.FONT, Brushes.Black, bounds,
                    PrintingFormat.LEFT_ALIGNMENT);
                e.Graphics.DrawString("-" + discount.Amount.ToString("N2") + " €", PrintingFormat.FONT, Brushes.Black, bounds,
                    PrintingFormat.RIGHT_ALIGNMENT);
                bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics);
            }
        }

        private void PrintTotal(PrintPageEventArgs e, ref RectangleF bounds)
        {
            e.Graphics.DrawLine(Pens.Black, bounds.X, bounds.Y + PrintingFormat.FONT.GetHeight(e.Graphics) / 2, bounds.Width,
                bounds.Y + PrintingFormat.FONT.GetHeight(e.Graphics) / 2);
            bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics) * 2;

            e.Graphics.DrawString("Total", PrintingFormat.BOLD_FONT, Brushes.Black, bounds, PrintingFormat.LEFT_ALIGNMENT);
            e.Graphics.DrawString(bill.Total.ToString("N2") + " €", PrintingFormat.BOLD_FONT, Brushes.Black, bounds,
                PrintingFormat.RIGHT_ALIGNMENT);
            bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics) * 2;
        }

        private void PrintTaxes(PrintPageEventArgs e, ref RectangleF bounds)
        {
            foreach (var tax in bill.Taxes)
            {
                e.Graphics.DrawString($"TVA {tax.Rate * 100}%", PrintingFormat.FONT, Brushes.Black, bounds,
                    PrintingFormat.LEFT_ALIGNMENT);
                e.Graphics.DrawString(tax.Amount.ToString("N2") + " €", PrintingFormat.FONT, Brushes.Black, bounds,
                    PrintingFormat.RIGHT_ALIGNMENT);
                bounds.Y += PrintingFormat.FONT.GetHeight(e.Graphics);
            }
        }
    }
}
