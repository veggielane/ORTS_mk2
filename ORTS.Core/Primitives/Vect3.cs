using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORTS.Core.Primitives
{
    public class Vect3 : IFormattable, IComparable, IComparable<Vect3>, IEquatable<Vect3>
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public Vect3()
        {

        }

        public Vect3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vect3 Zero
        {
            get { return new Vect3(); }
        }

        public double Length
        {
            get { return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)); }
        }

        public double LengthSquared
        {
            get {return Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2);}
        }

        public Vect3 Normal
        {
            get
            {
                if (this.Length == 0)
                    return Vect3.Zero;

                return this.Divide(this.Length);
            }
        }

        public Vect3 Add(Vect3 v)
        {
            return new Vect3(X + v.X, Y + v.Y, Z + v.Z);
        }

        public Vect3 Subtract(Vect3 v)
        {
            return new Vect3(X - v.X, Y - v.Y, Z - v.Z);
        }

        public Vect3 Multiply(double v)
        {
            return new Vect3(X * v, Y * v, Z * v);
        }

        public Vect3 Divide(double v)
        {
            return new Vect3(X / v, Y / v, Z / v);
        }

        public Vect3 CrossProduct(Vect3 v)
        {
            return CrossProduct(this, v);
        }

        public double DotProduct(Vect3 v)
        {
            return DotProduct(this, v);
        }

        public Vect3 Normalize()
        {
            return Normalize(this);
        }

        public double Distance(Vect3 v)
        {
            return Distance(this, v);
        }

        public double Angle(Vect3 v)
        {
            return Angle(this, v);
        }

        public Vect3 Max(Vect3 v)
        {
            return Max(this, v);
        }

        public Vect3 Min(Vect3 v)
        {
            return Min(this, v);
        }

        public Vect3 Interpolate(Vect3 other, double control)
        {
            return Interpolate(this, other, control);
        }

        public static Vect3 operator +(Vect3 v1, Vect3 v2)
        {
            return new Vect3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vect3 operator -(Vect3 v1, Vect3 v2)
        {
            return new Vect3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vect3 operator *(Vect3 v1, double v2)
        {
            return v1.Multiply(v2);
        }

        public static Vect3 operator /(Vect3 v1, double v2)
        {
            return v1.Divide(v2);
        }

        public static Vect3 operator +(Vect3 v1)
        {
            return new Vect3(+v1.X,+v1.Y,+v1.Z);
        }

        public static Vect3 operator -(Vect3 v1)
        {
            return new Vect3(-v1.X,-v1.Y,-v1.Z);
        }

        public static bool operator <(Vect3 v1, Vect3 v2)
        {
            return v1.Length < v2.Length;
        }

        public static bool operator <=(Vect3 v1, Vect3 v2)
        {
            return v1.Length <= v2.Length;
        }

        public static bool operator >(Vect3 v1, Vect3 v2)
        {
            return v1.Length > v2.Length;
        }

        public static bool operator >=(Vect3 v1, Vect3 v2)
        {
            return v1.Length >= v2.Length;
        }

        public static bool operator ==(Vect3 v1, Vect3 v2)
        {
            return((v1.X == v2.X) && (v1.Y == v2.Y) && (v1.Z == v2.Z));
        }

        public static bool operator !=(Vect3 v1, Vect3 v2)
        {
            return !(v1 == v2);
        }

        public static Vect3 CrossProduct(Vect3 v1, Vect3 v2)
        {
            return new Vect3(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
        }

        public static double DotProduct(Vect3 v1, Vect3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static Vect3 Normalize(Vect3 v1)
        {
            if (v1.Length == 0)
            {
                throw new DivideByZeroException();
            }
            else
            {
                return new Vect3(v1.X / v1.Length, v1.Y / v1.Length, v1.Z / v1.Length);
            }
        }

        public static double Distance(Vect3 v1, Vect3 v2)
        {
            return Math.Sqrt((v1.X - v2.X) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y) + (v1.Z - v2.Z) * (v1.Z - v2.Z));
        }

        public static double Angle(Vect3 v1, Vect3 v2)
        {
            return Math.Acos(Normalize(v1).DotProduct(Normalize(v2)));
        }

        public static Vect3 Max(Vect3 v1, Vect3 v2)
        {
            if (v1 >= v2) { return v1; }
            return v2;
        }

        public static Vect3 Min(Vect3 v1, Vect3 v2)
        {
            if (v1 <= v2) { return v1; }
            return v2;
        }

        public static Vect3 Interpolate(Vect3 v1, Vect3 v2, double control)
        {
            if (control > 1 || control < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                return new Vect3 (v1.X * (1 - control) + v2.X * control, v1.Y * (1 - control) + v2.Y * control, v1.Z * (1 - control) + v2.Z * control);
            }
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return (System.String.Format("Vector3({0},{1},{2})",X,Y,Z));
        }

        public int CompareTo(object obj)
        {
            if (obj is Vect3)
            {
                Vect3 otherVector = (Vect3)obj;
                if (this < otherVector) { return -1; }
                else if (this > otherVector) { return 1; }
                return 0;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public int CompareTo(Vect3 other)
        {
            if (this < other)
            {
                return -1;
            }
            else if (this > other)
            {
                return 1;
            }

            return 0;
        }

        public bool Equals(Vect3 other)
        {
            return this == other;
        }
    }
}
