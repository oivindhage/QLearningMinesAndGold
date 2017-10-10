using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Block[,] blocks = new Block[20, 20];
        public Random r = new Random();
        public Image Neutral = new Bitmap("Images\\Neutral.png");
        public Image Bad = new Bitmap("Images\\Bad.png");
        public Image Good = new Bitmap("Images\\Good.png");
        public Image PlusGood = new Bitmap("Images\\PlusGood.png");
        public Image AI = new Bitmap("Images\\AI.png");
        int AIx = 0;
        int AIy = 0;
        float discount = 0.3f;
        Graphics g;
        int stepCount = 0;
        int episodeCount = 0;
        bool updateUi = true;

        Dictionary<ulong, int[]> QTable = new Dictionary<ulong, int[]>();

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
        }

        private void DrawMap()
        {
            for (int i = 0; i < 20; ++i)
                for (int j = 0; j < 20; ++j)
                    g.DrawImage(blocks[i, j].Sprite, i * 30, j * 30);
            DrawAI();
        }

        private void button1_Click(object sender, EventArgs e)
            => InitializeEpisode();

        private void InitializeEpisode()
        {
            for (int i = 0; i < 20; ++i)
                for (int j = 0; j < 20; ++j)
                    blocks[i, j] = CreateRandomBlock();
            AIx = r.Next(0, 19);
            AIy = r.Next(0, 19);
            ResetCurrentBlock();
            if (updateUi)
                DrawMap();
        }

        private Block CreateRandomBlock()
        {
            double n = r.Next(0, 4);
            if (n == 0)
                return new Block { Reward = -10, Sprite = Neutral, Kind = 2 };
            if (n == 1)
                return new Block { Reward = -30, Sprite = Bad, Kind = 3 };
            if (n == 2)
                return new Block { Reward = 20, Sprite = Good, Kind = 4 };
            return new Block { Reward = 100, Sprite = PlusGood, Kind = 5 };
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
                lblEpisodeCount.Text = $"{episodeCount}";
            }
            lblStepCount.Text = $"{stepCount++}";
            ulong state = GetState();
            int rotation = GetRotation(state);//or mirror?
            int[] stateActions = GetOrAddStateActions(state, rotation);
            if (updateUi)
                DrawNeutral();
            int direction = UpdateLocation(stateActions);
            var newState = GetState();
            var newRotation = GetRotation(newState);
            var newStateActions = GetOrAddStateActions(newState, newRotation);
            QTable[state][direction] = GetReward(newStateActions, QTable[state][direction]);//must set to rotated state
            if (updateUi)
                DrawAI();
            ResetCurrentBlock();
        }

        private int GetRotation(ulong state)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (QTable.Keys.Contains(state))
                    return i;
                state = (state << 16) + (state >> 48);
            }
            return 0;
        }

        private int GetReward(int[] stateActions, int currentReward)
        {
            int result = currentReward + blocks[AIx, AIy].Reward + (int)(stateActions.Max() * discount);
            if (result > 10000)
                return 10000;
            if (result < -10000)
                return -10000;
            return result;
        }

        private void DrawNeutral()
            => g.DrawImage(Neutral, AIx * 30, AIy * 30);

        private void DrawAI()
            => g.DrawImage(AI, AIx * 30, AIy * 30);

        private void ResetCurrentBlock()
        {
            blocks[AIx, AIy].Reward = 0;
            blocks[AIx, AIy].Sprite = Neutral;
            blocks[AIx, AIy].Kind = 2;
        }

        private int UpdateLocation(int[] states)
        {
            int direction = 0;
            int max = int.MinValue;
            for (int i = 0; i < 4; ++i)
            {
                var tmp = states[i] + r.Next(50);
                if (tmp > max)
                {
                    max = tmp;
                    direction = i;
                }
            }
            if (direction == 0)
            {
                if (AIy > 0)
                    AIy--;
            }
            else if (direction == 1)
            {
                if (AIx < 19)
                    AIx++;
            }
            else if (direction == 2)
            {
                if (AIy < 19)
                    AIy++;
            }
            else
            {
                if (AIx > 0)
                    AIx--;
            }
            return direction;
        }

        private int[] GetOrAddStateActions(ulong state, int rotation)
        {
            for (int i = 0; i < rotation; ++i)
                state = (state << 16) + (state >> 48);
            if (!QTable.Keys.Contains(state))
            {
                QTable.Add(state, new int[] { -100, -100, -100, -100 });
                lblQValues.Text = $"{QTable.Count}";
            }
            var result = QTable[state];
            for (int i = 0; i < rotation; ++i)
            {
                var tmp = result[0];
                result[0] = result[1];
                result[1] = result[2];
                result[2] = result[3];
                result[3] = tmp;
            }
            return result;
        }

        private ulong GetKind(int x, int y, ulong current)
        {
            if (x < 0 || x >= 20 || y < 0 || y >= 20)
                return current + 1;
            return current + (ulong)blocks[x, y].Kind;
        }

        private ulong GetState()
        {
            ulong result = 0;
            result = GetKind(AIx, AIy - 3, result) << 3;
            result = GetKind(AIx, AIy - 2, result) << 3;
            result = GetKind(AIx, AIy - 1, result) << 3;
            result = GetKind(AIx + 1, AIy - 1, result) << 7;

            result = GetKind(AIx + 3, AIy, result) << 3;
            result = GetKind(AIx + 2, AIy, result) << 3;
            result = GetKind(AIx + 1, AIy, result) << 3;
            result = GetKind(AIx + 1, AIy + 1, result) << 7;

            result = GetKind(AIx, AIy + 3, result) << 3;
            result = GetKind(AIx, AIy + 2, result) << 3;
            result = GetKind(AIx, AIy + 1, result) << 3;
            result = GetKind(AIx - 1, AIy + 1, result) << 7;

            result = GetKind(AIx - 3, AIy, result) << 3;
            result = GetKind(AIx - 2, AIy, result) << 3;
            result = GetKind(AIx - 1, AIy, result) << 3;
            result = GetKind(AIx - 1, AIy - 1, result);

            for (int i = AIx - 1; i <= AIx + 1; ++i)
                for (int j = AIy - 1; j <= AIy + 1; ++j)
                {
                    if (i == AIx || j == AIy)
                        continue;
                    if (i < 0 || i >= 20 || j < 0 || j <= 20)
                        result += 1;
                    else
                        result += (ulong)blocks[i, j].Kind;
                    result <<= 3;
                }
            result += AIy - 2 < 0 ? 1 : (ulong)blocks[AIx, AIy - 2].Kind;
            result <<= 3;
            result += AIx - 2 < 0 ? 1 : (ulong)blocks[AIx - 2, AIy].Kind;
            result <<= 3;
            result += AIy + 2 >= 18 ? 1 : (ulong)blocks[AIx, AIy + 2].Kind;
            result <<= 3;
            result += AIx + 2 >= 18 ? 1 : (ulong)blocks[AIx + 2, AIy].Kind;
            return result;
        }

        private void btnStartStop_Click(object sender, EventArgs e)
            => timer1.Enabled = !timer1.Enabled;

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
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
            => timer1.Interval = trackBar1.Value;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            updateUi = checkBox1.Checked;
            if (updateUi)
                DrawMap();
        }
    }
}
