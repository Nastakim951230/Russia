using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Russia
{
    public partial class Tour
    {
        public string actual
        {
            get
            {
                if (IsActual == true)
                {
                    return "Актуал";
                }
                else
                {
                    return "Не актуал";
                }
            }
        }
        public string kol_vo_biletov
        {
            get
            {
                return "Билетов: " + TicketCount;
            }
        }
        public SolidColorBrush actualBrush
        {
            get
            {
                var brushConverter = new BrushConverter();

                if (IsActual == true)
                {
                    return (SolidColorBrush)(Brush)brushConverter.ConvertFrom("#FF5FE35E");
                }
                else
                {
                    return (SolidColorBrush)(Brush)brushConverter.ConvertFrom("#e31e24");

                }

            }
        }

    }
}
