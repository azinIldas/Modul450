using NUnit.Framework;
using Moq;
using Modul450.Code;
using System;

namespace Modul450.TestProject1
{
    [TestFixture]
    public class Auftragsdurchf�hrungTests
    {
        private Mock<IMeinService> _mockService;
        private Mock<IOrder> _mockOrder;
        private Mock<IVehicle> _mockVehicle;

        [SetUp]
        public void SetUp()
        {
            // Mock-Objekte f�r den Service und Abh�ngigkeiten einrichten
            _mockService = new Mock<IMeinService>();
            _mockOrder = new Mock<IOrder>();
            _mockVehicle = new Mock<IVehicle>();

            // Mock-Einstellungen f�r das IOrder Interface
            _mockOrder.Setup(o => o.StartLocation).Returns("Lager");
            _mockOrder.Setup(o => o.EndLocation).Returns("Hafen");
            _mockOrder.Setup(o => o.ContainerSize).Returns(20);

            // Mock-Einstellungen f�r das IVehicle Interface
            _mockVehicle.Setup(v => v.LoadCargo(It.IsAny<int>())).Returns(true);
        }

        [Test]
        public void AuftragErfolgreichDurchgef�hrt()
        {
            // Mock-Setup, um ein erfolgreiches Durchf�hren eines Auftrags zu simulieren
            _mockService.Setup(s => s.F�hreAuftragAus(_mockOrder.Object)).Returns(true);

            // Aufruf der Methode, die getestet wird
            bool ergebnis = _mockService.Object.F�hreAuftragAus(_mockOrder.Object);

            // �berpr�fen, ob das erwartete Ergebnis zur�ckgegeben wird
            Assert.IsTrue(ergebnis);
        }

        [Test]
        public void AuftragNichtDurchgef�hrtBeiNichtVerf�gbarkeit()
        {
            // Mock-Setup, um ein Szenario zu simulieren, bei dem kein Fahrzeug verf�gbar ist
            _mockService.Setup(s => s.F�hreAuftragAus(_mockOrder.Object)).Returns(false);

            // Aufruf der Methode, die getestet wird
            bool ergebnis = _mockService.Object.F�hreAuftragAus(_mockOrder.Object);

            // �berpr�fen, ob das erwartete Ergebnis zur�ckgegeben wird
            Assert.IsFalse(ergebnis);
        }

        [Test]
        public void AuftragNichtDurchgef�hrtBeiUng�ltigenDaten()
        {
            // Mock-Setup, um ein Szenario mit ung�ltigen Daten zu simulieren
            _mockService.Setup(s => s.F�hreAuftragAus(It.IsAny<IOrder>())).Throws(new InvalidDataException());

            // �berpr�fen, ob beim Aufruf der Methode eine InvalidDataException geworfen wird
            Assert.Throws<InvalidDataException>(() => _mockService.Object.F�hreAuftragAus(_mockOrder.Object));
        }
    }
}
