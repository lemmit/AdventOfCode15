using AdventOfCode.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode7
{

    enum Operator
    {
        WIRE,
        AND,
        NOT,
        RSHIFT,
        LSHIFT,
        OR
    }

    enum ExpressionType
    {
        TwoArgumentExpression,
        NotExpression,
        WireExpression
    }

    interface Operation
    {
        ushort Compute();
    }

    interface OneArgumentOperation : Operation
    {
        ushort Operand { get; set; }
    }

    interface TwoArgumentOperation : Operation
    {
        ushort FirstOperand { get; set; }
        ushort SecondOperand { get; set; }
    }

    class WireOperation : OneArgumentOperation
    {
        public ushort Operand
        {
            get; set;
        }

        public ushort Compute()
        {
            return Operand;
        }
    }

    class NotOperation : OneArgumentOperation
    {
        public ushort Operand { get; set; }
        public ushort Compute()
        {
            return (ushort)~Operand;
        }
    }

    class AndOperation : TwoArgumentOperation
    {
        public ushort FirstOperand { get; set; }
        public ushort SecondOperand { get; set; }

        public ushort Compute()
        {
            return (ushort)(FirstOperand & SecondOperand);
        }
    }

    class OrOperation : TwoArgumentOperation
    {
        public ushort FirstOperand { get; set; }
        public ushort SecondOperand { get; set; }

        public ushort Compute()
        {
            return (ushort)(FirstOperand | SecondOperand);
        }
    }

    class ShiftROperation : TwoArgumentOperation
    {
        public ushort FirstOperand { get; set; }
        public ushort SecondOperand { get; set; }

        public ushort Compute()
        {
            return (ushort)(FirstOperand >> SecondOperand);
        }
    }

    class ShiftLOperation : TwoArgumentOperation
    {
        public ushort FirstOperand { get; set; }
        public ushort SecondOperand { get; set; }

        public ushort Compute()
        {
            return (ushort)(FirstOperand << SecondOperand);
        }
    }

    public interface Expression
    {
        ushort Evaluate();
    }

    class OneArgumentExpression : Expression
    {
        OneArgumentOperation Op { get; set; }
        string Operand { get; set; }

        public OneArgumentExpression(OneArgumentOperation op, string operand)
        {
            Op = op;
            Operand = operand;
        }
        public ushort Evaluate()
        {
            ushort a;
            bool firstIsValue = ushort.TryParse(Operand, out a);
            if (!firstIsValue)
            {
                a = ExpressionTree.GetExpression(Operand).Evaluate();

                /*CACHE*/
                ExpressionTree.Instance[Operand] = new OneArgumentExpression(
                        (OneArgumentOperation)OperationFactory.Create(Operator.WIRE),
                        a.ToString()
                        );
            }
            else
            {
            }

            Op.Operand = a;
            var result = Op.Compute(); 
            //System.Console.WriteLine($"OneArgumentExpression: {Operand} -> {result}");
            return result;
        }
    }

    class TwoArgumentExpression : Expression
    {
        string FirstOperand { get; set; }
        string SecondOperand { get; set; }
        TwoArgumentOperation Op { get; set; }

        public TwoArgumentExpression(TwoArgumentOperation op, string firstOperand, string secondOperand)
        {
            Op = op;
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
        }

        public ushort Evaluate()
        {
            
            ushort a, b;
            bool firstIsValue = ushort.TryParse(FirstOperand, out a);
            bool secondIsValue = ushort.TryParse(SecondOperand, out b);
            if(!firstIsValue)
            {
                a = ExpressionTree.GetExpression(FirstOperand).Evaluate();

                /*CACHE*/
                ExpressionTree.Instance[FirstOperand] = new OneArgumentExpression(
                        (OneArgumentOperation)OperationFactory.Create(Operator.WIRE),
                        a.ToString()
                        );
            }
            else
            {
            }
            if(!secondIsValue)
            {
                b = ExpressionTree.GetExpression(SecondOperand).Evaluate();

                /*CACHE*/
                ExpressionTree.Instance[SecondOperand] = new OneArgumentExpression(
                   (OneArgumentOperation)OperationFactory.Create(Operator.WIRE),
                   b.ToString()
                   );
            }
            else
            {
            }

            Op.FirstOperand = a;
            Op.SecondOperand = b;
            var result = Op.Compute();
            //System.Console.WriteLine($"TwoArgumentsExpression: {FirstOperand}, {SecondOperand} -> {result}");
            return result;
        }
    }

    class OperationFactory
    {
        static public Operation Create(Operator op)
        {
            switch (op)
            {
                case Operator.AND:
                    return new AndOperation();
                case Operator.NOT:
                    return new NotOperation();
                case Operator.RSHIFT:
                    return new ShiftROperation();
                case Operator.LSHIFT:
                    return new ShiftLOperation();
                case Operator.OR:
                    return new OrOperation();
                default:
                    return new WireOperation();
            }
        }
    }

    public class ExpressionTree : Dictionary<string, Expression>
    {
        public static ExpressionTree Instance { get; } = new ExpressionTree();

        public static Expression GetExpression(string key)
        {
            var expr = Instance[key];
            System.Console.WriteLine($"{key}");
            return expr;
        } 
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var sr = new StreamReader("../../input.txt"))
            {
                sr.ForEachLine((line) => {
                    //try parse 2 argument operator
                    var twoArgOp = MatchTwoArgumentOperator(line);
                    var notOp = MatchNotOperator(line);
                    var wire = MatchWire(line);
                    if (twoArgOp != null)
                    {

                        ExpressionTree.Instance[twoArgOp.Item4] = new TwoArgumentExpression(
                                (TwoArgumentOperation)OperationFactory.Create(twoArgOp.Item3),
                                twoArgOp.Item1,
                                twoArgOp.Item2);
                        return;
                    }
                    if(notOp != null)
                    {
                        ExpressionTree.Instance[notOp.Item2] = new OneArgumentExpression(
                               (OneArgumentOperation)OperationFactory.Create(Operator.NOT),
                               notOp.Item1);
                        return;
                    }
                    if(wire != null)
                    {

                        ExpressionTree.Instance[wire.Item2] = new OneArgumentExpression(
                               (OneArgumentOperation)OperationFactory.Create(Operator.WIRE),
                               wire.Item1);
                        return;
                    }
                    
                    System.Console.WriteLine("NOT RECOGNIZED COMMAND");
                });

                //Override "b"
                ExpressionTree.Instance["b"] = new OneArgumentExpression(
                               (OneArgumentOperation)OperationFactory.Create(Operator.WIRE),
                               "16076");

                //evaluate tree
                var wires = ExpressionTree.Instance.Keys.ToList();
                wires.Sort();
                wires.ForEach((wire) =>
                {
                    System.Console.WriteLine($"{wire} : {ExpressionTree.Instance[wire].Evaluate() }");
                });
                System.Console.WriteLine($"RESULT: a -> {ExpressionTree.Instance["a"].Evaluate() }");
                Console.ReadLine();
            }
        }

        static Tuple<string,string> MatchWire(string txt)
        {
            string re1 = ".*?"; // Non-greedy match on filler
            string re2 = "((?:[a-z0-9]+))";    // Alphanum 1
            string re3 = ".*?"; // Non-greedy match on filler
            string re4 = "((?:[a-z0-9]+))";    // Alphanum 2

            Regex r = new Regex(re1 + re2 + re3 + re4, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String alphanum1 = m.Groups[1].ToString();
                String alphanum2 = m.Groups[2].ToString();
                return new Tuple<string, string>(alphanum1, alphanum2);
            }
            return null;
        }
        static Tuple<string, string> MatchNotOperator(string txt)
        {
            string re1 = "(NOT)";   // Word 1
            string re2 = ".*?"; // Non-greedy match on filler
            string re3 = "((?:[a-z0-9]+))";   // Word 2
            string re4 = ".*?"; // Non-greedy match on filler
            string re5 = "((?:[a-z0-9]+))";   // Word 3

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String word1 = m.Groups[1].ToString();
                String word2 = m.Groups[2].ToString();
                String word3 = m.Groups[3].ToString();
                return new Tuple<string, string>(word2, word3);
            }
            return null;
        }
        static Tuple<string, string, Operator, string> MatchTwoArgumentOperator(string txt)
        {
            string re1 = "((?:[a-z0-9]+))";   // Word 1
            string re2 = ".*?"; // Non-greedy match on filler
            
            var last = Enum.GetNames(typeof(Operator)).Last();
            var ands = "";
            Enum.GetNames(typeof(Operator))
                .ToList()
                .ForEach((enumName) => ands += enumName + "|");
            ands = ands.TrimEnd('|');
            string re3 = "("+ ands +")";
            string re4 = ".*?"; // Non-greedy match on filler
            string re5 = "((?:[a-z0-9]+))";   // Word 3
            string re6 = ".*?"; // Non-greedy match on filler
            string re7 = "((?:[a-z0-9]+))";   // Word 4

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String word1 = m.Groups[1].ToString();
                String word2 = m.Groups[2].ToString();
                String word3 = m.Groups[3].ToString();
                String word4 = m.Groups[4].ToString();
                return new Tuple<string, string, Operator, string>(
                    word1,
                    word3,
                    (Operator)Enum.Parse(typeof(Operator), word2),
                    word4
                    );
            }
            return null;
        }
    }
}
