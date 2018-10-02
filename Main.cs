using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Xirgu
{
    public class main
    {
        public static void Main(String[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmXirgu());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ah Carajo! Ocurrió una excepción!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
