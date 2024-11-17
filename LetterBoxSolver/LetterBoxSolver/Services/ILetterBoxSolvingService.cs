namespace LetterBoxSolver.Services
{
    public interface ILetterBoxSolvingService
    {
        public List<(string, string)> Solve(List<string> chars, int sides);
    }
}
