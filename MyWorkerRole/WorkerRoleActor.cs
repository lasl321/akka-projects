using System.Diagnostics;
using Akka.Actor;

namespace MyWorkerRole
{
    internal class WorkerRoleActor : ReceiveActor
    {
        public WorkerRoleActor()
        {
            Receive<WorkerRoleMessage>(greet => Trace.TraceInformation("Hello {0}", greet.Message));
        }
    }
}