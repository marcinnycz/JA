#define DLL_EXPORT __declspec(dllexport)

//Funkcja obliczaj¹ca odleg³oœæ wszystkich punktów od punktu koñcowego
//Wynik zapisuje w tablicy hTable
extern "C" DLL_EXPORT void calculateHDistanceCpp(int endX, int endY, int* hTable)
{   
    //Zmienne pomocnicze
    int x, y, diff;

    //Pêtla zagnie¿d¿ona, przechodz¹ca po wspó³rzêdnych wszystkich punktów na planszy
    for (int pointX = 0; pointX < 50; pointX++)
    {
        for (int pointY = 0; pointY < 50; pointY++)
        {
            //Sprawdzenie czy wspó³rzêdna X aktualnego punktu jest wiêksza od wspó³rzêdnej X punktu koñcowego
            if (pointX > endX) {
                //Wyliczenie odleg³oœci miêdzy punktami na osi X
                x = pointX - endX;
            }
            else {
                //Wyliczenie odleg³oœci miêdzy punktami na osi X
                x = endX - pointX;
            }

            //Sprawdzenie czy wspó³rzêdna Y aktualnego punktu jest wiêksza od wspó³rzêdnej Y punktu koñcowego
            if (pointY > endY) {
                //Wyliczenie odleg³oœci miêdzy punktami na osi Y
                y = pointY - endY;
            }
            else {
                //Wyliczenie odleg³oœci miêdzy punktami na osi Y
                y = endY - pointY;
            }

            //Sprawdzenie czy odleg³oœæ na osi X jest wiêksza od odleg³oœci na osi Y
            if (x > y) {
                //Wyliczenie ró¿nicy miêdzy odleg³oœciami
                diff = x - y;
                //Zapisanie wartoœci do tablicy wynikowej
                //Ró¿nica mno¿ona jest razy 10 (krok po osi X lub Y), a mniejsza odleg³oœæ razy 14 (krok pod k¹tem)
                *hTable = diff * 10 + y * 14;
            }
            else {
                //Wyliczenie ró¿nicy miêdzy odleg³oœciami
                diff = y - x;
                //Zapisanie wartoœci do tablicy wynikowej
                //Ró¿nica mno¿ona jest razy 10 (krok po osi X lub Y), a mniejsza odleg³oœæ razy 14 (krok pod k¹tem)
                *hTable = diff * 10 + x * 14;
            }
            
            //Przesuniêcie wskaŸnika tabeli wynikowej
            hTable++;
        }
    }
    
}