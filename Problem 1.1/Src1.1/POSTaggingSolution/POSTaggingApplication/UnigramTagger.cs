using NLP.POS.Taggers;
using NLP.POS;
using NLP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSTaggingApplication
{
    public class UnigramTagger : POSTagger
    {
        private Dictionary<string, string> mostCommonTagForWord;

        public UnigramTagger(POSDataSet trainingDataSet)
        {
            mostCommonTagForWord = GenerateMostCommonTagForWord(trainingDataSet);
        }

        private Dictionary<string, string> GenerateMostCommonTagForWord(POSDataSet trainingDataSet)
        {
            Dictionary<string, Dictionary<string, int>> wordTagCounts = new Dictionary<string, Dictionary<string, int>>();

            foreach (Sentence sentence in trainingDataSet.SentenceList)
            {
                foreach (TokenData tokenData in sentence.TokenDataList)
                {
                    string word = tokenData.Token.Spelling.ToLower();
                    string posTag = tokenData.Token.POSTag;

                    if (wordTagCounts.ContainsKey(word))
                    {
                        if (wordTagCounts[word].ContainsKey(posTag))
                        {
                            wordTagCounts[word][posTag]++;
                        }
                        else
                        {
                            wordTagCounts[word][posTag] = 1;
                        }
                    }
                    else
                    {
                        wordTagCounts[word] = new Dictionary<string, int> { { posTag, 1 } };
                    }
                }
            }

            Dictionary<string, string> mostCommonTagForWord = new Dictionary<string, string>();

            foreach (var kvp in wordTagCounts)
            {
                mostCommonTagForWord[kvp.Key] = kvp.Value.OrderByDescending(x => x.Value).First().Key;
            }

            return mostCommonTagForWord;
        }


        public override List<string> Tag(Sentence sentence)
        {
            List<string> tags = new List<string>();

            foreach (TokenData tokenData in sentence.TokenDataList)
            {
                Token token = tokenData.Token;
                string word = token.Spelling.ToLower();

                if (mostCommonTagForWord.ContainsKey(word))
                {
                    tags.Add(mostCommonTagForWord[word]);
                }
                else
                {
                    // Default to a common POS tag if the word is not found in the training set
                    tags.Add("NOUN"); // You may choose a default POS tag based on your requirements
                }
            }

            return tags;
        }
    }
}
