using CheckSquareAPI.Models;
using CheckSquareAPI.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace CheckSquareAPI.Services
{
    public class SquareService
    {
        public List<string> GetSquares(string coordinates)
        {
            List<string> squareCoordinatesStringsList = new List<string>();
            if (coordinates == "")
            {
                squareCoordinatesStringsList.Add("ERROR: Coordinates not found");
                return squareCoordinatesStringsList;
            }
            List<Coordinate> coordinatesList = SetCoordnatesList(coordinates);
            if (coordinatesList.Count == 0)
            {
                squareCoordinatesStringsList.Add("ERROR: Incorrect coordinate format");
                return squareCoordinatesStringsList;
            }
            if (coordinatesList.Count < 4)
            {
                squareCoordinatesStringsList.Add("ERROR: Not enaugh points to make a square");
                return squareCoordinatesStringsList;
            }
            List<Coordinate[]> squareCoordinatesList = new List<Coordinate[]>();
            foreach (Coordinate coordinate in coordinatesList) 
            {
                foreach (Coordinate coordinate2 in coordinatesList) 
                {
                    if (coordinate != coordinate2)
                    {
                        foreach (Coordinate coordinate3 in coordinatesList)
                        {
                            if (coordinate != coordinate3 && coordinate2 != coordinate3)
                            {
                                foreach (Coordinate coordinate4 in coordinatesList)
                                {
                                    if (coordinate != coordinate4 && coordinate2 != coordinate4 && coordinate3 != coordinate4)
                                    {
                                        if (IsSquare(coordinate, coordinate2, coordinate3, coordinate4))
                                        {
                                            Coordinate[] squareCoordinates = { coordinate, coordinate2, coordinate3, coordinate4 };
                                            squareCoordinates = squareCoordinates.OrderBy(x => x.X).ThenBy(x => x.Y).ToList().ToArray();
                                            var squareCoordinatesDplicate = 
                                                squareCoordinatesList.Where(coordinatesInList => 
                                                Enumerable.SequenceEqual(squareCoordinates, coordinatesInList)).FirstOrDefault();
                                            if (squareCoordinatesDplicate == null)
                                            {
                                                squareCoordinatesList.Add(squareCoordinates);
                                                squareCoordinatesStringsList.Add(SetSquareCoordinatesString(squareCoordinates));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (squareCoordinatesStringsList.Count == 0)
                squareCoordinatesStringsList.Add("No squares can be made form the given coordinates");
            return squareCoordinatesStringsList;
        }
        public string AddCoordinates(string coordinates, double x, double y)
        {
            if (coordinates == "")
                return "ERROR: Initial coordinates not found";
            Coordinate newCoordinates = new Coordinate() { X = x, Y = y };
            List<Coordinate> coordinatesList = SetCoordnatesList(coordinates);
            if (coordinatesList.Count == 0)
                return "ERROR: Incorrect initial coordinate format";
            Coordinate? duplicateCoordinate = coordinatesList.Where(x => x.X == newCoordinates.X && x.Y == newCoordinates.Y).FirstOrDefault();
            if (duplicateCoordinate != null)
                return "ERROR: Coordinates already in the list";
            coordinatesList.Add(new Coordinate() { X = x, Y = y });
            return SetSquareCoordinatesString(coordinatesList.ToArray());
        }
        public string DeleteCoordinates(string coordinates, double x, double y)
        {
            if (coordinates == "")
                return "ERROR: Initial coordinates not found";
            List<Coordinate> coordinatesList = SetCoordnatesList(coordinates);
            if (coordinatesList.Count == 0)
                return "ERROR: Incorrect initial coordinate format";
            Coordinate? deletePoint = coordinatesList.Where(c=> c.X == x && c.Y == y).FirstOrDefault();
            if (deletePoint != null) 
                coordinatesList.Remove(deletePoint);
            else 
                return "ERROR: Coordinate not found";
            return SetSquareCoordinatesString(coordinatesList.ToArray());
        }
        public List<Coordinate> SetCoordnatesList(string coordinates) 
        {
            List<Coordinate> coordinatesList = new List<Coordinate>();
            coordinates = coordinates.Replace("[", "").Replace("]","").Replace("(", "").Replace(")", "").ToString();
            List<string> coordinatesStringList = coordinates.Split(',').ToList();
            foreach (string coordinatesString in coordinatesStringList) 
            {
                if (coordinatesString != "")
                {
                    Coordinate coordinate = new Coordinate();
                    string[] coordinatePoints = coordinatesString.Split(';');
                    try
                    {
                        coordinate.X = Convert.ToDouble(coordinatePoints[0]);
                        coordinate.Y = Convert.ToDouble(coordinatePoints[1]);
                    }
                    catch (Exception e) {
                        Debug.WriteLine(e.Message);
                        return new List<Coordinate>();
                    }
                    coordinatesList.Add(coordinate);
                }
            }
            return coordinatesList;
        }
        public bool IsSquare(Coordinate point1, Coordinate point2, Coordinate point3, Coordinate point4)
        {
            bool isSquare = false;
            double distance1 = GetPointsDistance(point1, point2);
            double distance2 = GetPointsDistance(point2, point3);
            double distance3 = GetPointsDistance(point3, point4);
            double distance4 = GetPointsDistance(point1, point4);
            if (distance1 == distance2 && distance1 == distance3 && distance1 == distance4)
                isSquare = true;

            return isSquare;
        }
        public double GetPointsDistance(Coordinate point1, Coordinate point2)
        {
            return (point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y);
        }
        public string SetSquareCoordinatesString(Coordinate[] coordinates)
        {
            string coordinatesString = "";
            foreach (Coordinate coordinate in coordinates)
            {
                coordinatesString += "(" + coordinate.X.ToString() + ";" + coordinate.Y.ToString() + "),";
            }
            return coordinatesString.Remove(coordinatesString.Length-1);
        }
    }
}

