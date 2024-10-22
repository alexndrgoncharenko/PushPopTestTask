using PushPopTestTask;

class Program
{
    static void Main(string[] args)
    {
        int[] values = { 10, 5, 20, 3, 7 };
        ProcessOperations(values, new QuickPopDataStructure<int>(), new QuickPushDataStructure<int>());

        Person[] persons = {
            new("SpongeBob", 30),
            new("Patrick", 35),
            new("Squidward", 40),
            new("Mr. Krabs", 55),
            new("Sandy", 28)
        };
        ProcessOperations(persons, new QuickPopDataStructure<Person>(), new QuickPushDataStructure<Person>());
    }

    static void ProcessOperations<T>(T[] values, QuickPopDataStructure<T> quickPopDS, QuickPushDataStructure<T> quickPushDS) where T : IComparable<T>
    {
        SemaphoreSlim semaphore = new SemaphoreSlim(0, 1);

        RunPushPopTasks(values, quickPopDS, "QuickPop", semaphore);

        RunPushPopTasks(values, quickPushDS, "QuickPush", semaphore);
    }

    static void RunPushPopTasks<T>(T[] values, dynamic dataStructure, string structureName, SemaphoreSlim semaphore)
    {
        Task[] tasks = new Task[2];

        tasks[0] = Task.Run(() =>
        {
            foreach (var value in values)
            {
                dataStructure.Push(value);
                Console.WriteLine($"{structureName} - Pushed {value}");
            }
            semaphore.Release();
        });

        tasks[1] = Task.Run(() =>
        {
            semaphore.Wait();
            foreach (var value in values)
            {
                var popped = dataStructure.Pop();
                Console.WriteLine($"{structureName} - Popped {popped}");
            }
        });

        Task.WaitAll(tasks);
    }
}