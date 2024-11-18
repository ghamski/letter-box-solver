using LetterBoxSolver.Models;
using LetterBoxSolver.Repos;
using System.Collections.Generic;

namespace LetterBoxSolver.Services
{
    public class LetterBoxSolverService : ILetterBoxSolvingService
    {
        private readonly IDictionaryRepository _dictionaryRepository;
        public LetterBoxSolverService(IDictionaryRepository dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
        }

        public List<LetterBoxSolution> Solve(string allLetters)
        {
            var banList = new List<string>();
            var potentialAnswers = new List<LetterBoxSolution>();
            for (int i = 0; i < allLetters.Length; i+= 3)
            {
                for (int j = i; j < i + 3; j++)
                {
                    for (int k = j; k < i + 3; k++)
                    {
                        banList.Add($"{allLetters[j]}{allLetters[k]}");
                        if (allLetters[j] != allLetters[k])
                        {
                            banList.Add($"{allLetters[k]}{allLetters[j]}");
                        }
                    }
                    
                }
            }
            var bannedLetters = GetRemainingLetters("abcdefghijklmnopqrstuvwxyz-", allLetters).Select(x => $"{x}");
            banList.AddRange(bannedLetters);

            var dictionary = _dictionaryRepository.GetDictionary(banList);

            foreach (var letters in dictionary.Keys.OrderByDescending(x => x.Length))
            {
                var remainingLetters = GetRemainingLetters(allLetters, letters);

                foreach (var key in dictionary.Keys)
                {
                    if (key.ContainsAllLetters(remainingLetters))
                    {
                        AddAnswers(dictionary.GetValueOrDefault(letters)!, dictionary.GetValueOrDefault(key)!, potentialAnswers);
                    };
                }
            }

            return potentialAnswers.Where(x => x.WordTwo.StartsWith(x.WordOne[x.WordOne.Length-1])).ToList();
        }

        private string GetRemainingLetters(string allLetters, string lettersUsed)
        {
            return string.Concat(allLetters.Where(x => !lettersUsed.Contains(x)));
        }

        private void AddAnswers(HashSet<string> setOne, HashSet<string> setTwo, List<LetterBoxSolution> answers)
        {
            foreach(var wordOne in setOne)
            {
                foreach (var wordTwo in setTwo)
                {
                    answers.Add(new LetterBoxSolution
                    {
                        WordOne = wordOne,
                        WordTwo = wordTwo
                    });
                }
            }
        }
    }
}
