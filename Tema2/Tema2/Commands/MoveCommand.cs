using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema2.ViewModels;

namespace Tema2.Commands
{
    class MoveCommand : ICommand
    {
        private readonly BoardViewModel _viewModel;
        public MoveCommand(BoardViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is Tuple<int, int, int, int, int> moveParams)
            {
                int fromLine = moveParams.Item1;
                int fromColumn = moveParams.Item2;
                int toLine = moveParams.Item3;
                int toColumn = moveParams.Item4;
                int playerTurn = moveParams.Item5;

                if (toLine > 7 || toLine < 0 || toColumn > 7 || toColumn < 0) { return false; }

                if (_viewModel.pieces[toLine,toColumn] != null)
                {
                    if (toLine > 6 || toColumn > 6 || toLine < 1 || toColumn < 1)
                        return false;
                }
                if (_viewModel.pieces[fromLine, fromColumn] == null)
                    return false;

                if (_viewModel.pieces[fromLine, fromColumn].isItQueen() != true)
                {
                    if (playerTurn == 1)
                    {
                        if (_viewModel.pieces[fromLine, fromColumn].getColor() == 1) //daca piese selectata de el e a lui
                        {
                            if (fromLine + 1 == toLine && fromColumn + 1 == toColumn) //verificam daca merge jos dreapta
                            {
                                if (_viewModel.pieces[toLine, toColumn] != null) //daca nu e liber locul
                                {
                                    if (_viewModel.pieces[toLine, toColumn].getColor() != playerTurn) //se verifica daca nu e piesa lui
                                    {
                                        if (toLine + 1 < 8 && toColumn + 1 < 8)
                                        {
                                            if (_viewModel.pieces[toLine + 1, toColumn + 1] == null) //si daca nu este , se verifica daca este liber urmatorul spatiu de dupa piesa si daca exista
                                                return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                }
                                else return true;
                            }
                            else if (fromLine + 1 == toLine && fromColumn - 1 == toColumn) //verificam daca merge jos stanga
                            {
                                if (_viewModel.pieces[toLine, toColumn] != null) //verificam daca este o piesa deja acolo
                                {
                                    if (_viewModel.pieces[toLine, toColumn].getColor() != playerTurn) //daca nu este piesa lui
                                    {
                                        if (toLine + 1 < 8 && toColumn - 1 >= 0) {
                                            if (_viewModel.pieces[toLine + 1, toColumn - 1] == null) //verificam daca dupa acea piesa e liber locul si exista
                                                return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                    else return false;
                                }
                                else return true;
                            }
                        }

                    }
                    else
                    {
                        if (_viewModel.pieces[fromLine, fromColumn].getColor() == 2)
                            if (fromLine - 1 == toLine && fromColumn + 1 == toColumn) //verificam daca merge sus dreapta
                            {
                                if (_viewModel.pieces[toLine, toColumn] != null) //daca nu e liber locul
                                {
                                    if (_viewModel.pieces[toLine, toColumn].getColor() != playerTurn) //se verifica daca nu e piesa lui
                                    {
                                        if (toLine - 1 >= 0 && toColumn + 1 < 8)
                                        {
                                            if (_viewModel.pieces[toLine - 1, toColumn + 1] == null) //si daca nu este , se verifica daca este liber urmatorul spatiu de dupa piesa si daca exista
                                                return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                }
                                else return true;
                            }
                            else if (fromLine - 1 == toLine && fromColumn - 1 == toColumn) //verificam daca merge sus stanga
                            {
                                if (_viewModel.pieces[toLine, toColumn] != null) //verificam daca este o piesa deja acolo
                                {
                                    if (_viewModel.pieces[toLine, toColumn].getColor() != playerTurn) //daca nu este piesa lui
                                    {
                                        if (toLine - 1 >= 0 && toColumn - 1 >= 0)
                                        {
                                            if (_viewModel.pieces[toLine - 1, toColumn - 1] == null) //verificam daca dupa acea piesa e liber locul si exista
                                                return true;
                                            else return false;
                                        }
                                        else return false;
                                    }
                                    else return false;
                                }
                                else return true;
                            }
                    }

                }
                else
                {
                    if (playerTurn == 1 && _viewModel.pieces[fromLine,fromColumn].getColor()==1 || playerTurn == 2 && _viewModel.pieces[fromLine, fromColumn].getColor() == 2) //daca este tura jucatorului 1 si are piesa lui selectata sau -||- 2
                    {
                        if (_viewModel.pieces[toLine, toColumn] == null && toLine-fromLine<2 && toLine-fromLine>-2 && toColumn-fromColumn<2 && toColumn-fromColumn>-2) //se verifica daca locul selectat de el este ocupat sau nu.
                                return true;
                        else
                        {
                            if(toLine-fromLine==1)
                            {
                                if(toColumn-fromColumn==1)
                                {
                                    if (_viewModel.pieces[toLine + 1, toColumn + 1] == null)
                                        return true;
                                }
                                else if(toColumn-fromColumn==0)
                                {
                                    if (_viewModel.pieces[toLine + 1, toColumn] == null)
                                        return true;
                                }
                                else if(toColumn-fromColumn==-1) { if (_viewModel.pieces[toLine + 1, toColumn - 1] == null) return true; }
                            }
                            else if (toLine - fromLine == 0)
                            {
                                if (toColumn - fromColumn == -1) { if (_viewModel.pieces[toLine, toColumn - 1] == null) return true; }
                                else if (toColumn - fromColumn == +1) { if (_viewModel.pieces[toLine, toColumn + 1] == null) return true; }
                            }
                            else if (toLine - fromLine == -1)
                            {
                                if (toColumn - fromColumn == -1) { if (_viewModel.pieces[toLine - 1, toColumn - 1] == null) return true; }
                                else if (toColumn - fromColumn == +1) { if (_viewModel.pieces[toLine - 1, toColumn + 1] == null) return true; }
                                else if (toColumn - fromColumn == 0) { if (_viewModel.pieces[toLine - 1, toColumn] == null) return true; }
                            }
                        }
                    } 
                }
            }
            return false;
        }

        public void Execute(object parameter)
        {
            //if (CanExecute(parameter))
            // {
            if (parameter is Tuple<int, int, int, int, int, bool> moveParams)
            {
                int fromLine = moveParams.Item1;
                int fromColumn = moveParams.Item2;
                int toLine = moveParams.Item3;
                int toColumn = moveParams.Item4;
                int playerTurn = moveParams.Item5;

                Tuple<int, int, int, int, int> coordonate = new Tuple<int, int, int, int, int>(moveParams.Item1, moveParams.Item2, moveParams.Item3, moveParams.Item4, moveParams.Item5);


                if (moveParams.Item6 == false)
                {
                    if (CanExecute(coordonate))
                        OneExecute(coordonate);
                }
                else MultipleExecute(coordonate);
            }
            //}
        }

        public void OneExecute(object parameter)
        {
            if (parameter is Tuple<int, int, int, int, int> moveParams)
            {
                int fromLine = moveParams.Item1;
                int fromColumn = moveParams.Item2;
                int toLine = moveParams.Item3;
                int toColumn = moveParams.Item4;
                int playerTurn = moveParams.Item5;


                if (_viewModel.pieces[fromLine, fromColumn].isItQueen() == false)
                {
                    if (_viewModel.pieces[toLine, toColumn] == null)
                    {
                        _viewModel.pieces[toLine, toColumn] = _viewModel.pieces[fromLine, fromColumn];
                        _viewModel.pieces[fromLine, fromColumn] = null;
                        if (playerTurn == 1 && toLine == 7)
                            _viewModel.pieces[toLine, toColumn].becomeQueen();
                        if (playerTurn == 2 && toLine == 0)
                            _viewModel.pieces[toLine, toColumn].becomeQueen();
                    }
                    else
                    {
                        if (toLine - fromLine == -1 && toColumn - fromColumn == -1)
                        {
                            _viewModel.pieces[toLine - 1, toColumn - 1] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;
                            if (playerTurn == 2 && toLine - 1 == 0)
                                _viewModel.pieces[toLine - 1, toColumn - 1].becomeQueen();

                        }
                        if (toLine - fromLine == -1 && toColumn - fromColumn == +1)
                        {
                            _viewModel.pieces[toLine - 1, toColumn + 1] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;
                            if (playerTurn == 2 && toLine - 1 == 0)
                                _viewModel.pieces[toLine - 1, toColumn + 1].becomeQueen();
                        }
                        if (toLine - fromLine == +1 && toColumn - fromColumn == -1)
                        {
                            _viewModel.pieces[toLine + 1, toColumn - 1] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;
                            if (playerTurn == 1 && toLine + 1 == 7)
                                _viewModel.pieces[toLine + 1, toColumn - 1].becomeQueen();
                        }
                        if (toLine - fromLine == +1 && toColumn - fromColumn == +1)
                        {
                            _viewModel.pieces[toLine + 1, toColumn + 1] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;
                            if (playerTurn == 1 && toLine + 1 == 7)
                                _viewModel.pieces[toLine + 1, toColumn + 1].becomeQueen();
                        }
                    }
                }
                else
                {

                    if (_viewModel.pieces[toLine, toColumn] == null)
                    {
                        _viewModel.pieces[toLine, toColumn] = _viewModel.pieces[fromLine, fromColumn];
                        _viewModel.pieces[fromLine, fromColumn] = null;
                    }
                    else
                    {
                        if (toLine - fromLine == -1 && toColumn - fromColumn == -1)
                        {
                            _viewModel.pieces[toLine - 1, toColumn - 1] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;

                        }
                        else if (toLine - fromLine == -1 && toColumn - fromColumn == +1)
                        {
                            _viewModel.pieces[toLine - 1, toColumn + 1] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;
                        }
                        else if (toLine - fromLine == -1 && toColumn - fromColumn == 0)
                        {
                            _viewModel.pieces[toLine - 1, toColumn] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;
                        }


                        else if (toLine - fromLine == +1 && toColumn - fromColumn == -1)
                        {
                            _viewModel.pieces[toLine + 1, toColumn - 1] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;

                        }
                        else if (toLine - fromLine == +1 && toColumn - fromColumn == +1)
                        {
                            _viewModel.pieces[toLine + 1, toColumn + 1] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;

                        }
                        else if (toLine - fromLine == +1 && toColumn - fromColumn == 0)
                        {
                            _viewModel.pieces[toLine + 1, toColumn] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;

                        }

                        else if (toLine - fromLine == 0 && toColumn - fromColumn == -1)
                        {
                            _viewModel.pieces[toLine, toColumn - 1] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;

                        }
                        else if (toLine - fromLine == 0 && toColumn - fromColumn == 1)
                        {
                            _viewModel.pieces[toLine, toColumn + 1] = _viewModel.pieces[fromLine, fromColumn];
                            _viewModel.pieces[toLine, toColumn] = null;
                            _viewModel.pieces[fromLine, fromColumn] = null;

                        }


                    }
                }
            }
        }

        public void MultipleExecute(object parameter)
        {
            if (parameter is Tuple<int, int, int, int, int> moveParams)
            {
                int fromLine = moveParams.Item1;
                int fromColumn = moveParams.Item2;
                int toLine = moveParams.Item3;
                int toColumn = moveParams.Item4;
                int playerTurn = moveParams.Item5;
                bool becomesQueen = false;

                bool jumpedAPiece = true;
                bool continueLoop = true;

                if (_viewModel.pieces[toLine, toColumn] == null) 
                { 
                    if (CanExecute(parameter))
                    { 
                        OneExecute(parameter);
                        _viewModel.CurrentColumn = toColumn;
                        _viewModel.CurrentLine = toLine;
                    } 
                }
                else
                {
                    if (_viewModel.pieces[fromLine,fromColumn].isItQueen()==true)
                        becomesQueen=true;

                    while (jumpedAPiece && continueLoop)
                    {
                        jumpedAPiece = false;

                        for (int i = -1; i <= 1; i ++)
                        {
                            for (int j = -1; j <= 1; j ++)
                            {
                                int targetLine = fromLine + i;
                                int targetColumn = fromColumn + j;

                                if (targetLine > 0 && targetLine < 7)
                                    if (targetColumn > 0 && targetColumn < 7)
                                    {
                                        if (targetLine >= 0 && targetLine < 7 && targetColumn >= 0 && targetColumn < 7)
                                        {
                                            Tuple<int, int, int, int, int> newMoveParams = new Tuple<int, int, int, int, int>(fromLine, fromColumn, targetLine, targetColumn, playerTurn);
                                            _viewModel.FutureLine = newMoveParams.Item3;
                                            _viewModel.FutureColumn = newMoveParams.Item4;
                                            if (_viewModel.pieces[targetLine, targetColumn] != null)
                                            {
                                                if (CanExecute(newMoveParams))
                                                {
                                                    OneExecute(newMoveParams);
                                                    jumpedAPiece = true;
                                                    fromLine = targetLine + i;
                                                    fromColumn = targetColumn + j;
                                                    targetLine = fromLine;
                                                    targetColumn = fromColumn;
                                                    _viewModel.CurrentLine = fromLine;
                                                    _viewModel.CurrentColumn = fromColumn;
                                                    if (_viewModel.pieces[fromLine, fromColumn].isItQueen() == true && becomesQueen == false)
                                                        continueLoop = false;
                                                    break;
                                                }
                                            }
                                            
                                        }
                                    }
                            }
                        }
                    }
                }
            }
        }


        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
