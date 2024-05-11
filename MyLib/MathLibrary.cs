using System.Runtime.InteropServices;

namespace function_isolated_cpp;

public class MathLibrary
{
    [DllImport("MathLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int AddNumbers(int a, int b);

    [DllImport("MathLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int MultiplyNumbers(int a, int b);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
    private static extern IntPtr LoadLibrary(string dllToLoad);

    public int CallAddNumbers(int a, int b)
    {
        return AddNumbers(a, b);
    }

    public int CallMultiplyNumbers(int a, int b)
    {
        return MultiplyNumbers(a, b);
    }

    public void LoadLibraryUsingKernel()
    {
        try
        {
            var result = LoadLibrary("MathLibrary.dll");
            if (result == IntPtr.Zero)
            {
                throw new Exception("Failed to load library - IntPtr.Zero returned");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
