using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tema2.Commands;
using Tema2.ViewModels;

namespace Tema2.Views
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : Window
    {
        public BoardViewModel viewModel { get; set; }

        public ICommand MoveCommand { get; set; }
        int playerTurn;

        bool pieces1 = true;
        bool pieces2 = true;

        bool firstRun = false;
        bool multipleJumps;

        public BoardView(bool multipleJumps)
        {
            InitializeComponent();
            initializeGame();
            MoveCommand = new MoveCommand(viewModel);
            playerTurn = 1;
            PlayerNameText.Text = "1";
            this.multipleJumps = multipleJumps;
            firstRun = true;

            DataContext = viewModel;
        }

        public BoardView(bool multipleJumps , int playerTurn , int[,] piecesLocation)
        {
            InitializeComponent();
            this.multipleJumps=multipleJumps;
            this.playerTurn = playerTurn;
            initializaLoadedGame(piecesLocation);

            MoveCommand = new MoveCommand(viewModel);
            fullyUpdateBoard();
            PlayerNameText.Text = playerTurn.ToString();
        }

        private void ButtonCommand(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonName = clickedButton.Name;
            int row = buttonName[1] -48 ;
            int column = buttonName[3] -48;

            if (viewModel.getCurrentColumn() == -1)
            {
                viewModel.setCurrentPiece(row, column);
                PieceSelectedColumn.Text = viewModel.CurrentColumn.ToString();
                PieceSelectedRow.Text = viewModel.CurrentLine.ToString();
            } else
            {
                viewModel.setFuturePiece(row, column);
                PieceSelectedColumn2.Text = viewModel.FutureColumn.ToString();
                PieceSelectedRow2.Text = viewModel.FutureLine.ToString();
            }
        }

        private void DeselectCommand(object sender, RoutedEventArgs e)
        {
            viewModel.setCurrentPiece( -1 , -1);
            PieceSelectedRow.Text = "";
            PieceSelectedColumn.Text = "";
        }

        

        public void initializeGame()
        {

            viewModel = new BoardViewModel();
            viewModel.initializeGame();
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++)
                {
                    string imagine = "ImageL" + i + "C" + j;
                    
                    if (viewModel.pieces[i,j] != null)
                    {
                        Image image = (Image)this.FindName(imagine);
                        image.Source = new BitmapImage(new Uri(viewModel.pieces[i, j].getPhoto()));
                    }
                }
            }
        }
        public void initializaLoadedGame(int[,] piecesLocation)
        {
            viewModel = new BoardViewModel();
            viewModel.LoadGame(piecesLocation);
            fullyUpdateBoard();
        }

        private void MoveButtonToNewPosition(object sender, RoutedEventArgs e)
        {


            if (multipleJumps == false)
            {
                Tuple<int, int, int, int, int> coordonate = new Tuple<int, int, int, int, int>(viewModel.CurrentLine, viewModel.CurrentColumn, viewModel.FutureLine, viewModel.FutureColumn, playerTurn);
                if (MoveCommand.CanExecute(coordonate) == true)
                {
                    Tuple<int, int, int, int, int, bool> coordonate2 = new Tuple<int, int, int, int, int, bool>(viewModel.CurrentLine, viewModel.CurrentColumn, viewModel.FutureLine, viewModel.FutureColumn, playerTurn, false);
                    MoveCommand.Execute(coordonate2);
                    fullyUpdateBoard();
                    if (playerTurn == 1)
                        playerTurn = 2;
                    else playerTurn = 1;
                }
            }
            else
            {
                Tuple<int, int, int, int, int> coordonate = new Tuple<int, int, int, int, int>(viewModel.CurrentLine, viewModel.CurrentColumn, viewModel.FutureLine, viewModel.FutureColumn, playerTurn);
                if (MoveCommand.CanExecute(coordonate) == true)
                {
                    
                    Tuple<int, int, int, int, int, bool> coordonate2 = new Tuple<int, int, int, int, int, bool>(viewModel.CurrentLine, viewModel.CurrentColumn, viewModel.FutureLine, viewModel.FutureColumn, playerTurn, true);
                    MoveCommand.Execute(coordonate2); //treb modificat in view model line si column
                    fullyUpdateBoard();
                    if (playerTurn == 1)
                        playerTurn = 2;
                    else playerTurn = 1;
                    
                }
            }


        }



        private void updateBoard(Tuple<int, int, int, int,int> coordonate)
        {
            string imagine = "ImageL" + coordonate.Item1 + "C" + coordonate.Item2;
            Image image = (Image)this.FindName(imagine);
            string imagine2 = "ImageL" + coordonate.Item3 + "C" + coordonate.Item4;
            Image image2 = (Image)this.FindName(imagine2);

            image2.Source = image.Source;
            image.Source = null;
            if(playerTurn == 1)
                PlayerNameText.Text = "2";
            else PlayerNameText.Text = "1";
        }
        private void fullyUpdateBoard()
        {
            pieces1 = false;
            pieces2 = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string imagine = "ImageL" + i + "C" + j;
                    Image image = (Image)this.FindName(imagine);
                    if (viewModel.pieces[i, j] != null)
                    {
                        image.Source = new BitmapImage(new Uri(viewModel.pieces[i, j].getPhoto()));
                        if (viewModel.pieces[i, j].getColor() == 1)
                            pieces1 = true;
                        else pieces2 = true;
                    }
                    else image.Source = null;
                }
            }
            if (pieces1 == false || pieces2 == false)
            {
                MessageBox.Show("Game ended");

                if (pieces1 == false)
                {
                    MessageBox.Show("Player2 won");
                }
                else MessageBox.Show("Player1 won");
                Tema2.Commands.WriteCommand writeCommand = new Tema2.Commands.WriteCommand();
                writeCommand.StatsUpdate(playerTurn);
                this.Close();
            }
            if (firstRun == false)
            {
                firstRun = true;
            }
            else
            {
                if (playerTurn == 1)
                    PlayerNameText.Text = "2";
                else PlayerNameText.Text = "1";
            }
        }
        private void deleteFromBoard(Tuple<int,int> coordonate)
        {
            string imagine = "ImageL" + coordonate.Item1 + "C" + coordonate.Item2;
            Image image = (Image)this.FindName(imagine);
            image.Source = null;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Tema2.Commands.WriteCommand writeCommand = new Tema2.Commands.WriteCommand();
            writeCommand.SaveGame("C:\\Csharp\\SaveData.txt", viewModel.pieces , playerTurn);    
        }
    }
}