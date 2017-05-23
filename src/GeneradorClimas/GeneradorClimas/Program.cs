using System.Collections.Generic;
using System.Drawing;

namespace GeneradorClimas
{
    class Program
    {
        static void Main(string[] args)
        {
            const int CANTIDAD_DIAS_AÑO = 365;
            const int CANTIDAD_AÑOS= 10;

            SistemasSolar sistemasSolar = new SistemasSolar();

            for (int i = 0; i < CANTIDAD_AÑOS * CANTIDAD_DIAS_AÑO; i++)
            {
                sistemasSolar.AvanzarDia();
            }

        }
    }



    public class Planeta
    {
        public string Nombre { get; }
        public short Distancia { get; }
        public short VelocidadAngular { get; }

        public PointF PosicionActual { get; private set; }

        public Planeta(string nombre, short distancia, short velocidadAngular)
        {
            Nombre = nombre;
            Distancia = distancia;
            VelocidadAngular = velocidadAngular;
            PosicionActual = new PointF(0,0);
        }

        public void Mover()
        {
            
        }
    }

    public class SistemasSolar
    {
        readonly List<Planeta> planetas = new List<Planeta>(3);

        public SistemasSolar()
        {
            planetas.Add(new Planeta("Ferengi", 500, 1));
            planetas.Add(new Planeta("Betasoide", 2000, 3));
            planetas.Add(new Planeta("Vulcano", 1000, -5));
        }

        public void AvanzarDia()
        {
            foreach (var planeta in planetas)
            {
                planeta.Mover();
            }
        }

    }
}
