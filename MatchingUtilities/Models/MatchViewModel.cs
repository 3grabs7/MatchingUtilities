using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingUtilities.Models
{
    public class MatchViewModel
    {
        public List<string> SjukaGrejer { get; set; }
        public int DunderSiffran { get; set; }
        public AppUser User { get; set; }
    }
}
