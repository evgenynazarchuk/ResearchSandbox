using System;
using System.Threading.Tasks;

namespace AsyncAwaitException
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CallFunctionWithException1Async(); // ok
            CallFunctionWithException2(); // another exception (Wait)
            CallFunctionWithException3(); // miss exception (GetAwaiter)
            await CallFunctionWithException4Async(); // ok
            CallFunctionWithException5Async().Wait(); // ok
            CallFunctionWithException6(); // miss exception (GetAwaiter)
            CallFunctionWithException7(); // miss exception (GetAwaiter)

            //try { } catch(Exception e) { Console.WriteLine($"{nameof(Main)}" + e.Message); }
            //try { } catch (Exception e) { Console.WriteLine($"{nameof(Main)}" + e.Message); }
            //try { } catch (Exception e) { Console.WriteLine($"{nameof(Main)}" + e.Message); }
            //try { } catch (Exception e) { Console.WriteLine($"{nameof(Main)}" + e.Message); }
            //try { } catch (Exception e) { Console.WriteLine($"{nameof(Main)}" + e.Message); }
            //try { } catch (Exception e) { Console.WriteLine($"{nameof(Main)}" + e.Message); }
            //try { } catch (Exception e) { Console.WriteLine($"{nameof(Main)}" + e.Message); }
        }

        //
        public static async Task FunctionThrowExceptionAsync()
        {
            await Task.Delay(100);
            throw new ApplicationException($"{nameof(FunctionThrowExceptionAsync)}");
        }

        public static async Task CallFunctionWithException1Async()
        {
            try
            {
                await FunctionThrowExceptionAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(CallFunctionWithException1Async)} catch message: " + e.Message);
            }
        }

        public static void CallFunctionWithException2()
        {
            try
            {
                FunctionThrowExceptionAsync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(CallFunctionWithException2)} catch message: " + e.Message);
            }
        }

        public static void CallFunctionWithException3()
        {
            try
            {
                FunctionThrowExceptionAsync().GetAwaiter();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(CallFunctionWithException3)} catch message: " + e.Message);
            }
        }

        public static async Task CallFunctionWithException4Async()
        {
            try
            {
                await FunctionThrowExceptionAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(CallFunctionWithException4Async)} catch message: " + e.Message);
            }
        }

        public static async Task CallFunctionWithException5Async()
        {
            try
            {
                await FunctionThrowExceptionAsync().ConfigureAwait(true);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(CallFunctionWithException5Async)} catch message: " + e.Message);
            }
        }

        public static void CallFunctionWithException6()
        {
            try
            {
                FunctionThrowExceptionAsync().ConfigureAwait(false).GetAwaiter();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(CallFunctionWithException6)} catch message: " + e.Message);
            }
        }

        public static void CallFunctionWithException7()
        {
            try
            {
                FunctionThrowExceptionAsync().ConfigureAwait(true).GetAwaiter();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{nameof(CallFunctionWithException7)} catch message: " + e.Message);
            }
        }
    }
}