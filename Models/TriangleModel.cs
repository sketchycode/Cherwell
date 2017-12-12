
namespace Cherwell.Models
{
    public class TriangleModel
    {
        public char Row { get; private set; }
        public int Column { get; private set; }
        public CoordinateModel[] Coordinates { get; private set; }

        public TriangleModel(char row, int column, int cellWidth, int cellHeight)
        {
            Row = row;
            Column = column;
            Coordinates = GetCoordinatesFromRowColumn(row, column, cellWidth, cellHeight);
        }

        private CoordinateModel[] GetCoordinatesFromRowColumn(char row, int column, int cellWidth, int cellHeight)
        {
            int cellY = row - 'A';
            int cellX = (column - 1) / 2;

            if (column % 2 == 1) // bottom left triangle in cell
            {
                return new CoordinateModel[] {
                    new CoordinateModel() { X = cellX * cellWidth, Y = cellY * cellHeight }, // top left corner
                    new CoordinateModel() { X = cellX * cellWidth, Y = (cellY + 1) * cellHeight - 1 }, // bottom left corner
                    new CoordinateModel() { X = (cellX + 1) * cellWidth - 1, Y = (cellY + 1) * cellHeight - 1}, // bottom right corner
                };
            }
            else
            { // top right triangle in cell
                return new CoordinateModel[] {
                    new CoordinateModel() { X = cellX * cellWidth, Y = cellY * cellHeight }, // top left corner
                    new CoordinateModel() { X = (cellX + 1) * cellWidth - 1, Y = (cellY + 1) * cellHeight - 1 }, // bottom right corner
                    new CoordinateModel() { X = (cellX + 1) * cellWidth - 1, Y = cellY * cellHeight }, // top right corner
                };
            }
        }
    }
}