using DAL.Models;
using MatchingUtilities.Data;
using MatchingUtilities.Models;
using MatchingUtilities.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MatchingUtilities.Controllers
{
    public class MatchController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMatchingRepository _matchRepo;
        public MatchController(AppDbContext context, IMatchingRepository matchRepo)
        {
            _context = context;
            _matchRepo = matchRepo;
        }

        public IActionResult Index()
        {
            var viewModel = new MatchViewModel();
            viewModel.SjukaGrejer = new List<string> { "hehehehehe" };
            viewModel.DunderSiffran = 420;
            viewModel.User = new AppUser();
            return View(viewModel);
        }

        public void Match()
        {

        }
    }
}
