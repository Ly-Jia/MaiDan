using System.Drawing;

namespace MaiDan.Infrastructure
{
    public static class PrintingFormat
    {
        public static readonly Font FONT;
        public static readonly Font BOLD_FONT;
        public static readonly int DISH_NAME_MAX_CHARACTERS;
        public static readonly StringFormat LEFT_ALIGNMENT;
        public static readonly StringFormat RIGHT_ALIGNMENT;
        public static readonly StringFormat CENTER_ALIGNMENT;

        static PrintingFormat()
        {
            FONT = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            BOLD_FONT = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            DISH_NAME_MAX_CHARACTERS = 30;

            LEFT_ALIGNMENT = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };

            RIGHT_ALIGNMENT = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };

            CENTER_ALIGNMENT = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }
    }
}
