using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;

namespace SwitchableBehaviors
{
    internal static class Program
    {
        public static void Main()
        {
            var system = ActorSystem.Create("Sample");

            var actor = system.ActorOf<FreeBusyActor>();

            Task.Run(async () =>
            {
                for (var i = 0; i < 10; i++)
                {
                    actor.Tell("get busy");
                    await Task.Delay(40);
                }

                await Task.Delay(500);
            }).Wait();
        }
    }
}