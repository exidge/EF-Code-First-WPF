using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Code_First_WPF
{

    class Baza
    {
        public static UczestnicyKonferencji context;
        private static string ConnString = @"Data Source = localhost\SQLEXPRESS; Integrated Security = true;";
        public static void initContext()
        {
            context = new UczestnicyKonferencji(ConnString);
        }

    }
}
