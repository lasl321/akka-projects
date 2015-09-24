using System;
using Akka.Actor;

namespace HelloWorld
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Create a new actor system (a container for your actors)
            var system = ActorSystem.Create("MySystem");

            // Create your actor and get a reference to it.
            // This will be an "IActorRef", which is not a reference to the actual actor
            // instance but rather a client or proxy to it.
            var greeter1 = system.ActorOf<GreetingActor1>("greeter1");
            var greeter2 = system.ActorOf<GreetingActor2>("greeter2");

            // Send a message to the actor.
            greeter1.Tell(new Greet("World 1"));
            greeter2.Tell(new Greet("World 2"));

            // This prevents the app from exiting
            // before the async work is done.
            Console.ReadLine();
        }
    }
}