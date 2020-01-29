using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace AStar
{
    //Klasa odpowiedzialna za wyświetlanie okienka głównego
    public partial class MainWindow : Form
    {
        private Result resultForm;
        public MainWindow()
        {
            InitializeComponent();
            //Ustawienie ilości procesorów logicznych
            numericUpDown1.Value = Environment.ProcessorCount;
        }
        
        //Funkcja sprawdzająca czy podany punkt był już odwiedzony, lub jest ścianą
        public bool checkNeighbour(Point p, List<Point> nonTraversableList, List<Point> closedPointsList)
        { 
            //Punkt jest ścianą
            if (nonTraversableList.Contains(p))
            {
                return false;
            } 
            //Punkt leży poza planszą
            else if (p.X < 0 || p.X > 49 || p.Y < 0 || p.Y > 49)
            {
                return false;
            } 
            //Punkt był już odwiedzony
            else if (closedPointsList.Contains(p))
            {
                return false;
            } else
            {
                return true;
            }
        }

        //Funkcja konwertująca współżędne na indeks tabeli
        private int conv(int x, int y)
        {
            return x * 50 + y;
        }

        //Funkcja odpowiedzialna za wykonanie algorytmu
        private void runAStar(List<List<Point>>[] listArray, ThreadManager threadManager)
        {
            //Pobranie listy plansz do konwersji
            List<List<Point>> list = threadManager.getList(listArray);

            //Dla każdel planszy
            foreach (List<Point> li in list)
            {
                //Stworzenie zmiennych przechowujących współżędne punktu startowego i końcowego
                int startX = li[0].X;
                int startY = li[0].Y;
                int endX = li[1].X;
                int endY = li[1].Y;

                //Stworzenie tablicy wynikowej odległości H
                int[] hArray = new int[2500];

                //Stworzenie listy punktów przez które nie można przechodzić (ścian)
                List<Point> nonTraversableList = li.GetRange(2,li.Count-2);
                
                //Wykonanie funkcji obliczającej tablicę H
                //W zależności od wyboru użytkownika, będzie to funcja c++ lub asm
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
                
                //Stworzenie dwóch list węzłów zawierających punkty "otwarte" i "zamknięte"
                List<Node> open = new List<Node>();
                List<Node> closed = new List<Node>();

                //Stworzenie węzła początkowego i końcowego
                Node startNode = new Node(new Point(startX, startY), null, hArray[conv(startX, startY)], hArray[conv(startX, startY)]);
                Node endNode = new Node(new Point(endX, endY), null, 0, 0);

                //Stworzenie i ustawienie aktualego węzła na węzeł początkowy
                Node currentNode = startNode;
                
                //Dodanie aktualnego węzła do listy węzłów otwartych
                open.Add(currentNode);

                //Pętla trwająca do momentu dotarcia do punktu końcowego lub wyczerpania zbioru punktów otwartych
                while (!currentNode.point.Equals(endNode.point) && open.Count > 0)
                {
                    //Stworzenie listy otwartych punktów
                    List<Point> openPointList = new List<Point>();
                    //Stworzenie zmiennej przechowującej minimalną odległość F każdego otwartego punktu
                    int minF = 50*50*10*2;

                    //Dla każdego otwartego węzła
                    foreach(Node node in open)
                    {
                        //Dodanie punktów z węzłów do listy otwartych punktów
                        openPointList.Add(node.point);
                        
                        //Sprawdzenie czy wartość f jest mniejsza niż minimalna,
                        //jeżeli tak, ustawienie nowej wartości minimalnej
                        int value = node.fcost;
                        if (value < minF)
                        {
                            minF = value;
                        }
                    }

                    //Stworzenie zmiennej przechowującej minimalną odległość H każdego otwartego punktu
                    int minH = 50 * 50 * 10 * 2;

                    //Dla każdego otwartego węzła
                    foreach (Node node in open)
                    {
                        //Sprawdzenie czy wartość f jest równa minimalnej,
                        //jeżeli tak, srawdzenie czy wartość H jest mniejsza od minimalnej,
                        //jeżeli tak, ustawienie węzła jako aktualny
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
                    
                    //Usunięcie węzła z listy węzłów otwartych
                    open.Remove(currentNode);
                    openPointList.Remove(currentNode.point);

                    //Dodanie węzła do listy węzłów zamkniętych
                    closed.Add(currentNode);

                    //Stworzenie listy punktów zamkniętych
                    List<Point> closedPointList = new List<Point>();
                    foreach (Node node in closed)
                    {
                        closedPointList.Add(node.point);
                    }

                    //Stworzenie listy sąsiadów aktualnego węzła
                    List<Point> validNeighbours = new List<Point>();

                    //Sprawdzenie czy sąsiedzi z każdej strony węzła nadają się do ewaluacji,
                    //jeśli tak, dodanie go do listy sąsiadów
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

                    //Ustalenie nowej wartości F dla każdego poprawnego sąsiada
                    foreach(Point neighbour in validNeighbours)
                    {
                        //Zmienna przechowująca informację o tym, czy sąsiad był już w liście węzłów otwartych
                        bool inOpen = false;

                        //Zmienna przechowująca dystans do sąsiada od aktualnego węzła
                        int distance;

                        //Jeżeli znajduje się pod kątem, dystans wynosi 14, jeżeli nie dystans wynosi 10
                        if (currentNode.point.X == neighbour.X || currentNode.point.Y == neighbour.Y)
                        {
                            distance = 10;
                        }
                        else
                        {
                            distance = 14;
                        }

                        //Zmienna przechowująca odległość aktualnego węzła od węzła początkowego
                        int currentGCost = currentNode.fcost - hArray[conv(currentNode.point.X, currentNode.point.Y)];
                        
                        //Dla każdego otwartego węzła
                        foreach (Node node in open)
                        {
                            //Jeżeli sąsiad znajduje się w zbiorze węzłów otwartych,
                            //aktualizowane są dane węzła
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
                        //Jeżeli sąsiad nie był w liście węzłów otwartych, dodanie go do listy węzłów otwartych i obliczenie danych węzła
                        if(!inOpen)
                        {
                            int fcost = currentGCost + hArray[conv(neighbour.X, neighbour.Y)] + distance;
                            open.Add(new Node(neighbour, currentNode, fcost, hArray[conv(neighbour.X, neighbour.Y)]));
                        }
                    }
                }
                
                //Znaleziono poprawną trasę
                if (open.Count > 0 || currentNode.point.Equals(endNode.point))
                {
                    //Dodanie węzła -1,-1 jako separatora między danymi a wynikami
                    li.Add(new Point(-1, -1));
                    currentNode = currentNode.parent;
                    //Przejście po wszystkich węzłach tworzących trasę i dodanie ich do listy
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
            //wykonanie algorytmu A* na każdym zbiorze danych
            if (list.Count > 0)
            {
                System.Windows.Forms.MessageBox.Show("Znaleziono " + list.Count + " poprawnych plików.");

                //Odczyt wybranej ;liczby wątków
                int threadCount = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));

                //Stworzenie obiektu pomocniczego ThreadManager
                ThreadManager threadManager = new ThreadManager(threadCount);

                //Stworzenie tablicy wątków
                Thread[] threadArray = new Thread[threadCount];

                //Stworzenie tablicy z podziałem danych na poszczególne wątki
                List<List<Point>>[] listArray = new List<List<Point>>[threadCount];

                //Zmienne służące podziałowi danych wejściowych między wątki
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
                }
                
                //Początek pomiaru czasu wykonania algorytmu
                Stopwatch st = new Stopwatch();
                st.Start();

                //Stworzenie i uruchomienie wątków
                for (int i = 0; i < threadCount; i++)
                {                    
                    threadArray[i] = new Thread(() => runAStar(listArray, threadManager));
                    threadArray[i].Name = "Thread " + i;
                    threadArray[i].Start();
                }

                //Oczekiwanie na zakończenie pracy wszystkich wątków
                for (int i = 0; i < threadCount; i++)
                {
                    threadArray[i].Join();
                }

                //Koniec pomiaru czasu
                st.Stop();
                String time = st.ElapsedMilliseconds.ToString();

                //Zmienna przechowująca wyniki z wszystkich wątków
                List<List<Point>> resultList = new List<List<Point>>();

                //Połączenie wyników wszystkich wątków
                for (int i = 0; i < threadCount; i++)
                {
                    resultList.AddRange(listArray[i]);
                }

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
            //Wyświetlenie okienka wyboru katalogu
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                folderPathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        //Funkcja testująca instrukcje SIMD
        public float[] testSIMDInstructions()
        {
            //Rozmiar (w bajtach) tablic z danymi i wynikami. Musi być podzielny przez 16
            const int size = 160;

            //Tablice z danymi i tablica wynikowa
            float[] src1 = new float[size / 4];
            float[] src2 = new float[size / 4];
            float[] result = new float[size / 4];

            //Wpisanie danych do tablic
            for (int i = 0; i < size / 4; i++)
            {
                src1[i] = i + (float)0.5;
                src2[i] = i * (float)0.1;
            }

            //Wykonanie funkcji SIMD
            if(size % 16 == 0)
            {
                Program.SIMDExample(src1, src2, result, size);
            }
            
            return result;
        }

        //Ukryta metoda wywołująca test instrukcji SIMD
        private void threadLabel_Click(object sender, EventArgs e)
        {
            String result = "";
            foreach (float f in testSIMDInstructions())
            {
                result += f.ToString() + "\n";
            }
            System.Windows.Forms.MessageBox.Show("Hidden results!\n" + result);
        }
    }

    //Klasa pomocnicza podczas przekazywania parametrów do wątków
    //Zapewnia poprawny podział listy z danymi pomiędzy wątki dzięki zastosowaniu semafora
    class ThreadManager
    {
        //Semafor
        Object tmLock = new Object();
        
        //Zmienna przechowująca liczbę wątków którym już przypisano listę z danymi
        private int threadNumber = 0;
        //Zmienna przechowująca ilu wątkom należy przypisać dane 
        private int threadCount;

        //Konstruktor
        public ThreadManager(int numberOfThreads)
        {
            threadCount = numberOfThreads;
        }

        //Funkcja zwracająca listę z danymi
        //Zabezpieczona w przypadku próby dostępu przez kilka wątków na raz
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
