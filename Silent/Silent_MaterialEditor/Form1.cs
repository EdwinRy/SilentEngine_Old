using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Silent_MaterialEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        void FormClosing(object sender,FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
               //remind the user to save their model
            }


            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                //Manually save the model
            }

        }

    }
}
