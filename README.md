# About <a href="https://docs.microsoft.com/en-us/dotnet/framework/whats-new/#v45"><img align="right" src="https://img.shields.io/badge/.Net%20Framework-4.5-5C2D91?logo=.net" alt=".Net Framework 4.5" /></a>

This is a C# wrapper for the [SAL-8 C99 implementation](https://github.com/exom-dev/SAL-8), an interpreted 8-bit simple assembly-like language.

For more details, see the [SAL-8](https://github.com/exom-dev/SAL-8) repository.

# Usage

The wrapper is already documented (see the source file). Here's an example showcasing most of the wrapper functionality:

```cs
using SAL8;

...

static void Main(string[] args) {
    try
    {
        string src = File.ReadAllText("script.sal8");
        
        Compiler compiler = new Compiler();

        Cluster cl = compiler.Compile(src);

        VM vm = new VM();

        vm.Load(cl);
        vm.Run(); // Runs all instructions. You can also run a limited number of them like this:
        
        /* while(!vm.Finished())
         *     vm.Run(1); // One at a time. You can do anything between the runs.
         */

        // Outputs the values of all registers.
        for(byte i = 0; i < vm.GetRegisterCount(); ++i)
            Console.WriteLine("R" + i + "  " + vm.GetRegisterValue(i));

        // Outputs the value of the CMP register.
        Console.WriteLine("\nCMP " + vm.GetCmpRegisterValue() + "\n");

        // Outputs the whole stack.
        for(byte i = (byte)(vm.GetStackCapacity() - 1); ; --i)
        {
            Console.WriteLine("S" + i + "  " + vm.GetStackValue(i));

            if(i == 0)
                break;
        }

        // Important: after you're done using the Cluster object, destroy it.
        cl.Destroy();
    }
    catch(CompileTimeException)
    {
        // Compile time error
    }
    catch(RuntimeException)
    {
        // Runtime error
    }
    catch(Exception ex)
    {
        // Something else
    }
}
...
```

# License <a href="https://github.com/exom-dev/SAL-8-Sharp/blob/master/LICENSE"><img align="right" src="https://img.shields.io/badge/License-MIT-blue.svg" alt="License: MIT"></a>

This project was created by [The Exom Developers](https://github.com/exom-dev). It is licensed under the [MIT](https://github.com/exom-dev/SAL-8-Sharp/blob/master/LICENSE) license.