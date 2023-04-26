using System.Collections.Generic;
using System.IO;


namespace YandexContestgistogramm
{
    /*Описание:
     * Формат ввода
     * Входной файл содержит зашифрованный текст сообщения. 
     * Он содержит строчные и прописные латинские буквы, цифры, знаки препинания («.», «!», «?», «:», «-», «,», «;», «(», «)»), 
     * пробелы и переводы строк. Размер входного файла не превышает 10000 байт. Текст содержит хотя бы один непробельный символ. 
     * Все строки входного файла не длиннее 200 символов.Для каждого символа c кроме пробелов и переводов строк выведите столбик
     * из символов «#», количество которых должно быть равно количеству символов c в данном тексте. Под каждым столбиком напишите
     * символ, соответствующий ему. Отформатируйте гистограмму так, чтобы нижние концы столбиков были на одной строке, первая 
     * строка и первый столбец были непустыми. Не отделяйте столбики друг от друга. Отсортируйте столбики в порядке увеличения 
     * кодов символов.
     * Формат вывода
     * Для каждого символа c кроме пробелов и переводов строк выведите столбик из символов «#», количество которых должно
     * быть равно количеству символов c в данном тексте. Под каждым столбиком напишите символ, соответствующий ему. Отформатируйте
     * гистограмму так, чтобы нижние концы столбиков были на одной строке, первая строка и первый столбец были непустыми. Не отделяйте 
     * столбики друг от друга. Отсортируйте столбики в порядке увеличения кодов символов.
     * 
     * //Ввод чрерз input.txt, вывод в iutput.txt
     */
    public class Gistogramm
    {

        static void Main(string[] args)
        {

            string inputString = File.ReadAllText("input.txt");

            GistogrammCreator creator = new GistogrammCreator();
            string cleared = creator.ClearString(inputString);
            Dictionary<char, int> map = creator.CreateDictionary(cleared);
            List<char> order = creator.CreateOrder(map);
            char[,] gist = creator.CreateGistogramm(map, order);
            gist = creator.Transpose(gist);

            creator.dumpMatrix(gist);


        }
        class GistogrammCreator
        {
            public string ClearString(string inputString)
            {

                inputString = inputString.Replace(" ", "");
                inputString = inputString.Replace("\r", "");
                inputString = inputString.Replace("\n", "");

                return inputString;
            }
            public Dictionary<char, int> CreateDictionary(string inputString)
            {

                Dictionary<char, int> gestogrammColumns = new Dictionary<char, int>();
                foreach (char c in inputString)
                {
                    if (!gestogrammColumns.ContainsKey(c))
                    {
                        gestogrammColumns.Add(c, 1);
                    }
                    else
                    {
                        ++gestogrammColumns[c];
                    }
                }
                return gestogrammColumns;
            }
            public char[,] CreateGistogramm(Dictionary<char, int> dict, List<char> order)
            {

                int maxValue = 0;
                foreach (char c in dict.Keys)
                {
                    if (dict[c] > maxValue)
                    {
                        maxValue = dict[c];
                    }
                }
                char[,] gistogramm = new char[order.Count, maxValue + 1];
                int i = 0;
                foreach (char c in order)
                {
                    gistogramm[i, maxValue] = c;
                    for (int j = maxValue - 1; j > maxValue - dict[c] - 1; --j)
                    {
                        gistogramm[i, j] = '#';
                    }
                    ++i;
                }

                return gistogramm;
            }
            public List<char> CreateOrder(Dictionary<char, int> dict)
            {

                List<char> list = new List<char>();
                foreach (char c in dict.Keys)
                {
                    list.Add(c);
                }
                list.Sort();

                return list;
            }

            public T[,] Transpose<T>(T[,] matrix)
            {
                int w = matrix.GetLength(0);
                int h = matrix.GetLength(1);

                T[,] result = new T[h, w];

                for (int i = 0; i < w; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        result[j, i] = matrix[i, j];
                    }
                }
                return result;
            }
            public void dumpMatrix<T>(T[,] a)
            {
                using (StreamWriter sw = new StreamWriter("output.txt"))
                {
                    int m = a.GetLength(0);
                    int n = a.GetLength(1);
                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            sw.Write(a[i, j]);
                        }
                        sw.WriteLine();
                    }
                }
            }
        }

    }
}
