using System.Diagnostics.SymbolStore;
using System.Reflection.Metadata.Ecma335;

namespace prac9;

public class Operation
{
    public string Trigger;
    public Func<double, double, double> Fn;
    public static List<Operation> List = new();

    public Operation(string trigger, Func<double, double, double> fn)
    {
        Trigger = trigger;
        Fn = fn;
        List.Add(this);
    }

    public static void CreateOperations()
    {
        double Sum(double x, double y)
        {
            return x + y;
        }
            
        double Minus(double x, double y)
        {
            return x - y;
        }
            
        double Multiply(double x, double y)
        {
            return x * y;
        }
            
        double Divide(double x, double y)
        {
            return x / y;
        }
            
        double Divide2(double x, double y)
        {
            return x % y;
        }
            
        double Degree(double x, double y)
        {
            return Math.Pow(x, y);
        }

        new Operation("+", Sum);
        new Operation("-", Minus);
        new Operation("*", Multiply);
        new Operation("/", Divide);
        new Operation("%", Divide2);
        new Operation("^", Degree);
    }
}