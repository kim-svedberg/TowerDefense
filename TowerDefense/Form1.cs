using System.Windows.Forms;
using System.Windows;
using System;
using System.Media;

namespace TowerDefense
{
    public partial class Form1 : Form
    {
        public bool startClicked;
        public bool quitClicked;
        public SoundPlayer soundPlayer;
        public Form1()
        {
            InitializeComponent();
            soundPlayer = new SoundPlayer();
            soundPlayer.SoundLocation = @"C:\Users\kimas\OneDrive\Desktop\Projects_and_Solutions\TowerDefense\TowerDefense\Content\menuMusicWAV.wav";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = false;
            Bounds = Screen.PrimaryScreen.Bounds;
            soundPlayer.Play();

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