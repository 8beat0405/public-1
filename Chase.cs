using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static readonly int[] dx = { 0, 0, 1, -1 };
    private static readonly int[] dy = { 1, -1, 0, 0 };

    public static void Main()
    {
        string[] field = GetStdin();
        int n = field.Length;
        int m = field[0].Length;

        (int, int) hunter = (0, 0);
        List<(int, int)> ghosts = new List<(int, int)>();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (field[i][j] == 'A')
                {
                    hunter = (i, j);
                }
                else if (field[i][j] == 'B')
                {
                    ghosts.Add((i, j));
                }
            }
        }

        int totalDistance = 0;

        while (ghosts.Count > 0)
        {
            int minDistance = int.MaxValue;
            (int, int) closestGhost = (0, 0);

            foreach (var ghost in ghosts)
            {
                int distance = BFS(field, hunter, ghost);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestGhost = ghost;
                }
            }

            totalDistance += minDistance;
            hunter = closestGhost;
            ghosts.Remove(closestGhost);
        }

        Console.WriteLine(totalDistance);
    }

    private static int BFS(string[] field, (int, int) start, (int, int) target)
    {
        int n = field.Length;
        int m = field[0].Length;

        bool[,] visited = new bool[n, m];
        Queue<((int, int) pos, int dist)> queue = new Queue<((int, int), int)>();

        queue.Enqueue((start, 0));
        visited[start.Item1, start.Item2] = true;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var pos = current.pos;
            var dist = current.dist;

            if (pos == target)
                return dist;

            for (int i = 0; i < 4; i++)
            {
                int newX = pos.Item1 + dx[i];
                int newY = pos.Item2 + dy[i];

                if (newX >= 0 && newX < n && newY >= 0 && newY < m && !visited[newX, newY])
                {
                    queue.Enqueue(((newX, newY), dist + 1));
                    visited[newX, newY] = true;
                }
            }
        }

        return -1;
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