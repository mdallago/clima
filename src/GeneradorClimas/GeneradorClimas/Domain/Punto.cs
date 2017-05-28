namespace GeneradorClimas.Domain
{
    public class Punto
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Punto(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X:{X} Y:{Y}";
        }

        public static bool operator ==(Punto lhs, Punto rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (((object)lhs == null) || ((object)rhs == null))
            {
                return false;
            }

            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }

        public static bool operator !=(Punto lhs, Punto rhs)
        {
            return !(lhs == rhs);
        }


        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Punto p = obj as Punto;
            if ((System.Object)p == null)
            {
                return false;
            }

            return X == p.X && Y == p.Y;
        }

        public bool Equals(Punto p)
        {
            if ((object)p == null)
            {
                return false;
            }

            return X == p.X && Y == p.Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}