using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Sudoku2;

internal class VisualGen
{
    private Grid Root;
    internal VisualGen(Grid root){ Root = root; }

    internal TextBox[,] Generate(int SizeOfGrid,int SquareRootOfGrid)
    {
        GridSetup(Root,SquareRootOfGrid);
        AddBorders(Root, SquareRootOfGrid);
        AddInnerGrids(Root, SquareRootOfGrid);
        AddButton(Root);
        return CreateTextBoxes(Root, SizeOfGrid, SquareRootOfGrid);
    }

    private void AddButton(Grid root)
    {
        root.RowDefinitions.Add(new RowDefinition());

        var button = new Button();
        button.Name = "buttonFinish";
        root.Children.Add(button);
        root.RegisterName(button.Name, button);
        Grid.SetColumn(button, 1);
        Grid.SetRow(button, 3);
    }

    private TextBox[,] CreateTextBoxes(Grid Root, int SizeOfGrid, int SquareRootOfGrid)
    {
        TextBox[,] textBoxes = new TextBox[SizeOfGrid,SizeOfGrid];
        for(int i = 0; i < SquareRootOfGrid; i++)
        {
            for(int j = 0; j < SquareRootOfGrid; j++)
            {
                var obj = (Grid)Root.FindName($"grid{i}{j}");
                for(int k = 0; k < SquareRootOfGrid; k++)
                {
                    for(int l = 0; l < SquareRootOfGrid; l++)
                    {
                        var row = i * SquareRootOfGrid + k;
                        var column = j * SquareRootOfGrid + l;
                        textBoxes[row, column] = new TextBox();
                        textBoxes[row, column].HorizontalContentAlignment = HorizontalAlignment.Center;
                        textBoxes[row, column].VerticalContentAlignment = VerticalAlignment.Center;
                        textBoxes[row, column].FontSize = 26;
                        textBoxes[row, column].Margin = new System.Windows.Thickness(1);
                        textBoxes[row, column].IsEnabled = false;
                        textBoxes[row,column].MaxLength = 1;
                        obj.Children.Add(textBoxes[row, column]);
                        Grid.SetRow(textBoxes[row, column], k);
                        Grid.SetColumn(textBoxes[row,column], l);
                    }
                }
            }
        }
        return textBoxes;
    }

    private void AddInnerGrids(Grid GridObject,int SquareRootOfGrid)
    {
        for(int i = 0; i < SquareRootOfGrid; i++)
        {
            for(int j = 0; j < SquareRootOfGrid; j++)
            {
                var border = (Border)GridObject.FindName($"border{i}{j}");
                var innerGrid = new Grid();
                innerGrid.Name = $"grid{i}{j}";
                border.Child = innerGrid;
                GridObject.RegisterName(innerGrid.Name, innerGrid);
                Grid.SetRow(innerGrid, i);
                Grid.SetColumn(innerGrid,j);
                GridSetup(innerGrid, SquareRootOfGrid);
            }
        }
    }

    private void GridSetup(Grid GridObject,int SquareRootOfGrid)
    {
        for(int  i = 0; i < SquareRootOfGrid ; i++)
        {
            GridObject.RowDefinitions.Add(new RowDefinition());
            GridObject.ColumnDefinitions.Add(new ColumnDefinition());
        }
    }

    private void AddBorders(Grid GridObject, int SquareRootOfGrid)
    {
        for (int i = 0; i < SquareRootOfGrid; i++)
        {
            for (int j = 0; j < SquareRootOfGrid; j++)
            {
                var border = new Border();
                border.Name = $"border{i}{j}";
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = new SolidColorBrush(Colors.Navy);
                border.Background = new SolidColorBrush(Colors.Pink);
                border.Padding = new Thickness(1);
                GridObject.Children.Add(border);
                GridObject.RegisterName(border.Name, border);
                Grid.SetColumn(border, j);
                Grid.SetRow(border, i);
            }
        }
    }
}
