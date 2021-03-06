﻿using System;
using Akka.Actor;

namespace GettingStarted
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Create a new actor system (a container for your actors)
            var system = ActorSystem.Create("MySystem");

            // Create your actor and get a reference to it.
            // This will be an "ActorRef", which is not a
            // reference to the actual actor instance
            // but rather a client or proxy to it.
            var greeter = system.ActorOf<GreetingActor>("greeter");

            // Send a message to the actor
            greeter.Tell(new Greet("World"));

            // This prevents the app from exiting
            // before the async work is done
            Console.ReadLine();
        }
    }
}