using System;
using Akka.Actor;

namespace HelloWorld
{
    public class GreetingActor1 : ReceiveActor
    {
        public GreetingActor1()
        {
            Receive<Greet>(greet => Console.WriteLine("Hello {0}", greet.Who));
        }
    }

    public class GreetingActor2 : TypedActor, IHandle<Greet>
    {
        public void Handle(Greet greet)
        {
            Console.WriteLine("Hello {0}!", greet.Who);
        }
    }

}