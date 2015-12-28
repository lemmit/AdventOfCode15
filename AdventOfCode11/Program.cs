using System;
using AdventOfCode.Toolkit;
using System.Linq;

namespace AdventOfCode11
{
    class Program
    {
        static void Main(string[] args)
        {
            var lastPassword = "hxbxwxba";
            var passwordGenerator = new PasswordGenerator(
                    new SantaCorporatePasswordRuleChecker()
                );

            var password = passwordGenerator.NextPassword(lastPassword);
            System.Console.WriteLine($"First password: {password}");

            password = passwordGenerator.NextPassword(password);
            System.Console.WriteLine($"Second password: {password}");

            System.Console.ReadLine();
        }

    }
}
