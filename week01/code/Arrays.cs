using System.ComponentModel.DataAnnotations;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>

    
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        //My solution.
        //1. create array to store multiples
        double[] multiples = new double[length];

        //2. I will use for loop to generate multiples
        for(int i = 0; i<length; i++)
        {
            multiples[i] = number * (i + 1); //3. this calculates and stores multiples
        }

        return multiples; // 4. returns multiples.
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        //My solution
        //step 1: validate input
        if(amount < 1 || amount > data.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be in the range of 1 to data.Count.");
        }

        //step 2: Calculate Effective Rotation
        amount = amount % data.Count;

        //step 3: split the list
        List<int> rotatedList = new List<int>(data.Count);
        //last 'amount' elements
        rotatedList.AddRange(data.GetRange(data.Count - amount, amount));
      //remaining elements
        rotatedList.AddRange(data.GetRange(0, data.Count - amount));

        //step 4: modify original List
        for(int i = 0; i<data.Count; i++)
        {
            data[i] = rotatedList[i];
        }
    }
}
