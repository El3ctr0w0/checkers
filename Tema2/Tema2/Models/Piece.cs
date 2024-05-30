using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2.View
{
    public class Piece
    {

        private short color;
        private string photo;
        private bool isQueen;

        //private short line;
        //private short column;

        public Piece() {

            color = 0;
            photo = "-";
            isQueen = false;

        }
        public Piece(string color)
        {
            if(color == "white")
            {
                this.color = 1;
                this.photo = "C:\\Csharp\\piesaRosie.jpg";
                isQueen=false;
            }
            else
            {
                this.color = 2;
                this.photo = "C:\\Csharp\\piesaMaro.jpg";
                isQueen = false;
            }
        }

        public void becomeQueen()
        {
            this.isQueen = true;
            if(this.color == 1)
            {
                this.photo = "C:\\Csharp\\ReginaRosie.jpg";
            }
            else
            {
                this.photo = "C:\\Csharp\\ReginaMaro.jpg";
            }
        }

        public bool isItQueen()
        {
            return isQueen;
            
        }

        public string getPhoto()
        {
            return photo;
        }
        public int getColor()
        {
            return color;
        }

    }
}
