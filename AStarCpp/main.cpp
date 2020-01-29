#define DLL_EXPORT __declspec(dllexport)

//Funkcja obliczaj�ca odleg�o�� wszystkich punkt�w od punktu ko�cowego
//Wynik zapisuje w tablicy hTable
extern "C" DLL_EXPORT void calculateHDistanceCpp(int endX, int endY, int* hTable)
{   
    //Zmienne pomocnicze
    int x, y, diff;

    //P�tla zagnie�d�ona, przechodz�ca po wsp�rz�dnych wszystkich punkt�w na planszy
    for (int pointX = 0; pointX < 50; pointX++)
    {
        for (int pointY = 0; pointY < 50; pointY++)
        {
            //Sprawdzenie czy wsp�rz�dna X aktualnego punktu jest wi�ksza od wsp�rz�dnej X punktu ko�cowego
            if (pointX > endX) {
                //Wyliczenie odleg�o�ci mi�dzy punktami na osi X
                x = pointX - endX;
            }
            else {
                //Wyliczenie odleg�o�ci mi�dzy punktami na osi X
                x = endX - pointX;
            }

            //Sprawdzenie czy wsp�rz�dna Y aktualnego punktu jest wi�ksza od wsp�rz�dnej Y punktu ko�cowego
            if (pointY > endY) {
                //Wyliczenie odleg�o�ci mi�dzy punktami na osi Y
                y = pointY - endY;
            }
            else {
                //Wyliczenie odleg�o�ci mi�dzy punktami na osi Y
                y = endY - pointY;
            }

            //Sprawdzenie czy odleg�o�� na osi X jest wi�ksza od odleg�o�ci na osi Y
            if (x > y) {
                //Wyliczenie r�nicy mi�dzy odleg�o�ciami
                diff = x - y;
                //Zapisanie warto�ci do tablicy wynikowej
                //R�nica mno�ona jest razy 10 (krok po osi X lub Y), a mniejsza odleg�o�� razy 14 (krok pod k�tem)
                *hTable = diff * 10 + y * 14;
            }
            else {
                //Wyliczenie r�nicy mi�dzy odleg�o�ciami
                diff = y - x;
                //Zapisanie warto�ci do tablicy wynikowej
                //R�nica mno�ona jest razy 10 (krok po osi X lub Y), a mniejsza odleg�o�� razy 14 (krok pod k�tem)
                *hTable = diff * 10 + x * 14;
            }
            
            //Przesuni�cie wska�nika tabeli wynikowej
            hTable++;
        }
    }
    
}