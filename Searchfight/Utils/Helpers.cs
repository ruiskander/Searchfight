using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Searchfight.Utils
{
    public static class Helpers
    {
        public static async Task RunWithMaxDegreeOfConcurrency<T>(this IEnumerable<T> collection,
            int maxDegreeOfConcurrency, Func<T, Task> selectTask)
        {
            var activeTasks = new List<Task>(maxDegreeOfConcurrency);
            foreach (var task in collection.Select(selectTask))
            {
                activeTasks.Add(task);

                if (activeTasks.Count != maxDegreeOfConcurrency)
                    continue;

                await Task.WhenAny(activeTasks.ToArray());

                activeTasks.RemoveAll(t => t.IsCompleted);
            }

            await Task.WhenAll(activeTasks.ToArray());
        }
    }
}
