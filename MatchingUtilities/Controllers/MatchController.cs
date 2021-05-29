using MatchingUtilities.Data;
using MatchingUtilities.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MatchingUtilities.Controllers
{
    public class MatchController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMatchingRepository _matching;
        public MatchController(AppDbContext context, IMatchingRepository matching)
        {
            _context = context;
            _matching = matching;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
