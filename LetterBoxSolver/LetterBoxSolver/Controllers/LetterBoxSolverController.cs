using LetterBoxSolver.Models;
using LetterBoxSolver.Services;
using Microsoft.AspNetCore.Mvc;

namespace LetterBoxSolver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LetterBoxSolverController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<LetterBoxSolverController> _logger;
        private readonly ILetterBoxSolvingService _letterBoxSolvingService;

        public LetterBoxSolverController(ILogger<LetterBoxSolverController> logger, ILetterBoxSolvingService letterBoxSolvingService)
        {
            _logger = logger;
            _letterBoxSolvingService = letterBoxSolvingService;
        }

        [HttpPost(Name = "Solve")]
        public IEnumerable<(string, string)> Solve([FromBody] LetterBoxSolveRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException("Invalid request");
            }

            if (request.Sides == 0 || request.Chars.Count == 0)
            {
                throw new ArgumentException("Letters and sides must be > 0");
            }

            if (request.Sides * 3 != request.Chars.Count)
            {
                throw new ArgumentException("Length of chars array should equal sides * 3");
            }

            return _letterBoxSolvingService.Solve(request.Chars, request.Sides);
        }
    }
}