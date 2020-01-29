using System;
using System.Collections.Generic;
using System.Drawing;


namespace AStar
{
    //Klasa przechowująca dane pojedynczego węzła
    public class Node
    {
        //Zmienne przechowujące punkt, węzeł rodzica, odległość sumaryczną i odległość od punktu końcowego
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
