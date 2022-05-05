using System.Windows.Media;

namespace CellularAutomaton.Cells
{
    abstract class ChromaticCell : LivingCell
    {
        public SolidColorBrush Chrome
        {
            get; set;
        }

        private static SolidColorBrush BLACK = new SolidColorBrush(Colors.Black);
        private int max = 255 * 6;
        private int chromeCount = 0;
        private int increment = 64;

        public override void Update(int lifespan = 0)
        {
            base.Update(lifespan);

            if (Life == lifespan)
            {
                Chrome = new SolidColorBrush(Color.FromRgb(Red(), Green(), Blue()));
            }
            else if (Life == 0)
            {
                Chrome = BLACK;
                chromeCount += increment;
                chromeCount %= max;
            }
        }

        private byte Red()
        {
            int mod = chromeCount % 255;
            if (chromeCount / 255 == 0 || chromeCount / 255 == 5)
            {
                return 255;
            }
            if (chromeCount / 255 == 1)
            {
                return (byte)(255 - mod);
            }
            if (chromeCount / 255 == 4)
            {
                return (byte)mod;
            }
            return 0;
        }

        private byte Green()
        {
            int mod = chromeCount % 255;
            if (chromeCount / 255 == 1 || chromeCount / 255 == 2)
            {
                return 255;
            }
            if (chromeCount / 255 == 3)
            {
                return (byte)(255 - mod);
            }
            if (chromeCount / 255 == 0)
            {
                return (byte)mod;
            }
            return 0;
        }

        private byte Blue()
        {
            int mod = chromeCount % 255;
            if (chromeCount / 255 == 3 || chromeCount / 255 == 4)
            {
                return 255;
            }
            if (chromeCount / 255 == 5)
            {
                return (byte)(255 - mod);
            }
            if (chromeCount / 255 == 2)
            {
                return (byte)mod;
            }
            return 0;
        }
    }
}
