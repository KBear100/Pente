using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pente
{
    class Board
    {
        public static int numPlayers = 0;
        public static string player1Name = string.Empty;
        public static string player2Name = string.Empty;
        public static string currentPlayerName = string.Empty;
        public static int boardSize = 19;
        public static bool loadedGame = false;
        public static List<int> redIndexs = new List<int>();
        public static List<int> blueIndexs = new List<int>();
        public static int nonButtons = 2;
    }
}
