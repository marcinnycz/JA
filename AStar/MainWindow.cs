using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace AStar
{
    
    public partial class MainWindow : Form
    {
        private Result resultForm;
        public MainWindow()
        {
            InitializeComponent();
        }

        public bool checkNeighbour(Point p, List<Point> nonTraversableList, List<Point> closedPointsList)
        { 
            if (nonTraversableList.Contains(p))
            {
                return false;
            } else if (p.X < 0 || p.X > 49 || p.Y < 0 || p.Y > 49)
            {
                return false;
            } else if (closedPointsList.Contains(p))
            {
                return false;
            } else
            {
                return true;
            }
        }

        public long makeLong(int left, int right)
        {
            //implicit conversion of left to a long
            long res = left;

            //shift the bits creating an empty space on the right
            // ex: 0x0000CFFF becomes 0xCFFF0000
            res = (res << 32);

            //combine the bits on the right with the previous value
            // ex: 0xCFFF0000 | 0x0000ABCD becomes 0xCFFFABCD
            res = res | (long)(uint)right; //uint first to prevent loss of signed bit

            //return the combined result
            return res;
        }

        private int conv(int x, int y)
        {
            return x * 50 + y;
        }
        private void runAStar(List<List<Point>>[] listArray, ThreadManager threadManager)
        {
            //System.Windows.Forms.MessageBox.Show(Program.calculateDistanceAsm(5, 2, 3, 7).ToString());
            //System.Windows.Forms.MessageBox.Show(Program.calculateDistanceCpp(5, 2, 3, 7).ToString());
            List<List<Point>> list = threadManager.getList(listArray);

            foreach (List<Point> li in list)
            {
                int startX = li[0].X;
                int startY = li[0].Y;
                int endX = li[1].X;
                int endY = li[1].Y;
                long start = makeLong(startX, startY);
                long end = makeLong(endX, endY);
                int[] hArray = new int[2500];
                //int[] gArray = new int[2500];

                List<Point> nonTraversableList = li.GetRange(2,li.Count-2);

                //Stopwatch stopWatch = new Stopwatch();
                //stopWatch.Restart();
                
                if (cppRadioButton.Checked)
                {
                    Program.calculateHDistanceAsm(endX, endY, hArray);
                }
                else if(asmRadioButton.Checked)
                {
                    Program.calculateHDistanceCpp(endX, endY, hArray);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No radio button checked?", "ERROR");
                    return;
                }
                
                //stopWatch.Stop();
                //TimeSpan t = stopWatch.Elapsed;


                List<Node> open = new List<Node>();
                List<Node> closed = new List<Node>();

                Node startNode = new Node(new Point(startX, startY), null, hArray[conv(startX, startY)], hArray[conv(startX, startY)]);
                Node endNode = new Node(new Point(endX, endY), null, 0, 0);

                Node currentNode = startNode;
                
                open.Add(currentNode);

                while (!currentNode.point.Equals(endNode.point) && open.Count > 0)
                {
                    //previousNode = currentNode;
                    List<Point> openPointList = new List<Point>();
                    int minF = 50*50*10*2;
                    foreach(Node node in open)
                    {
                        openPointList.Add(node.point);
                        int value = node.fcost;
                        if (value < minF)
                        {
                            minF = value;
                        }
                    }
                    
                    int minH = 50 * 50 * 10 * 2;
                    foreach (Node node in open)
                    {
                        int value = node.fcost;
                        if (value == minF)
                        {
                            if (node.hcost < minH)
                            {
                                currentNode = node;
                                minH = value;
                            }
                        }
                    }
                    
                    open.Remove(currentNode);
                    openPointList.Remove(currentNode.point);

                    closed.Add(currentNode);
                    List<Point> closedPointList = new List<Point>();
                    foreach (Node node in closed)
                    {
                        closedPointList.Add(node.point);
                    }

                    List<Point> validNeighbours = new List<Point>();

                    if (checkNeighbour(new Point(currentNode.point.X - 1, currentNode.point.Y - 1), nonTraversableList, closedPointList))
                    {
                        validNeighbours.Add(new Point(currentNode.point.X - 1, currentNode.point.Y - 1));
                    }
                    if (checkNeighbour(new Point(currentNode.point.X - 1, currentNode.point.Y    ), nonTraversableList, closedPointList))
                    {
                        validNeighbours.Add(new Point(currentNode.point.X - 1, currentNode.point.Y));
                    }
                    if (checkNeighbour(new Point(currentNode.point.X - 1, currentNode.point.Y + 1), nonTraversableList, closedPointList))
                    {
                        validNeighbours.Add(new Point(currentNode.point.X - 1, currentNode.point.Y + 1));
                    }
                    if (checkNeighbour(new Point(currentNode.point.X    , currentNode.point.Y - 1), nonTraversableList, closedPointList))
                    {
                        validNeighbours.Add(new Point(currentNode.point.X, currentNode.point.Y - 1));
                    }
                    if (checkNeighbour(new Point(currentNode.point.X    , currentNode.point.Y + 1), nonTraversableList, closedPointList))
                    {
                        validNeighbours.Add(new Point(currentNode.point.X, currentNode.point.Y + 1));
                    }
                    if (checkNeighbour(new Point(currentNode.point.X + 1, currentNode.point.Y - 1), nonTraversableList, closedPointList))
                    {
                        validNeighbours.Add(new Point(currentNode.point.X + 1, currentNode.point.Y - 1));
                    }
                    if (checkNeighbour(new Point(currentNode.point.X + 1, currentNode.point.Y    ), nonTraversableList, closedPointList))
                    {
                        validNeighbours.Add(new Point(currentNode.point.X + 1, currentNode.point.Y));
                    }
                    if (checkNeighbour(new Point(currentNode.point.X + 1, currentNode.point.Y + 1), nonTraversableList, closedPointList))
                    {
                        validNeighbours.Add(new Point(currentNode.point.X + 1, currentNode.point.Y + 1));
                    }

                    foreach(Point neighbour in validNeighbours)
                    {
                        bool inOpen = false;

                        int distance;
                        if (currentNode.point.X == neighbour.X || currentNode.point.Y == neighbour.Y)
                        {
                            distance = 10;
                        }
                        else
                        {
                            distance = 14;
                        }
                        int currentGCost = currentNode.fcost - hArray[conv(currentNode.point.X, currentNode.point.Y)];
                        foreach (Node node in open)
                        {
                            if(node.point.Equals(neighbour))
                            {
                                inOpen = true;
                                if (node.fcost > currentGCost + node.hcost + distance)
                                {
                                    node.fcost = currentGCost + node.hcost + distance;
                                    node.parent = currentNode;
                                }                                
                            }
                        }
                        if(!inOpen)
                        {
                            int fcost = currentGCost + hArray[conv(neighbour.X, neighbour.Y)] + distance;
                            open.Add(new Node(neighbour, currentNode, fcost, hArray[conv(neighbour.X, neighbour.Y)]));
                        }
                    }
                }
                //End of foreach
                //Znaleziono poprawną trasę
                if (open.Count > 0 || currentNode.point.Equals(endNode.point))
                {
                    li.Add(new Point(-1, -1));
                    currentNode = currentNode.parent;
                    while (!currentNode.point.Equals(startNode.point))
                    {
                        li.Add(currentNode.point);
                        currentNode = currentNode.parent;
                    }
                }
                //Nie znaleziono trasy
                else
                {
                    li.Add(new Point(-1, -1));
                }                              
            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            //Zmienna przechowująca dane o zawartości wszystkich wczytanych plików
            List<List<Point>> list = new List<List<Point>>();

            //Zmienna przechowująca listę nazw znalezionych plików
            string[] files = null;

            try
            {
                //Pobranie listy plików
                files = Directory.GetFiles(folderPathTextBox.Text);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "ERROR");
            }
            if (files != null)
            {
                //Odczyt danych z każdego znalezionego pliku
                foreach (string file in files)
                {
                    //Zmienna przechowująca informację o tym, czy wystąpił błąd podczas czytania danych z pliku
                    bool dataError = false;
                    //Zmienna przechowująca listę punktów z pojedynczego pliku
                    List<Point> pointList = new List<Point>();
                    try
                    {
                        using (StreamReader sr = new StreamReader(file))
                        {
                            //Licznik ilości pobranych punktów. Maksymalnie 2500.
                            int counter = 0;

                            //Zmienne przechowujące dane o współżędnych i separatorach 
                            Point start = new Point();
                            Point end = new Point();
                            int separator;
                            int newLineChar1;
                            int newLineChar2;
                            int intNumber1;
                            int intNumber2;
                            char[] number1 = new char[2];
                            char[] number2 = new char[2];

                            //Odczyt danych
                            while (!sr.EndOfStream || counter >= 2500)
                            {
                                //Odczyt pierwszej współżędnej
                                sr.ReadBlock(number1, 0, 2);

                                //Odczyt separatora
                                separator = sr.Read();

                                //Odczyt drugiej współżędnej
                                sr.ReadBlock(number2, 0, 2);

                                //Odczyt znaku nowej linii
                                newLineChar1 = sr.Read();
                                newLineChar2 = sr.Read();

                                //Sprawdzenie poprawności separatora i znaku nowej linii
                                if (separator != 44 || newLineChar1 != 13 || newLineChar2 != 10)
                                {
                                    System.Windows.Forms.MessageBox.Show("Plik: " + file + "\nNiepoprawny separator.", "ERROR");
                                    dataError = true;
                                    break;
                                }

                                try
                                {
                                    //Konwersja wartości współżędnych do zmiennej typu Point
                                    intNumber1 = Int32.Parse(new String(number1));
                                    intNumber2 = Int32.Parse(new String(number2));
                                    Point p = new Point(intNumber1, intNumber2);
                                    if (intNumber1 > 49 || intNumber2 > 49)
                                    {
                                        throw new ArgumentOutOfRangeException();
                                    }
                                    if(counter > 1)
                                    {
                                        //Sprawdzenie czy nie powtarza się wartość punktu startowego lub końcowego
                                        if(!p.Equals(start) && !p.Equals(end))
                                        {
                                            pointList.Add(p);
                                        }
                                    }else if(counter == 0)
                                    {
                                        //Znaleziono punkt startowy
                                        start = p;
                                        pointList.Add(p);
                                    }
                                    else if(counter == 1)
                                    {
                                        //Znaleziono punkt końcowy
                                        end = p;
                                        pointList.Add(p);
                                    }
                                    
                                }
                                catch (Exception)
                                { 
                                    //Sprawdzenie poprawności wartości współżędnych
                                    System.Windows.Forms.MessageBox.Show("Plik: " + file + "\nNiepoprawne dane punktu. Wartości punktu powinny należeć to zakresu od 00 do 49.", "ERROR");
                                    dataError = true;
                                    break;
                                }

                                //Inkermentacja licznika
                                counter++;
                            }
                            //Jeżeli nie wystąpił błąd i ilość punktów jest większa niż 1,
                            //dodanie listy punktów do zbioru poprawnie odczytanych danych.
                            if (!dataError)
                            {
                                if (pointList.Count < 2)
                                {
                                    System.Windows.Forms.MessageBox.Show("Plik: " + file + "\nZbyt mało danych. Plik musi zawierać punkt startowy oraz punkt końcowy.", "ERROR");
                                }
                                else
                                {
                                    list.Add(pointList);
                                }

                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message, "ERROR");
                    }
                }
            }
            else
            {
                return;
            }

            //Jeżeli odczytano chociaż jeden poprawny plik
            if(list.Count > 0)
            {
                
                //TODO Multithreading
                //Wykonanie algorytmu A* na każdym zbiorze danych
                int threadCount = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));

                ThreadManager threadManager = new ThreadManager(threadCount);

                Thread[] threadArray = new Thread[threadCount];
                List<List<Point>>[] listArray = new List<List<Point>>[threadCount];

                int remainder = list.Count % threadCount;
                int full = list.Count / threadCount;
                
                //Podział listy z danymi dla każdego wątku 
                for (int i = 0; i < threadCount; i++)
                {
                    listArray[i] = new List<List<Point>>();
                    for(int j = 0; j < full; j++)
                    {
                        listArray[i].Add(list[0]);
                        list.RemoveAt(0);
                    }
                    if(remainder > 0)
                    {
                        listArray[i].Add(list[0]);
                        list.RemoveAt(0);
                        remainder--;
                    }
                    //listArray[i].Add();
                }
                //TODO
                //Create listArray from list
                Stopwatch st = new Stopwatch();
                st.Start();
                //Stworzenie i uruchomienie wątków
                for (int i = 0; i < threadCount; i++)
                {
                    //List<List<Point>> threadResult = listArray[i];
                    threadArray[i] = new Thread(() => runAStar(listArray, threadManager));
                    threadArray[i].Name = "Thread " + i;
                    threadArray[i].Start();
                }
                //List<List<Point>> list1 = list.GetRange(0,1);
                //List<List<Point>> list2 = list.GetRange(1,1);
                //Thread t1 = new Thread(() => runAStar(list1));
                //Thread t2 = new Thread(() => runAStar(list2));

                //t1.Start();
                //t2.Start();

                //runAStar(list);

                //t1.Join();
                //t2.Join();

                for (int i = 0; i < threadCount; i++)
                {
                    threadArray[i].Join();
                }
                st.Stop();
                String time = st.ElapsedMilliseconds.ToString();
                //Zmienna przechowująca wyniki z wszystkich wątków
                List<List<Point>> resultList = new List<List<Point>>();

                //Połączenie wyników wszystkich wątków
                for (int i = 0; i < threadCount; i++)
                {
                    resultList.AddRange(listArray[i]);
                }

                System.Windows.Forms.MessageBox.Show("Znaleziono " + resultList.Count + " poprawnych plików.");
                
                //Wyświetlenie rezultatu
                resultForm = new Result(resultList, time);
                resultForm.Show();
                resultForm.Draw();
            }else
            {
                System.Windows.Forms.MessageBox.Show("Nie znaleziono plików o poprawnym formacie.", "ERROR");
            }
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                folderPathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }

    class ThreadManager
    {
        Object tmLock = new Object();
        private int threadNumber = 0;
        private int threadCount;
        public ThreadManager(int numberOfThreads)
        {
            threadCount = numberOfThreads;
        }

        public List<List<Point>> getList(List<List<Point>>[] listArray)
        {
            List<List<Point>> li;
            lock (tmLock)
            {
                if(threadNumber < threadCount)
                {
                    li = listArray[threadNumber];
                    threadNumber++;
                }else
                {
                    li = null;
                }
            }
            return li;
        }
    }
}
