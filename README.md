
# ESPAS MAT05 Ausdauer

## Description
Internal program, used to test a student's ability to concentrate over a period of 60 minutes. Continuously displays simple addition calculations, which can be answered by pressing the corresponding number on the num pad, with the exception of results bigger than 9. In those cases, the student has to enter the last digit 

```
input = result % 10
```

## Steps after completion
Once the student is finished, a simple .txt file is saved on the student's appdata LocalLow folder (see figure 1).
To prevent score manipulation by students, the app has a way of checking the result's authenticity. 

The Score Checksum asks for the result string first. After that, the supervisor can enter the hexvalue. This is the sum of the score, taking only the last two digits of each score, turned into a hexadecimal value. This hexvalue is disguised as a student-ID.

If everything is correct, the prompt will display a "Match". Otherwise, it'll display "no match", which means the file was edited.




## Screenshots

[Figure 1]
![Figure 1](https://i.postimg.cc/3R3m1Y9X/grafik.png)


## Checksum bat file:
 
```bash
@echo off
setlocal enabledelayedexpansion

:: Get user input (continuous string of 3-digit numbers)
set /p numString=Enter the result number string: 

:: Initialize an empty variable to store the processed number
set processedNum=

:: Loop through each 3-character substring in the string
set i=0
:loop
set num=!numString:~%i%,3!
if not "!num!"=="" (
    :: Take only the last two digits from each three-character segment
    set processedNum=!processedNum!!num:~1,2!
    set /a i+=3
    goto loop
)

:: Convert to hex using PowerShell (supports big integers)
for /f %%A in ('powershell -command "[System.Numerics.BigInteger]::Parse('%processedNum%').ToString('X')"') do set hexOutput=%%A

:: Ask user for hex input
set /p userHex=Enter your hex value to compare: 
cls
:: Compare hex values
if /i "%hexOutput%"=="%userHex%" (
    echo Match!
) else (
    echo No match.
)



:: Add spaces after every second character in processedNum
set spacedNum=
set len=0
for /l %%i in (0,1,100) do (
    set char=!processedNum:~%%i,1!
    if not "!char!"=="" (
        set /a len+=1
        set spacedNum=!spacedNum!!char!
        if !len! geq 2 (
            set spacedNum=!spacedNum! 
            set len=0
        )
    )
)

:: Display the processed number with spaces
echo Points (last two digits): !spacedNum!
echo Hex value: %hexOutput%

pause


```
