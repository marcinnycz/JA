using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class Node
    {
        public Point point;
        public Node parent;
        public int fcost;
        public int hcost;

        public Node(Point point, Node parent, int fcost, int hcost)
        {
            this.point = point;
            this.parent = parent;
            this.fcost = fcost;
            this.hcost = hcost;
        }
    }
}
