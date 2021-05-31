using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class MatchData
    {
        [LoadColumn(1), ColumnName("Label")]
        public int MatchScore { get; set; }
        [LoadColumn(1)]
        public int ActivityScore { get; set; }
        [LoadColumn(1)]
        public int MessageScore { get; set; }
    }
}
