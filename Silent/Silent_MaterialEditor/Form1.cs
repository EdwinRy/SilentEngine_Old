using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silent.Entities;
using Silent.Tools;
using System.Windows.Forms;

namespace Silent_MaterialEditor
{
    public partial class Form1 : Form
    {

        

        public Form1()
        {
            InitializeComponent();
            ImportButton.Click += ImportButton_Click1;
        }


        private void ImportButton_Click1(object sender, EventArgs e)
        {
            EditorManager.level.AddUniversalEntity();          
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                
               EditorManager.ConsoleRunning = false;
               //remind the user to save their model
            }


            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                //Manually save the model

                EditorManager.ConsoleRunning = false;
            }

        }

    }
}
