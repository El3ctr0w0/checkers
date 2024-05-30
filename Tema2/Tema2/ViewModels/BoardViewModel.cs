using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Tema2.View;

namespace Tema2.ViewModels
{
    public class BoardViewModel
    {
        int currentLine, currentColumn;
        int futureLine, futureColumn;


        public Piece[,] pieces;

        public event PropertyChangedEventHandler PropertyChanged;

        public int CurrentLine
        {
            get { return currentLine; }
            set
            {
                if (currentLine != value && value < 8)
                {
                    currentLine = value;
                    OnPropertyChanged(nameof(CurrentLine));
                }
            }
        }

        public int CurrentColumn
        {
            get { return currentColumn; }
            set
            {
                if (currentColumn != value && value<8)
                {
                    currentColumn = value;
                    OnPropertyChanged(nameof(CurrentColumn));
                }
            }
        }

        public int FutureLine
        {
            get { return futureLine; }
            set
            {
                if (currentLine != value && value < 8)
                {
                    futureLine = value;
                    OnPropertyChanged(nameof(CurrentLine));
                }
            }
        }

        public int FutureColumn
        {
            get { return futureColumn; }
            set
            {
                if (currentColumn != value && value < 8)
                {
                    futureColumn = value;
                    OnPropertyChanged(nameof(CurrentColumn));
                }
            }
        }
        public BoardViewModel()
        {
            pieces = new Piece[8, 8];

        }

        public void setCurrentPiece(int row , int column)
        {
            if (row < 8 && column < 8)
            {
                currentLine = row;
                currentColumn = column;
                OnPropertyChanged(nameof(CurrentColumn));
                OnPropertyChanged(nameof(CurrentLine));
            }
        }
        public void setFuturePiece(int row , int column) 
        {
            if (row < 8 && column < 8)
            {
                futureLine = row;
                futureColumn = column;
                OnPropertyChanged(nameof(CurrentColumn));
                OnPropertyChanged(nameof(CurrentLine));
            }
        }
        public int getCurrentLine() { return currentLine; }
        public int getCurrentColumn() { return currentColumn;}

        public int getFutureLine() { return futureLine; }
        public int getFutureColumn() {  return futureColumn; }

        public void initializeGame()
        {
            for(int i=0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if( (i+j)%2==1)
                        pieces[i, j] = new Piece("white");
                    if((i+j) %2==0 )
                        pieces[7 - i, j] = new Piece("black");
                }
            }
        }

        public void LoadGame(int[,] piecesLocation)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (piecesLocation[i, j] == 1)
                        pieces[i, j] = new Piece("white");
                    else if (piecesLocation[i, j] == 2)
                        pieces[i, j] = new Piece("black");
                    else if (piecesLocation[i, j] == 3)
                    { pieces[i, j] = new Piece("white"); pieces[i, j].becomeQueen(); }
                    else if (piecesLocation[i, j] == 4)
                    {
                        pieces[i, j] = new Piece("black"); pieces[i, j].becomeQueen();
                    }
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
