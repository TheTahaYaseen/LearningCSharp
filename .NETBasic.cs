using System;

namespace DotNetBasics
{
    class DotNetBasics
    {
        /* StartOfNotes

         Non .Net App - 
             VB6 -> VB6 Compiler -> Assembly (.dll/.exe) [native/machine code {code will only run in that particular os}] -> OS
         .Net App -
             Any language (c#, vb, c++, j# and others) -> Compiler for respective language (c#, vb, c++, j# and others) -> Assembly (Intermediate Language) -> CLR (Common Language Runtime) [Contains JIT{Just in time} compiler, According to the os, converts the intermediate language to native/machine code] -> OS
             CLR also provides garbage collection (for unused memory)

        ILDASM to disassemble assemblies into IL
        ILASM to assemble assemblies from IL

        ILDASM shows the IL and Manifest
        Manifest containing all info such as no. of classes and such and such to properly structure your program
        IL is IL

        All assemblies such as System stored in GAC - Global Assembly Cache which is "C:\Windows\assembly"

        EndOfNotes */
    }
}
