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

        private bool on;
        public bool Enabled
        {
            get
            {
                return on;
            }
            set
            {
                on = value;
                Update();
            }
        }

        public SolidColorBrush White
        {
            get; set;
        } = new SolidColorBrush(Colors.White);

        public SolidColorBrush Black
        {
            get; set;
        } = new SolidColorBrush(Colors.Black);

        private void Update()
        {
            Background = on ? Black : White;
        }
    }
}
