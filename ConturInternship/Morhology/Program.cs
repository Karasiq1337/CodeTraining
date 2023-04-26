using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Morphology.Utils;

namespace Morphology
{
    public static class Program
    {
        public static string DictionaryPath => Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!,
            @"Resources\dict.opcorpora.txt"
        );
        
        private static void Main()
        {
            var sw = Stopwatch.StartNew();
            var morpher = SentenceMorpher.Create(FileLinesEnumerable.Create(DictionaryPath));
            Console.WriteLine($"Init took {sw.Elapsed}");
            RunLoop(morpher);
        }

        public static void RunLoop(SentenceMorpher morpher)
        {
            var input = "ОДНАЖДЫ{ADVB} В{NOUN,anim,ms-f,Sgtm,Fixd,Abbr,Init,nomn} СТУДЁНЫЙ{adjf,Qual,femn,sing,accs} ЗИМНИЙ{ADJF,femn,accs} ПОРА{sing,accs} Я{NOUN,anim,ms-f,Sgtm,Fixd,Abbr,Patr,Init,sing,nomn} " +
                "ИЗА{NOUN,anim,plur,gent} ЛЕСА{NOUN,inan,femn,sing,accs} ВЫШЕЛ{VERB,perf,intr,sing,indc} ЕСТЬ{VERB,impf,intr,masc,sing,past,indc} СИЛЬНЫЙ{Qual,masc,nomn} МОРОЗ{anim,femn,Sgtm,Surn,sing,nomn} " +
                "ГЛЯЖУ{VERB,impf,tran,sing,pres,indc} ПОДНИМАЮСЬ{VERB,impf,intr,3per,pres,indc} МЕДЛЕН{Qual,neut} В{NOUN,ms-f,Fixd,Abbr,Patr,Init,sing,nomn} ГОРА{NOUN,inan,femn,sing,accs} ЛОШАДКА{NOUN,anim,femn,sing} " +
                "ВЕЗУЩИЙ{impf,pres,actv,femn,sing,nomn} ХВОРОСТ{NOUN,gen2} ВОЗ{NOUN,femn,Fixd,Abbr,Orgn,sing,nomn}";
            var input1 = "студёный{ADJF,Qual,femn,sing,accs}";
            var sw = new Stopwatch();
            var result = morpher.Morph(input);
            Console.WriteLine($"[took {sw.Elapsed}] {result}");
            var result1 = morpher.Morph(input1);
            Console.WriteLine($"[took {sw.Elapsed}] {result1}");
        }
    }
}