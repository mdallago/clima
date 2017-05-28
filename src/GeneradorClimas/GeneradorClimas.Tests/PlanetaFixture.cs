using GeneradorClimas.Domain;
using NUnit.Framework;

namespace GeneradorClimas.Tests
{
    [TestFixture]
    public class PlanetaFixture
    {
        [Test]
        public void MoverActualizaAnguloActual()
        {
            Planeta p = new Planeta("test",500,1);
            p.Mover();
            Assert.AreEqual(1,p.AnguloActual.Grados);
        }
    }
}