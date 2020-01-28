
#define DLL_EXPORT __declspec(dllexport)

extern "C" DLL_EXPORT int calculateDistanceCpp(int x1, int y1, int x2, int y2)
{

    return 0;
}

extern "C" DLL_EXPORT void calculateHDistanceCpp(int endX, int endY, int* hTable)
{   
    int x, y, diff;
    for (int pointX = 0; pointX < 50; pointX++)
    {
        for (int pointY = 0; pointY < 50; pointY++)
        {
            if (pointX > endX) {
                x = pointX - endX;
            }
            else {
                x = endX - pointX;
            }

            if (pointY > endY) {
                y = pointY - endY;
            }
            else {
                y = endY - pointY;
            }

            if (x > y) {
                diff = x - y;
                *hTable = diff * 10 + y * 14;
            }
            else {
                diff = y - x;
                *hTable = diff * 10 + x * 14;
            }
            
            hTable++;
        }
    }
    
}