using GeneradorClimas.Domain;
using NUnit.Framework;

namespace GeneradorClimas.Tests
{
    [TestFixture]
    public class AnguloFixture
    {
        [Test]
        public void PuedoCrearAngulo()
        {
            var a = new Angulo(90);
            Assert.AreEqual(90, a.Grados);
            Assert.AreEqual(270, a.Inverso);
            Assert.AreEqual(1.5707963267948966, a.Radianes);
        }

        [Test]
        public void AnguloConMismosGradosSonIguales()
        {
            var a1 = new Angulo(90);
            var a2 = new Angulo(90);
            Assert.AreEqual(a1, a2);
        }

        [Test]
        public void IncrementarAngulo()
        {
            var a1 = new Angulo(90);
            a1.Increment(1);
            Assert.AreEqual(91, a1.Grados);
        }

        [Test]
        public void IncrementarAnguloMayorA360()
        {
            var a1 = new Angulo(360);
            a1.Increment(1);
            Assert.AreEqual(1, a1.Grados);
        }

    }
}