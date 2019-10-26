using System;

namespace Odyssey.Benchmarks
{
    /// <summary>
    /// Serial benchmark.
    /// </summary>
    public class SerialBenchmark : IBenchmark
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name => nameof(SerialBenchmark);

        /// <summary>
        /// Description.
        /// </summary>
        public string Description => "Perform a ton of operations in serial.";

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
