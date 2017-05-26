using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GeneradorClimas
{
    class Program
    {
        static void Main(string[] args)
        {
            const int CANTIDAD_DIAS_AÑO = 365;
            const int CANTIDAD_AÑOS = 1;

            XmlConfigurator.ConfigureAndWatch(
                new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\log4net.config"));

            SistemaSolar sistemasSolar = new SistemaSolar();

            for (int i = 0; i < CANTIDAD_AÑOS * CANTIDAD_DIAS_AÑO; i++)
            {
                sistemasSolar.AvanzarDia();
                //Console.ReadLine();
            }

        }
    }


    public class Angulo
    {
        double angle;
        public double Radianes { get; private set; }
        public double Inverso { get; private set; }

        public Angulo(double _angle)
        {
            angle = _angle;
            ActualizarRadianes();
            ActualizarInverso();
        }

        private void ActualizarRadianes()
        {
            Radianes = Math.PI * angle / 180.0;
        }

        private void ActualizarInverso()
        {
            Inverso = (angle + 180.0) % 360;
        }

        public void Increment(double inc)
        {
            angle += inc;
            if (angle > 360)
                angle -= 360;
            else if (angle < 0)
                angle += 360;

            ActualizarRadianes();
            ActualizarInverso();
        }

        public void Decrement(double dec)
        {
            Increment(-dec);
        }
        public override string ToString()
        {
            return angle.ToString();
        }
        public override int GetHashCode()
        {
            return angle.GetHashCode();
        }
        public static implicit operator double(Angulo angleObj)
        {
            return angleObj.angle;
        }
        public static implicit operator Angulo(double _angle)
        {
            return new Angulo(_angle);
        }
        public static Angulo operator +(Angulo lhs, Angulo rhs)
        {
            Angulo angle = new Angulo(lhs.angle);
            angle.Increment(rhs.angle);
            return angle;
        }
        public static Angulo operator -(Angulo lhs, Angulo rhs)
        {
            Angulo angle = new Angulo(lhs.angle);
            angle.Decrement(rhs.angle);
            return angle;
        }
        public static bool operator <(Angulo lhs, Angulo rhs)
        {
            if (lhs.angle < rhs.angle)
                return true;
            return false;
        }
        public static bool operator <=(Angulo lhs, Angulo rhs)
        {
            if (lhs.angle <= rhs.angle)
                return true;
            return false;
        }
        public static bool operator >(Angulo lhs, Angulo rhs)
        {
            if (lhs.angle > rhs.angle)
                return true;
            return false;
        }
        public static bool operator >=(Angulo lhs, Angulo rhs)
        {
            if (lhs.angle >= rhs.angle)
                return true;
            return false;
        }

    }

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
            return string.Format("X:{0} Y:{1}", X, Y);
        }
    }

    public class Planeta
    {
        public string Nombre { get; }
        public short Distancia { get; }
        public short VelocidadAngular { get; }

        public Punto PosicionActual { get; private set; }

        public Angulo AnguloActual { get; private set; }

        public Planeta(string nombre, short distancia, short velocidadAngular)
        {
            Nombre = nombre;
            Distancia = distancia;
            VelocidadAngular = velocidadAngular;
            AnguloActual = new Angulo(0);
            PosicionActual = new Punto(0, 0);
            ActualizarPosicionActual();
        }

        private void ActualizarPosicionActual()
        {
            PosicionActual.X = Distancia * Math.Cos(AnguloActual.Radianes);
            PosicionActual.Y = Distancia * Math.Sin(AnguloActual.Radianes);
        }

        public void Mover()
        {
            AnguloActual = AnguloActual + VelocidadAngular;

            ActualizarPosicionActual();
        }
    }

    public class SistemaSolar
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SistemaSolar));

        readonly List<Planeta> planetas = new List<Planeta>(3);

        public SistemaSolar()
        {
            planetas.Add(new Planeta("Ferengi", 500, -1));
            planetas.Add(new Planeta("Betasoide", 2000, -3));
            planetas.Add(new Planeta("Vulcano", 1000, 5));
        }

        public void AvanzarDia()
        {
            foreach (var planeta in planetas)
            {
                logger.Info(string.Format("Planeta -> {0} Angulo {1} {2} Pos {3}", planeta.Nombre, planeta.AnguloActual, planeta.AnguloActual.Inverso, planeta.PosicionActual));
                planeta.Mover();
            }

            if (PlanetasAlineadosConSol())
            {
                logger.Info("Planetas alineados con el sol");

                foreach (var planeta in planetas)
                {
                    logger.Info(string.Format("Planeta -> {0} Angulo {1} {2} Pos {3}", planeta.Nombre, planeta.AnguloActual, planeta.AnguloActual.Inverso, planeta.PosicionActual));
                }

                Console.ReadLine();
            }


        }

        private bool PlanetasAlineadosConSol()
        {
            var first = planetas.First();
            return planetas.All(x => x.AnguloActual == first.AnguloActual || x.AnguloActual == first.AnguloActual.Inverso);

        }
    }
}
