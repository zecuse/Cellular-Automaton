using System.Windows.Controls;
using System.Windows.Media;

namespace CellularAutomaton
{
    public class CellButton : Button
    {
        public int Row
        {
            get; set;
        }

        public int Col
        {
            get; set;
        }

        public int Life
        {
            get; set;
        } = 0;

        public bool Enabled
        {
            get; set;
        } = false;

        public bool Immortal
        {
            get; set;
        } = false;

        private int max = 255 * 6;
        private int chromeCount = 0;
        private int increment = 64;
        public bool Chromatic
        {
            get; set;
        } = false;

        public SolidColorBrush White
        {
            get; set;
        } = new SolidColorBrush(Colors.White);

        public SolidColorBrush Black
        {
            get; set;
        } = new SolidColorBrush(Colors.Black);

        public void Update(int lifespan = 0)
        {
            if(Immortal)
            {
                Background = Black;
                return;
            }
            --Life;
            if(Life < 0)
            {
                if(Chromatic)
                {
                    Background = new SolidColorBrush(Color.FromRgb(Red(), Green(), Blue()));
                }
                else
                {
                    Background = Black;
                }
                Life = lifespan;
                return;
            }
            else if(Life == 0)
            {
                Background = Chromatic ? Black : White;
                Enabled = false;
                chromeCount += increment;
                chromeCount %= max;
                return;
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
