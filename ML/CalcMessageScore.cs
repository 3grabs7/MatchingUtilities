using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    class CalcMessageScore : CalcBase
    {
        private MLContext context { get; set; }
        public CalcMessageScore(string path, char separator) : base(path, separator)
        {
            context = new MLContext();
            var data = context.Data.LoadFromTextFile<MessageData>(TestDataFilePath, separatorChar: SeparatorChar);

            var split = context.Data.TrainTestSplit(data, testFraction: 0.2);

            var features = split.TrainSet.Schema.Select(c => c.Name).Where(c => c != "Label").ToArray();

            var pipeline = context.Transforms.Text.FeaturizeText("Text", "Content")
                .Append(context.Transforms.Concatenate("Features", features))
                .Append(context.Transforms.Concatenate("Feature", "Features", "Text"))
                .Append(context.Regression.Trainers.LbfgsPoissonRegression());
        }
    }
}
