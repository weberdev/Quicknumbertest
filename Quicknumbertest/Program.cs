using System;

namespace Quicknumbertest // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Main is updated regularly and largely exists to test functions in. 
            DiceMechanics mechanics = new DiceMechanics();
            //mechanics.RollDistribution();
            //mechanics.OpposedRollDistribution();
            for (int i = 0; i < 10; i++)
            {
                int sum = mechanics.BestOfNetHits(3);
                Console.WriteLine(sum);
            }
        }


    }

    

    public class DiceMechanics {

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
        //calls RollAttributes with an input of numDice numTrials times
        //prints out a list of the various values
        //anything over twenty is ignored for scaling reasons
        public void RollDistribution(int numTrials, int numDice)
        {
            /*Console.WriteLine("How many iterations?");
            int numTrials = int.Parse(Console.ReadLine());
            Console.WriteLine("How many dice in pool?");
            int numDice = int.Parse(Console.ReadLine());
            */
            if (numTrials > 0)
            {
                //total is the sum of all rolls, used for creating the average
                int total = 0;
                //highest is the highest result listed
                int highest = 0;
                //highestCount is the number o times the highest result appears
                int highestCount = 0;
                //An array to hold all the values for ease of printing
                //runtime is not important here
                int[] holder = new int[20];
                foreach (int i in holder)
                {
                    holder[i] = 0;
                }
                for (int i = 0; i < numTrials; i++)
                {
                    //sets currVall to a RollAttribute call
                    //assigns it to the matching holder index
                    int currVal = this.RollAttribute(numDice, 8, 6, true);
                    if (currVal < 20) { holder[currVal]++; }
                    //adds currVal to the total
                    total += currVal;
                    //if currVal exceeds the highest
                    //highest is now currVal, and the count is reset to one
                    if (currVal > highest)
                    {
                        highest = currVal;
                        highestCount = 1;
                    }
                    //if currVal is the highest, the count of the highest result increases
                    else if (currVal == highest) { highestCount++; }
                }
                //divides the sum of the values by the number of trials rolled
                double average = (double)total / (double)numTrials;
                Console.WriteLine($"Average result is {average}.");
                Console.WriteLine($"Highest result is {highest}, showed up {highestCount} times.");
                //prints frequencies
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
        //rolls a pool of numberOfDice size-sided dice
        // returns their sum
        public int SumNdice(int numberOfDice, int size)
        {
            int sum = 0;
            for(int i = 0; i < numberOfDice; i++)
            {
                sum += RollOneDie(size);
            }
            return sum;
        }
        //rolls netHits pools of netHits dice
        //returns the largest sum
        //right now this is d6 based
        //this will change
        public int BestOfNetHits(int netHits)
        {
            int sum = 0;
            for (int i = 0; i < netHits; i++)
            {
                int val = SumNdice(netHits, 6);
                if (sum < val)
                {
                    sum = val;
                }
            }
            return sum;
        }
        //generates two attribute pools
        //subtracts the defender's from the attackers.
        //if the difference is > 0, the attacker achieves that many net successes
        public int OpposedRoll(int attPool, int defPool){
            int difference = 0;
            int attSucc = RollAttribute(attPool, 8, 6, true);
            int defSucc = RollAttribute(defPool, 8, 6, true);
            difference = attSucc-defSucc;
            if (difference < 0) { difference = 0;} 
            return difference;
}        
        //this looks messy
        //takes in an integer numberOfDice representing the user's attribute
        //diceSize and successValue are largely there for future proofing
        //quiet is a test value
        //makes numberOfDice attempts at generating successes
        //in tabletop parlance, rolls numberOfDice
        //if the roll is equal to diceSize, a success is marked and the iterator decreases by two
        //in effect, rolling two additional dice
        //if the roll is greater than or equal to successValue but less than diceSize, a success is marked
        //otherwise, no successes are marked
        
        //THEORETICALLY I could implement a botching system on ones
        //I should do so
        //later
        
        public int RollAttribute(int numberOfDice, int diceSize, int successValue, bool quiet = false)
    {   //standard passive aggressive error handling
        //the way of the peaceful warrior
        if (successValue >= diceSize)
            {
                Console.WriteLine("That's not right, but okay.");
            }
        Random random = new Random();
        int numberOfSuccesses = 0;
        //start the rolling with a for loop
        //we've all seen these before
        //I have been informed that for loops are obsolete
        //press X to doubt
        for (int i = 0; i < numberOfDice; i++)
        {
            int roll = RollOneDie(diceSize);
                //quiet suppresses print statements for testing
                if (quiet == false) { Console.Write($"{roll} "); }
            //realizing now that this runs forever if you roll a one sided die
            //don't roll a one sided die
            //if you hit the maximum value on the die, you roll two more dice
            if (roll == diceSize)
            {
                i -= 2;
            }
            //if the roll meeds or exceeds the threshold, a success is logged
            //I should standardize these variable names
            //count could mean more than I want it to
            if (roll >= successValue)
            {
                numberOfSuccesses++;

            }
        }
            if (quiet == false) { Console.WriteLine($"\n                                             Total return is: {numberOfSuccesses}"); }
        return numberOfSuccesses;

    }
        //rolls a dieSize sided die
        int RollOneDie(int dieSize)
        {
            Random r = new Random();
            return r.Next(dieSize)+1;
        }

        //testing for spellcasting system
        //it's closer to blackjack than anything else
        //except with replacement
        //scaling needs work
        //the player is offered the option to roll a die
        //they want to log as many hits as they can without pushing their total over the target
        //player can quit at any time
        //if they exceed twenty, all is lost
        //not sure how to tempt hubris here
        //that is a major design goal
        public int AttemptASpell(int attribute)
        {   //initializes a random
            Random random = new Random();
            //using a multiplier of 4 right now
            int SCALAR = 4;
            //rolling d8s, as per usje
            int DIESIZE = 8;
            //the target is equal to the caster's attribute multiplied by the scalar.
            int target = attribute * SCALAR;
            //initializing the total power drawn to zero
            int currentTotal = 0;
            //this is here for console use
            string choice = "t";
            //initializing the caster's score at zero
            int currentScore = 0;
            while (choice == "t")
            {


                    //creates a cute little bar on the console showing how much power the caster has drawn.
                    for (int i = 0; i < currentTotal; i++)
                    {
                        Console.Write("|");
                    }
                    //finishes the bar
                    for (int i = 0; i < (target - currentTotal); i++)
                    { Console.Write("."); }
                    Console.WriteLine("X\n");
                    //prompts the player if they wish to continue casting
                    Console.WriteLine($"You cannot exceed {target}.\n Would you like to roll a die?\n t for yes, anything else for no.");
                    choice = Console.ReadLine();
                if (choice == "t")
                {   //rolls a die
                    //this may be familiar for those who've been paying attention
                    int roll = RollOneDie(DIESIZE);
                    //if the roll is equal to the maximum value, the player scores a point
                    //and has the scalar multiplier remove from their total
                    if (roll == DIESIZE) { currentTotal -= SCALAR; currentScore++; }
                    //if the die roll exceeds six, the player scores a point
                    else if (roll >= 6)
                    {
                        currentScore++;
                    }
                    //if the value is odd
                    //the total is increased by the die roll
                    if (roll % 2 == 1)
                    {
                        currentTotal += roll;
                    }
                    //if the total is negative at the end of the round, it isn't
                    //it's zero
                    //sshhhhh
                    if (currentTotal < 0)
                    {
                        currentTotal = 0;
                    }
                    //if the current total exceeds the target
                    //the spell fails
                    //no successes are logged
                    else if (currentTotal > target)
                    {
                        Console.WriteLine("You got greedy, as foul dabblers in the arcane often do.");
                        return 0;
                    }
                    //if the total exceeds the target, the spell succeeds masterfully
                    //two additional successes
                    //casting ends
                    //successes are returned
                    else if (currentTotal == target)
                    {
                        Console.WriteLine("A masterful success.");
                        return currentTotal + 2;
                    }
                    Console.WriteLine($"Last roll was {roll}.");
                    Console.WriteLine($"Your score is {currentScore}.");
                }

                }
            
            Console.WriteLine($"Your score is {currentScore}.");
            return currentScore;
        }

    }
}