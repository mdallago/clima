using GeneradorClimas.Domain;
using NUnit.Framework;

namespace GeneradorClimas.Tests
{
    [TestFixture]
    public class PuntoFixture
    {
        [Test]
        public void PuedoCrearPunto()
        {
            var p = new Punto(10.5, 9.8);
            Assert.AreEqual(10.5, p.X);
            Assert.AreEqual(9.8, p.Y);
        }

        [Test]
        public void PuntosConMismasCoordenadasSonIguales()
        {
            var p1 = new Punto(10.5, 9.8);
            var p2 = new Punto(10.5, 9.8);
            Assert.AreEqual(p1, p2);
        }
    }
}