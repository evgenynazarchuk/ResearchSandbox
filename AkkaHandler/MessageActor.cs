using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaHandler
{
    public class InformationMessage
    {
        private readonly string _message;
        public string Message { get => _message; }

        public InformationMessage(string message)
        {
            _message = message;
        }
    }

    public class WarningMessage
    {
        private readonly string _message;
        public string Message { get => _message; }

        public WarningMessage(string message)
        {
            _message = message;
        }
    }

    public class InformationMessageActor : ReceiveActor
    {
        public InformationMessageActor()
        {
            Receive<InformationMessage>(message =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(message.Message);
                Console.ResetColor();
            });
        }
    }

    public class YellowMessageActor : ReceiveActor
    {
        public YellowMessageActor()
        {
            Receive<InformationMessage>(message =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(message.Message);
                Console.ResetColor();
            });
        }
    }

    public class WarningMessageActor : ReceiveActor
    {
        public WarningMessageActor()
        {
            Receive<WarningMessage>(message =>
            {
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(message.Message);
                Console.ResetColor();
            });
        }
    }
}
