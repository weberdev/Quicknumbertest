using System;

namespace Quicknumbertest // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DiceMechanics test = new DiceMechanics();
            Console.WriteLine("How many iterations?");
            int numTrials = int.Parse(Console.ReadLine());
            if (numTrials > 0)
            {
                int total = 0;
                int highest = 0;
                int highestCount = 0;
                int[] holder = new int[20];
                foreach (int i in holder)
                {
                    holder[i] = 0;
                }
                for (int i = 0; i < numTrials; i++)
                {
                    int currVal = test.RollNDice(5, 8, 6);
                    if (currVal < 20) { holder[currVal]++; }

                    total += currVal;
                    if (currVal > highest)
                    {
                        highest = currVal;
                        highestCount = 1;
                    }
                    else if (currVal == highest) { highestCount++; }
                }
                double average = (double)total / (double)numTrials;
                Console.WriteLine($"Average result is {average}.");
                Console.WriteLine($"Highest result is {highest}, showed up {highestCount} times.");

                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine($"{i}: {holder[i]}");
                }
                Console.ReadLine();
            }
            Console.WriteLine("Opposed Trials. How many iterations?");
            numTrials = int.Parse(Console.ReadLine());
            Console.WriteLine("Attacker's pool?");
            int attPool = int.Parse(Console.ReadLine());
            Console.WriteLine("Defender's pool?");
            int defPool = int.Parse(Console.ReadLine());
            int[] diffHolder = new int[20];
            foreach (int i in diffHolder)
            {
                diffHolder[i] = 0;
            }
            for (int i = 0;i < numTrials; i++)
            {   
        int  currVal=test.OpposedRoll(attPool, defPool);
                if (currVal < 20) { diffHolder[currVal]++; }

            }
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"{i}: {diffHolder[i]}");
            }
            Console.ReadLine();
        }


    }

    public class DiceMechanics {

        public int OpposedRoll(int attPool, int defPool){
            int difference = 0;
            int attSucc = RollNDice(attPool, 8, 6, true);
            int defSucc = RollNDice(defPool, 8, 6, true);
            difference = defSucc - attSucc;
            if (difference < 0) { difference = 0;} 
            return difference;
}        
        public int RollNDice(int numberOfDice, int diceSize, int successValue, bool quiet = false)
    {
        if (successValue >= diceSize)
            {
                Console.WriteLine("That's not right, but okay.");
            }
        Random random = new Random();
        int count = 0;
        for (int i = 0; i < numberOfDice; i++)
        {
            int roll = random.Next(diceSize)+1;
                if (quiet == false) { Console.Write($"{roll} "); }
            if (roll == diceSize)
            {
                count++;
                i -= 2;
            }
            else if (roll >= successValue)
            {
                count++;

            }
        }
            if (quiet == false) { Console.WriteLine($"\n                                             Total return is: {count}"); }
        return count;

    } 
}
}