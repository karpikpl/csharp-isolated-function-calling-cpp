using System.Runtime.InteropServices;

namespace function_isolated_cpp;

public class MathLibrary
{
        [DllImport("MathLibrary.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int AddNumbers(int a, int b);

        public int CallAddNumbers(int a, int b)
        {
            return AddNumbers(a, b);
        }
}
