using System;

namespace GeneradorClimas.Domain
{
    public class Planeta
    {
        public string Nombre { get; }
        public short Distancia { get; }
        public short VelocidadAngular { get; }

        public Punto PosicionActual { get; }

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
}