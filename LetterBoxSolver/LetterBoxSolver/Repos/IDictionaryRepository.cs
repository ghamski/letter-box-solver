namespace LetterBoxSolver.Repos
{
    public interface IDictionaryRepository
    {
        public Dictionary<string, HashSet<string>> GetDictionary(List<string> banList);
    }
}
