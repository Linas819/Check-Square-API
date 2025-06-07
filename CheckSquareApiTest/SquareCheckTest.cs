using CheckSquareAPI.Services;

namespace CheckSquareApiTest
{
    public class SquareCheckTest
    {
        private SquareService _squareService;

        [OneTimeSetUp]
        public void Setup()
        {
            _squareService = new SquareService(); 
        }

        [Test]
        public void GetSquaresTest()
        {
            string coordinatesString = "[(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;-1.5)]";
            List<string> squareCoordinates = _squareService.GetSquares(coordinatesString);
            List<string> expectedResult = new List<string>() {
                "(-1.5;-1.5),(-1.5;1.5),(1.5;-1.5),(1.5;1.5)"
            };
            Assert.That(squareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void GetSquaresTestFail()
        {
            string coordinatesString = "[(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;-1)]";
            List<string> squareCoordinates = _squareService.GetSquares(coordinatesString);
            List<string> expectedResult = new List<string>() {
                "No squares can be made form the given coordinates"
            };
            Assert.That(squareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void GetSquaresTestIncorrectInput()
        {
            string coordinatesString = "[(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;]";
            List<string> squareCoordinates = _squareService.GetSquares(coordinatesString);
            List<string> expectedResult = new List<string>() {
                "ERROR: Incorrect coordinate format"
            };
            Assert.That(squareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void GetSquaresTestNotEnaughPoints()
        {
            string coordinatesString = "[(-1.5;1.5),(1.5;1.5),(1.5;-1.5)]";
            List<string> squareCoordinates = _squareService.GetSquares(coordinatesString);
            List<string> expectedResult = new List<string>() {
                "ERROR: Not enaugh points to make a square"
            };
            Assert.That(squareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void GetSquaresTestNoInput()
        {
            string coordinatesString = "";
            List<string> squareCoordinates = _squareService.GetSquares(coordinatesString);
            List<string> expectedResult = new List<string>() {
                "ERROR: Coordinates not found"
            };
            Assert.That(squareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void AddCoordinates()
        {
            string coordinatesString = "[(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;-1.5)]";
            double x = 1;
            double y = 1;
            string newSquareCoordinates = _squareService.AddCoordinates(coordinatesString, x, y);
            string expectedResult = "(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;-1.5),(1;1)";
            Assert.That(newSquareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void AddCoordinatesNoInput()
        {
            string coordinatesString = "";
            double x = 1;
            double y = 1;
            string newSquareCoordinates = _squareService.AddCoordinates(coordinatesString, x, y);
            string expectedResult = "ERROR: Initial coordinates not found";
            Assert.That(newSquareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void AddCoordinatesIncorrectInput()
        {
            string coordinatesString = "[(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;)]";
            double x = 1;
            double y = 1;
            string newSquareCoordinates = _squareService.AddCoordinates(coordinatesString, x, y);
            string expectedResult = "ERROR: Incorrect initial coordinate format";
            Assert.That(newSquareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void AddCoordinatesDuplicateInput()
        {
            string coordinatesString = "[(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;-1.5)]";
            double x = 1.5;
            double y = 1.5;
            string newSquareCoordinates = _squareService.AddCoordinates(coordinatesString, x, y);
            string expectedResult = "ERROR: Coordinates already in the list";
            Assert.That(newSquareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void DeleteCoordinates()
        {
            string coordinatesString = "[(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;-1.5),(1;1)]";
            double x = 1;
            double y = 1;
            string newSquareCoordinates = _squareService.DeleteCoordinates(coordinatesString, x, y);
            string expectedResult = "(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;-1.5)";
            Assert.That(newSquareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void DeleteCoordinatesNoInput()
        {
            string coordinatesString = "";
            double x = 1;
            double y = 1;
            string newSquareCoordinates = _squareService.DeleteCoordinates(coordinatesString, x, y);
            string expectedResult = "ERROR: Initial coordinates not found";
            Assert.That(newSquareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void DeleteCoordinatesIncorrectInput()
        {
            string coordinatesString = "[(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;-1.5),(1;)]";
            double x = 1;
            double y = 1;
            string newSquareCoordinates = _squareService.DeleteCoordinates(coordinatesString, x, y);
            string expectedResult = "ERROR: Incorrect initial coordinate format";
            Assert.That(newSquareCoordinates, Is.EqualTo(expectedResult));
        }
        [Test]
        public void DeleteCoordinatesNotFound()
        {
            string coordinatesString = "[(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;-1.5)]";
            double x = 1;
            double y = 1;
            string newSquareCoordinates = _squareService.DeleteCoordinates(coordinatesString, x, y);
            string expectedResult = "ERROR: Coordinate not found";
            Assert.That(newSquareCoordinates, Is.EqualTo(expectedResult));
        }
    }
}