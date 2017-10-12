using System;
using System.Drawing;
using System.Linq;
using Accord.Neuro;
using Accord.Neuro.Learning;

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
        float learningRate = 0.2f;
        public ActivationNetwork network;
        public ResilientBackpropagationLearning teacher;

        public Board()
        {
            blocks = new Block[20, 20];
            var activationFunction = new Accord.Neuro.ActivationFunctions.GaussianFunction();
            network = new ActivationNetwork(new SigmoidFunction(0.01), 845, new[] {  4 });
            //new GaussianWeights(network, 0.1).Randomize();
            teacher = new ResilientBackpropagationLearning(network);
            teacher.LearningRate = 0.1;
        }

        private Block CreateRandomBlock()
        {
            double n = r.Next(0, 4);
            if (n == 0)
                return new Block { Reward = -0.1, Sprite = Neutral, Kind = 1 };
            if (n == 1)
                return new Block { Reward = -2, Sprite = Bad, Kind = 2 };
            if (n == 2)
                return new Block { Reward = 1, Sprite = Good, Kind = 3 };
            return new Block { Reward = 2, Sprite = PlusGood, Kind = 4 };
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
            blocks[AIx, AIy].Reward = -0.3;
            blocks[AIx, AIy].Sprite = Neutral;
            blocks[AIx, AIy].Kind = 1;
        }

        public double[] GetDirectionRewards(double[] state)
            => network.Compute(state);

        public int UpdateLocation(double[] directionRewards, float epsilon)
        {
            int direction = 0;
            var max = directionRewards.Max();
            for (int i = 0; i < 4; ++i)
                if (directionRewards[i] == max)
                {
                    direction = i;
                    break;
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

        public double GetReward()
            => blocks[AIx, AIy].Reward;

        private int GetKind(int x, int y)
        {
            if (x < 0 || x >= 20 || y < 0 || y >= 20)
                return 0;
            return blocks[x, y].Kind;
        }

        public double[] GetState(int aix, int aiy)
        {
            double[] state = new double[845];
            int counter = 0;
            for (int i = -6; i <= 6; ++i)
                for (int j = -6; j <= 6; ++j)
                {
                    var tmp = GetKind(aix + i, aiy + j);
                    state[counter + tmp] = 1;
                    counter += 5;
                }
            return state;
        }

        internal void Save(string path)
            => network.Save(path);

        internal void Load(string path)
        {
            network = (ActivationNetwork)Network.Load(path);
            teacher = new ResilientBackpropagationLearning(network);
        }

        public void Learn(int newAix, int newAiy, double[] oldState, double[] newState, int direction, double[] directionRewards)
        {
            var currentReward = blocks[AIx, AIy].Reward;
            var nextStateReward = network.Compute(newState).Max();
            directionRewards[direction] = currentReward + (discount * nextStateReward);
            teacher.Run(oldState, directionRewards);
        }
    }
}
