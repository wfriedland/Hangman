using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//
//  Never to old to play a child's game. 
//
namespace hangMan
{
    public partial class Form1 : Form
    {
        enum letters {A=0, B, C, D, E, F, G, H, I, J, K, L, M
                    , N,   O, P, Q, R, S, T, U, V, W, X, Y, Z};

        const int MAX_WORD_LENGTH = 9;
        const int MAX_WORDS = 70;
        string[] words = new string[MAX_WORDS] {"dog","cat","building","swimmer","running","walking","great",
        "better","call","saul","program","bellevue","college","house","microsoft","table","picture",
        "coder","sandbox","playpen","chair","hobby","oven","piece","cake","bigger","justified","kitchen",
        "radio","table","flower","morning","evening","hungry","humbug","coffee","machine","computer",
        "success","failure","radio","cushion","trail","blazers","path","less","taken","easy",
        "flower","tree","root","knowing","decimal","system","ice","creame","chicken","turkey","steak",
        "road","street","avenue","paved","dirt","sideways","wishing","well","smarter","nicer","kindness"};
        string selectedWord;

        Random r;

        private int quessCnt = 0;
        private int answerCnt = 0;

        //
        // 
        public Form1()
        {
            InitializeComponent();

            // Game Initialization

            // Random Number Generator
            r = new Random();

            // Application Image
            hangman.Image = Properties.Resources.hangman;
            hangman.Visible = true;
            
            // This program has an icon to represent it on the toolbar        
            notifyIcon1.Icon = Properties.Resources.Hangman_Game;
            notifyIcon1.Text = "HangMan";

            this.helpProvider1.SetShowHelp(this, true);
            this.helpProvider1.SetHelpString(this, "HangMan. Guess the correct spelling before your man is hung!!");
            
        }

        //
        //  newGame!!!  The game board is reset. The correct number of letters are displayed for the player
        //  and all letter buttons are enabled. The hangman is hidden, ready for the player to save him.
        //
        private void newGame()
        {
            int idx = 0;

            // Hide the hang man
            quessCnt = 0;
            head.Visible = false;
            body.Visible = false;
            leftLeg.Visible = false;
            rightLeg.Visible = false;
            leftArm.Visible = false;
            rightArm.Visible = false;

            // Select the game word at random from our word repository
            int wordIdx = r.Next(0, MAX_WORDS - 1);
            selectedWord = words[wordIdx].ToUpper();
            answerCnt = selectedWord.Length;
            cheatLabel.ResetText();

            // Enable all letter buttons
            foreach (Button b in letterPanel.Controls)
            {
                b.Enabled = true;
                b.Visible = true;
            }

            // reset text dashes of the selected word. Un-used spots are invisible
            for (idx = 0; idx < MAX_WORD_LENGTH; idx++)
            {
                if (idx < answerCnt)
                {
                    dashPanel.Controls[idx].Visible = true;
                    dashPanel.Controls[idx].Text = "";
                }
                else
                {
                    dashPanel.Controls[idx].Visible = false;
                }
            }
        }

        //
        //  Method chooseLetter processes the choosen letter. The counter for correct
        //  letters is decremented for every occurance of the letter. The counter for 
        //  wrong guesses is also processed.
        //
        private void chooseLetter(letters x)
        {
            DialogResult result;
            bool answeredCorrectly = false;
            int letterIndex = (int)x;
            letterPanel.Controls[letterIndex].Enabled = false;
            
            //
            //  Search the slected word for the letter we are processing.
            //
            for (int idx = 0; idx < selectedWord.Length; idx++)
            {
                if (selectedWord[idx] == letterPanel.Controls[letterIndex].Text[0])
                {
                    // The Player has guessed correctly
                    answerCnt--;
                    answeredCorrectly = true;
                    dashPanel.Controls[idx].Text = letterPanel.Controls[letterIndex].Text;
                }
            }
            if (answeredCorrectly == false)
            {
                // The Player has guessed wrong
                quessWrong();
            }
            else if (answerCnt == 0)
            {
                //
                // The player has won! The player has won!
                //
                result = MessageBox.Show("You have won!!\n\nWay to Go!\n\nDo you want to play again?",
                "Big Winner", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes) 
                    newGame();
                else 
                    this.Close();
            }

        }


        //
        // The player has guessed incorrectly. Display the next part of the hangman and increment the 
        // counter for incorrect answers. 
        //
        private void quessWrong()
        {
            DialogResult result;
            quessCnt++;
            switch (quessCnt)
            {
                case 1:
                    head.Visible = true;
                    break;
                case 2:
                    body.Visible = true;
                    break;
                case 3:
                    leftLeg.Visible = true;
                    break;
                case 4:
                    rightLeg.Visible = true;
                    break;
                case 5:
                    leftArm.Visible = true;
                    break;
                case 6:
                    rightArm.Visible = true;
                    result = MessageBox.Show("You have lost\n\nThe answer was: "+selectedWord+"\n\nDo you want to play again?","Lost Again!!"
                        ,MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes) 
                        newGame();
                    else 
                        this.Close();
                    break;
            }
        }

        //
        // Events to processed Letter Buttons being clicked
        //
        private void A_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.A);
        }
        private void B_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.B);
        }
        private void C_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.C);
        }
        private void D_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.D);
        }
        private void E_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.E);
        }
        private void F_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.F);
        }
        private void G_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.G);
        }
        private void H_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.H);
        }
        private void I_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.I);
        }
        private void J_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.J);
        }
        private void K_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.K);
        }
        private void L_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.L);
        }
        private void M_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.M);
        }
        private void N_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.N);
        }
        private void O_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.O);
        }
        private void P_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.P);
        }
        private void Q_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.Q);
        }
        private void R_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.R);
        }
        private void S_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.S);
        }
        private void T_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.T);
        }
        private void U_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.U);
        }
        private void V_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.V);
        }
        private void W_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.W);
        }
        private void X_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.X);
        }
        private void Y_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.Y);
        }
        private void Z_Click(object sender, EventArgs e)
        {
            chooseLetter(letters.Z);
        }

        //
        // Add the letter and dash controls to their tableLayoutPanels when the form loads
        //
        private void Form1_Load(object sender, EventArgs e)
        {
            this.dashPanel.SuspendLayout();
            this.letterPanel.SuspendLayout();
            this.SuspendLayout();
            dashPanel.Controls.Add((Control)dash1);
            dashPanel.Controls.Add((Control)dash2);
            dashPanel.Controls.Add((Control)dash3);
            dashPanel.Controls.Add((Control)dash4);
            dashPanel.Controls.Add((Control)dash5);
            dashPanel.Controls.Add((Control)dash6);
            dashPanel.Controls.Add((Control)dash7);
            dashPanel.Controls.Add((Control)dash8);
            dashPanel.Controls.Add((Control)dash9);
            letterPanel.Controls.Add((Control)A);
            letterPanel.Controls.Add((Control)B);
            letterPanel.Controls.Add((Control)C);
            letterPanel.Controls.Add((Control)D);
            letterPanel.Controls.Add((Control)E);
            letterPanel.Controls.Add((Control)F);
            letterPanel.Controls.Add((Control)G);
            letterPanel.Controls.Add((Control)H);
            letterPanel.Controls.Add((Control)I);
            letterPanel.Controls.Add((Control)J);
            letterPanel.Controls.Add((Control)K);
            letterPanel.Controls.Add((Control)L);
            letterPanel.Controls.Add((Control)M);
            letterPanel.Controls.Add((Control)N);
            letterPanel.Controls.Add((Control)O);
            letterPanel.Controls.Add((Control)P);
            letterPanel.Controls.Add((Control)Q);
            letterPanel.Controls.Add((Control)R);
            letterPanel.Controls.Add((Control)S);
            letterPanel.Controls.Add((Control)T);
            letterPanel.Controls.Add((Control)U);
            letterPanel.Controls.Add((Control)V);
            letterPanel.Controls.Add((Control)W);
            letterPanel.Controls.Add((Control)X);
            letterPanel.Controls.Add((Control)Y);
            letterPanel.Controls.Add((Control)Z);
            this.letterPanel.ResumeLayout();
            this.dashPanel.ResumeLayout();
            this.ResumeLayout();
            newGame();
        }

        // It is always more fun when you cheat a little
        private void cheatButton_Click(object sender, EventArgs e)
        {
            cheatLabel.Text = selectedWord;
        }
    }
}
