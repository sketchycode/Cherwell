using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cherwell.Models;
using Cherwell.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cherwell.Controllers
{
    [Route("api/[controller]")]
    public class CoordinateController : Controller
    {
        private IImageCoordinateService service;

        public CoordinateController(IImageCoordinateService service)
        {
            this.service = service;
        }

        [HttpGet("row/{row}/col/{col}", Name = "GetByRowCol")]
        public IActionResult GetByRowCol(char row, int col)
        {
            try
            {
                var triangle = service.GetByRowAndColumn(row, col);
                return new ObjectResult(triangle.Coordinates);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("x/{x}/y/{y}", Name = "GetByCoordinate")]
        public IActionResult GetByCoordinate(int x, int y)
        {
            try
            {
                var triangle = service.GetByCoordinate(x, y);
                return new ObjectResult(triangle);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost(Name = "GetByCoordinates")]
        public IActionResult GetByCoordinates([FromBody] CoordinateModel[] coordinates)
        {
            try
            {
                var triangles = coordinates.Select(c => service.GetByCoordinate(c.X, c.Y));
                var firstTriangle = triangles.FirstOrDefault();
                if (firstTriangle == null) { return NotFound(); }

                if (triangles.All(t => t.Row == firstTriangle.Row && t.Column == firstTriangle.Column))
                {
                    return new ObjectResult(firstTriangle);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                return NotFound(e.Message);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
