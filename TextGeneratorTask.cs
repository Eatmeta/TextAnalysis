using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            var builder = new StringBuilder();
            var lastWords = new List<string>();

            if (phraseBeginning.Contains(" "))
            {
                var words = phraseBeginning.Split(' ');
                lastWords.Add(words[words.Length - 2]);
                lastWords.Add(words[words.Length - 1]);
                builder.Append(phraseBeginning);
            }
            else
            {
                lastWords.Add(phraseBeginning);
                builder.Append(phraseBeginning);
            }

            for (var i = 0; i < wordsCount; i++)
            {
                if (lastWords.Count > 1)
                {
                    var key = $"{lastWords[0]} {lastWords[1]}";
                    if (nextWords.ContainsKey(key))
                    {
                        builder.Append($" {nextWords[key]}");
                        lastWords.Add(nextWords[key]);
                        lastWords.RemoveAt(0);
                    }
                    else if (nextWords.ContainsKey(lastWords[1]))
                    {
                        builder.Append($" {nextWords[lastWords[1]]}");
                        lastWords.Add(nextWords[lastWords[1]]);
                        lastWords.RemoveAt(0);
                    }
                    else return builder.ToString();
                }
                else
                {
                    if (nextWords.ContainsKey(lastWords[0]))
                    {
                        builder.Append($" {nextWords[lastWords[0]]}");
                        lastWords.Add(nextWords[lastWords[0]]);
                    }
                    else return builder.ToString();
                }
            }
            
            return builder.ToString();
        }
    }
}