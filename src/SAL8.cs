/**
 * SAL-8-Sharp (https://github.com/exom-dev/SAL-8-Sharp)
 * C# wrapper for the SAL-8 C99 implementation.
 *
 * Version 1.0.0 'INITUS' - This wrapper is NOT guaranteed to work with any other version of the implementation.
 *
 * This project is licensed under the MIT license.
 * Copyright (c) 2021 The Exom Developers (https://github.com/exom-dev)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
 * associated documentation files (the "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial
 * portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;
using System.Runtime.InteropServices;

/// <summary>
/// Contains wrapper classes for the SAL-8 C99 implementation.
/// </summary>
namespace SAL8
{
    /// <summary>
    /// Contains some PInvoke functions which are part of the SAL-8 C99 API.
    /// </summary>
    public static class PInvoke
    {
        // The default path is 'SAL8.dll'. Change this to fit your needs.
        [DllImport("SAL8.dll", EntryPoint = "sal8_cluster_delete", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Sal8ClusterDelete(ref Cluster cluster);
        [DllImport("SAL8.dll", EntryPoint = "sal8_cluster_instruction_count", CallingConvention = CallingConvention.Cdecl)]
        public extern static uint Sal8ClusterInstructionCount(ref Cluster cluster);

        [DllImport("SAL8.dll", EntryPoint = "sal8_io_init", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Sal8IoInit(ref IO io);

        [DllImport("SAL8.dll", EntryPoint = "sal8_io_redirect_in", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Sal8IoRedirectIn(ref IO io, IO.InHandler handler);
        [DllImport("SAL8.dll", EntryPoint = "sal8_io_redirect_out", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Sal8IoRedirectOut(ref IO io, IO.OutHandler handler);
        [DllImport("SAL8.dll", EntryPoint = "sal8_io_redirect_err", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Sal8IoRedirectErr(ref IO io, IO.OutHandler handler);

        [DllImport("SAL8.dll", EntryPoint = "sal8_stack_size", CallingConvention = CallingConvention.Cdecl)]
        public extern static byte Sal8StackSize(ref Stack stack);
        [DllImport("SAL8.dll", EntryPoint = "sal8_stack_capacity", CallingConvention = CallingConvention.Cdecl)]
        public extern static byte Sal8StackCapacity(ref Stack stack);
        [DllImport("SAL8.dll", EntryPoint = "sal8_stack_get", CallingConvention = CallingConvention.Cdecl)]
        public extern static byte Sal8StackGet(ref Stack stack, byte index);

        public enum Sal8CompilerStatus { OK, ERROR }

        [DllImport("SAL8.dll", EntryPoint = "sal8_compiler_init", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Sal8CompilerInit([In, Out] Compiler compiler, byte registerCount);
        [DllImport("SAL8.dll", EntryPoint = "sal8_compiler_delete", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Sal8CompilerDelete([In, Out] Compiler compiler);
        [DllImport("SAL8.dll", EntryPoint = "sal8_compiler_clean", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Sal8CompilerClean([In, Out] Compiler compiler);
        [DllImport("SAL8.dll", EntryPoint = "sal8_compiler_compile", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static Sal8CompilerStatus Sal8CompilerCompile([In, Out] Compiler compiler, [MarshalAs(UnmanagedType.LPStr)] string str);

        public enum Sal8VmStatus { OK, ERROR }

        [DllImport("SAL8.dll", EntryPoint = "sal8_vm_init", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Sal8VmInit([In, Out] VM vm, byte registerCount, byte stackCapacity);
        [DllImport("SAL8.dll", EntryPoint = "sal8_vm_delete", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Sal8VmDelete([In, Out] VM vm);
        [DllImport("SAL8.dll", EntryPoint = "sal8_vm_access_register", CallingConvention = CallingConvention.Cdecl)]
        public extern static byte Sal8VmAccessRegister([In, Out] VM vm, byte index);
        [DllImport("SAL8.dll", EntryPoint = "sal8_vm_current_instruction_index", CallingConvention = CallingConvention.Cdecl)]
        public extern static uint Sal8VmCurrentInstructionIndex([In, Out] VM vm);
        [DllImport("SAL8.dll", EntryPoint = "sal8_vm_load", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Sal8VmLoad([In, Out] VM vm, Cluster cluster);
        [DllImport("SAL8.dll", EntryPoint = "sal8_vm_run", CallingConvention = CallingConvention.Cdecl)]
        public extern static Sal8VmStatus Sal8VmRun([In, Out] VM vm, uint count);
        [DllImport("SAL8.dll", EntryPoint = "sal8_vm_finished", CallingConvention = CallingConvention.Cdecl)]
        public extern static byte Sal8VmFinished([In, Out] VM vm);
    }

    /// <summary>
    /// Represents a SAL-8 cluster.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Cluster
    {
        public IntPtr data;
        public uint size;
        public uint capacity;

        /// <summary>
        /// Destroys the cluster and frees any memory used by it.
        /// </summary>
        public void Destroy()
        {
            PInvoke.Sal8ClusterDelete(ref this);
        }
    }

    /// <summary>
    /// Represents a SAL-8 label map.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct LabelMap
    {
        IntPtr entries;
        uint count;
        uint capacity;
    }

    /// <summary>
    /// Represents a SAL-8 stack.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Stack
    {
        public IntPtr bottom;
        public IntPtr ptr;
        public IntPtr top;
    }

    /// <summary>
    /// Represents a SAL-8 IO interface.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IO
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ushort InHandler();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void OutHandler(string data);

        public InHandler input;
        public OutHandler output;
        public OutHandler error;
    }

    /// <summary>
    /// Represents an exception occurred during compile time.
    /// </summary>
    public class CompileTimeException : Exception
    {
        public CompileTimeException() { }
    }

    /// <summary>
    /// Represents a SAL-8 compiler.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class Compiler
    {
        private IO io;

        private byte registerCount;

        private Cluster cluster;
        private IntPtr parser;
        private LabelMap labels;
        private IntPtr ulabels;

        /// <summary>
        /// Initializes a new instance of the Compiler class, with the register count being 4.
        /// </summary>
        public Compiler() : this(4) { }

        /// <summary>
        /// Initializes a new instance of the Compiler class.
        /// </summary>
        /// 
        /// <param name="registerCount">The total number of available registers.</param>
        public Compiler(byte registerCount)
        {
            PInvoke.Sal8CompilerInit(this, registerCount);
        }

        ~Compiler()
        {
            PInvoke.Sal8CompilerDelete(this);
        }

        /// <summary>
        /// Redirects the STDIN stream to a handler. The handler should return either 0-255 for valid numbers, or >255 for errors/end-of-input.
        /// </summary>
        /// 
        /// <param name="handler">The input handler.</param>
        public void RedirectIn(IO.InHandler handler)
        {
            PInvoke.Sal8IoRedirectIn(ref io, handler);
        }

        /// <summary>
        /// Redirects the STDOUT stream to a handler.
        /// </summary>
        /// 
        /// <param name="handler">The output handler.</param>
        public void RedirectOut(IO.OutHandler handler)
        {
            PInvoke.Sal8IoRedirectOut(ref io, handler);
        }

        /// <summary>
        /// Redirects the STDERR stream to a handler.
        /// </summary>
        /// 
        /// <param name="handler">The error handler.</param>
        public void RedirectErr(IO.OutHandler handler)
        {
            PInvoke.Sal8IoRedirectErr(ref io, handler);
        }

        /// <summary>
        /// Compiles a string containing SAL-8 source code, and returns a SAL-8 cluster.
        /// </summary>
        /// 
        /// <param name="source">The SAL-8 source code to compile.</param>
        /// 
        /// <returns>A SAL-8 cluster containing the compiled source code.</returns>
        public Cluster Compile(string source)
        {
            if(PInvoke.Sal8CompilerCompile(this, source) == PInvoke.Sal8CompilerStatus.ERROR)
            {
                throw new CompileTimeException();
            }

            Cluster compiled = cluster;

            PInvoke.Sal8CompilerClean(this);

            return compiled;
        }

        /// <summary>
        /// Gets the total number of available registers.
        /// </summary>
        /// 
        /// <returns>The total number of available registers</returns>
        public byte GetRegisterCount()
        {
            return registerCount;
        }
    }

    /// <summary>
    /// Represents an exception occurred during runtime.
    /// </summary>
    public class RuntimeException : Exception
    {
        public RuntimeException() { }
    }

    /// <summary>
    /// Represents a SAL-8 virtual machine.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class VM
    {
        private IO io;

        private byte registerCount;

        private IntPtr registers;
        private Stack stack;

        private byte cmp;

        private Cluster cluster;

        private IntPtr ip;

        /// <summary>
        /// Initializes a new instance of the VM class, with the register count being 4 and the stack capacity being 8.
        /// </summary>
        public VM() : this(4, 8) { }

        /// <summary>
        /// Initializes a new instance of the VM class.
        /// </summary>
        /// 
        /// <param name="registerCount">The total number of available registers.</param>
        /// <param name="stackCapacity">The stack capacity.</param>
        public VM(byte registerCount, byte stackCapacity)
        {
            PInvoke.Sal8VmInit(this, registerCount, stackCapacity);
        }

        ~VM()
        {
            PInvoke.Sal8VmDelete(this);
        }

        /// <summary>
        /// Redirects the STDIN stream to a handler. The handler should return either 0-255 for valid numbers, or >255 for errors/end-of-input.
        /// </summary>
        /// 
        /// <param name="handler">The input handler.</param>
        public void RedirectIn(IO.InHandler handler)
        {
            PInvoke.Sal8IoRedirectIn(ref io, handler);
        }

        /// <summary>
        /// Redirects the STDOUT stream to a handler.
        /// </summary>
        /// 
        /// <param name="handler">The output handler.</param>
        public void RedirectOut(IO.OutHandler handler)
        {
            PInvoke.Sal8IoRedirectOut(ref io, handler);
        }

        /// <summary>
        /// Redirects the STDERR stream to a handler.
        /// </summary>
        /// 
        /// <param name="handler">The error handler.</param>
        public void RedirectErr(IO.OutHandler handler)
        {
            PInvoke.Sal8IoRedirectErr(ref io, handler);
        }

        /// <summary>
        /// Loads a cluster into the virtual machine.
        /// </summary>
        /// 
        /// <param name="cluster">The cluster to load.</param>
        public void Load(Cluster cluster)
        {
            PInvoke.Sal8VmLoad(this, cluster);
        }

        /// <summary>
        /// Runs the virtual machine.
        /// </summary>
        public void Run()
        {
            Run(0);
        }

        /// <summary>
        /// Runs the virtual machine.
        /// </summary>
        /// 
        /// <param name="count">The maximum number of instructions to run (0 to run all of them)</param>
        public void Run(uint count)
        {
            if (PInvoke.Sal8VmRun(this, count) == PInvoke.Sal8VmStatus.ERROR)
            {
                throw new RuntimeException();
            }
        }

        /// <summary>
        /// Whether or not the virtual machine finished executing all of the instructions from the cluster.
        /// </summary>
        /// 
        /// <returns>True, if there is nothing left to execute. False otherwise.</returns>
        public bool Finished()
        {
            return PInvoke.Sal8VmFinished(this) != 0;
        }

        /// <summary>
        /// Gets the total number of instructions from the loaded cluster.
        /// </summary>
        /// 
        /// <returns>The total number of loaded instructions.</returns>
        public uint GetInstructionCount()
        {
            return PInvoke.Sal8ClusterInstructionCount(ref cluster);
        }

        public uint GetCurrentInstructionIndex()
        {
            return PInvoke.Sal8VmCurrentInstructionIndex(this);
        }

        /// <summary>
        /// Gets the total number of available registers.
        /// </summary>
        /// 
        /// <returns>The total number of available registers</returns>
        public byte GetRegisterCount()
        {
            return registerCount;
        }

        /// <summary>
        /// Returns the value of a register.
        /// </summary>
        /// 
        /// <param name="index">The register index.</param>
        /// 
        /// <returns>The value stored in the register.</returns>
        public byte GetRegisterValue(byte index)
        {
            return PInvoke.Sal8VmAccessRegister(this, index);
        }

        /// <summary>
        /// Returns the value in the CMP register.
        /// </summary>
        /// 
        /// <returns>The value stored in the CMP register.</returns>
        public byte GetCmpRegisterValue()
        {
            return cmp;
        }

        /// <summary>
        /// Returns the virtual machine stack size.
        /// </summary>
        /// 
        /// <returns>The stack size.</returns>
        public byte GetStackSize()
        {
            return PInvoke.Sal8StackSize(ref stack);
        }

        /// <summary>
        /// Returns the virtual machine stac capacity.
        /// </summary>
        /// 
        /// <returns>The stack capacity.</returns>
        public byte GetStackCapacity()
        {
            return PInvoke.Sal8StackCapacity(ref stack);
        }

        /// <summary>
        /// Returns the value of a stack slot.
        /// </summary>
        /// 
        /// <param name="index">The slot index.</param>
        /// <returns>The value stored in the slot.</returns>
        public byte GetStackValue(byte index)
        {
            return PInvoke.Sal8StackGet(ref stack, index);
        }
    }
}
