using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Image AI = new Bitmap("Images\\AI.png");
        Board theBoard;
        Graphics g;
        int stepCount = 400;
        int episodeCount = 0;
        double currentEpisodeReward = -1000;
        double maxEpisodeReward =-1000;
        bool updateUi = true;
        bool fullSpeed = false;
        float epsilon = 0;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            theBoard = new Board();
            theBoard.Reset();
            UpdateExplorationExploitation();
        }

        private void DrawMap()
        {
            for (int i = 0; i < 20; ++i)
                for (int j = 0; j < 20; ++j)
                    g.DrawImage(theBoard.GetSprite(i, j), i * 30, j * 30);
            DrawAI();
        }

        private void button1_Click(object sender, EventArgs e)
            => InitializeEpisode();

        private void InitializeEpisode()
        {
            if (currentEpisodeReward > maxEpisodeReward)
                maxEpisodeReward = currentEpisodeReward;
            currentEpisodeReward = 0;
            theBoard.Reset();
            if (updateUi)
            {
                lblMaxEpisodeReward.Text = $"{(int)maxEpisodeReward}";
                DrawMap();
            }
        }

        private void btnNextStep_Click(object sender, EventArgs e)
            => Step();

        private void Step()
        {
            if (stepCount == 400)
            {
                InitializeEpisode();
                stepCount = 0;
                episodeCount++;
                if (updateUi)
                    lblEpisodeCount.Text = $"{episodeCount}";
            }
            stepCount++;
            double[] state = theBoard.GetState(theBoard.AIx, theBoard.AIy);
            if (updateUi)
                DrawNeutral();
            //update location based on state and nn
            //get reward for new state from nn
            //train nn with old state and reward

            double[] directionRewards = theBoard.GetDirectionRewards(state);
            int direction = theBoard.UpdateLocation(directionRewards, epsilon);
            currentEpisodeReward += theBoard.GetReward();
            var newState = theBoard.GetState(theBoard.AIx, theBoard.AIy);
            theBoard.Learn(theBoard.AIx, theBoard.AIy, state, newState, direction, directionRewards);
            if (updateUi)
            {
                lblCurrentEpisodeReward.Text = $"{(int)currentEpisodeReward}";
                DrawAI();
                lblStepCount.Text = $"{stepCount}";
            }
            theBoard.ResetCurrentBlock();
        }

        private void DrawNeutral()
            => g.DrawImage(theBoard.Neutral, theBoard.AIx * 30, theBoard.AIy * 30);

        private void DrawAI()
            => g.DrawImage(AI, theBoard.AIx * 30, theBoard.AIy * 30);

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            button1.Enabled = !button1.Enabled;
        }

        private void timer1_Tick(object sender, EventArgs e)
            => Step();

        private void btnSave_Click(object sender, EventArgs e)
            => theBoard.Save(textBox1.Text);

        private void btnLoad_Click(object sender, EventArgs e)
            => theBoard.Load(textBox1.Text);

        private void trackBar1_ValueChanged(object sender, EventArgs e)
            => timer1.Interval = trackBar1.Value;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            updateUi = checkBox1.Checked;
            if (updateUi)
                DrawMap();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (fullSpeed)
            {
                fullSpeed = false;
                btnNextStep.Enabled = true;
                btnStartStop.Enabled = true;
                updateUi = true;
                checkBox1.Checked = true;
            }
            else
            {
                fullSpeed = true;
                updateUi = false;
                btnNextStep.Enabled = false;
                btnStartStop.Enabled = false;
                checkBox1.Checked = false;
                Task t = new Task(() =>
                {
                    while (fullSpeed)
                    {
                        Step();
                    }
                });
                t.Start();
            }
        }

        private void tbrEpsilon_ValueChanged(object sender, EventArgs e)
        {
            epsilon = (float)tbrEpsilon.Value / 100;
            UpdateExplorationExploitation();
        }

        private void UpdateExplorationExploitation()
        {
            lblExploitation.Text = $"{100 - tbrEpsilon.Value} %";
            lblExploration.Text = $"{tbrEpsilon.Value} %";
        }
    }
}
