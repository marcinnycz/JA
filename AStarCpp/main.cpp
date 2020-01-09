
#define DLL_EXPORT __declspec(dllexport)

extern "C" DLL_EXPORT int calculateDistanceCpp(int x1, int y1, int x2, int y2)
{
    return x1 + x2 + y1 + y2;
}