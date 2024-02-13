using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLP.POS
{
    public class POSDataSet
    {
        private List<Sentence> sentenceList;

        public POSDataSet()
        {
            sentenceList = new List<Sentence>();
        }

        public List<Sentence> SentenceList
        {
            get { return sentenceList; }
            set { sentenceList = value; }
        }

        public static Tuple<POSDataSet, POSDataSet> Split(POSDataSet completeDataSet, double splitFraction)
        {
            int splitIndex = (int)(splitFraction * completeDataSet.SentenceList.Count);

            POSDataSet trainingSet = new POSDataSet();
            POSDataSet testSet = new POSDataSet();

            trainingSet.SentenceList.AddRange(completeDataSet.SentenceList.Take(splitIndex));
            testSet.SentenceList.AddRange(completeDataSet.SentenceList.Skip(splitIndex));

            return Tuple.Create(trainingSet, testSet);
        }
    }
}
