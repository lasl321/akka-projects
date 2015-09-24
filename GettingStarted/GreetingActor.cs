using System;
using Akka.Actor;

namespace GettingStarted
{
    public class GreetingActor : ReceiveActor
    {
        public GreetingActor()
        {
            Receive<Greet>(greet => Console.WriteLine($"Hello {greet.Who}"));
        }
    }
}