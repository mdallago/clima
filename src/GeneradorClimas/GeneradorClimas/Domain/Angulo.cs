using System;

namespace GeneradorClimas.Domain
{
    public class Angulo
    {
        public double Grados { get; private set; }
        public double Radianes { get; private set; }
        public double Inverso { get; private set; }

        public Angulo(double grados)
        {
            Grados = grados;
            ActualizarRadianes();
            ActualizarInverso();
        }

        private void ActualizarRadianes()
        {
            Radianes = Math.PI * Grados / 180.0;
        }

        private void ActualizarInverso()
        {
            Inverso = (Grados + 180.0) % 360;
        }

        public void Increment(double inc)
        {
            Grados += inc;
            if (Grados > 360)
                Grados -= 360;
            else if (Grados < 0)
                Grados += 360;

            ActualizarRadianes();
            ActualizarInverso();
        }

        public void Decrement(double dec)
        {
            Increment(-dec);
        }
        public override string ToString()
        {
            return Grados.ToString();
        }

        public override int GetHashCode()
        {
            return Grados.GetHashCode();
        }
        public static implicit operator double(Angulo angleObj)
        {
            return angleObj.Grados;
        }
        public static implicit operator Angulo(double _angle)
        {
            return new Angulo(_angle);
        }
        public static Angulo operator +(Angulo lhs, Angulo rhs)
        {
            Angulo angle = new Angulo(lhs.Grados);
            angle.Increment(rhs.Grados);
            return angle;
        }
        public static Angulo operator -(Angulo lhs, Angulo rhs)
        {
            Angulo angle = new Angulo(lhs.Grados);
            angle.Decrement(rhs.Grados);
            return angle;
        }
        public static bool operator <(Angulo lhs, Angulo rhs)
        {
            if (lhs.Grados < rhs.Grados)
                return true;
            return false;
        }
        public static bool operator <=(Angulo lhs, Angulo rhs)
        {
            if (lhs.Grados <= rhs.Grados)
                return true;
            return false;
        }
        public static bool operator >(Angulo lhs, Angulo rhs)
        {
            if (lhs.Grados > rhs.Grados)
                return true;
            return false;
        }
        public static bool operator >=(Angulo lhs, Angulo rhs)
        {
            if (lhs.Grados >= rhs.Grados)
                return true;
            return false;
        }

        public static bool operator ==(Angulo lhs, Angulo rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (((object)lhs == null) || ((object)rhs == null))
            {
                return false;
            }

            return lhs.Grados == rhs.Grados;
        }

        public static bool operator !=(Angulo lhs, Angulo rhs)
        {
            return !(lhs == rhs);
        }


        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var p = obj as Angulo;
            if ((Object)p == null)
            {
                return false;
            }

            return (Grados == p.Grados);
        }

        public bool Equals(Angulo p)
        {
            if ((object)p == null)
            {
                return false;
            }

            return (Grados == p.Grados);
        }

    }
}