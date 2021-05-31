using Microsoft.ML;
using System;
using System.Linq;

namespace ML
{
    class Program
    {
        const string TEST_DATA_FILE_PATH = "./Data/TestData.csv";
        const char SEPARATOR_CHAR = ',';
        static void Main(string[] args)
        {
            var context = new MLContext();
            var data = context.Data.LoadFromTextFile<MatchData>(TEST_DATA_FILE_PATH, separatorChar: SEPARATOR_CHAR);

            var split = context.Data.TrainTestSplit(data, testFraction: 0.2);

            var features = split.TrainSet.Schema.Select(c => c.Name).Where(c => c != "Label").ToArray();

            var pipeline = context.Transforms.Concatenate("Features", features)
                .Append(context.Regression.Trainers.LbfgsPoissonRegression());

            var model = pipeline.Fit(split.TrainSet);

            var predictions = model.Transform(split.TrainSet);

            var metrics = context.Regression.Evaluate(predictions);

            Console.WriteLine(metrics.RSquared);
        }
    }
}
