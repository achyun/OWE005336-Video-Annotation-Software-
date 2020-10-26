using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OWE005336__Video_Annotation_Software_
{
    static class Program
    {
        public static LabellingDB.ImageDatabaseAccess ImageDatabase = new LabellingDB.ImageDatabaseAccess();

        public static int[] LabelShortcuts = new int[10];

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program.LabelShortcuts = Properties.Settings.Default.Shortcuts.Split(',').Select(n => Convert.ToInt32(n)).ToArray();

            fSplash splash = new fSplash();

            Application.Run(splash);

            if (splash.LoginSuccessful)
            {
                Application.Run(new fMain());
            }
        }
    }
}
