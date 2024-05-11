// MathLibrary.cpp
#include <windows.h>

typedef int(*MultiplyNumbersFunc)(int, int);

extern "C" {
    __declspec(dllexport) int AddNumbers(int a, int b) {
        return a + b;
    }
}

extern "C" __declspec(dllexport) int MultiplyNumbers(int a, int b)
{
    HMODULE hMod = LoadLibraryW(L"MultiplyLibrary.dll");
    if (hMod == NULL)
    {
        // Handle error
        return 0;
    }

    MultiplyNumbersFunc MultiplyNumbers = (MultiplyNumbersFunc)GetProcAddress(hMod, "MultiplyNumbers");
    if (MultiplyNumbers == NULL)
    {
        // Handle error
        FreeLibrary(hMod);
        return 0;
    }

    int result = MultiplyNumbers(a, b);

    FreeLibrary(hMod);

    return result;
}