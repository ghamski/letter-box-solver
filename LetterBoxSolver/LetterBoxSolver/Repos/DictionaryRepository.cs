using System.IO;

namespace LetterBoxSolver.Repos
{
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly string _filePath = "dictionary.txt";
        public DictionaryRepository()
        {

        }

        public Dictionary<string, HashSet<string>> GetDictionary(List<string> banList)
        {
            var dictionary = new Dictionary<string, HashSet<string>>();
            try
            {
                var sr = new StreamReader(_filePath);
                var line = sr.ReadLine();

                while (line != null)
                {
                    line = line.Trim().ToLower();

                    // ignore words with length less than 5, assuming final answer will almost certainly be 2 words of length >=5
                    // only add words which follow the letter box rules
                    if (line.Length >= 5 && !line.Contains("'") && !IsBanned(line, banList))
                    {
                        var characters = line.ToArray();
                        Array.Sort(characters);
                        var key = new string(characters.Distinct().ToArray());
                        if (dictionary.ContainsKey(key))
                        {
                            var set = dictionary.GetValueOrDefault(key);
                            set!.Add(line);
                        }
                        else
                        {
                            var set = new HashSet<string>();
                            set.Add(line);
                            dictionary.Add(key, set);
                        }
                    }

                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch
            {
                throw;
            }
            return dictionary;
        }

        private static bool IsBanned(string line, List<string> banList)
        {
            foreach (var ban in banList)
            {
                if (line.Contains(ban))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
