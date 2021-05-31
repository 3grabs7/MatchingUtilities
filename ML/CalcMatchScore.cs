using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class CalcMatchScore : CalcBase
    {
        private MLContext context { get; set; }

        public CalcMatchScore(string path, char separator) : base(path, separator)
        {
            context = new MLContext();
        }

        public void Run()
        {
            var data = context.Data.LoadFromTextFile<MatchData>(TestDataFilePath, separatorChar: SeparatorChar);

            var split = context.Data.TrainTestSplit(data, testFraction: 0.2);

            var features = split.TrainSet.Schema.Select(c => c.Name).Where(c => c != "Label").ToArray();

            var pipeline = context.Transforms.Concatenate("Features", features)
                .Append(context.Regression.Trainers.LbfgsPoissonRegression());
        }
    }
}
