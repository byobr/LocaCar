using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public class DataUtil
    {
        public static int DiferencaEntreHoras(DateTime Data1, DateTime Data2)
        {
            TimeSpan Diferenca = ArredondarDataParaCima(Data2) - ArredondarDataParaCima(Data1);
            return Convert.ToInt32(Diferenca.TotalHours);
        }

        private static DateTime ArredondarDataParaCima(DateTime Data)
        {
            var updated = Data.AddMinutes(30);
            return new DateTime(updated.Year, updated.Month, updated.Day,
                                 updated.Hour, 0, 0, Data.Kind);
        }
    }
}
