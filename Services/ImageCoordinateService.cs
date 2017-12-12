
using System;
using System.Collections.Generic;
using Cherwell.Models;

namespace Cherwell.Services
{
    public class ImageCoordinateService : IImageCoordinateService
    {
        private int width;
        private int height;
        private int cellWidth;
        private int cellHeight;

        public ImageCoordinateService(int width = 60, int height = 60, int cellWidth = 10, int cellHeight = 10)
        {
            this.width = width;
            this.height = height;
            this.cellWidth = cellWidth;
            this.cellHeight = cellHeight;
        }
        public TriangleModel GetByCoordinate(int x, int y)
        {
            ValidateXY(x, y);
            return new TriangleModel(GetRowByY(y), GetTriangleColumnByXY(x, y), cellWidth, cellHeight);
        }

        public TriangleModel GetByRowAndColumn(char row, int col)
        {
            ValidateRowCol(row, col);
            return new TriangleModel(row, col, cellWidth, cellHeight);
        }

        private char GetRowByY(int y)
        {
            int rowIndexOffset = y / cellHeight; // integer division

            int rowIndexA = (int)'A';
            return (char)(rowIndexA + rowIndexOffset);
        }

        private int GetTriangleColumnByXY(int x, int y)
        {
            int gridColumn = x / cellWidth; // integer division
            int gridColumnStart = gridColumn * cellWidth;
            int gridRowStart = (y / cellHeight) * cellHeight;

            float normalizedX = (float)(x - gridColumnStart) / (float)cellWidth;
            float normalizedY = (float)(y - gridRowStart) / (float)cellHeight;

            return gridColumn * 2 + (normalizedX > normalizedY ? 2 : 1);
        }

        private void ValidateXY(int x, int y) {
            if (x < 0 || x >= width) {
                throw new ArgumentOutOfRangeException("x", x, "x must be in the range [0, " + width + ")");
            }
            if (y < 0 || y >= height) {
                throw new ArgumentOutOfRangeException("y", x, "y must be in the range [0, " + height + ")");
            }
        }

        private void ValidateRowCol(char row, int col) {
            char lowerRowBound = 'A';
            char upperRowBound = (char)(lowerRowBound + (height / cellHeight));
            
            if(row < lowerRowBound || row > upperRowBound) {
                throw new ArgumentOutOfRangeException("row", row, "row must be a value between \'" + lowerRowBound + "\' and \'" + upperRowBound + "\' inclusively");
            }

            if (col <= 0 || col > (width * 2 / cellWidth)) {
                throw new ArgumentOutOfRangeException("col", col, "col must be in the range [1, " + (width * 2 / cellWidth) + "]");
            }
        }
    }
}