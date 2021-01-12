using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase_Test1
{
    
    public partial class Form1 : Form
    {
        bool turn = true;
        int playerOneCount = 0, drawCount = 0, playerTwoCount =0, turnCount = 0;
        bool twoPlayersOff = true;
        bool winner = false;
        public Form1()
        {
          
            InitializeComponent();
            
        }
        private void button_Click(object sender, EventArgs e)
        {

            turnCount++;
            Button button = (Button)sender;
            if(turn)
            { 
                        button.Text = "X";                       
            }
            else {
                    button.Text = "O";
                
            }
            turn = !turn;
                
            button.Enabled = false;
            
            ScoreCount();
            if (twoPlayersOff && !turn && !winner)
            {
                computer_make_move();
            }

        }
        private void  ScoreCount()
        {
            
            if (!A1.Enabled && A1.Text == A2.Text && A2.Text == A3.Text)
                winner = true;
            if (!A1.Enabled && A1.Text == B1.Text && B1.Text == C1.Text)
                winner = true;
            if (!B1.Enabled && B1.Text == B2.Text && B2.Text == B3.Text)
                winner = true;
            if (!C1.Enabled && C1.Text == C2.Text && C2.Text == C3.Text)
                winner = true;
            if (!A2.Enabled && A2.Text == B2.Text && B2.Text == C2.Text)
                winner = true;
            if (!A3.Enabled && A3.Text == B3.Text && B3.Text == C3.Text)
                winner = true;
            if (!A1.Enabled && A1.Text == B2.Text && B2.Text == C3.Text)
                winner = true;
            if (!A3.Enabled && A3.Text == B2.Text && B2.Text == C1.Text)
                winner = true;

            if (winner)
            {
                DisableButtons();
                if (turn)
                {
                    playerTwoCount++;
                    if (twoPlayersOff)// If only playing with Computer
                       MessageBox.Show(" GAME OVER!!!");
                    else
                    {
                        MessageBox.Show($"{ player2Label.Text} Won the game");
                    }
                }
                else
                {
                    playerOneCount++;
                    if (twoPlayersOff)// If only playing with Computer
                        MessageBox.Show(" Congratulation!!!! \n You Won the Game!!!");
                    else
                    {
                        MessageBox.Show($"{ player1Label.Text} Won the game");
                    }
                }
            }
            else 
            {
                if (turnCount == 9)
                {
                    drawCount++;
                    MessageBox.Show(" It is a Draw!!!");
                }
                
            }
            playerScoreCountLabel();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developed by Biniyam Yemane");
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void DisableButtons() 
        {
            Button button = null;
          foreach(Control c in Controls)
            {
                button = c as Button;
                if(button != null)
                {
                    button.Enabled =false;
                }

            }
        
        }
        public void RessetAllButtons()
        {
            turnCount = 0;
            playerOneCount = 0;
            drawCount = 0;
            playerTwoCount = 0;
            playerScoreCountLabel();
            turn = true;
            winner = false;
            try
            {
                Button button = null;
                foreach (Control c in Controls)
                {
                    //Button button = (Button)c;

                    button = c as Button;
                    if (button != null)
                    {
                        button.Enabled = true;
                        button.Text = "";
                    }
                }
                if (enableButton.BackColor == Color.Lime)
                    enableButton.Text = "Enable Two Players";
                else
                    enableButton.Text = "Disable Two Players";
            }
            catch { }
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RessetAllButtons();
            player1Label.Text = "Player1";
            player2Label.Text = "Computer";
            player1TextBox.Text = "";
            player2TextBox.Text = "";
            twoPlayersOff = true;

        }
        private void anotherRoundToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            turn = true;
            turnCount = 0;
            winner = false;
            try
            {
                Button button = null;
                foreach (Control c in Controls)
                {

                    //Button button = (Button)c;

                    button = c as Button;
                    if (button != null)
                    {
                        button.Enabled = true;
                        button.Text = "";
                    }
                }
                if (enableButton.BackColor == Color.Lime)
                    enableButton.Text = "Enable Two Players";
                else
                    enableButton.Text = "Disable Two Players";
            }
            catch { }
        }
        private void mouseEnter(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (turn)
            {
                button.Text = "X";
            }
            else
            {
                button.Text = "O";
            }
        }
        private void mouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Enabled)
            {
                button.Text = "";
            }
         
        }
        private void playerScoreCountLabel()
        {
            player1CountLabel.Text = playerOneCount.ToString();
            player2CountLabel.Text = playerTwoCount.ToString();
            drawCountLabel.Text = drawCount.ToString();
        }
        private void enableButtonClick(object sender, EventArgs e)
        {
            if (enableButton.BackColor == Color.Lime)
            {
                twoPlayersOff = false;
                RessetAllButtons();
                player1Label.Text = player1TextBox.Text;
                player2Label.Text = player2TextBox.Text;
                enableButton.Text = "Disable Two Players";
                enableButton.BackColor = Color.IndianRed;  
            }
            else
            {
                twoPlayersOff = true;
                RessetAllButtons();
                player2TextBox.Enabled = true;
                player1Label.Text = player1TextBox.Text;
                player2Label.Text = "Computer";
                enableButton.Text = "Enable Two Players";
                enableButton.BackColor = Color.Lime;
            }
        }
        private void computer_make_move()
        {
            //priority 1:  get tick tac toe
            //priority 2:  block x tic tac toe
            //priority 3:  go for corner space
            //priority 4:  pick open space 
            Button move = null;
                //look for tic tac toe opportunities
                move = look_for_win_or_block("O"); //look for win
            if (move == null)
            {
                move = look_for_win_or_block("X"); //look for block
                if (move == null)
                {
                    move = look_for_corner();
                    if (move == null)
                    {
                        move = look_for_open_space();
                    }//end if
                }//end if
            }//end if
                if (move != null)
                    move.PerformClick();
            
        }
        private Button look_for_open_space()
        {
            //Console.WriteLine("Looking for open space");
            //Button b = null;
            //foreach (Control c in Controls)
            //{
            //    b = c as Button;
            //    if (b != null)
            //    {
            //        if (b.Text == "")
            //            return b;
            //    }//end if
            //}//end if
            if (C2.Text == "")
                return A3;
            if (C3.Text == "")
                return C3;
            if (C1.Text == "")
                return C1;
            if (A3.Text == "")
                return A3;
            if (A2.Text == "")
                return C3;
            if (A1.Text == "")
                return C1;
            if (B1.Text == "")
                return A3;
            if (B2.Text == "")
                return C3;
            if (B3.Text == "")
                return C1;
            return null;
        }
        private Button look_for_corner()
        {
            //Looking for corner
            if (A1.Text == "O")
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (A3.Text == "O")
            {
                if (A1.Text == "")
                    return A1;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (C3.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
            }

            if (C1.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }

            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;

            return null;
        }
        private Button look_for_win_or_block( string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);
            //HORIZONTAL TESTS
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            //VERTICAL TESTS
            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            //DIAGONAL TESTS
            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        }
    }
}
