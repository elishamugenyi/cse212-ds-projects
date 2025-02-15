public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1
        //check if value already exists.
        if (value == Data)
        {
            return; //if value already exixts, do not insert
        }

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2
        //if current node's data matches the value, return true
        if (value == Data)
        {
            return true;
        }
        //if the value is less than the current node's branch, search the left subtree
        if (value < Data && Left != null) 
        {
            return Left.Contains(value);
        }
        //if value is greater than current node's data, search the right subtree
        if (value > Data && Right != null)
        {
            return Right.Contains(value);
        }
        //if value is not found, return false
        return false;
    }

    public int GetHeight()
    {
        // TODO Start Problem 4
        if (this == null)
        {
            return 0;// Replace this line with the correct return statement(s)
        }
        
        // Recursively calculate the height of the left subtree
        int leftHeight = (Left != null) ? Left.GetHeight() : 0;

        // Recursively calculate the height of the right subtree
        int rightHeight = (Right != null) ? Right.GetHeight() : 0;

        // Return the maximum height of the left or right subtree, plus 1 for the current node
        return Math.Max(leftHeight, rightHeight) + 1;
    }
}