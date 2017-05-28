using System;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace GeneradorClimas.Domain
{
    public class SistemaSolar
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SistemaSolar));

        private readonly List<Planeta> planetas = new List<Planeta>(3);
        private readonly Punto sol = new Punto(0, 0);

        public List<Clima> Climas { get; }

        public List<int> DiasMaxLLuvia;
        private double? perimetroMaxLluvia;

        private int diaActual;

        public SistemaSolar()
        {
            planetas.Add(new Planeta("Ferengi", 500, -1));
            planetas.Add(new Planeta("Betasoide", 2000, -3));
            planetas.Add(new Planeta("Vulcano", 1000, 5));
            Climas = new List<Clima> {CalcularClima()};
        }

        private Clima CalcularClima()
        {
            if (PlanetasAlineadosConSol())
            {
                logger.Info("Planetas alineados con el sol");

                foreach (var planeta in planetas)
                {
                    logger.Info(
                        $"Planeta -> {planeta.Nombre} Angulo {planeta.AnguloActual} {planeta.AnguloActual.Inverso} Pos {planeta.PosicionActual}");
                }
                return Clima.Sequia;
            }

            if (PuntosColineares(planetas[0].PosicionActual, planetas[1].PosicionActual, planetas[2].PosicionActual))
            {
                logger.Info("Planetas Colineares");

                foreach (var planeta in planetas)
                {
                    logger.Info(
                        $"Planeta -> {planeta.Nombre} Angulo {planeta.AnguloActual} {planeta.AnguloActual.Inverso} Pos {planeta.PosicionActual}");
                }
                return Clima.Optimo;
            }

            if (PuntoEnTriangulo(sol, planetas[0].PosicionActual, planetas[1].PosicionActual, planetas[2].PosicionActual))
            {
                logger.Info("Sol en triangulo");

                foreach (var planeta in planetas)
                {
                    logger.Info(
                        $"Planeta -> {planeta.Nombre} Angulo {planeta.AnguloActual} {planeta.AnguloActual.Inverso} Pos {planeta.PosicionActual}");
                }

                var perimetro = Perimetro(planetas[0].PosicionActual, planetas[1].PosicionActual, planetas[2].PosicionActual);
                logger.Info($"Perimetro -> {perimetro}");


                if ((DiasMaxLLuvia == null) || perimetro > perimetroMaxLluvia)
                {
                    logger.Info($"Dia con mas lluvia {diaActual}");
                    DiasMaxLLuvia = new List<int> {diaActual};
                    perimetroMaxLluvia = perimetro;
                }
                else if (perimetro == perimetroMaxLluvia)
                {
                    DiasMaxLLuvia.Add(diaActual);
                }
                
                return Clima.Lluvia;
            }

            logger.Info("Clima no definido");
            return Clima.NoDefinido;
        }

        public void AvanzarDia()
        {
            diaActual++;

            foreach (var planeta in planetas)
            {
                logger.Info(
                    $"Planeta -> {planeta.Nombre} Angulo {planeta.AnguloActual} {planeta.AnguloActual.Inverso} Pos {planeta.PosicionActual}");
                planeta.Mover();
            }

            Climas.Add(CalcularClima());
        }

        private double Perimetro(Punto p1, Punto p2, Punto p3)
        {
            return DistanciaEntreDosPuntos(p1, p2) + DistanciaEntreDosPuntos(p2, p3) + DistanciaEntreDosPuntos(p1, p3);
        }

        private double DistanciaEntreDosPuntos(Punto p1, Punto p2)
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
            var b1 = Sign(pt, v1, v2) < 0.0;
            var b2 = Sign(pt, v2, v3) < 0.0;
            var b3 = Sign(pt, v3, v1) < 0.0;

            return ((b1 == b2) && (b2 == b3));
        }

        private bool PlanetasAlineadosConSol()
        {
            var first = planetas.First();
            return planetas.All(x => x.AnguloActual == first.AnguloActual || x.AnguloActual == first.AnguloActual.Inverso);
        }
    }
}