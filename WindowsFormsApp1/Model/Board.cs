using System;
using System.Drawing;
using System.Linq;

namespace WindowsFormsApp1.Model
{
    public class Board
    {
        public Random r = new Random();
        private Block[,] blocks;
        public Image Neutral = new Bitmap("Images\\Neutral.png");
        public Image Bad = new Bitmap("Images\\Bad.png");
        public Image Good = new Bitmap("Images\\Good.png");
        public Image PlusGood = new Bitmap("Images\\PlusGood.png");
        public int AIx = 0;
        public int AIy = 0;
        float discount = 0.9f;
        float learningRate = 0.1f;

        public Board()
        {
            blocks = new Block[20, 20];
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

        public Image GetSprite(int i, int j)
            => blocks[i, j].Sprite;

        public void Reset()
        {
            for (int i = 0; i < 20; ++i)
                for (int j = 0; j < 20; ++j)
                    blocks[i, j] = CreateRandomBlock();
            AIx = r.Next(0, 19);
            AIy = r.Next(0, 19);
            ResetCurrentBlock();
        }
        public void ResetCurrentBlock()
        {
            blocks[AIx, AIy].Reward = -10;
            blocks[AIx, AIy].Sprite = Neutral;
            blocks[AIx, AIy].Kind = 2;
        }

        public int UpdateLocation(int[] states, float epsilon)
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
            if ((float)r.NextDouble() < epsilon)
                direction = r.Next(0, 4);
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

        internal int CurrentBlockReward()
            => blocks[AIx, AIy].Reward;

        private ulong GetKind(int x, int y, ulong current)
        {
            if (x < 0 || x >= 20 || y < 0 || y >= 20)
                return current + 1;
            return current + (ulong)blocks[x, y].Kind;
        }

        public ulong GetState()
        {
            ulong result = 0;
            result = GetKind(AIx, AIy - 2, result) << 3;
            result = GetKind(AIx, AIy - 1, result) << 13;
            result = GetKind(AIx + 2, AIy, result) << 3;
            result = GetKind(AIx + 1, AIy, result) << 13;
            result = GetKind(AIx, AIy + 2, result) << 3;
            result = GetKind(AIx, AIy + 1, result) << 13;
            result = GetKind(AIx - 2, AIy, result) << 3;
            result = GetKind(AIx - 1, AIy, result);
            return result;
        }

        public int GetReward(int[] stateActions, int earlierReward)
        {
            var currentReward = blocks[AIx, AIy].Reward;
            var nextStateReward = (int)(stateActions.Max() * discount);
            return earlierReward + (int)(learningRate * (currentReward + (discount * nextStateReward) - earlierReward));
        }
    }
}
