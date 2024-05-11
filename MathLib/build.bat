cl /EHsc /LD MultiplyLibrary.cpp
cl /EHsc /LD MathLibrary.cpp

REM move the generated files to the lib folder and overwrite the existing files
move /Y MultiplyLibrary.dll ../MyLib/
move /Y MathLibrary.dll ../MyLib/