// MathLibrary.cpp
extern "C" {
    __declspec(dllexport) int AddNumbers(int a, int b) {
        return a + b;
    }
}