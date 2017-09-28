using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtractExcelCellInfo
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        
        private static FormArchiva window;

    [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
<<<<<<< HEAD:Archiva/Archiva/Program.cs
            Application.Run(window = new FormArchiva());
=======
            Application.Run(new Form1());
>>>>>>> ffd0558803aea2defa0d89b577cd1314c31c1e30:Others/ExtractExcelCellInfo/ExtractExcelCellInfo/Program.cs
        }
    }
}
