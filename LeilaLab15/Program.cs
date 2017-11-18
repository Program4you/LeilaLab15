using System;
using System.IO;

namespace LeilaLab15 {
    class Program {
        // convert AGTC (1234) to AG (12)  
        static string unifiq(string dna) {
            string res = "";

            for (int i = 0; i < dna.Length; i++)
                res += (dna[i] == '1' || dna[i] == '3' ? '1' : '2');

            return res;
        }

        static bool naive(string president, string relative) {
            int width = president.Length - relative.Length;

            for (int i = 0; i < width; i++) {
                int j = 0;

                while (j < relative.Length) {
                    if (president[i + j] != relative[j])
                        break;

                    j++;
                }

                if (j == relative.Length)
                    return true;
            }

            return false;
        }

        static int[] prefixFunction(string s) {
            int n = s.Length;
            int[] pi = new int[n];
            pi[0] = 0;

            for (int i = 1; i < n; i++) {
                int j = pi[i - 1];

                while (j > 0 && s[i] != s[j])
                    j = pi[j - 1];

                if (s[i] == s[j])
                    j++;

                pi[i] = j;
            }

            return pi;
        }

        static bool morrisonPratt(string president, string relative) {
            int n = relative.Length;
            int m = president.Length;
            string tmp = relative + '#' + president;

            int[] function = prefixFunction(tmp);

            for (int i = n + 1; i < n + m + 1; i++)
                if (function[i] == n)
                    return true;

            return false;
        }

        static void Main(string[] args) {
            StreamReader sr = new StreamReader("input_15_1.txt");

            int n = int.Parse(sr.ReadLine()); // number or relatives of the president
            string[] relatives = new string[n];

            for (int i = 0; i < n; i++) {
                relatives[i] = unifiq(sr.ReadLine()); // DNA of relatives of the president
            }

            string president = unifiq(sr.ReadLine()); // DNA of the president

            sr.Close();

            StreamWriter sw = new StreamWriter("output_15_1.txt");

            for (int i = 0; i < n; i++)
                sw.WriteLine(morrisonPratt(president, relatives[i]) ? "yes" : "no");
                //sw.WriteLine(naive(president, relatives[i]) ? "yes" : "no");

            sw.Close();
        }
    }
}
