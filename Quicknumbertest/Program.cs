using System;

namespace Quicknumbertest // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DiceMechanics mechanics = new DiceMechanics();
            //mechanics.RollDistribution();
            //mechanics.OpposedRollDistribution();
            for (int i = 0; i < 10; i++)
            {
                mechanics.RollDistribution(10000, i);
            }
        }


    }

    

    public class DiceMechanics {
        public int RollDie(int maxVal, Random rng)
        {
            int result = rng.Next(maxVal)+1;
            return result;
        }
        public void OpposedRollDistribution() {
            DiceMechanics test = new DiceMechanics();

            Console.WriteLine("Opposed Trials. How many iterations?");
            int numTrials = int.Parse(Console.ReadLine());
            Console.WriteLine("Attacker's pool?");
            int attPool = int.Parse(Console.ReadLine());
            Console.WriteLine("Defender's pool?");
            int defPool = int.Parse(Console.ReadLine());
            int[] diffHolder = new int[20];
            foreach (int i in diffHolder)
            {
                diffHolder[i] = 0;
            }
            for (int i = 0; i < numTrials; i++)
            {
                int currVal = test.OpposedRoll(attPool, defPool);
                if (currVal < 20) { diffHolder[currVal]++; }

            }
            for (int i = 0; i < 20; i++)
            {
                double rate;
                rate = (double)diffHolder[i] / (double)numTrials;
                rate *= 100;
                rate = Math.Round(rate, 3);
                Console.WriteLine($"{i}: {diffHolder[i]}, with a proportion of {rate} % of the total.");
            }
            Console.ReadLine();
        }

        public void RollDistribution(int numTrials, int numDice)
        {
            /*Console.WriteLine("How many iterations?");
            int numTrials = int.Parse(Console.ReadLine());
            Console.WriteLine("How many dice in pool?");
            int numDice = int.Parse(Console.ReadLine());
            */
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
                    int currVal = this.RollAttribute(numDice, 8, 6, true);
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
                    double rate;
                    rate = (double)holder[i] / (double)numTrials;
                    rate *= 100;
                    Console.WriteLine($"{i}: {holder[i]}, with {rate}% of the trials.");
                }
                Console.ReadLine();
            }
        }
        public int OpposedRoll(int attPool, int defPool){
            int difference = 0;
            int attSucc = RollAttribute(attPool, 8, 6, true);
            int defSucc = RollAttribute(defPool, 8, 6, true);
            difference = attSucc-defSucc;
            if (difference < 0) { difference = 0;} 
            return difference;
}        
        public int RollAttribute(int numberOfDice, int diceSize, int successValue, bool quiet = false)
    {
        if (successValue >= diceSize)
            {
                Console.WriteLine("That's not right, but okay.");
            }
        Random random = new Random();
        int count = 0;
        for (int i = 0; i < numberOfDice; i++)
        {
            int roll = RollOneDie(diceSize);
                if (quiet == false) { Console.Write($"{roll} "); }
            if (roll == diceSize)
            {
                i -= 2;
            }
            if (roll >= successValue)
            {
                count++;

            }
        }
            if (quiet == false) { Console.WriteLine($"\n                                             Total return is: {count}"); }
        return count;

    }
        int RollOneDie(int dieSize)
        {
            Random r = new Random();
            return r.Next(dieSize)+1;
        }
        public int AttemptASpell(int attribute)
        {
            Random random = new Random();
            int SCALAR = 4;
            int DIESIZE = 8;
            int target = attribute * SCALAR;
            int currentTotal = 0;
            string choice = "t";
            int currentScore = 0;
            while (choice == "t")
            {

                if (choice == "t")
                {
                    for (int i = 0; i < currentTotal; i++)
                    {
                        Console.Write("|");
                    }
                    for (int i = 0; i < (target - currentTotal); i++)
                    { Console.Write("."); }
                    Console.WriteLine("X\n");
                    Console.WriteLine($"You cannot exceed {target}.\n Would you like to roll a die?\n t for yes, anything else for no.");
                    choice = Console.ReadLine();
                    int roll = RollOneDie(DIESIZE);
                    if (roll == DIESIZE) { currentTotal -= SCALAR; currentScore++; }
                    else if (roll >= 6)
                    {
                        currentScore++;
                    }
                    if(roll %2 == 1)
                    {
                        currentTotal += roll;
                    }
                    if (currentTotal < 0)
                    {
                        currentTotal = 0;
                    }
                    else if (currentTotal > target)
                    {
                        Console.WriteLine("You got greedy, as foul dabblers in the arcane often do.");
                        return 0;
                    }
                    else if (currentTotal == target)
                    {
                        Console.WriteLine("A masterful success.");
                        return currentTotal + 2;
                    }
                    Console.WriteLine($"Last roll was {roll}.");
                    Console.WriteLine($"Your score is {currentScore}.");


                }
            }
            return currentScore;
        }

        int DamageRoll(int damageDie, int numberOfDice)
        {int sum = 0;
            for (int i = 0; i< numberOfDice; i++)
            {
                sum += RollOneDie(damageDie);
            }
            return sum; }

    }
}