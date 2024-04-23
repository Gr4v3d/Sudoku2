using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sudoku2;

internal class Search
{
    private int[,] Numbers;
    private TextBox[,] TextBoxes;
    private int SizeOfGrid;
    private int SquareRootOfGrid;

    internal Search(int[,] numbers, TextBox[,] textBoxes)
    {
        Numbers = numbers;
        TextBoxes = textBoxes;
        SizeOfGrid = Numbers.GetLength(0);
        SquareRootOfGrid = ((int)Math.Floor(Math.Sqrt(SizeOfGrid)));
    }

    internal bool LookThrough(Grid Root)
    {
        var result = true;
        for(int i = 0; i < SizeOfGrid; i++)
        {
            for(int j = 0;  j < SizeOfGrid; j++)
            {
                var x = (int)Math.Floor((double)i / SquareRootOfGrid);
                var y = (int)Math.Floor((double)j / SquareRootOfGrid);
                var border = (Border)Root.FindName($"border{x}{y}");
                if (!CheckAll(i, j, border)) result = false;
            }
        }
        return result;
    }
    internal bool CheckAll(int Row, int Column, Border border)
    {
        var x = (int)Math.Floor((double)Row / SquareRootOfGrid);
        var y = (int)Math.Floor((double)Column / SquareRootOfGrid);
        var val1 = CheckBox(x * SquareRootOfGrid, y * SquareRootOfGrid, border);
        var val2 = CheckColumn(Column);
        var val3 = CheckRow(Row);
        return val1 && val2 && val3;
    }
    internal bool CheckBox(int Row, int Column,Border border)
    {
        var result = true;
        var knownNumbers = new List<int>();
        for(int i = 0; i < SquareRootOfGrid; i++)
        {
            for(int j = 0; j < SquareRootOfGrid; j++)
            {
                var tempNumber = Numbers[Row + i, Column + j];
                if(tempNumber == -1)
                {
                    result = false;
                    TextBoxes[Row + i, Column + j].Foreground = Brushes.Blue;
                    knownNumbers.Add(tempNumber);
                    continue;
                }
                else if(tempNumber == 0)
                {
                    result = false;
                    TextBoxes[Row + i, Column + j].Background = Brushes.Yellow;
                    knownNumbers.Add(tempNumber);
                    continue ;
                }
                if (knownNumbers.Contains(tempNumber))
                {
                    border.BorderBrush = Brushes.Red;
                    border.BorderThickness = new Thickness(3);
                    result = false;
                    continue;
                }
                else knownNumbers.Add(tempNumber);
            }
        }
        return result;
    }

    private bool CheckRow(int Row)
    {
        var result = true;
        var knownNumbers = new List<int>();
        for (int i = 0; i < SizeOfGrid; i++)
        {
            if (Numbers[Row,i] == -1)
            {
                result = false;
                TextBoxes[Row,i].Foreground = Brushes.Blue;
            }
            else if (Numbers[Row, i] == 0)
            {
                result = false;
                TextBoxes[Row, i].Background = Brushes.Yellow;
            }
            else if (knownNumbers.Contains(Numbers[Row, i]))
            {
                var index = knownNumbers.IndexOf(Numbers[Row, i]);
                TextBoxes[Row, i].Foreground = Brushes.Red;
                TextBoxes[Row, index].Foreground = Brushes.Red;
                result = false;
            }
            knownNumbers.Add(Numbers[Row, i]);

        }
        return result;
    }

    private bool CheckColumn(int Column)
    {
        var result = true;
        var knownNumbers = new List<int>();
        for(int i = 0; i < SizeOfGrid; i++)
        {
            if (Numbers[i,Column] == -1)
            {
                result = false;
                TextBoxes[i,Column].Foreground = Brushes.Blue;
            }
            else if (Numbers[i,Column] == 0)
            {
                result = false;
            }            
            else if (knownNumbers.Contains(Numbers[i,Column])) 
            {
                var index = knownNumbers.IndexOf(Numbers[i, Column]);
                TextBoxes[i, Column].Foreground = Brushes.Red;
                TextBoxes[index,Column].Foreground = Brushes.Red;
                result = false; 
            }
            knownNumbers.Add(Numbers[i,Column]);
            
        }
        return result;
    }
}
