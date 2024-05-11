// MathLibrary.cpp
#include <windows.h>
#include <iostream>

typedef int (*MultiplyNumbersFunc)(int, int);
typedef void (*LogCallbackFunc)(const char *);

// Define a global variable to hold the log callback
LogCallbackFunc logCallback = nullptr;

extern "C" __declspec(dllexport) void SetLogCallback(void (*callback)(const char *))
{
    // Store the callback function in a global variable
    logCallback = callback;
}

extern "C"
{
    __declspec(dllexport) int AddNumbers(int a, int b)
    {
        return a + b;
    }
}

extern "C" __declspec(dllexport) int MultiplyNumbers(int a, int b)
{
    std::cout << "Loading MultiplyLibrary.dll.." << std::endl;
    logCallback("Loading MultiplyLibrary.dll..");

    // Get the path of the current DLL
    HMODULE hModCurrent;
    GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT, (LPCWSTR)&MultiplyNumbers, &hModCurrent);
    WCHAR path[MAX_PATH];
    GetModuleFileNameW(hModCurrent, path, MAX_PATH);

    // Replace the filename in the path with MultiplyLibrary.dll
    WCHAR *lastSlash = wcsrchr(path, L'\\');
    if (lastSlash != NULL)
    {
        wcscpy(lastSlash + 1, L"MultiplyLibrary.dll");
    }

    std::wcout << L"Path of MultiplyLibrary.dll is: " << path << std::endl;
    // Buffer to hold the formatted string
    char buffer[1024];
    // Format the string
    snprintf(buffer, sizeof(buffer), "Path of MultiplyLibrary.dll is: %ls", path);
    logCallback(buffer);

    // loading by path
    // HMODULE hMod = LoadLibraryW(path);
    // loading by name
    HMODULE hMod = LoadLibraryW(L"MultiplyLibrary.dll");
    if (hMod == NULL)
    {
        // Handle error
        std::cerr << "Failed to load MultiplyLibrary.dll" << std::endl;
        logCallback("Failed to load MultiplyLibrary.dll");
        DWORD error = GetLastError();

        LPVOID lpMsgBuf;
        FormatMessage(
            FORMAT_MESSAGE_ALLOCATE_BUFFER |
                FORMAT_MESSAGE_FROM_SYSTEM |
                FORMAT_MESSAGE_IGNORE_INSERTS,
            NULL,
            error,
            MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
            (LPTSTR)&lpMsgBuf,
            0, NULL);

        std::cerr << "Failed to load MultiplyLibrary.dll, error: " << (LPCTSTR)lpMsgBuf << std::endl;
        snprintf(buffer, sizeof(buffer), "Failed to load MultiplyLibrary.dll, error:  %hs", (LPCTSTR)lpMsgBuf);
        logCallback(buffer);
        return 0;
    }

    MultiplyNumbersFunc MultiplyNumbers = (MultiplyNumbersFunc)GetProcAddress(hMod, "MultiplyNumbers");
    if (MultiplyNumbers == NULL)
    {
        // Handle error
        std::cerr << "Failed to get MultiplyNumbers function" << std::endl;
        logCallback("Failed to get MultiplyNumbers function");
        FreeLibrary(hMod);
        return 0;
    }

    int result = MultiplyNumbers(a, b);
    std::cout << "Multiplication result: " << result << std::endl;
    snprintf(buffer, sizeof(buffer), "Multiplication result: %d", result);
    logCallback(buffer);
    FreeLibrary(hMod);

    return result;
}