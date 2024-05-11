// See https://aka.ms/new-console-template for more information
using function_isolated_cpp;

Console.WriteLine("Hello, World!");

MathLibrary mathLibrary = new MathLibrary();
int sum = mathLibrary.CallAddNumbers(1, 2);
Console.WriteLine($"The sum of 1 and 2 is {sum}");

int product = mathLibrary.CallMultiplyNumbers(3, 4);
Console.WriteLine($"The product of 3 and 4 is {product}");