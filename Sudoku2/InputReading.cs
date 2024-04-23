using System.Windows.Controls;

namespace Sudoku2;

internal class InputReading
{
    static internal int[,] ReadInputs(int SizeOfGrid, TextBox[,] TextBoxes)
    {
        int[,] results = new int[9,9];
        for(int i = 0; i< SizeOfGrid; i++)
        {
            for(int j = 0; j< SizeOfGrid; j++)
            {
                try
                {
                    if(TextBoxes[i, j].Text == "")
                    {
                        results[i, j] = 0;
                        continue;
                    }
                    var temp = Convert.ToInt32(TextBoxes[i, j].Text);
                    if(temp >9 || temp < 0)
                    {
                        results[i, j] = -1;
                        continue;
                    }
                    results[i, j] = temp;
                }
                catch(Exception ex)
                {
                    results[i, j] = -1;
                }
            }
        }
        return results;
    }
}
