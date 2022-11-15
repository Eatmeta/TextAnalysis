using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var bigramDictionary = GetBigrams(text);
            var trigramDictionary = GetTrigrams(text);
            var result = bigramDictionary.ToDictionary(
                keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);
            foreach (var keyValuePair in trigramDictionary)
            {
                result.Add(keyValuePair.Key, keyValuePair.Value);
            }
            return result;
        }

        private static Dictionary<string, string> GetBigrams(List<List<string>> sentences)
        {
            var resultBigram = new Dictionary<string, Dictionary<string, int>>();

            foreach (var sentence in sentences)
            {
                for (var i = 0; i < sentence.Count - 1; i++)
                {
                    if (resultBigram.ContainsKey(sentence[i]))
                    {
                        if (resultBigram[sentence[i]].ContainsKey(sentence[i + 1]))
                        {
                            var number = resultBigram[sentence[i]][sentence[i + 1]] + 1;
                            resultBigram[sentence[i]].Remove(sentence[i + 1]);
                            resultBigram[sentence[i]].Add(sentence[i + 1], number);
                        }
                        else resultBigram[sentence[i]].Add(sentence[i + 1], 1);
                    }
                    else
                    {
                        var innerDict = new Dictionary<string, int> {{sentence[i + 1], 1}};
                        resultBigram.Add(sentence[i], innerDict);
                    }
                }
            }
            return SortNgram(resultBigram);
        }

        private static Dictionary<string, string> GetTrigrams(List<List<string>> sentences)
        {
            var resultTrigram = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in sentences)
            {
                for (var i = 0; i < sentence.Count - 2; i++)
                {
                    var key = $"{sentence[i]} {sentence[i + 1]}";
                    if (resultTrigram.ContainsKey(key))
                    {
                        if (resultTrigram[key].ContainsKey(sentence[i + 2]))
                        {
                            var number = resultTrigram[key][sentence[i + 2]] + 1;
                            resultTrigram[key].Remove(sentence[i + 2]);
                            resultTrigram[key].Add(sentence[i + 2], number);
                        }
                        else resultTrigram[key].Add(sentence[i + 2], 1);
                    }
                    else
                    {
                        var innerDict = new Dictionary<string, int> {{sentence[i + 2], 1}};
                        resultTrigram.Add(key, innerDict);
                    }
                }
            }
            return SortNgram(resultTrigram);
        }

        public static Dictionary<string, string> SortNgram(Dictionary<string, Dictionary<string, int>> ngram)
        {
            var resultMax = new Dictionary<string, string>();
            foreach (var keyValuePair in ngram)
            {
                var first = keyValuePair.Value.First();
                var maxIndex = first.Value;
                var wordMaxIndex = first.Key;
                foreach (var valuePair in keyValuePair.Value)
                {
                    if (valuePair.Value > maxIndex)
                    {
                        maxIndex = valuePair.Value;
                        wordMaxIndex = valuePair.Key;
                    }
                    if (valuePair.Value != maxIndex) continue;
                    if (string.CompareOrdinal(valuePair.Key, wordMaxIndex) >= 0) continue;
                    maxIndex = valuePair.Value;
                    wordMaxIndex = valuePair.Key;
                }
                resultMax.Add(keyValuePair.Key, wordMaxIndex);
            }
            return resultMax;
        }
    }
}