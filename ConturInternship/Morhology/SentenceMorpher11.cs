using System.Collections.Generic;
using System;

namespace Morphology
{
  
    public class SentenceMorpher1
    {
       
        public static Dictionary<string, Dictionary<HashSet<string>,string>> dictionary = new Dictionary<string, Dictionary<HashSet<string>, string>>(); 

        public static SentenceMorpher Create(IEnumerable<string> dictionaryLines)
        {
            bool isNewWord = false;
            bool isNewWordForm = false;
            string subDictionaryKey ="";
            foreach (var line in dictionaryLines)
            {
         
                
                if (line == "" || int.TryParse(line, out int number))
                {
                    isNewWord = true;
                  continue;
                }

                if (isNewWord)
                {
                   string[] subStrings = line.Split('\t', ' ', ',');
                    if (dictionary.ContainsKey(subStrings[0]))
                    {
                        isNewWord = false;
                        continue;
                    }
                    dictionary.Add(subStrings[0], new Dictionary<HashSet<string>, string>());
                    HashSet<string> wordAtributes = new HashSet<string>();
                    for (int i = 1; i < subStrings.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(subStrings[i]))
                        {
                            wordAtributes.Add(subStrings[i].ToLower());
                        }     
                    }
                    subDictionaryKey = subStrings[0];
                    dictionary[subDictionaryKey].Add(wordAtributes, subStrings[0]);
                    isNewWord = false;
                    isNewWordForm = true;
                    continue;
                }
                if (isNewWordForm)
                {
                    string[] subStrings = line.Split('\t', ' ', ',');
                    HashSet<string> wordAtributes = new HashSet<string>();
                    for (int i = 1; i < subStrings.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(subStrings[i]))
                        {
                            wordAtributes.Add(subStrings[i].ToLower());
                        }
                    } 
                    dictionary[subDictionaryKey].Add(wordAtributes, subStrings[0]);
                }       
            }
            return new SentenceMorpher();
        }

        public virtual string Morph(string sentence)
        {
            if (String.IsNullOrWhiteSpace(sentence))
            {
                return "";
            }
            string[] splitSentance = sentence.Split(' ') ;
            sentence = "";
            foreach (string sentenceWord in splitSentance)
            {
                string sentenceWordCleared = "";
                if (sentenceWord.Contains('}')) 
                {
                     sentenceWordCleared = sentenceWord.Remove(sentenceWord.Length - 1).ToLower();
                }
                else
                {
                     sentenceWordCleared = sentenceWord.ToLower();
                }
                string[] wordPlusAttribue = sentenceWordCleared.Split('{', ' ', ',');
                wordPlusAttribue[0  ] = wordPlusAttribue[0].ToUpper();
                if (wordPlusAttribue.Length == 1)
                {
                    sentence = sentence + wordPlusAttribue[0] + " ";
                    continue;
                }

                HashSet<string> atributs = new HashSet<string>(wordPlusAttribue);
                atributs.Remove(wordPlusAttribue[0]);
                if (!dictionary.ContainsKey(wordPlusAttribue[0]))
                {
                    sentence = sentence + wordPlusAttribue[0] + " ";
                }

                Dictionary<HashSet<string>, string> hashSets = dictionary[wordPlusAttribue[0].ToUpper()];
                foreachLoop();
                void foreachLoop()
                {
                    foreach (var set in hashSets)
                    {
                        if (atributs.IsSubsetOf(set.Key))
                        {
                            sentence = sentence + set.Value + " ";
                            return;
                        }

                    }
                    sentence = sentence + wordPlusAttribue[0] + " ";
                }   
            }
            return sentence.Trim();
        }
    }
}
