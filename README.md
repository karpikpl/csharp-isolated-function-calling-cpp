# Function Isolated C# Project calling C++

This project aims to demonstrate the concept of Azure Function that calls into a C++ library.

## Directories

### `FuncCpp`

Azure Function.

### `MathLib`

C++ class that performs addition of two integers.

### `MyLib`

C# class that uses `DllImport` to call the C++ library.

> Note: it contains compiled version of the C++ library and includes it in build using:
> ```xml
>   <ItemGroup>
>    <None Update="MathLibrary.dll">
>      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
>    </None>
>  </ItemGroup>

### `consoleApp`

A test console app for testing C# to C++ call.

## Test HTTP File

The `test.http` file in the project root directory contains sample HTTP requests that can be used to test the functionality of the project.

## License

This project is licensed under the MIT License.