using System.Windows.Controls;
using System.Windows.Media;

namespace CellularAutomaton
{
    public class Cell : Button
    {
        public int Row
        {
            get; set;
        }

        public int Col
        {
            get; set;
        }

        public bool Enabled
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

        public virtual void Update()
        {
            Background = Black;
        }
    }
}
