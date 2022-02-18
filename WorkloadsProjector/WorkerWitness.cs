namespace WorkloadsProjector
{
    public class WorkerWitness
    {
        private static readonly object lockObject = new object();
        public DateTime LastExecution
        {
            get
            {
                lock (lockObject)
                {
                    return lastExecution;
                }
            }
            set
            {
                lock (lockObject)
                {
                    lastExecution = value;
                }
            }
        }
        private DateTime lastExecution;
    }
}