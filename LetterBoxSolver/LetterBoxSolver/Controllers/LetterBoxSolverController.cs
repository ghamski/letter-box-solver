using LetterBoxSolver.Models;
using LetterBoxSolver.Services;
using Microsoft.AspNetCore.Mvc;

namespace LetterBoxSolver.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LetterBoxSolverController : ControllerBase
    {
        private readonly ILogger<LetterBoxSolverController> _logger;
        private readonly ILetterBoxSolvingService _letterBoxSolvingService;

        public LetterBoxSolverController(ILogger<LetterBoxSolverController> logger, ILetterBoxSolvingService letterBoxSolvingService)
        {
            _logger = logger;
            _letterBoxSolvingService = letterBoxSolvingService;
        }

        [HttpPost(Name = "Solve")]
        public ActionResult<IEnumerable<(string, string)>> Solve([FromBody] LetterBoxSolveRequest request)
        {
            if (request == null)
            {
                return BadRequest("Requset can not be empty");
            }

            if (request.Sides == 0 || request.Chars.Length == 0)
            {
                return BadRequest("Letters and sides must be > 0");
            }

            if (request.Sides * 3 != request.Chars.Length)
            {
                return BadRequest("Length of chars array should equal sides * 3");
            }

            try
            {
                return Ok(_letterBoxSolvingService.Solve(request.Chars));
            }
            catch(Exception e)
            {
                _logger.LogError($"Encountered error while attempting to solve letter box request. Message: {e.Message}", e);
                throw;
            }
        }
    }
}