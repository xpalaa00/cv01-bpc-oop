using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cv01_bpc_oop
{
    public class Complex
    {
        public double Real;
        public double Imaginar;

        public Complex(double re = 0.0, double im = 0.0)
        {
            Real = re;
            Imaginar = im;
        }

        public static Complex operator +(Complex a, Complex b)
        {
            Complex output = new Complex();
            output.Real = a.Real + b.Real;
            output.Imaginar = a.Imaginar + b.Imaginar;
            return output;
        }

        public static Complex operator -(Complex a)
        {
            Complex output = new Complex();
            output.Real = -(a.Real);
            output.Imaginar = -(a.Imaginar);
            return output;
        }
        public static Complex operator -(Complex a, Complex b)
        {
            Complex output = new Complex();
            output.Real = a.Real - b.Real;
            output.Imaginar = a.Imaginar - b.Imaginar;
            return output;
        }
        public static Complex operator *(Complex a, Complex b)
        {
            Complex output = new Complex();
            output.Real = a.Real * b.Real - a.Imaginar * b.Imaginar;
            output.Imaginar = a.Real * b.Imaginar + a.Imaginar * b.Real;
            return output;
        }
        public static Complex operator /(Complex a, Complex b)
        {
            if (b.Real == 0 || b.Imaginar == 0)
            {
                throw new Exception("Dividing by 0!");
            }

            Complex output = new Complex();
            output.Real = (a.Real * b.Real + a.Imaginar * b.Imaginar) / (Math.Pow(b.Real, 2.0) + Math.Pow(b.Imaginar, 2.0));
            output.Imaginar = (a.Imaginar * b.Real - a.Real * b.Imaginar) / (Math.Pow(b.Real, 2.0) + Math.Pow(b.Imaginar, 2.0));
            return output;
        }
        public static bool operator ==(Complex a, Complex b)
        {
            return (a.Real == b.Real) && (a.Imaginar == b.Imaginar);
        }
        public static bool operator !=(Complex a, Complex b)
        {
            return (a.Real != b.Real) || (a.Imaginar != b.Imaginar);
        }
        public static Complex Conjg(Complex a)
        {
            Complex output = new Complex();
            output.Real = a.Real;
            output.Imaginar = -(a.Imaginar);
            return output;
        }
        public static double Mod(Complex a)
        {
            return Math.Sqrt(Math.Pow(a.Real, 2.0) + Math.Pow(a.Imaginar, 2.0));
        }
        public static double Arg(Complex a)
        {
            if (a.Real == 0.0)
            {
                if(a.Imaginar == 0.0)
                {
                    throw new Exception("Undefined argument of complex number");
                }
                else if(a.Imaginar > 0.0)
                {
                    //90° (pi/2 rad)
                    return Math.PI / 2.0;
                }
                else
                {
                    //270° (pi*3/2 rad)
                    return Math.PI * 3.0 / 2.0;
                }
            }
            else if(a.Real > 0.0)
            {
                if(a.Imaginar >= 0.0)
                {
                    //I. segment
                    return Math.Atan(a.Imaginar / a.Real);
                }
                else
                {
                    //IV. segment
                    return 2.0 * Math.PI + Math.Atan(a.Imaginar / a.Real);
                }
            }
            else
            {
                //II. and III. segment
                return Math.PI + Math.Atan(a.Imaginar / a.Real);
            }
        }
        public override string ToString()
        {
            if (Real == 0.0)
            {
                if (Imaginar == 0.0)
                {
                    return "0.0";
                }
                else if (Imaginar < 0.0)
                {
                    return "- j" + Math.Abs(Imaginar).ToString();
                }
                else
                {
                    return "j" + Imaginar.ToString();
                }
            }
            else if (Real < 0.0)
            {
                if (Imaginar == 0.0)
                {
                    return "- " + Math.Abs(Real).ToString();
                }
                else if (Imaginar < 0.0)
                {
                    return "- " + Math.Abs(Real).ToString() + " - j" + Math.Abs(Imaginar).ToString();
                }
                else
                {
                    return "- " + Math.Abs(Real).ToString() + " + j" + Imaginar.ToString();
                }
            }
            else
            {
                if (Imaginar == 0.0)
                {
                    return Real.ToString();
                }
                else if (Imaginar < 0.0)
                {
                    return Real.ToString() + " - j" + Math.Abs(Imaginar).ToString();
                }
                else
                {
                    return Real.ToString() + " + j" + Imaginar.ToString();
                }
            }
        }
    }

    public class TestComplex
    {
        public static void Test(Complex calculated, Complex expected, string testName = "")
        {
            const double epsilon = 1.0E-6;

            Complex delta = new Complex();
            delta = calculated - expected;

            if(Math.Abs(delta.Real) < epsilon && Math.Abs(delta.Imaginar) < epsilon)
            {
                Console.WriteLine("{0}: Test passed... Calc: {1}, Exp: {2}, Dif: {3}", testName, calculated.ToString(), expected.ToString(), delta.ToString());
            }
            else
            {
                Console.WriteLine("{0}: Test failed... Calc: {1}, Exp: {2}, Dif: {3}", testName, calculated.ToString(), expected.ToString(), delta.ToString());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Complex a = new Complex(4.8, -1.2);
            Complex b = new Complex(-4.4, 0.6);

            Console.WriteLine("{0} + {1} = {2}", a.ToString(), b.ToString(), (a + b).ToString());
            TestComplex.Test((a + b), new Complex(0.4, -0.6), "Sum");
            Console.WriteLine();

            Console.WriteLine("{0} - {1} = {2}", a.ToString(), b.ToString(), (a - b).ToString());
            TestComplex.Test((a - b), new Complex(9.2, -1.8), "Dif");
            Console.WriteLine();

            Console.WriteLine("{0} * {1} = {2}", a.ToString(), b.ToString(), (a * b).ToString());
            TestComplex.Test((a * b), new Complex(-20.4, 8.16), "Mul");
            Console.WriteLine();

            Console.WriteLine("{0} / {1} = {2}", a.ToString(), b.ToString(), (a / b).ToString());
            TestComplex.Test((a / b), new Complex(-1.107505071, 0.121703854), "Div");
            Console.WriteLine();

            Console.WriteLine("-( {0} ) = {1}", a.ToString(), (-a).ToString());
            TestComplex.Test((-a), new Complex(-4.8, 1.2), "Unary");
            Console.WriteLine();

            Console.WriteLine("Conjg( {0} ) = {1}", a.ToString(), Complex.Conjg(a).ToString());
            TestComplex.Test(Complex.Conjg(a), new Complex(4.8, 1.2), "Conjg");
            Console.WriteLine();

            Console.WriteLine("{0} == {1} = {2}", a.ToString(), b.ToString(), (a == b).ToString());
            Console.WriteLine();

            Console.WriteLine("{0} != {1} = {2}", a.ToString(), b.ToString(), (a != b).ToString());
            Console.WriteLine();

            Console.WriteLine("Arg( {0} ) = {1}", a.ToString(), Complex.Arg(a).ToString());
            Console.WriteLine();

            Console.WriteLine("Mod( {0} ) = {1}", a.ToString(), Complex.Mod(a).ToString());
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
