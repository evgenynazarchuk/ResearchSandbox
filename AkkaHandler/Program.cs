using System;
using System.Diagnostics;
using Akka;
using Akka.Actor;

namespace AkkaHandler
{
    class Program
    {
        static void Main()
        {
            ManyMessage();
        }

        static void ManyMessage()
        {
            const int CountInvoke = 100_000;
            Stopwatch watch = new();

            var system = ActorSystem.Create($"{nameof(Program)}");
            var simpleMessage = system.ActorOf<InformationMessageActor>($"{nameof(InformationMessageActor)}");
            var yellowMessage = system.ActorOf<YellowMessageActor>($"{nameof(YellowMessageActor)}");

            watch.Start();
            for (int i = 0; i < CountInvoke; i++)
            {
                simpleMessage.Tell(new InformationMessage("Hello world\r"));
                yellowMessage.Tell(new InformationMessage("Hello world\r"));
            }
            watch.Stop();
            var runTime = watch.ElapsedMilliseconds;

            Console.ReadKey();
            Console.WriteLine($"Watch: {runTime} ms");
            Console.ReadKey();
        }

        static void CheckMessage()
        {
            var system = ActorSystem.Create($"{nameof(Program)}");
            var actor1 = system.ActorOf<InformationMessageActor>($"{nameof(InformationMessageActor)}");
            var actor2 = system.ActorOf<WarningMessageActor>($"{nameof(WarningMessageActor)}");
            var actor3 = system.ActorOf<YellowMessageActor>($"{nameof(YellowMessageActor)}");

            actor1.Tell(new InformationMessage("Hello world"));
            actor2.Tell(new InformationMessage("Wow!")); // Print INFO, failed

            actor2.Tell(new WarningMessage("Wow!"));

            actor3.Tell(new InformationMessage("Bay"));

            Console.ReadKey();
        }
    }
}
