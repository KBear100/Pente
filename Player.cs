using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Pente
{
    class Player
    {
        public Player(Brush playerColor, string playerName = "Player")
        {
            this.playerColor = playerColor;
            this.playerName = playerName;
        }

        public Brush playerColor;
        public string playerName;

    }
}
