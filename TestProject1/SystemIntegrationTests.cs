using NUnit.Framework;
using Moq;
using Modul450.Code;  // Ersetzen Sie dies durch den tats�chlichen Namespace Ihrer Klassen

namespace Modul450.TestProject1
{
    [TestFixture]
    public class SystemIntegrationTests
    {
        private Mock<IVehicle> _mockVehicle;
        private MeinService _meinService;  // Angenommen, dies ist Ihre konkrete Service-Implementierung
        private Mock<IOrder> _mockOrder;   // Mock f�r IOrder

        [SetUp]
        public void SetUp()
        {
            // Mock-Objekte erstellen
            _mockVehicle = new Mock<IVehicle>();
            _mockOrder = new Mock<IOrder>();

            // IOrder Mock konfigurieren
            _mockOrder.Setup(o => o.StartLocation).Returns("Start");
            _mockOrder.Setup(o => o.EndLocation).Returns("Ziel");
            _mockOrder.Setup(o => o.ContainerSize).Returns(20);

            // MeinService mit den Mocks initialisieren
            _meinService = new MeinService(_mockVehicle.Object);
        }

        [Test]
        public void ServiceVerarbeitetAuftrag()
        {
            // Aktion: Der Service versucht, den Auftrag zu verarbeiten
            _meinService.F�hreAuftragAus(_mockOrder.Object);

            // Assertions: �berpr�fen, ob die erwarteten Methoden aufgerufen wurden
            _mockVehicle.Verify(v => v.LoadCargo(It.IsAny<int>()), Times.Once);
            // Hier w�rden Sie weitere Assertions hinzuf�gen, um das Verhalten zu verifizieren
        }

        // Weitere Integrationstests und TearDown-Methode wie ben�tigt...
    }
}
