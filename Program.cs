using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        var input = Console.ReadLine().Split(' ');
        var mapping = new Dictionary<int, string>();

        for (int i = 0; i < input.Length - 1; i++)
        {
            var parts = input[i].Split(':');

            // ここでa[i]のフォーマットをチェック
            if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
            {
                Console.WriteLine("invalid input");
                return;
            }
            
            if (parts[0].StartsWith("0"))
            {
                Console.WriteLine("invalid input");
                return;
            }
            
            if (!int.TryParse(parts[0], out int key))
            {
                Console.WriteLine("invalid input");
                return;
            }

            string value = parts[1];
            
            if (mapping.ContainsKey(key))
            {
                Console.WriteLine("invalid input");
                return;
            }
            
            mapping[key] = value;
        }

        if (input.Last().StartsWith("0"))
        {
            Console.WriteLine("invalid input");
            return;
        }

        if (!long.TryParse(input.Last(), out long n) || n < 1 || n > 10000000)
        {
            Console.WriteLine("invalid input");
            return;
        }

        if (!IsValidInput(mapping, (int)n))
        {
            Console.WriteLine("invalid input");
            return;
        }

        int maxDivisibleKey = mapping.Keys.Where(k => n % k == 0).DefaultIfEmpty(0).Max();

        if (maxDivisibleKey != 0)
        {
            Console.WriteLine(mapping[maxDivisibleKey]);
        }
        else
        {
            Console.WriteLine(n);
        }
    }

    private static bool IsValidInput(Dictionary<int, string> pairs, int n)
    {
        if (n < 1 || n > 10000000) return false;
        if (pairs.Count < 1 || pairs.Count > 5) return false;

        foreach (var pair in pairs)
        {
            if (pair.Key <= 0 || pair.Key > 100) return false;
            if (pair.Value.Length < 1 || pair.Value.Length > 49) return false;
            if (!Regex.IsMatch(pair.Value, @"^[\u0020-\u007E\u4E00-\u9FFF]+$")) return false;
        }

        return pairs.Keys.Distinct().Count() == pairs.Count;
    }
}
