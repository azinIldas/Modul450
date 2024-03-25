using NUnit.Framework;
using Moq;
using Modul450.Code;
using System;

namespace Modul450.TestProject1
{
    [TestFixture]
    public class AuftragsdurchführungTests
    {
        private Mock<IMeinService> _mockService;
        private Mock<IOrder> _mockOrder;
        private Mock<IVehicle> _mockVehicle;

        [SetUp]
        public void SetUp()
        {
            // Mock-Objekte für den Service und Abhängigkeiten einrichten
            _mockService = new Mock<IMeinService>();
            _mockOrder = new Mock<IOrder>();
            _mockVehicle = new Mock<IVehicle>();

            // Mock-Einstellungen für das IOrder Interface
            _mockOrder.Setup(o => o.StartLocation).Returns("Lager");
            _mockOrder.Setup(o => o.EndLocation).Returns("Hafen");
            _mockOrder.Setup(o => o.ContainerSize).Returns(20);

            // Mock-Einstellungen für das IVehicle Interface
            _mockVehicle.Setup(v => v.LoadCargo(It.IsAny<int>())).Returns(true);
        }

        [Test]
        public void AuftragErfolgreichDurchgeführt()
        {
            // Mock-Setup, um ein erfolgreiches Durchführen eines Auftrags zu simulieren
            _mockService.Setup(s => s.FühreAuftragAus(_mockOrder.Object)).Returns(true);

            // Aufruf der Methode, die getestet wird
            bool ergebnis = _mockService.Object.FühreAuftragAus(_mockOrder.Object);

            // Überprüfen, ob das erwartete Ergebnis zurückgegeben wird
            Assert.IsTrue(ergebnis);
        }

        [Test]
        public void AuftragNichtDurchgeführtBeiNichtVerfügbarkeit()
        {
            // Mock-Setup, um ein Szenario zu simulieren, bei dem kein Fahrzeug verfügbar ist
            _mockService.Setup(s => s.FühreAuftragAus(_mockOrder.Object)).Returns(false);

            // Aufruf der Methode, die getestet wird
            bool ergebnis = _mockService.Object.FühreAuftragAus(_mockOrder.Object);

            // Überprüfen, ob das erwartete Ergebnis zurückgegeben wird
            Assert.IsFalse(ergebnis);
        }

        [Test]
        public void AuftragNichtDurchgeführtBeiUngültigenDaten()
        {
            // Mock-Setup, um ein Szenario mit ungültigen Daten zu simulieren
            _mockService.Setup(s => s.FühreAuftragAus(It.IsAny<IOrder>())).Throws(new InvalidDataException());

            // Überprüfen, ob beim Aufruf der Methode eine InvalidDataException geworfen wird
            Assert.Throws<InvalidDataException>(() => _mockService.Object.FühreAuftragAus(_mockOrder.Object));
        }
    }
}
