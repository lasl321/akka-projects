using System;
using System.Threading.Tasks;
using Akka.Actor;

namespace SwitchableBehaviors
{
    public class FreeBusyActor : ReceiveActor
    {
        public FreeBusyActor()
        {
            // the actor starts as "Free"
            Free();
        }

        private void Free()
        {
            // when free, only "get busy" commands are handled
            Receive<string>(s =>
            {
                if (s == "get busy")
                {
                    Console.WriteLine("Getting busy...");

                    // the actor becomes busy, so the next messages are handled differently
                    Become(Busy);

                    // the actor starts some work in the background
                    // when it's done tell itself it's free
                    Task.Delay(800).ContinueWith(_ => "you're free").PipeTo(Self, Self);
                }
            });
        }

        private void Busy()
        {
            Receive<string>(s =>
            {
                // when busy, only accept messages from itself to get free
                if (s == "you're free" && Sender.Equals(Self))
                {
                    Console.WriteLine("Getting free...");
                    Become(Free);
                }
                // otherwise we won't do anything
                else
                {
                    Console.WriteLine("Not doing anything, I'm busy...");
                }
            });
        }
    }
}