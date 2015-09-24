namespace MyWorkerRole
{
    internal class WorkerRoleMessage
    {
        public string Message { get; }

        public WorkerRoleMessage(string message)
        {
            Message = message;
        }
    }
}