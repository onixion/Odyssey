using System;

namespace Odyssey.Benchmarks
{
    /// <summary>
    /// Parallel benchmark.
    /// </summary>
    public class ParallelBenchmark : IBenchmark
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name => nameof(ParallelBenchmark);

        /// <summary>
        /// Description.
        /// </summary>
        public string Description => "Performs a ton of operations in parallel.";

        /// <summary>
        /// Perform benchmark.
        /// </summary>
        /// <returns></returns>
        public TimeSpan PerformBenchmark()
        {
            throw new NotImplementedException();
        }
    }
}
