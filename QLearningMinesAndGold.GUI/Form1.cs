using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
        int currentEpisodeReward = 0;
        int maxEpisodeReward = 0;
        bool updateUi = true;
        bool fullSpeed = false;
        float epsilon = 0;

        Dictionary<ulong, int[]> QTable = new Dictionary<ulong, int[]>();

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
                lblMaxEpisodeReward.Text = $"{maxEpisodeReward}";
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
            ulong state = theBoard.GetState();
            int[] stateActions = GetOrAddStateActions(state);
            if (updateUi)
                DrawNeutral();

            int direction = theBoard.UpdateLocation(stateActions, epsilon);
            var newState = theBoard.GetState();

            var newStateActions = GetOrAddStateActions(newState);
            currentEpisodeReward += theBoard.CurrentBlockReward();
            QTable[state][direction] = theBoard.GetReward(newStateActions, QTable[state][direction]);
            if (updateUi)
            {
                lblCurrentEpisodeReward.Text = $"{currentEpisodeReward}";
                DrawAI();
                lblStepCount.Text = $"{stepCount}";
            }
            theBoard.ResetCurrentBlock();
        }

        private void DrawNeutral()
            => g.DrawImage(theBoard.Neutral, theBoard.AIx * 30, theBoard.AIy * 30);

        private void DrawAI()
            => g.DrawImage(AI, theBoard.AIx * 30, theBoard.AIy * 30);

        private int[] GetOrAddStateActions(ulong state)
        {
            if (!QTable.Keys.Contains(state))
            {
                QTable.Add(state, new int[] { -100, -100, -100, -100 });
                if (updateUi)
                    lblQValues.Text = $"{QTable.Count}";
            }
            return QTable[state];
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            button1.Enabled = !button1.Enabled;
        }

        private void timer1_Tick(object sender, EventArgs e)
            => Step();

        private void btnSave_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in QTable.Keys)
            {
                var val = QTable[key];
                sb.AppendLine($"{key},{val[0]},{val[1]},{val[2]},{val[3]}");
            }
            File.WriteAllText(textBox1.Text, sb.ToString());
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            QTable = new Dictionary<ulong, int[]>();
            foreach (var line in File.ReadAllLines(textBox1.Text))
            {
                string[] values = line.Split(',');
                var key = ulong.Parse(values[0]);
                var vals = new int[4];
                for (int i = 0; i < 4; ++i)
                    vals[i] = int.Parse(values[i + 1]);
                QTable.Add(key, vals);
            }
            lblQValues.Text = $"{QTable.Count}";
        }

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
