using System.Windows.Forms;
using System.Windows;
using System;

namespace TowerDefense
{
    public partial class Form1 : Form
    {
        public bool startClicked;
        public bool quitClicked;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = false;
            Bounds = Screen.PrimaryScreen.Bounds;
        }

        private void lblHeader_Click(object sender, EventArgs e)
        {

        }

        public void btnStart_Click(object sender, EventArgs e)
        {
            startClicked = true;

        }

        public void btnQuit_Click(object sender, EventArgs e)
        {
            quitClicked = true;
        }

    }
}