namespace LetterBoxSolver.Services
{
    public static class StringExtensions
    {
        public static bool ContainsAllLetters(this string str, IEnumerable<char> letters)
        {
            foreach (var let in letters)
            {
                if (!str.Contains(let))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
