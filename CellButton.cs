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
        }

        public bool Immortal
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
                Background = Black;
                Life = lifespan;
                return;
            }
            else if(Life == 0)
            {
                Background = White;
                Enabled = false;
                return;
            }
        }
    }
}
