using System;
using System.IO;
using System.Net.NetworkInformation;

class Program
{

    //public static string[] lines = {};
    public static List<string> lines = new List<string>();
    public static bool quit = false;

    static int exts = 2;
    public static string[] extensions = {
        ".txt",
        ".nc",
        ".cs",
        ".py"
    };

    public static bool confirm(string message, string confirms, string cancels)
    {
        if (confirms.Trim() == "" || cancels.Trim() == "") return false;
        string[] cons = confirms.Split(" ");
        string[] cans = cancels.Split(" ");

        Console.WriteLine(message + "(y/n)");
        bool confirming = true;
        while (confirming)
        {
            string reply = Console.ReadLine();
            foreach (string con in cons)
            {
                if (reply == con) return true;
            }

            foreach (string can in cans)
            {
                if (reply == can) return false;
            }
        }
        return false;
    }

    public static string choosefromlist(string[] choosefrom, string def)
    {
        Console.WriteLine("Please type a number or the option to select one");
        bool choosing = true;
        while (choosing)
        {
            for (int i = 0; i < choosefrom.Length; i++) Console.WriteLine((i + 1).ToString() + ": " + choosefrom[i]);
            string reply = Console.ReadLine();
            for (int i = 0; i < choosefrom.Length; i++)
            {
                string option = choosefrom[i];
                if (reply == (i + 1).ToString() || reply == option) return option;
            }
        }
        return def;
    }

    static void Main()
    {
        Console.WriteLine("Welcome to Nuclear Pasta's file writer!");
        Console.WriteLine("Type anything to add it to the list of lines");
        Console.WriteLine("Type \"--show\" to show all the lines you've typed");
        Console.WriteLine("Type \"--save [filename]\" all the lines you've typed to a file");

        bool noextension = true;

        while (!quit)
        {
            string input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("Input is null");
                return;
            }

            if (!input.StartsWith("--save") && !input.StartsWith("--show")) lines.Add(input);

            if (input.StartsWith("--show"))
            {
                for (int i = 0; i < lines.Count; i++) Console.WriteLine((i + 1).ToString() + ": " + lines[i]);
            }

            if (input.StartsWith("--save "))
            {
                string[] inp = input.Split(' ');
                if (inp.Length <= 1)
                {
                    Console.WriteLine("Could not save file, no file name given");
                    continue;
                }
                if (inp[1].Trim() == "")
                {
                    Console.WriteLine("Could not save file, no file name given");
                    continue;
                }

                foreach (string ext in extensions)
                {
                    if (inp[1].EndsWith(ext)) noextension = false;
                }

                if (noextension)
                {
                    bool saidyes = confirm("file name does not have file extension, would you like to add one?", "y yes sure", "n no nah");
                    if (saidyes) inp[1] = inp[1] + choosefromlist(extensions, ".txt");
                    else continue;
                    //Convert.ToInt32(inp[1]);
                }


                // Set a variable to the Documents path.
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Write the string array to a new file named what the user gave
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, inp[1])))
                {
                    foreach (string line in lines.ToArray()) outputFile.WriteLine(line);
                }
                return;
            }
        }
    }
}