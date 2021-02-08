using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib
{
    public static class Contents
    {
        public static Dictionary<string, List<string>> StudentsGroups(string students)
        {
            var res = new Dictionary<string, List<string>>();
            foreach (var gr in students.Split(';'))
            {
                var splitted = gr.Split(new char[] { '-' }, (char)2);
                if (!res.ContainsKey(splitted[1].Trim(' ')))
                {
                    res[splitted[1].Trim(' ')] = new List<string>() { splitted[0].Trim(' ') };
                }
                else
                    res[splitted[1].Trim(' ')].Add(splitted[0].Trim(' '));
            }
            return res;
        }

        public static Dictionary<string, Dictionary<string, List<int>>> StudentsPoints(int[] points, string students)
        {
            var res = new Dictionary<string, Dictionary<string, List<int>>>();
            foreach (var group in StudentsGroups(students))
            {
                res[group.Key] = new Dictionary<string, List<int>>();
                foreach (var student in group.Value)
                {
                    res[group.Key][student] =
                        Enumerable.Range(0, points.Length).Select(x => RandomValue(points[x])).ToList();
                }
            }
            return res;
        }

        private static int RandomValue(int max)
        {
            var rand = new System.Random();
            switch(rand.Next(5))
            {
                case 0:
                    return Convert.ToInt32(Math.Ceiling(max * 0.7));
                case 1:
                    return Convert.ToInt32(Math.Ceiling(max * 0.9));
                case 2:
                case 3:
                case 4:
                    return max;
                default:
                    return 0;
            }
        }

        public static Dictionary<string, Dictionary<string, int>>
            SumPoints(Dictionary<string, Dictionary<string, List<int>>> studentPoints)
        {
            var res = new Dictionary<string, Dictionary<string, int>>();

            foreach (var group in studentPoints)
            {
                res[group.Key] = new Dictionary<string, int>();
                foreach (var student in group.Value)
                {
                    res[group.Key][student.Key] = student.Value.Sum();
                }
            }

            return res;
        }

        public static Dictionary<string, double>
            GroupAvg(Dictionary<string, Dictionary<string, int>> groups)
        {
            var res = new Dictionary<string, double>();

            foreach (var group in groups)
            {
                res[group.Key] = group.Value.Values.Average();
            }

            return res;
        }

        public static Dictionary<string, List<string>>
            PassedPerGroup(Dictionary<string, Dictionary<string, int>> studentPoints)
        {
            var res = new Dictionary<string, List<string>>();

            foreach (var group in studentPoints)
            {
                res[group.Key] = group.Value.Where(kv => kv.Value >= 60).Select(kv => kv.Key).ToList();
            }
            return res;
        }
    }
}
