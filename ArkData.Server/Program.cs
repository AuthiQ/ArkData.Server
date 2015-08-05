using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkData.Server
{
    static class Program
    {
        public static frmConfig cfgForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            cfgForm = new frmConfig();
            Application.Run(cfgForm);
        }
    }
}
