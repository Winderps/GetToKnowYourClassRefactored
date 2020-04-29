using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace GetToKnowYourClass2
{
    class Program
    {
        static List<StudentInfo> ClassInfo;
        static void Main(string[] args)
        {
            ClassInfo = new List<StudentInfo>()
            {
                //            Name            Hometown       Favorite food             Favorite band
                new StudentInfo("Mike Reardon", "Wyoming, MI", "Homemade Mac n' Cheese", "Bayside"),
                new StudentInfo("Jim Dickens", "Letterkenny, Ontario", "Steak", "Queen"),
                new StudentInfo("Jared Keeso", "Letterkenny, Ontario", "Fresh Produce", "Aerosmith"),
                new StudentInfo("Squirrely Dan", "New York, NY", "Mashed Potatoes n' Gravy", "Smashing Pumpkins"),
                new StudentInfo("Tyler Johnston", "Wyoming, MI", "Lasagna", "Black Sabbath"),
                new StudentInfo("Leroy Jethro Gibbs", "Washington DC", "Taco Casserole", "Def Leppard"),
                new StudentInfo("Donald Mallard", "Centralia, PN", "Burgers", "Lynyrd Skynyrd"),
                new StudentInfo("Timothy McGee", "Colma, CA", "Omelettes", "Ozzy Osbourne"),
                new StudentInfo("Anthony DiNozzo", "Roswell, NM", "Pizza", "The Who"),
                new StudentInfo("Abby Sciuto", "Hildale, UT", "Beef Stew", "Kid Rock"),
                new StudentInfo("Nathan Dales", "Miracle Village, FL", "Black Bean Soup", "AC/DC"),
                new StudentInfo("Dylan Playfair", "Coffee Springs, AL", "Chocolate Cake", "Bon Jovi"),
                new StudentInfo("Andrew Herr", "Why, AZ", "Grilled Cheese", "Survivor"),
                new StudentInfo("Jacob Tierney", "Yolo, CA", "Crab Rangoon", "Journey"),
                new StudentInfo("Nicholas Torres", "Hel, MI", "Orange Chicken", "Scorpions"),
                new StudentInfo("Jenny Shepard", "Slaughter Beach, DL", "Sushi", "Earth Wind & Fire"),
                new StudentInfo("Caitlin Todd", "Hazardville, CT", "Fried Fish", "Led Zeppelin"),
                new StudentInfo("Eleanor Bishop", "Last Chance, CO", "Clam Chowder", "Guns n' Roses"),
                new StudentInfo("Leon Vance", "Bliss Corner, MA", "Mushroom Chicken", "The Beatles"),
                new StudentInfo("Mark Forward", "Tinkerville, NH", "Roast Beef", "The Rolling Stones"),
                new StudentInfo("Jacqueline Sloane", "Truth or Consequences, NM", "Kielbasa", "Foghat")
            };

            ClassInfo = ClassInfo.OrderByDescending(x => x.Name).ToList();

            Console.Title = "Classmate Information App";
            Console.WriteLine("Welcome to the Class Information Database");

            bool cont = true;
            while (cont)
            {
                while (true)
                {
                    Console.Write("Please type add to add a student to the database or fetch to get information about an existing student: ");
                    string mode = Console.ReadLine().ToLower().Trim();
                    if (mode.Equals("add"))
                    {
                        string name = StudentInfoInput("Please enter a name: ");
                        string hometown = StudentInfoInput($"Please enter {name + (name.EndsWith('s') ? "'" : "'s")} hometown: ");
                        string food = StudentInfoInput($"Please enter {name + (name.EndsWith('s') ? "'" : "'s")} favorite food: ");
                        string band = StudentInfoInput($"Please enter {name + (name.EndsWith('s') ? "'" : "'s")} favorite band: ");
                        ClassInfo.Add(new StudentInfo(name, hometown, food, band));
                        ClassInfo = ClassInfo.OrderBy(x => x.Name).ToList();
                        int i = 0;
                        Console.WriteLine("Current class roster:");
                        ClassInfo.ForEach(x => Console.WriteLine($"{++i}. {x.Name}"));
                        break;
                    }
                    else if (mode.Equals("fetch"))
                    {
                        LookupStudent();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("I didn't recognize that! Try again!");
                        continue;
                    }
                }

                Console.Write("Enter y(es) to continue or anything else to exit: ");
                cont = Console.ReadLine().ToLower().StartsWith('y');
            }
        }

        private static void LookupStudent()
        {
            Console.Write($"Please enter the name or student number of someone you would like to look up (1-{ClassInfo.Count}): ");
            string input = Console.ReadLine().ToLower().Trim();
            StudentInfo studentInfo = null;
            bool validInput = false;
            while (!validInput)
            {
                try
                {
                    try
                    {
                        int studentId = int.Parse(input);
                        studentInfo = GetStudentInfo(studentId - 1);
                        validInput = true;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("That student does not exist!");
                    }
                }
                catch (FormatException)
                {
                    List<StudentInfo> possibleResults = GetStudentInfo(input);
                    if (possibleResults.Equals(null))
                    {
                        Console.WriteLine("I couldn't find any students with that name");
                    }
                    else
                    {
                        if (possibleResults.Count > 1)
                        {
                            bool validInput2 = false;
                            while (!validInput2)
                            {
                                try
                                {
                                    Console.WriteLine("Multiple matches found!");
                                    for (int i = 0; i < possibleResults.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {possibleResults[i].Name}");
                                    }
                                    Console.Write("Please enter the number of the student you want: ");
                                    studentInfo = possibleResults[int.Parse(Console.ReadLine()) - 1];
                                    validInput2 = true;
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    Console.WriteLine("That was too high of a number!");
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("That wasn't a number at all!");
                                }
                            }
                        }
                        else
                        {
                            studentInfo = possibleResults[0];
                        }
                        validInput = true;
                    }
                }
            }
            bool validInput3 = false;
            while (!validInput3)
            {
                Console.Write($"What information would you like on {studentInfo.Name}? Please enter hometown, food, band, or all: ");

                switch (Console.ReadLine().ToLower())
                {
                    case "hometown":
                        validInput3 = true;
                        Console.WriteLine($"{studentInfo.Name} is from {studentInfo.Hometown}");
                        break;
                    case "food":
                        validInput3 = true;
                        Console.WriteLine($"{studentInfo.Name} likes to eat {studentInfo.FavoriteFood}");
                        break;
                    case "band":
                        validInput3 = true;
                        Console.WriteLine($"{studentInfo.Name} listens to {studentInfo.FavoriteBand}");
                        break;
                    case "all":
                        Console.WriteLine($"{studentInfo.Name} is from {studentInfo.Hometown}, they like to eat {studentInfo.FavoriteFood} and listen to {studentInfo.FavoriteBand}");
                        validInput3 = true;
                        break;
                    default:
                        Console.WriteLine("Oops! I couldn't understand that, try again!");
                        break;
                }
            }
        }

        static string StudentInfoInput(string prompt)
        {
            string ret = "";
            while (ret.Equals(""))
            {
                Console.Write(prompt);
                ret = Console.ReadLine();

                if (ret.Equals(""))
                {
                    Console.WriteLine("Input cannot be empty! Please try again.");
                }
            }

            return ret;
        }
        static StudentInfo GetStudentInfo(int id)
        {
            return ClassInfo[id];
        }
        static List<StudentInfo> GetStudentInfo(string name)
        {
            List<StudentInfo> ret = ClassInfo.Where(x => x.Name.ToLower().StartsWith(name)).ToList();
            if (ret.Count == 0)
                return null;
            return ret;
        }
    }
}
