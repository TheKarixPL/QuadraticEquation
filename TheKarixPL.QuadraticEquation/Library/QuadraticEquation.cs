using System.Numerics;

namespace TheKarixPL.QuadraticEquation.Library;

/// <summary>
/// Class representing quadratic equation
/// </summary>
public class QuadraticEquation
{
    /// <summary>
    /// Enum with forms of quadratic equation
    /// </summary>
    public enum Form
    {
        Standard,
        Vertex,
        Factored
    }
    
    public double A { get; }
    
    public double B { get; }
    
    public double C { get; }

    public double Delta => Math.Pow(B, 2) - 4 * A * C;

    public double P => -B / (2 * A);

    public double Q => -Delta / (4 * A);

    /// <summary>
    /// Roots of quadratic equation
    /// </summary>
    /// <exception cref="ComplexResultException">Thrown if roots are complex numbers</exception>
    public double[] Roots
    {
        get
        {
            if (Delta > 0)
            {
                var deltaRoot = Math.Sqrt(Delta);
                return new double[] { (-B - deltaRoot) / (2 * A), (-B + deltaRoot) / (2 * A) };
            }
            else if (Delta == 0)
            {
                return new double[] { P };
            }
            else
            {
                throw new ComplexResultException("Delta is negative");
            }
        }
    }
    
    /// <summary>
    /// Complex roots of quadratic equation
    /// </summary>
    public Complex[] ComplexRoots
    {
        get
        {
            if (Delta > 0)
            {
                var deltaRoot = Math.Sqrt(Delta);
                return new Complex[] { (-B - deltaRoot) / (2 * A), (-B + deltaRoot) / (2 * A) };
            }
            else if (Delta == 0)
            {
                return new Complex[] { P };
            }
            else
            {
                var deltaRoot = Complex.Sqrt(Delta);
                return new Complex[] { (-B - deltaRoot) / (2 * A), (-B + deltaRoot) / (2 * A) };
            }
        }
    }
    
    private QuadraticEquation(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }

    /// <summary>
    /// Create <c>QuadraticEquation</c> from standard form
    /// </summary>
    /// <param name="a">a</param>
    /// <param name="b">b</param>
    /// <param name="c">c</param>
    /// <returns><c>QuadraticEquation</c> instance</returns>
    /// <exception cref="InvalidQuadraticEquationException">Thrown if a is 0</exception>
    public static QuadraticEquation FromStandardForm(double a, double b, double c)
        => a != 0 ? new QuadraticEquation(a, b, c) : throw new InvalidQuadraticEquationException("a is 0");

    /// <summary>
    /// Create <c>QuadraticEquation</c> from vertex form
    /// </summary>
    /// <param name="a">a</param>
    /// <param name="p">p</param>
    /// <param name="q">q</param>
    /// <returns><c>QuadraticEquation</c> instance</returns>
    /// <exception cref="InvalidQuadraticEquationException">Thrown if a is 0</exception>
    public static QuadraticEquation FromVertexForm(double a, double p, double q)
        => a != 0 ? new QuadraticEquation(a, -2 * a * p, a * Math.Pow(p, 2) + q) : throw new InvalidQuadraticEquationException("a is 0");

    /// <summary>
    /// Create <c>QuadraticEquation</c> from factored form
    /// </summary>
    /// <param name="a">a</param>
    /// <param name="x1">First root</param>
    /// <param name="x2">Second root</param>
    /// <returns><c>QuadraticEquation</c> instance</returns>
    /// <exception cref="InvalidQuadraticEquationException">Thrown if a is 0</exception>
    public static QuadraticEquation FromFactoredForm(double a, double x1, double x2)
        => a != 0 ? new QuadraticEquation(a, -a * (x1 + x2), a * x1 * x2) : throw new InvalidQuadraticEquationException("a is 0");

    /// <summary>
    /// Get value for argument
    /// </summary>
    /// <param name="x">Function argument</param>
    /// <returns>Value for argument</returns>
    public double GetValue(double x)
        => A * Math.Pow(x, 2) + B * x + C;

    public override string ToString()
        => ToString(Form.Standard);

    /// <summary>
    /// Get string representation of specific form
    /// </summary>
    /// <param name="form">Form of quadratic equation</param>
    /// <returns>String representation of specific form</returns>
    public string ToString(Form form)
    {
        switch (form)
        {
            case Form.Standard:
                return $"{FormatNumber(A, false, false)}*x^2 {FormatNumber(B, true, true)}*x {FormatNumber(C, true, true)}";
            case Form.Vertex:
                return $"{FormatNumber(A, false, false)}*(x{FormatNumber(-B, false, true)}) {FormatNumber(C, true, true)}";
            case Form.Factored:
                try
                {
                    var roots = Roots;
                    if (roots.Length == 2)
                        return
                            $"{FormatNumber(A, false, false)} * (x{FormatNumber(-roots[0], false, true)}) * (x{FormatNumber(-roots[1], false, true)}";
                    else
                        return
                            $"{FormatNumber(A, false, false)} * (x{FormatNumber(-roots[0], false, true)}) * (x{FormatNumber(-roots[0], false, true)}";
                }
                catch (ComplexResultException e)
                {
                    return "Complex roots";
                }
            default:
                return "";
        }
    }

    private static string FormatNumber(double number, bool separated, bool showPlusSign)
    {
        if (number > 0)
            return $"{(showPlusSign ? "+" : "")}{(separated ? " " : "")}{Math.Abs(number)}";
        else if (number < 0)
            return $"-{(separated ? " " : "")}{Math.Abs(number)}";
        else
            return Math.Abs(number).ToString();
    }
}