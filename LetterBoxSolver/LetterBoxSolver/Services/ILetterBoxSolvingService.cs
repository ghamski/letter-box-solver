using LetterBoxSolver.Models;

namespace LetterBoxSolver.Services
{
    public interface ILetterBoxSolvingService
    {
        public List<LetterBoxSolution> Solve(string allLetters);
    }
}
