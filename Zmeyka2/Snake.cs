using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Zmeyka2
{
    class Snake
    {
        public Snake(int size)
        {
            Rectangle rect = new Rectangle();
            rect.Width = size;
            rect.Height = size;
        }
        public UIElement UIElement { get; set; }

        public bool IsHead { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

    }
}
