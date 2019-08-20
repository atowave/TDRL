using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MovingEngine.classes
{
    class LoopItem
    {
        public Action action;
        public DispatcherTimer dispatcher;
        public static List<LoopItem> Loops = new List<LoopItem>();
        public LoopItem()
        {

        }
    }
}
