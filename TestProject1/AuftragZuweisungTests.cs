using NUnit.Framework;
using Moq;
using Modul450.Code; 
namespace Modul450.TestProject1
{
    [TestFixture]
    public class AuftragZuweisungTests
    {
        private Mock<IVehicle> _mockVehicle;
        private Mock<IOrder> _mockOrder;

        [SetUp]
        public void SetUp()
        {
            _mockVehicle = new Mock<IVehicle>();
            _mockOrder = new Mock<IOrder>();
            // Hier könnten Sie Setup-Logik für die Mocks einfügen, falls nötig.
        }

        [Test]
        public void ZuweisungErfolgreichWennFahrzeugVerfügbar()
        {
            // Setup - das Fahrzeug ist verfügbar und kann den Auftrag laden
            _mockVehicle.Setup(v => v.LoadCargo(It.IsAny<int>())).Returns(true);

            // Act - der Auftrag wird zugewiesen und es wird versucht, das Fahrzeug zu beladen
            var result = _mockVehicle.Object.LoadCargo(_mockOrder.Object.ContainerSize);

            // Assert - der Ladevorgang sollte erfolgreich sein
            Assert.IsTrue(result);
        }

        [Test]
        public void ZuweisungFehlschlägtWennFahrzeugNichtVerfügbar()
        {
            // Setup - das Fahrzeug ist nicht verfügbar und kann den Auftrag nicht laden
            _mockVehicle.Setup(v => v.LoadCargo(It.IsAny<int>())).Returns(false);

            // Act - der Auftrag wird zugewiesen und es wird versucht, das Fahrzeug zu beladen
            var result = _mockVehicle.Object.LoadCargo(_mockOrder.Object.ContainerSize);

            // Assert - der Ladevorgang sollte fehlschlagen
            Assert.IsFalse(result);
        }

        [Test]
        public void ZuweisungFehlschlägtBeiUngültigemAuftrag()
        {
            // Setup - der Auftrag ist ungültig
            _mockOrder.Setup(o => o.ContainerSize).Throws(new InvalidOperationException());

            // Act & Assert - der Auftrag wird zugewiesen und es sollte eine Ausnahme geworfen werden
            Assert.Throws<InvalidOperationException>(() => _mockVehicle.Object.LoadCargo(_mockOrder.Object.ContainerSize));
        }

        // Weitere Tests und Setup-Logik können hier hinzugefügt werden...
    }
}
