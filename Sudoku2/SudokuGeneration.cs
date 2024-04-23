using System.Windows.Controls;
using System.Windows.Media;

namespace Sudoku2;

internal class SudokuGeneration
{
    private int SizeOfGrid;
    private int SquareRootOfGrid;
    private int[,] Numbers;
    private TextBox[,] Boxes;
    private int ClueNumber;
    public SudokuGeneration(TextBox[,] TextBoxes, int clueNumber)
    {
        SizeOfGrid = TextBoxes.GetLength(0);
        SquareRootOfGrid = (int)Math.Floor(Math.Sqrt(SizeOfGrid));
        Numbers = new int[SizeOfGrid, SizeOfGrid];
        Boxes = TextBoxes;
        ClueNumber = clueNumber;
    }
    internal TextBox[,] SetUp()
    {
        for(int i =0; i < SizeOfGrid; i+= SquareRootOfGrid)
        {
            FillBox(i, i);
        }
        FillCells(0, SquareRootOfGrid);
        for(int i = 0; i < SizeOfGrid; i++) 
        {
            for(int j = 0 ; j < SizeOfGrid; j++)
            {
                Boxes[i,j].Text = Numbers[i,j].ToString();
            }
        }
        RemoveElements((SizeOfGrid * SizeOfGrid) - ClueNumber);
        return Boxes;
    }

    private void FillBox(int RowStart, int ColStart)
    {
        var rng = new Random();
        var elements = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        for (int i = 0; i < SquareRootOfGrid; i++)
        {
            for (int j = 0; j < SquareRootOfGrid; j++)
            {
                while (true)
                {
                    var index = rng.Next(elements.Count);
                    Numbers[RowStart + i, ColStart + j] = elements[index];
                    if (CheckBox(RowStart, ColStart))
                    {
                        elements.RemoveAt(index);
                        Boxes[RowStart + i, ColStart + j].Text = Numbers[RowStart + i, ColStart + j].ToString();
                        break;
                    }
                }
            }
        }
    }

    private void RemoveElements(int AmountToRemove)
    {
        while (AmountToRemove > 0)
        {
            var rng = new Random();
            var x = rng.Next(SizeOfGrid);
            var y = rng.Next(SizeOfGrid);
            if (Numbers[x, y] == 0) continue;
            Numbers[x, y] = 0;
            Boxes[x, y].Text = "";
            Boxes[x, y].IsEnabled = true;
            AmountToRemove--;
        }
    }

    private bool FillCells(int Row, int Column)
    {
        if (Column >= SizeOfGrid && Row < SizeOfGrid - 1)
        {
            Row = Row + 1;
            Column = 0;
        }
        if (Row >= SizeOfGrid && Column >= SizeOfGrid)
        {
            return true;
        }
        if (Row < SquareRootOfGrid)
        {
            if (Column < SquareRootOfGrid)
                Column = SquareRootOfGrid;
        }
        else if (Row < SizeOfGrid - SquareRootOfGrid)
        {
            if (Column == (int)(Row / SquareRootOfGrid) * SquareRootOfGrid)
                Column = Column + SquareRootOfGrid;
        }
        else
        {
            if (Column == SizeOfGrid - SquareRootOfGrid)
            {
                Row = Row + 1;
                Column = 0;
                if (Row >= SizeOfGrid)
                    return true;
            }
        }
        for (int num = 1; num <= SizeOfGrid; num++)
        {
            Numbers[Row, Column] = num;
            if (CheckAll(Row, Column))
            {
                if (FillCells(Row, Column + 1))
                    return true;

            }
            Numbers[Row, Column] = 0;
        }
        return false;
    }
    internal bool CheckAll(int Row, int Column)
    {
        var x = (int)Math.Floor((double)Row / SquareRootOfGrid);
        var y = (int)Math.Floor((double)Column / SquareRootOfGrid);
        var val1 = CheckBox(x * SquareRootOfGrid, y * SquareRootOfGrid);
        var val2 = CheckColumn(Column);
        var val3 = CheckRow(Row);
        return val1 && val2 && val3;
    }
    internal bool CheckBox(int Row, int Column)
    {
        var knownNumbers = new List<int>();
        for (int i = 0; i < SquareRootOfGrid; i++)
        {
            for (int j = 0; j < SquareRootOfGrid; j++)
            {
                var tempNumber = Numbers[Row + i, Column + j];
                if (tempNumber == 0)
                {
                    continue;
                }
                else if (knownNumbers.Contains(tempNumber))
                {
                    return false;
                }
                knownNumbers.Add(tempNumber);
            }
        }
        return true;
    }

    private bool CheckRow(int Row)
    {
        var knownNumbers = new List<int>();
        for (int i = 0; i < SizeOfGrid; i++)
        {
            if (Numbers[Row, i] == 0)
            {
                continue;
            }
            else if (knownNumbers.Contains(Numbers[Row, i]))
            {
                return false;
            }
            knownNumbers.Add(Numbers[Row, i]);
        }
        return true;
    }

    private bool CheckColumn(int Column)
    {
        var knownNumbers = new List<int>();
        for (int i = 0; i < SizeOfGrid; i++)
        {
            if (Numbers[i, Column] == 0)
            {
                continue;
            }
            else if (knownNumbers.Contains(Numbers[i, Column]))
            {
                return false;
            }
            knownNumbers.Add(Numbers[i, Column]);

        }
        return true;
    }
}
