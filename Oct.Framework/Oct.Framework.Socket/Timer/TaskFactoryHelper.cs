using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oct.Framework.Socket.Timer
{
    public static class TaskFactoryHelper
    {
        public static Task Iterate(this TaskFactory factory, IEnumerable<Task> asyncIterator)
        {
            var scheduler = factory.Scheduler ?? TaskScheduler.Current;

            // Get an enumerator from the enumerable 
            var enumerator = asyncIterator.GetEnumerator();
            if (enumerator == null) 
                throw new InvalidOperationException();

            // Create the task to be returned to the caller. And ensure 
            // that when everything is done, the enumerator is cleaned up. 
            var trs = new TaskCompletionSource<object>(factory.CreationOptions);
            trs.Task.ContinueWith(_ => enumerator.Dispose(), scheduler);

            // This will be called every time more work can be done. 
            Action<Task> recursiveBody = null;
            recursiveBody = antecedent =>
            {
                try
                {
                    // If the previous task completed with any exceptions, bail 
                    if (antecedent != null && antecedent.IsFaulted)
                        trs.TrySetException(antecedent.Exception);

                    else if (trs.Task.IsCanceled) trs.TrySetCanceled();

                    else if (enumerator.MoveNext())
                        enumerator.Current.ContinueWith(recursiveBody, scheduler);

                    // Otherwise, we're done! 
                    else trs.TrySetResult(null);
                }
                // If MoveNext throws an exception, propagate that to the user 
                catch (Exception exc) { trs.TrySetException(exc); }
            };

            // Get things started by launching the first task 
            factory.StartNew(_ => recursiveBody(null), scheduler);

            // Return the representative task to the user 
            return trs.Task;
        }
    }
}
