using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sudoku2;
public partial class MainWindow : Window
{
    private TextBox[,] Boxes;
    private static int GridSize = 9;
    private int SquareRootOfGrid = 3;
    public MainWindow()
    {
        InitializeComponent();
        VisualGen gen = new VisualGen(root);
        Boxes = gen.Generate(GridSize,SquareRootOfGrid);
        var button = (Button)root.FindName("buttonFinish");
        button.Click += new RoutedEventHandler(Game);
        Start();
    }
    private void Start()
    {
        var clues = new int();
        HomeScreen homeScreen = new HomeScreen();
        homeScreen.ClueNumber += value => clues = value;
        homeScreen.ShowDialog();
        var gameGen = new SudokuGeneration(Boxes, clues);
        Boxes = gameGen.SetUp();
    }

    private void Game(object sender, RoutedEventArgs e)
    {
        ReSet();
        var readValues = InputReading.ReadInputs(GridSize,Boxes);
        var search = new Search(readValues, Boxes);
        if(search.LookThrough(root) == true)
        { 
            var result = MessageBox.Show("Czy chcesz zagrać ponownie?", "Wygrałeś!", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Start();
                    break;
                case MessageBoxResult.No:
                    Application.Current.Shutdown();
                    break;
            }
        }
    }
    private void ReSet()
    {
        for(int i = 0; i <GridSize; i++)
        {
            for(int j = 0; j <GridSize; j++) 
            {
                Boxes[i, j].Foreground = Brushes.Black;
                Boxes[i, j].Background = Brushes.White;
                var border = (Border)root.FindName($"border{i%SquareRootOfGrid}{j%SquareRootOfGrid}");
                border.BorderThickness = new System.Windows.Thickness(1);
                border.BorderBrush = Brushes.Navy;
            }
        }
    }
}