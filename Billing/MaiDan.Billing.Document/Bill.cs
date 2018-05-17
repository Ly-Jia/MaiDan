using System;
using System.Drawing;
using System.Drawing.Printing;

namespace MaiDan.Billing.Document
{
    public class Bill : PrintDocument
    {
        private readonly Domain.Bill bill;
        private readonly Font font;
        private readonly Font boldFont;

        public Bill(Domain.Bill bill, Font font, Font boldFont)
        {
            this.bill = bill;
            this.font = font;
            this.boldFont = boldFont;
            PrintController = new StandardPrintController();
            PrintPage += billPrintDocument_PrintPage;
        }

        private void billPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            RectangleF bounds = new RectangleF(
                (float)5,
                (float)0,
                (float)e.PageBounds.Width - 30,
                (float)this.font.GetHeight(e.Graphics));


            bounds.Y += this.font.GetHeight(e.Graphics) * 2;
            e.Graphics.DrawString(Printer.bill.Id.ToString("dd/MM/yyyy"), this.font, Brushes.Black, bounds, this.leftStringFormat);

            if (Printer.bill.IsTakeAway == true)
            {
                e.Graphics.DrawString("À emporter", this.billFont, Brushes.Black, headerBounds, this.rightStringFormat);
            }
            else
            {
                bounds.Y += this.billFont.GetHeight(e.Graphics);
                e.Graphics.DrawString("Table : " + Printer.bill.Table.Id.ToString(), this.billFont, Brushes.Black, headerBounds, this.leftStringFormat);
                e.Graphics.DrawString("Couverts : " + Printer.bill.PlaceCount.ToString(), this.billFont, Brushes.Black, headerBounds, this.rightStringFormat);
            }

            Decimal billHT = Math.Round(Printer.bill.HT_5_5 + Printer.bill.HT_19_6, 2);

            if (bill is)
            {
                bounds.Y = 10 + this.font.GetHeight(e.Graphics) * 10;
            }
            else
            {
                bounds.Y = 10 + this.font.GetHeight(e.Graphics) * 11;
            }

            foreach (BillDetail billDetail in Printer.bill.BillDetails)
            {
                if (billDetail.Course != null)
                {
                    if (billDetail.Quantity == 0)
                    {
                        if (billDetail.Course.Name.Length > 23)
                        {
                            e.Graphics.DrawString("- " + billDetail.Course.Name.Substring(0, 23), this.font, Brushes.Black, bounds, this.leftStringFormat);
                        }
                        else
                        {
                            e.Graphics.DrawString("- " + billDetail.Course.Name, this.font, Brushes.Black, bounds, this.leftStringFormat);
                        }
                        e.Graphics.DrawString("Offert", this.font, Brushes.Black, bounds, this.rightStringFormat);
                        bounds.Y += this.font.GetHeight(e.Graphics);
                    }
                    else
                    {
                        if (billDetail.Course.Name.Length > 23)
                        {
                            e.Graphics.DrawString(billDetail.Quantity.ToString() + " " + billDetail.Course.Name.Substring(0, 23), this.font, Brushes.Black, bounds, this.leftStringFormat);
                        }
                        else
                        {
                            e.Graphics.DrawString(billDetail.Quantity.ToString() + " " + billDetail.Course.Name, this.font, Brushes.Black, bounds, this.leftStringFormat);
                        }
                        e.Graphics.DrawString(billDetail.Total.ToString("N2"), this.font, Brushes.Black, bounds, this.rightStringFormat);
                        bounds.Y += this.font.GetHeight(e.Graphics);
                    }
                }
            }

            if (Printer.bill.IsTakeAway == true)
            {
                e.Graphics.DrawString("Remise 10% - à emporter", this.font, Brushes.Black, bounds, this.leftStringFormat);
                e.Graphics.DrawString("-" + Printer.bill.TakeAwayDiscount.ToString("N2"), this.font, Brushes.Black, bounds, this.rightStringFormat);
                bounds.Y += this.font.GetHeight(e.Graphics);
            }

            if (Printer.bill.Discount > 0)
            {
                e.Graphics.DrawString("Remise spéciale", this.font, Brushes.Black, bounds, this.leftStringFormat);
                e.Graphics.DrawString("-" + Printer.bill.Discount.ToString("N2"), this.font, Brushes.Black, bounds, this.rightStringFormat);
                bounds.Y += this.font.GetHeight(e.Graphics);
            }

            e.Graphics.DrawLine(Pens.Black, bounds.X, bounds.Y + this.font.GetHeight(e.Graphics) / 2, bounds.Width, bounds.Y + this.font.GetHeight(e.Graphics) / 2);
            bounds.Y += this.font.GetHeight(e.Graphics);

            e.Graphics.DrawString("Total", this.fontBold, Brushes.Black, bounds, this.leftStringFormat);
            e.Graphics.DrawString(Printer.bill.Total.ToString("N2"), this.fontBold, Brushes.Black, bounds, this.rightStringFormat);
            bounds.Y += this.font.GetHeight(e.Graphics) * 2;

            e.Graphics.DrawString("HT", this.font, Brushes.Black, bounds, this.leftStringFormat);
            e.Graphics.DrawString(billHT.ToString(), this.font, Brushes.Black, bounds, this.rightStringFormat);
            bounds.Y += this.font.GetHeight(e.Graphics);

            if (Printer.bill.Id < this.firstTvaChangeDate)
                e.Graphics.DrawString("TVA 5.5%", this.font, Brushes.Black, bounds, this.leftStringFormat);
            else
            {
                if (Printer.bill.Id < this.secondTvaChangeDate)
                    e.Graphics.DrawString("TVA 7%", this.font, Brushes.Black, bounds, this.leftStringFormat);
                else
                    e.Graphics.DrawString("TVA 10%", this.font, Brushes.Black, bounds, this.leftStringFormat);
            }
            e.Graphics.DrawString(Printer.bill.TVA_5_5.ToString("N2"), this.font, Brushes.Black, bounds, this.rightStringFormat);
            bounds.Y += this.font.GetHeight(e.Graphics);

            if (Printer.bill.TVA_19_6 > 0)
            {
                if (Printer.bill.Id < this.secondTvaChangeDate)
                    e.Graphics.DrawString("TVA 19.6%", this.font, Brushes.Black, bounds, this.leftStringFormat);
                else
                    e.Graphics.DrawString("TVA 20%", this.font, Brushes.Black, bounds, this.leftStringFormat);
                e.Graphics.DrawString(Printer.bill.TVA_19_6.ToString("N2"), this.font, Brushes.Black, bounds, this.rightStringFormat);
                bounds.Y += this.font.GetHeight(e.Graphics) * 2;
            }
            else
            {
                bounds.Y += this.font.GetHeight(e.Graphics);
            }

            e.Graphics.DrawString("Le Mandarin Pithiviers", this.font, Brushes.Black, bounds, this.centerStringFormat);
            bounds.Y += this.font.GetHeight(e.Graphics);
            e.Graphics.DrawString("vous remercie de votre visite", this.font, Brushes.Black, bounds, this.centerStringFormat);
            bounds.Y += this.font.GetHeight(e.Graphics);
            e.Graphics.DrawString("À bientôt !", this.font, Brushes.Black, bounds, this.centerStringFormat);
            bounds.Y += this.font.GetHeight(e.Graphics) * 2;
            e.Graphics.DrawString(" ", this.font, Brushes.Black, bounds, this.centerStringFormat);

        }

    }
}
