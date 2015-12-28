using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode12
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var sw = new StreamReader("../../input.txt"))
            {
                var input = sw.ReadToEnd();
                var root = JObject.Parse(input);
                System.Console.WriteLine($"FirstPart: { FirstPart(input) }");
                System.Console.WriteLine($"SecondPart: { TraverseProperties(root) }");
                System.Console.WriteLine($"SecondPart 2nd way: { Traverse(root) }");
                System.Console.WriteLine($"SecondPart functional way: { TraverseFun(root) }");
            }
            System.Console.ReadLine();
        }

        static int FirstPart(string input)
        {
            var r = new Regex(@"[-\d]+");
            var matches = r.Matches(input);
            var sum = 0;
            foreach (var match in matches)
            {
                sum += int.Parse(match.ToString());
            }
            return sum;
        }

        #region Functional solution
        static bool IsPropertyOfTypeAndValue(JToken token, JTokenType type, string val)
        {
            return token.ToList().Any(subtoken => subtoken.Type == type 
                                      && subtoken.Value<string>() == val);
        }

        static bool HasPropertyWhere(JToken root, Func<JToken, bool> predicate)
        {
           return root.ToList().Any(token =>
                                    token.Type == JTokenType.Property
                                    && predicate(token));
        }

        static bool TokenContainsRedItem(JToken token)
        {
            return HasPropertyWhere(token, 
                    (property) => IsPropertyOfTypeAndValue(property, JTokenType.String, "red"));
        }

        static int TraverseFun(JToken root)
        {
            if (TokenContainsRedItem(root))
            {
                return 0;
            }

            return root.ToList().Sum((token) => {
                if(token.Type == JTokenType.Integer) { 
                    return token.Value<int>();
                }
                return TraverseFun(token);
            });
        }
        #endregion

        #region Standard traversal solution
        static int Traverse(JToken root)
        {
            
            var sum = 0;
            foreach (var token in root)
            {
                switch (token.Type)
                {
                    case JTokenType.Property:
                        if (token.ToList().Any(subtoken =>
                                    subtoken.Type == JTokenType.String
                                    && subtoken.Value<string>() == "red")
                                )
                        {
                            return 0;
                        }
                        break;
                    case JTokenType.Integer:
                        sum += token.Value<int>();
                        break;
                }
                sum += Traverse(token);
            }
            return sum;
        }
        #endregion

        #region Naive solution
        static int TraverseArray(JToken array)
        {
            int sum = 0;
            foreach (var item in array)
            {
                switch (item.Type)
                {
                    case JTokenType.Object:
                        sum += TraverseProperties(JObject.Parse(item.ToString()));
                        break;
                    case JTokenType.Array:
                        sum += TraverseArray(item);
                        break;
                    case JTokenType.Integer:
                        sum += item.Value<int>();
                        break;
                    case JTokenType.String:
                        break;
                }
            }
            return sum;
        }

        static int TraverseProperties(JObject root, int offset = 0)
        {
            int sum = 0;
            foreach (var token in root.Properties().Select(p => p.Value))
            {
                switch (token.Type)
                {
                    case JTokenType.Array:
                        sum += TraverseArray(token);
                        break;
                    case JTokenType.Integer:
                        sum += (int)token;
                        break;
                    case JTokenType.String:
                        if(token.ToString() == "red")
                        {
                            return 0;
                        }
                        break;
                    case JTokenType.Object:
                        JObject child = JObject.Parse(token.ToString());
                        sum += TraverseProperties(child, offset+1);
                        break;
                }
            }
            return sum;
        }
        #endregion

    }
} 
