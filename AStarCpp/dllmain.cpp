// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
//#define DLL_EXPORT __declspec(dllexport)

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

//extern "C" int calculateDistanceCpp(int x1, int x2, int y1, int y2);
//typedef int(__fastcall* calculateDistanceASM)(int x1, int x2, int y1, int y2);

//calculateDistanceASM calculate;

//extern "C" DLL_EXPORT int calculateDistance(int x1, int x2, int y1, int y2)
//{
//    return calculateDistanceCpp(x1, x2, y1, y2);
//}


