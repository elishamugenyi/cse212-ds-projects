using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.
//implemented solution to the needed requirements.
namespace PriorityQueues {
    public class PriorityQueue
{
    private List<(int priority, string value)> queue = new List<(int, string)>();

    public void Enqueue(string value, int priority)
    {
        queue.Add((priority, value));
    }

    public string Dequeue()
    {
        if (queue.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        var highestPriorityItem = queue[0];
        int highestPriorityIndex = 0;

        for (int i = 1; i < queue.Count; i++)
        {
            if (queue[i].priority > highestPriorityItem.priority)
            {
                highestPriorityItem = queue[i];
                highestPriorityIndex = i;
            } 
            else if (queue[i].priority == highestPriorityItem.priority && i < highestPriorityIndex)
            {
                highestPriorityItem = queue[i];
                highestPriorityIndex = i;
            }
        }

        queue.RemoveAt(highestPriorityIndex);
        return highestPriorityItem.value;
    }

    public int Count => queue.Count;
}
[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities and then dequeue them.
    // Expected Result: Items should be dequeued in the order of their priority, with higher priority items dequeued first. If priorities are the same, the item added first should be dequeued first.
    // Defect(s) Found: The Dequeue method is not correctly identifying the highest priority item.
    public void TestPriorityQueue_EnqueueDequeue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 1);
        priorityQueue.Enqueue("medium", 2);
        priorityQueue.Enqueue("high", 3);

        Assert.AreEqual("high", priorityQueue.Dequeue());
        Assert.AreEqual("medium", priorityQueue.Dequeue());
        Assert.AreEqual("low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: An exception should be thrown with an appropriate error message.
    // Defect(s) Found: The exception message in the Dequeue method does not match the expected message.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("Queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same priority and then dequeue them.
    // Expected Result: Items with the same priority should be dequeued in the order they were added (FIFO).
    // Defect(s) Found: The Dequeue method is not maintaining the FIFO order for items with the same priority.
    public void TestPriorityQueue_SamePriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 1);
        priorityQueue.Enqueue("second", 1);
        priorityQueue.Enqueue("third", 1);

        Assert.AreEqual("first", priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());
        Assert.AreEqual("third", priorityQueue.Dequeue());
    }
}
}
