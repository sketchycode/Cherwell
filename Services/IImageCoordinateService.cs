
using System.Collections.Generic;
using Cherwell.Models;

namespace Cherwell.Services {
    public interface IImageCoordinateService {
        TriangleModel GetByRowAndColumn(char row, int col);

        TriangleModel GetByCoordinate(int x, int y);
    }
}