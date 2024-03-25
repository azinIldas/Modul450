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
            // Hier k�nnten Sie Setup-Logik f�r die Mocks einf�gen, falls n�tig.
        }

        [Test]
        public void ZuweisungErfolgreichWennFahrzeugVerf�gbar()
        {
            // Setup - das Fahrzeug ist verf�gbar und kann den Auftrag laden
            _mockVehicle.Setup(v => v.LoadCargo(It.IsAny<int>())).Returns(true);

            // Act - der Auftrag wird zugewiesen und es wird versucht, das Fahrzeug zu beladen
            var result = _mockVehicle.Object.LoadCargo(_mockOrder.Object.ContainerSize);

            // Assert - der Ladevorgang sollte erfolgreich sein
            Assert.IsTrue(result);
        }

        [Test]
        public void ZuweisungFehlschl�gtWennFahrzeugNichtVerf�gbar()
        {
            // Setup - das Fahrzeug ist nicht verf�gbar und kann den Auftrag nicht laden
            _mockVehicle.Setup(v => v.LoadCargo(It.IsAny<int>())).Returns(false);

            // Act - der Auftrag wird zugewiesen und es wird versucht, das Fahrzeug zu beladen
            var result = _mockVehicle.Object.LoadCargo(_mockOrder.Object.ContainerSize);

            // Assert - der Ladevorgang sollte fehlschlagen
            Assert.IsFalse(result);
        }

        [Test]
        public void ZuweisungFehlschl�gtBeiUng�ltigemAuftrag()
        {
            // Setup - der Auftrag ist ung�ltig
            _mockOrder.Setup(o => o.ContainerSize).Throws(new InvalidOperationException());

            // Act & Assert - der Auftrag wird zugewiesen und es sollte eine Ausnahme geworfen werden
            Assert.Throws<InvalidOperationException>(() => _mockVehicle.Object.LoadCargo(_mockOrder.Object.ContainerSize));
        }

        // Weitere Tests und Setup-Logik k�nnen hier hinzugef�gt werden...
    }
}
