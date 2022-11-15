using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var wordList = new List<string>();
            var separators = new char[] {'.', '!', '?', ';', ':', '(', ')'};
            var builder = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                if (!char.IsLetter(text[i]))
                {
                    if (text[i] == '\'')
                    {
                        builder.Append(text[i]);
                        continue;
                    }

                    if (separators.Contains(text[i]))
                    {
                        if (builder.ToString().Length != 0)
                        {
                            wordList.Add(builder.ToString().ToLower());
                            builder.Clear();
                        }
                        AddWordListToSentenceList(wordList, sentencesList);
                        continue;
                    }

                    if (i == text.Length - 1)
                    {
                        wordList.Add(builder.ToString().ToLower());
                        AddWordListToSentenceList(wordList, sentencesList);
                        continue;
                    }

                    if (builder.ToString().Length != 0)
                    {
                        wordList.Add(builder.ToString().ToLower());
                        builder.Clear();
                    }
                }
                else
                {
                    if (i == text.Length - 1)
                    {
                        builder.Append(text[i]);
                        wordList.Add(builder.ToString().ToLower());
                        AddWordListToSentenceList(wordList, sentencesList);
                    }
                    builder.Append(text[i]);
                }
            }

            return sentencesList;
        }

        private static void AddWordListToSentenceList(List<string> wordList, List<List<string>> sentencesList)
        {
            if (wordList.Count != 0)
                sentencesList.Add(wordList.Where(word => word.Length != 0).ToList());
            wordList.Clear();
        }
    }
}