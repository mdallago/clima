using GeneradorClimas.Domain;
using NUnit.Framework;

namespace GeneradorClimas.Tests
{
    [TestFixture]
    public class SistemaSolarFixture
    {
        [Test]
        public void CrearSistemaSolarCalculaClimaCero()
        {
            SistemaSolar sistemaSolar = new SistemaSolar();
            Assert.AreEqual(1, sistemaSolar.Climas.Count);
        }

        [Test]
        public void AvanzarDiaCalculaNuevoClima()
        {
            SistemaSolar sistemaSolar = new SistemaSolar();
            sistemaSolar.AvanzarDia();
            Assert.AreEqual(2, sistemaSolar.Climas.Count);
        }
    }
}