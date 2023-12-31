using System;


namespace crud_op
{
    internal class Program
    {
       

        static void Main(string[] args)
        {

           List<string> tasks = new List<string>();  // Here we create a list to store tasks



            bool exit = false;

                while (!exit)
                {
                    Console.WriteLine("select the task");
                    Console.WriteLine("1. Create a task");
                    Console.WriteLine("2. Read tasks");
                    Console.WriteLine("3. Update a task");
                    Console.WriteLine("4. Delete a task");
                    Console.WriteLine("5. Exit");

                Console.Write("Enter your choice (1-5): ");
                    int choice = Convert.ToInt16(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter task title: ");

                            string title = Console.ReadLine();
                            tasks.Add(title);                             //we used Add inbuilt function to store the task in list
                            Console.WriteLine("Task created successfully!");
                            break;

                        case 2:
                            if (tasks.Count == 0)
                            {
                                Console.WriteLine("No tasks available.");
                            }
                            else
                            {
                                Console.WriteLine("List of tasks:");
                                foreach (var task in tasks)
                                {
                                    Console.WriteLine($"Title: {task}");
                                }
                            }
                            break;

                        case 3:
                            if (tasks.Count == 0)
                            {
                                Console.WriteLine("No tasks available to update.");
                            }
                            else
                            {
                                Console.WriteLine("List of tasks:");
                                for (int i = 0; i < tasks.Count; i++)
                                {
                                    Console.WriteLine($"{i+1}. Title: {tasks[i]}");
                                }

                                Console.Write("Enter the index of the task to update: ");
                                int update = Convert.ToInt32(Console.ReadLine());

                                if (update >= 0 && update < tasks.Count)
                                {
                                    Console.Write("Enter new task title: ");
                                    string newTitle = Console.ReadLine();
                                    tasks[update-1] = newTitle;
                                    Console.WriteLine("Task updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid index. Please enter a valid index.");
                                }
                            }
                            break;

                        case 4:
                            if (tasks.Count == 0)   // 
                            {
                                Console.WriteLine("No tasks available to delete.");
                            }
                            else
                            {
                                Console.WriteLine("List of tasks:");
                                for (int i = 0; i < tasks.Count; i++)
                                {
                                    Console.WriteLine($"{i+1}. Title: {tasks[i]}");
                                }

                                Console.Write("Enter the index of the task to delete: ");
                                int Delete = Convert.ToInt32(Console.ReadLine());

                                if (Delete >= 0 && Delete < tasks.Count)
                                {
                                    tasks.RemoveAt(Delete-1);                        // here we used RemoveAt() function to delete the task by providing index 
                                    Console.WriteLine("Task deleted successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid index. Please enter a valid index.");
                                }
                            }
                            break;

                        case 5:
                            exit = true;     // here we set  exit true so it will go out of the while loop.
                                              
                            Console.WriteLine("Exiting the application.");
                            break;

                        default:           
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                            break;
                    }
                }
            }
        

    
}
}