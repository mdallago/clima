using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
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
            }

            Console.ReadLine();
        }
    }


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
            if (System.Object.ReferenceEquals(lhs, rhs))
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

            Angulo p = obj as Angulo;
            if ((System.Object)p == null)
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


        public static bool operator ==(Punto lhs, Punto rhs)
        {
            if (System.Object.ReferenceEquals(lhs, rhs))
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

    public enum Clima
    {
        Sequia, LLuvia, Optimo, NoDefinido
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
            AnguloActual += VelocidadAngular;
            ActualizarPosicionActual();
        }
    }

    public class SistemaSolar
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SistemaSolar));

        private readonly List<Planeta> planetas = new List<Planeta>(3);
        private Punto sol = new Punto(0, 0);

        public List<Clima> Climas { get; private set; }

        public int? DiaMaxLLuvia;
        private double? perimetroMaxLluvia;

        private int diaActual;

        public SistemaSolar()
        {
            planetas.Add(new Planeta("Ferengi", 500, -1));
            planetas.Add(new Planeta("Betasoide", 2000, -3));
            planetas.Add(new Planeta("Vulcano", 1000, 5));
            Climas = new List<Clima>();
            Climas.Add(CalcularClima());
        }

        private Clima CalcularClima()
        {
            if (PlanetasAlineadosConSol())
            {
                logger.Info("Planetas alineados con el sol");

                foreach (var planeta in planetas)
                {
                    logger.Info(string.Format("Planeta -> {0} Angulo {1} {2} Pos {3}", planeta.Nombre, planeta.AnguloActual, planeta.AnguloActual.Inverso, planeta.PosicionActual));
                }
                return Clima.Sequia;
            }

            if (PuntosColineares(planetas[0].PosicionActual, planetas[1].PosicionActual, planetas[2].PosicionActual))
            {
                logger.Info("Planetas Colineares");

                foreach (var planeta in planetas)
                {
                    logger.Info(string.Format("Planeta -> {0} Angulo {1} {2} Pos {3}", planeta.Nombre, planeta.AnguloActual, planeta.AnguloActual.Inverso, planeta.PosicionActual));
                }
                return Clima.Optimo;
            }

            if (PuntoEnTriangulo(sol, planetas[0].PosicionActual, planetas[1].PosicionActual, planetas[2].PosicionActual))
            {
                logger.Info("Sol en triangulo");

                foreach (var planeta in planetas)
                {
                    logger.Info(string.Format("Planeta -> {0} Angulo {1} {2} Pos {3}", planeta.Nombre, planeta.AnguloActual, planeta.AnguloActual.Inverso, planeta.PosicionActual));
                }

                var perimetro = Perimetro(planetas[0].PosicionActual, planetas[1].PosicionActual, planetas[2].PosicionActual);
                logger.Info(string.Format("Perimetro -> {0}", perimetro));


                if ((DiaMaxLLuvia == null) || perimetro >= perimetroMaxLluvia)
                {
                    DiaMaxLLuvia = diaActual;
                    perimetroMaxLluvia = perimetro;
                }

                return Clima.LLuvia;
            }

            logger.Info("Clima no definido");
            return Clima.NoDefinido;
        }

        public void AvanzarDia()
        {
            diaActual++;

            foreach (var planeta in planetas)
            {
                logger.Info(string.Format("Planeta -> {0} Angulo {1} {2} Pos {3}", planeta.Nombre, planeta.AnguloActual, planeta.AnguloActual.Inverso, planeta.PosicionActual));
                planeta.Mover();
            }

            Climas.Add(CalcularClima());
        }

        public double Perimetro(Punto p1, Punto p2, Punto p3)
        {
            return DistanciaEntreDosPuntos(p1, p2) + DistanciaEntreDosPuntos(p2, p3) + DistanciaEntreDosPuntos(p1, p3);
        }

        public double DistanciaEntreDosPuntos(Punto p1, Punto p2)
        {
            double a = p2.X - p1.X;
            double b = p2.Y - p1.Y;
            return Math.Sqrt(a * a + b * b);
        }

        private bool PuntosColineares(Punto p1, Punto p2, Punto p3)
        {
            return ((int)p2.Y - (int)p1.Y) / ((int)p2.X - (int)p1.X) == ((int)p3.Y - (int)p1.Y) / ((int)p3.X - (int)p1.X);
        }

        private double Sign(Punto p1, Punto p2, Punto p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        private bool PuntoEnTriangulo(Punto pt, Punto v1, Punto v2, Punto v3)
        {
            bool b1, b2, b3;

            b1 = Sign(pt, v1, v2) < 0.0;
            b2 = Sign(pt, v2, v3) < 0.0;
            b3 = Sign(pt, v3, v1) < 0.0;

            return ((b1 == b2) && (b2 == b3));
        }

        private bool PlanetasAlineadosConSol()
        {
            var first = planetas.First();
            return planetas.All(x => x.AnguloActual == first.AnguloActual || x.AnguloActual == first.AnguloActual.Inverso);
        }
    }
}
