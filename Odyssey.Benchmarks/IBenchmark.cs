using System;

namespace Odyssey.Benchmarks
{
    /// <summary>
    /// Benchmark interface.
    /// </summary>
    public interface IBenchmark
    {
        /// <summary>
        /// Name of the benchmark.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Benchmark description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Perform benchmark.
        /// </summary>
        TimeSpan PerformBenchmark();
    }
}
