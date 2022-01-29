using System;
using System.Linq;

namespace WpfSimpleTest.Helpers
{
    class DataGenerator
    {
        public static double[] GetSinwave(int number_of_samples, double frequency)
        {
            var random = new Random().NextDouble() / 10d;

            double[] samples = new double[number_of_samples];

            for (var sample_number = 0; sample_number < number_of_samples; sample_number++)
                samples[sample_number] =  Math.Sin(sample_number * 0.002 + random + frequency) * 1;

            return samples;
        }

        public static double[] GetLinspace(double startval, double endval, int steps)
        {
            double interval = endval / Math.Abs(endval) * Math.Abs(endval - startval) / (steps - 1);
            return (from val in Enumerable.Range(0, steps)
                    select startval + (val * interval)).ToArray();
        }

    }
}
