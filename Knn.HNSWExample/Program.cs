using HNSW.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Knn.HNSWExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var parameters = new SmallWorld<float[], float>.Parameters()
            {
                M = 50,
                LevelLambda = 1 / Math.Log(15),
            };

            var r = new Random();
            const int dimensions = 100;
            var vectors = GetFloatVectors(dimensions, r);
            var graph = new SmallWorld<float[], float>(CosineDistance.SIMD);
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            graph.BuildGraph(vectors, new Random(42), parameters);
            stopWatch.Stop();
            var buildTime = stopWatch.Elapsed;
            Console.WriteLine($"graph build for {vectors.Count} items in {buildTime}");
            byte[] buffer = graph.SerializeGraph();
            Console.WriteLine($"graph serialized in {buffer.Length} bytes");
            float[] query = GetRandomVector(dimensions, r);

            for (var i = 0; i < 100; i++)
            {
                stopWatch = new Stopwatch();
                stopWatch.Start();
                var best20 = graph.KNNSearch(query, 20);
                stopWatch.Stop();
                buildTime = stopWatch.Elapsed;
                Console.WriteLine($"Top 20 items retrieved in {buildTime}");
            }
            /*foreach (var item in best20)
            {
                Console.WriteLine($"{item.Id} -> {item.Distance}");
            }*/
        }


        private static IList<float[]> GetFloatVectors(int dimensions, Random generator)
        {
            var result = new List<float[]>();
            for (var i = 0; i < 10000; i++)
            {
                result.Add(GetRandomVector(dimensions, generator));
            }
            return result;
        }

        private static float[] GetRandomVector(int dimensions, Random generator)
        {
            var v = new float[dimensions];
            for (var j = 0; j < dimensions; j++)
            {
                v[j] = (float)generator.NextDouble();
            }
            return v;
        }
    }
}
