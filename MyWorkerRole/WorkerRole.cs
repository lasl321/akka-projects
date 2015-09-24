using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace MyWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
        private ActorSystem _actorSystem;

        public override void Run()
        {
            Trace.TraceInformation("MyWorkerRole is running");

            try
            {
                RunAsync(cancellationTokenSource.Token).Wait();
            }
            finally
            {
                runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Setup the Actor System
            _actorSystem = ActorSystem.Create("MySystem");

            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            var result = base.OnStart();

            Trace.TraceInformation("MyWorkerRole has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("MyWorkerRole is stopping");

            cancellationTokenSource.Cancel();
            runCompleteEvent.WaitOne();

            // Shutdown the Actor System
            _actorSystem.Shutdown();

            base.OnStop();

            Trace.TraceInformation("MyWorkerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // Create an instance to the top-level user Actor
            var workerRoleActor = _actorSystem.ActorOf<WorkerRoleActor>("WorkerRole");

            // Send a message to the Actor
            workerRoleActor.Tell(new WorkerRoleMessage("Hello World!"));

            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000).ConfigureAwait(false);
            }
        }
    }
}