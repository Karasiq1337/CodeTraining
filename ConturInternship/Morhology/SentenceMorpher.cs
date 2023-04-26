using System;
using System.Collections.Generic;

namespace Morphology
{
    public class SentenceMorpher
    {
        private Dictionary<string, DictionaryLine> morpherDictionary;
        public SentenceMorpher()
        {
            morpherDictionary = new Dictionary<string, DictionaryLine>();    
        }
        public Dictionary<string, DictionaryLine> MorpherDictionary { get { return morpherDictionary; } set { morpherDictionary = value; } }
        
        public class DictionaryLine
        {
            public List<Line> lines = new List<Line>();
            public struct Line
            {
                public string word;
                public HashSet<string> attributes;
            }

        }

        public static SentenceMorpher Create(IEnumerable<string> dictionaryLines)
        {
            SentenceMorpher sentenceMorpher = new SentenceMorpher();
            bool isNewWord = false;
            bool isNewWordForm = false;
            string subDictionaryKey = "";
            Dictionary<string, DictionaryLine> currentMorpherDictionary = new Dictionary<string, DictionaryLine>();
            foreach (var line in dictionaryLines)
            {

                string trimedLine = line.Trim();
                if (trimedLine == "" || int.TryParse(trimedLine, out int number))
                {
                    isNewWord = true;
                    continue;
                }

                if (isNewWord)
                {
                    string[] subStrings = trimedLine.Split('\t', ' ', ',');
                    if (currentMorpherDictionary.ContainsKey(subStrings[0].ToUpper()))
                    {
                        isNewWord = false;
                        continue;
                    }
                    HashSet<string> wordAtributes = new HashSet<string>();
                    for (int i = 1; i < subStrings.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(subStrings[i]))
                        {
                            wordAtributes.Add(subStrings[i].ToLower());
                        }
                    }
                    DictionaryLine dictionaryLine = new DictionaryLine();
                    dictionaryLine.lines.Add(new DictionaryLine.Line { word = subStrings[0].ToUpper(), attributes = wordAtributes });
                    currentMorpherDictionary.Add(subStrings[0].ToUpper(), dictionaryLine);
                    subDictionaryKey = subStrings[0].ToUpper();
                    isNewWord = false;
                    isNewWordForm = true;
                    continue;
                }
                if (isNewWordForm)
                {
                    string[] subStrings = trimedLine.Split('\t', ' ', ',');
                    HashSet<string> wordAtributes = new HashSet<string>();
                    for (int i = 1; i < subStrings.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(subStrings[i]))
                        {
                            wordAtributes.Add(subStrings[i].ToLower());
                        }
                    }
                    currentMorpherDictionary[subDictionaryKey].lines.Add(new DictionaryLine.Line { word = subStrings[0].ToUpper(), attributes = wordAtributes });
                }
                sentenceMorpher.MorpherDictionary = currentMorpherDictionary;
            }
            return sentenceMorpher;
        }

        public virtual string Morph(string sentence)
        {
            if (String.IsNullOrWhiteSpace(sentence))
            {
                return "";
            }
            string[] splitSentance = sentence.Split(' ');
            string line = "";
            foreach (string sentenceWord in splitSentance)
            {
                string sentenceWordCleared = "";
                if (sentenceWord.Contains('}') || sentenceWord.Contains('{'))
                {
                    sentenceWordCleared = sentenceWord.Replace('{', ' ').Replace('}', ' ');
                    sentenceWordCleared = sentenceWordCleared.Trim();
                }
                else
                {
                    sentenceWordCleared = sentenceWord;
                    sentenceWordCleared = sentenceWordCleared.Trim();
                }
                string[] wordPlusAttribue = sentenceWordCleared.Split('\t', ' ', ',');

                if (wordPlusAttribue.Length == 1)
                {
                    line = line + wordPlusAttribue[0] + " ";
                    continue;
                }
                else
                {
                    for (int i = 1; i < wordPlusAttribue.Length; i++)
                    {
                        wordPlusAttribue[i] = wordPlusAttribue[i].ToLower();
                    }

                }

                HashSet<string> sentenceWordAtributs = new HashSet<string>(wordPlusAttribue);
                sentenceWordAtributs.Remove(wordPlusAttribue[0]);
                if (!MorpherDictionary.ContainsKey(wordPlusAttribue[0].ToUpper()))
                {
                    line = line + wordPlusAttribue[0] + " ";
                    continue;
                }

                DictionaryLine dictionaryLine = MorpherDictionary[wordPlusAttribue[0].ToUpper()];
                foreachLoop();
                void foreachLoop()
                {
                    foreach (DictionaryLine.Line item in dictionaryLine.lines)
                    {
                        if (sentenceWordAtributs.IsSubsetOf(item.attributes))
                        {
                            line = line + item.word + " ";
                            return;
                        }

                    }
                    line = line + wordPlusAttribue[0] + " ";
                }
            }
            return line.Trim();
        }
    }
}

