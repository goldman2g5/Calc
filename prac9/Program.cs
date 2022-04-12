using System.Globalization;
using prac9;

public class Program
{
    private static void Main(string[] args)
        {
            Operation.CreateOperations();

            void Func(ref List<string> strLs, string value = "null")
            {
                while (true)
                {
                    Console.Clear();
                    Console.Write($"Input expression\n >> {(value == "null" ? "" : value)} ");
                    string input = Console.ReadLine().ToLower();

                    switch (input)
                    {
                        case "":
                            Func(ref strLs, value);
                            break;
                        
                        case "exit":
                            strLs.Add("exit");
                            break;

                        case "clear":
                            strLs.Add("clear");
                            value = "null";
                            continue;

                        case "sqrt":
                            if (value == "null")
                            {
                                Console.WriteLine("Input string was not in a correct format\nType 'clear' to work with a new number\nPress any key");
                                strLs.Add("Input string was not in a correct format.");
                                Console.ReadKey();
                                Func(ref strLs, value);
                            }

                            double valuesqrt = Math.Sqrt(Convert.ToDouble(value));
                            strLs.Add($"sqrt of {value} = {valuesqrt.ToString()}");
                            value = Math.Round(valuesqrt, 2).ToString();
                            continue;

                        default:
                            try
                            {
                                if (value != "null" & !Operation.List.Select(x => x.Trigger).ToList().Contains(input[0].ToString()))
                                {
                                    Console.WriteLine("Input string was not in a correct format\nType 'clear' to work with a new number\nPress any key");
                                    strLs.Add("Input string was not in a correct format.");
                                    Console.ReadKey();
                                    Func(ref strLs, value);
                                }

                                if (value != "null")
                                {
                                    input = $"{value}" + input;
                                }

                                var queue = new Queue<string>();
                                string temp = "";
                                foreach (string i in input.ToCharArray().Select(x => x.ToString()).Where(x => x != " ").ToList())
                                {
                                    if (Operation.List.Select(x => x.Trigger).ToList().Contains(i))
                                    {
                                        queue.Enqueue(temp);
                                        queue.Enqueue(i);
                                        temp = "";
                                        continue;
                                    }

                                    temp += i;
                                }

                                queue.Enqueue(temp);

                                double x = Convert.ToDouble(queue.Dequeue());
                                while (queue.Count != 0)
                                {
                                    string t = queue.Dequeue();
                                    x = Operation.List.Where(x => x.Trigger == t).ToList()[0].Fn(x, Convert.ToDouble(queue.Dequeue()));
                                }

                                strLs.Add($"{input} = {value}");
                                Func(ref strLs, Math.Round(x, 2).ToString());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"{e.Message}\nPress any key");
                                strLs.Add($"{e.Message}");
                                Console.ReadKey();
                                Func(ref strLs);
                            }

                            break;
                    }

                    break;
                }
            }

            void Log()
            {
                try
                {
                    List<string> ls = new List<string>();
                    Func(ref ls);
                    string path = @$"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\\{DateTime.Now.ToLongDateString()}.txt";
                    if (!File.Exists(path))
                    {
                        File.CreateText(path).Close();
                    }
                    using StreamWriter w = File.AppendText(path);
                    w.WriteLine($"{DateTime.Now.ToLongTimeString()}");
                    foreach (string i in ls)
                    {
                        w.WriteLine(i);   
                    }
                    w.WriteLine("");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}\nPress any key to retry");
                    Console.ReadKey();
                    Log();
                }
            }
            
            Log();
        }
        
    }