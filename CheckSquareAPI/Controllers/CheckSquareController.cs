using CheckSquareAPI.Models;
using CheckSquareAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CheckSquareAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckSquareController : ControllerBase
    {
        private SquareService _squareService;

        public CheckSquareController(SquareService squareService)
        {
            _squareService = squareService;
        }
        /// <summary>
        /// Post the coordinates you wish to check if it makes a square. 4 coordinates minimum.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /CheckSquare
        ///     {
        ///         "coordinates": [(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;-1.5),(-1.5;2.5),(2.5;2.5),(2.5;-1.5)]
        ///     }
        /// </remarks>
        /// <param name="coordinates"></param>
        /// <returns>A confirmation that the coordinates have been posted to the internal sessions string</returns>
        [HttpPost]
        public IActionResult PostCoordinates(string coordinates)
        {
            // Expected coordinates eaxmple: "[(-1.5;1.5),(1.5;1.5),(1.5;-1.5),(-1.5;-1.5),(-1.5;2.5),(2.5;2.5),(2.5;-1.5)]"
            HttpContext.Session.SetString("coordinates", coordinates);
            return Ok(new { 
                Success = true
            });
        }
        /// <summary>
        /// Gets all possible squares from the posted coordinates
        /// </summary>
        /// <remarks>
        /// If the posted coordinates were incorrectly formated, less than 4 were added or were never provided, 
        /// an appropriate messege will be shown:<br/>
        /// ERROR: Coordinates not found - no coordinates found in the session<br/>
        /// ERROR: Incorrect coordinate format - missing number or incorrect format used<br/>
        /// ERROR: Not enaugh points to make a square - not enaugh coordinates to make a square
        /// </remarks>
        /// <returns>Returns all possible square coordinates strings inside a list</returns>
        [HttpGet]
        public IActionResult GetSquares()
        {
            string coordinates = HttpContext.Session.GetString("coordinates") ?? "";
            List<string> squares = _squareService.GetSquares(coordinates);
            return Ok(new
            {
                Squares = squares
            });
        }
        /// <summary>
        /// Adds an additional coordinate to an already posted coordinates list
        /// </summary>
        /// <remarks>
        /// If the posted coordinates were incorrectly formated, or were never provided, 
        /// an appropriate messege will be shown:<br/>
        /// ERROR: Initial coordinates not found - no coordinates found in the session<br/>
        /// ERROR: Incorrect initial coordinate format - missing number or incorrect format used<br/>
        /// ERROR: Coordinates already in the list - coordinate point already in the list
        /// </remarks>
        /// <returns>A new coordinates string with the additional coordinates</returns>
        [HttpPut]
        public IActionResult PutCoordinates(double x, double y) 
        {
            string coordinates = HttpContext.Session.GetString("coordinates") ?? "";
            string newCoordinates = _squareService.AddCoordinates(coordinates, x, y);
            if(!newCoordinates.Contains("ERROR"))
                HttpContext.Session.SetString("coordinates", newCoordinates);
            return Ok(new
            {
                Coordinates = newCoordinates
            });
        }
        /// <summary>
        /// Deletes a selected coordinate from an already posted coordinates list
        /// </summary>
        /// <remarks>
        /// If the posted coordinates were incorrectly formated, or were never provided, 
        /// an appropriate messege will be shown:<br/>
        /// ERROR: Initial coordinates not found - no coordinates found in the session<br/>
        /// ERROR: Incorrect initial coordinate format - missing number or incorrect format used<br/>
        /// ERROR: Coordinate not found - if the coordinate is not found
        /// </remarks>
        /// <returns>A new coordinates string with the targeted coordinate deleted coordinates</returns>
        [HttpDelete]
        public IActionResult DeleteCoordinates(double x, double y) 
        {
            string coordinates = HttpContext.Session.GetString("coordinates") ?? "";
            string newCoordinates = _squareService.DeleteCoordinates(coordinates, x, y);
            if (!newCoordinates.Contains("ERROR"))
                HttpContext.Session.SetString("coordinates", newCoordinates);
            return Ok(new
            {
                Coordinates = newCoordinates
            });
        }
    }
}
