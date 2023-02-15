namespace Connect4;

using System;
public class MainMenu {
    public void Start() { // static void Main(string[] args)

        int[,] gameBoard = new int[6, 7];
        int gridCols = gameBoard.Length/gameBoard.GetLength(0);
        int gridRows = gameBoard.GetLength(0);
        
        int[] colProgress = new int[gridCols];
        Array.Fill(colProgress, gridRows-1);

        int turn = gameBoard.Length;
        int currPlayer = 1;
        int numberToWin = 4;

        int player1 = 1;
        int player2 = 2;
        

        
//*
        while(turn != 0) {

            drawGrid(gameBoard);
            // Console.ForegroundColor = (currPlayer == player1) ? ConsoleColor.Red : ConsoleColor.Blue;
            // promptRow(currPlayer);

        //* PROMPTING USER FOR COLUMN INPUT
            Console.ForegroundColor = (currPlayer == player1) ? ConsoleColor.Red : ConsoleColor.Blue;
            int playerCol = promptCol(currPlayer) - 1;
        //*/

        // IF PLAYER DID NOT INPUT A VALUE OR A VALID COLUMN, PROMPT PLAYER TO TRY AGAIN
            if(playerCol > gridCols-1 || playerCol < 0){
                Console.WriteLine("Invalid Column. Please input a number 1 through {0}\n", gridCols);
                continue;
            }
        // IF COLUMN IS FULL, PROMPT PLAYER TO TRY AGAIN
            if(colProgress[playerCol] < 0) {
                Console.WriteLine("That column is full. Choose another column.\n");
                continue;
            }
        // IF ABLE TO, PLACE PIECE IN DESIRED COLUMN, AND CHECK WIN
            if(gameBoard[colProgress[playerCol], playerCol] == 0) {
                gameBoard[colProgress[playerCol], playerCol] = currPlayer;
                int[] coords = {colProgress[playerCol], playerCol};
                bool playerWon = checkWin(gameBoard, currPlayer, numberToWin, coords); // REMEMBER to adjust numToWin accordingly
                colProgress[playerCol]--;
                if(playerWon) {
                    drawGrid(gameBoard);
                    Console.ForegroundColor = currPlayer == player1 ? ConsoleColor.Red : ConsoleColor.Blue;
                    displayWin(currPlayer);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }   

        // ALTERNATE BETWEEN PLAYER 1 AND 2
            currPlayer = (currPlayer == player1) ? player2 : player1;
        // LIMITS HOW MANY TIMES PLAYERS CAN GO ACCUMULATIVELY
            turn--; 
        }
//*/
    }

    public static int promptRow(int currPlayer){
        Console.Write("Row: ", currPlayer);
        Console.ForegroundColor = ConsoleColor.White;
        int playerRow = 0;
        bool playerInput = int.TryParse(Console.ReadLine(), out playerRow);
        return playerInput ? playerRow : -1;
    }

    public static int promptCol(int currPlayer){
        Console.Write("Column: ");
        Console.ForegroundColor = ConsoleColor.White;
        int playerCol = 0;
        bool playerInput = int.TryParse(Console.ReadLine(), out playerCol);
        return playerInput ? playerCol : -1;
    }

    public static void drawGrid(int[,] grid) {
        int gridRows = grid.GetLength(0);
        int gridCols = grid.Length/grid.GetLength(0);

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.WriteLine("*****************************************************");
        for(int i = 0; i < gridRows; i++) {
            Console.Write("     ");
            for(int k = 0; k < gridCols; k++) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("|     ");
                if(k == gridCols - 1) Console.Write("|");
            }
            Console.Write("\n     ");
            for(int j = 0; j < gridCols; j++) {
                int val = grid[i, j];
                switch (val)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("  O  ");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("  O  ");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;                        
                    default: 
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("|");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("  -  ");
                        break;
                }
                if(j == gridCols - 1) {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("|");
                }
            }
            Console.Write("\n     ");
            for(int k = 0; k < gridCols; k++) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("|     ");
                if(k == gridCols - 1) Console.Write("|");
            }
            Console.Write("\n     -");
            for(int k = 0; k < gridCols; k++) {
                Console.Write("------");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
        Console.Write("     ");
        for(int i = 0; i < gridCols; i++) {
            Console.Write("   {0}  ", i + 1);
        }
        Console.WriteLine("\n       ---   ---   ---   ---   ---   ---   ---  \n");
    }

    private static bool checkWin(int[,] grid, int player, int numToWin, int[] coord) {
        int gridRows = grid.GetLength(0);
        int gridCols = grid.Length/grid.GetLength(0);
        int y = coord[0]; // rows
        int x = coord[1]; // cols
        int startingY;
        int startingX = x - numToWin;

        int winV = 0;
        int winH = 0;
        startingY = y - numToWin;
        for(int i = 0; i < numToWin*2; i++) {
            if(startingX+i > gridCols-1 || startingY+i > gridRows-1) break;
            if(startingX+i < 0 || startingY+i < 0) continue;
            
            // CHECKING FOR HORIZONTAL WIN
            if(grid[y, startingX+i] == player) {
                winH++;
                if(winH == numToWin) return true;
            } else winH = 0;
            
            // CHECKING FOR VERTICAL WIN
            if(grid[startingY+i, x] == player) {
                winV++;
                if(winV == numToWin) return true;
            } else winV = 0;
        }


        // CHECK FOR LEFT DOWN DIAGONAL WIN
        int diagCountLD = 0;
        for(int i = 0; i < numToWin*2; i++) {
            if(startingX + i < 0 || startingY + i < 0) continue;
            if(startingX+i > gridCols-1 || startingY+i > gridRows-1) break;
            if(grid[startingY + i, startingX + i] == player) {
                diagCountLD++;
                if(diagCountLD == numToWin) return true;
            }
            else diagCountLD = 0;
        }

        // CHECK FOR RIGHT UP DIAGONAL WIN
        startingY = y + numToWin;
        int diagCountRU = 0;
        for(int i = 0; i < numToWin*2; i++) {
            if(startingX + i < 0 || startingY-i > gridRows-1) continue;
            if(startingX+i > gridCols-1 || startingY - i < 0) break;
            if(grid[startingY - i, startingX + i] == player) {
                diagCountRU++;
                if(diagCountRU == numToWin) return true;
            }
            else diagCountRU = 0;
        }
        return false;
    }
    
    public static void displayWin(int player) {
        Console.WriteLine(@"
        ***************************************
                  PLAYER {0} WON !!!           
        ***************************************
        ", player);
    }


}