using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics; // 追加

public class MainApp
{
    static public void Main(string[] args)
    {
        var lines = GetStdin();

        // 入力を受け取る
        var lr = lines[0].Split().Select(BigInteger.Parse).ToArray(); // 変更
        BigInteger l = lr[0], r = lr[1]; // 変更
        int m = int.Parse(lines[1]);
        var ns = lines[2].Split().Select(BigInteger.Parse).ToList(); // 変更

        // レベルn デスで倒せるモンスターの数を計算
        BigInteger totalMonsters = r - l + 1; // 変更
        BigInteger defeatedMonsters = 0; // 変更

        for (int i = 1; i <= m; i++)
        {
            var combinations = GetCombinations(ns, i);

            foreach (var combination in combinations)
            {
                BigInteger lcmValue = combination.Aggregate((a, b) => LCM(a, b)); // 変更
                BigInteger count = (r / lcmValue) - ((l - 1) / lcmValue); // 変更

                if (i % 2 == 1)
                {
                    defeatedMonsters += count;
                }
                else
                {
                    defeatedMonsters -= count;
                }
            }
        }

        // 結果を出力
        Console.WriteLine(totalMonsters - defeatedMonsters);
    }

    // 組み合わせを取得
    static IEnumerable<IEnumerable<BigInteger>> GetCombinations(List<BigInteger> list, int length) // 変更
    {
        if (length == 1) return list.Select(t => new BigInteger[] { t });
        return GetCombinations(list, length - 1)
            .SelectMany(t => list.Where(o => o > t.Last()),
                (t1, t2) => t1.Concat(new BigInteger[] { t2 })); // 変更
    }

    // 最大公約数を計算
    static BigInteger GCD(BigInteger a, BigInteger b) // 変更
    {
        while (b != 0)
        {
            BigInteger temp = a % b; // 変更
            a = b;
            b = temp;
        }
        return a;
    }

    // 最小公倍数を計算
    static BigInteger LCM(BigInteger a, BigInteger b) // 変更
    {
        BigInteger gcd = GCD(a, b); // 変更
        return (a / gcd) * b;
    }

    static private string[] GetStdin()
    {
        var list = new List<string>();
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            list.Add(line);
        }
        return list.ToArray();
    }
}