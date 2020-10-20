using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OWE005336__Video_Annotation_Software_
{
    static class Program
    {
        public static ImageDatabaseAccess ImageDatabase = new ImageDatabaseAccess();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            fSplash splash = new fSplash();

            Application.Run(splash);

            if (splash.LoginSuccessful)
            {
                Application.Run(new fMain());
            }
        }
    }
}
