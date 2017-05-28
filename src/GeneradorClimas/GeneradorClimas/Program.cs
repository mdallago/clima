using log4net;
using log4net.Config;
using System;
using System.Linq;
using GeneradorClimas.Domain;

namespace GeneradorClimas
{
    class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            const int CANTIDAD_DIAS_AÑO = 365;
            const int CANTIDAD_AÑOS = 10;
            const int CANTIDAD_DIAS = CANTIDAD_DIAS_AÑO * CANTIDAD_AÑOS;

            XmlConfigurator.ConfigureAndWatch(
                new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\log4net.config"));

            var sistemasSolar = new SistemaSolar();

            for (var i = 0; i < CANTIDAD_DIAS - 1; i++)
            {
                sistemasSolar.AvanzarDia();
            }


            var group = sistemasSolar.Climas
                                    .Where(x => x != Clima.NoDefinido)
                                    .GroupBy(x => x)
                                    .Select(g => new
                                    {
                                        Clima = g.Key,
                                        Cantidad = g.Count()
                                    });

            logger.Info("Resultado del analisis de clima");

            logger.Info($"Se analizaron {sistemasSolar.Climas.Count} dias");

            foreach (var item in group)
            {
                logger.Info($"Clima {item.Clima} -> {item.Cantidad} dias");
            }

            logger.Info("Dias con picos maximos de lluvia");

            foreach (var item in sistemasSolar.DiasMaxLLuvia)
            {
                logger.Info($"Dia Nro -> {item}");
            }


            System.IO.File.WriteAllLines("resultado.txt", sistemasSolar.Climas.Select(x => ((byte)x).ToString()).ToArray());
        }
    }
}
