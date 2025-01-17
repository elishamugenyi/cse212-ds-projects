/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Iniatlize with valid size for example (5)
        // Expected Result: [size=0, max_size=5] =>
        Console.WriteLine("Test 1: Iniatlize with valid size for example (5)");
        var cs1 = new CustomerService(5);
        Console.WriteLine(cs1);

        // Defect(s) Found: 

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Iniatilize with invalide size (default to 10)
        // Expected Result: [size=0, max_size=10]
        Console.WriteLine("Test 2: Iniatilize with invalide size (default to 10)");
        var cs2 = new CustomerService(-5);
        Console.WriteLine(cs2);
        // Defect(s) Found: 

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below
        // Test 3: Add customers within max size
    Console.WriteLine("\nTest 3: Add customers within max size");
    cs1.AddNewCustomer();
    cs1.AddNewCustomer();
    cs1.AddNewCustomer();
    Console.WriteLine(cs1); // Expected: [size=3, max_size=5] => ...

    // Test 4: Add customers exceeding max size
    Console.WriteLine("\nTest 4: Add customers exceeding max size");
    cs1.AddNewCustomer();
    cs1.AddNewCustomer();
    cs1.AddNewCustomer(); // Should display "Maximum Number of Customers in Queue."

    // Test 5: Serve customers from the queue
    Console.WriteLine("\nTest 5: Serve customers from the queue");
    cs1.ServeCustomer(); // Should display first customer details
    cs1.ServeCustomer(); // Should display second customer details
    Console.WriteLine(cs1); // Expected: Remaining customers

    // Test 6: Serve customers when the queue is empty
    Console.WriteLine("\nTest 6: Serve customers when the queue is empty");
    var cs3 = new CustomerService(3);
    cs3.ServeCustomer(); // Should display "No customers to serve."

    Console.WriteLine("\nAll test cases executed.");
    }

    //A private list to hold the customer queue.
    private readonly List<Customer> _queue = new();

    //The maximus allowed size of the customer queue.
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        //Validate the provided size: default to 10 if invalid
        _maxSize = maxSize <= 0 ? 10 : maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        //Prompt the user for customer details.
        Console.Write("Customer Name: ");
        var name = Console.ReadLine()?.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()?.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()?.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
        Console.WriteLine("Customer added successfully.");
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        if (_queue.Count == 0) {
            Console.WriteLine("No customers to serve.");
            return;
        }

        //Retrieve and display the first customer in the queue.
        var customer = _queue[0];
        Console.WriteLine("Serving customer: " + customer);

        //Remove the first customer in the queue.
        _queue.RemoveAt(0);
        /*_queue.RemoveAt(0);
        var customer = _queue[0];
        Console.WriteLine(customer);*/
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}