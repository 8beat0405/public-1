using System;
using System.Collections.Generic;

public class Restraunt
{
    public static void Main()
    {
        int step = int.Parse(Console.ReadLine());
        if (step != 3) return;

        int M = int.Parse(Console.ReadLine());

        Queue<Tuple<int, int>> orders = new Queue<Tuple<int, int>>();
        Dictionary<int, Queue<Tuple<int, int>>> cookingDishes = new Dictionary<int, Queue<Tuple<int, int>>>();

        string input;
        while (!string.IsNullOrEmpty(input = Console.ReadLine()))
        {
            string[] parts = input.Split(' ');

            if (parts[0] == "received")
            {
                int seatNumber = int.Parse(parts[2]);
                int dishNumber = int.Parse(parts[3]);

                // Add order to the queue
                orders.Enqueue(new Tuple<int, int>(seatNumber, dishNumber));

                if (!cookingDishes.ContainsKey(dishNumber))
                {
                    cookingDishes[dishNumber] = new Queue<Tuple<int, int>>();
                }
                cookingDishes[dishNumber].Enqueue(new Tuple<int, int>(seatNumber, dishNumber));
            }
            else if (parts[0] == "complete")
            {
                int dishNumber = int.Parse(parts[1]);

                if (cookingDishes.ContainsKey(dishNumber) && cookingDishes[dishNumber].Count > 0)
                {
                    var order = cookingDishes[dishNumber].Dequeue();
                    Console.WriteLine($"ready {order.Item1} {order.Item2}");
                }
                else
                {
                    Console.WriteLine("unexpected input");
                }
            }
        }
    }
}