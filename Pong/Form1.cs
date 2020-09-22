/*
 * Description:     A basic PONG simulator
 * Author:           
 * Date:            
 */

#region libraries

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;

#endregion

namespace Pong
{
    public partial class Form1 : Form
    {
        #region global values

        //graphics objects for drawing
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Font drawFont = new Font("Courier New", 10);

        // Sounds for game
        SoundPlayer scoreSound = new SoundPlayer(Properties.Resources.score);
        SoundPlayer collisionSound = new SoundPlayer(Properties.Resources.collision);

        //determines whether a key is being pressed or not
        Boolean aKeyDown, zKeyDown, jKeyDown, mKeyDown;

        // check to see if a new game can be started
        Boolean newGameOk = true;

        //ball directions, speed, and rectangle
        Boolean ballMoveRight = true;
        Boolean ballMoveDown = true;
        int BALL_SPEED = 5;
        Rectangle ball;

        //paddle speeds and rectangles
        const int PADDLE_SPEED = 6;
        Rectangle p1, p2;

        //player and game scores
        int player1Score = 0;
        int player2Score = 0;
        int gameWinScore = 2;  // number of points needed to win game


        #endregion

        public Form1()
        {
            InitializeComponent();
        }



        // -- YOU DO NOT NEED TO MAKE CHANGES TO THIS METHOD
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //check to see if a key is pressed and set is KeyDown value to true if it has
            switch (e.KeyCode)
            {
                case Keys.A:
                    aKeyDown = true;
                    break;
                case Keys.Z:
                    zKeyDown = true;
                    break;
                case Keys.J:
                    jKeyDown = true;
                    break;
                case Keys.M:
                    mKeyDown = true;
                    break;
                case Keys.Y:
                case Keys.Space:
                    if (newGameOk)
                    {
                        SetParameters();
                    }
                    break;
                case Keys.N:
                    if (newGameOk)
                    {
                        Close();
                    }
                    break;
            }
        }

        // -- YOU DO NOT NEED TO MAKE CHANGES TO THIS METHOD
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //check to see if a key has been released and set its KeyDown value to false if it has
            switch (e.KeyCode)
            {
                case Keys.A:
                    aKeyDown = false;
                    break;
                case Keys.Z:
                    zKeyDown = false;
                    break;
                case Keys.J:
                    jKeyDown = false;
                    break;
                case Keys.M:
                    mKeyDown = false;
                    break;
            }
        }



        /// <summary>
        /// sets the ball and paddle positions for game start
        /// </summary>
        private void SetParameters()
        {
            if (newGameOk)
            {
                player1Score = player2Score = 0;
                newGameOk = false;
                startLabel.Visible = false;
                gameUpdateLoop.Start();
            }

            //set starting position for paddles on new game and point scored 
            const int PADDLE_EDGE = 20;  // buffer distance between screen edge and paddle            

            p1.Width = p2.Width = 10;    //height for both paddles set the same
            p1.Height = p2.Height = 50;  //width for both paddles set the same

            //p1 starting position
            p1.X = PADDLE_EDGE;
            p1.Y = this.Height / 2 - p1.Height / 2;

            //p2 starting position
            p2.X = this.Width - PADDLE_EDGE - p2.Width;
            p2.Y = this.Height / 2 - p2.Height / 2;

            // set Width and Height of ball
            ball.Width = 10;
            ball.Height = 10;

            // set starting X position for ball to middle; of screen, (use this.Width and ball.Width)
            ball.X = this.Width / 2 - ball.Width;



            // TODO set starting Y position for ball to middle of screen, (use this.Height and ball.Height)
            ball.Y = this.Height / 2 - ball.Height;

        }

        /// <summary>
        /// This method is the game engine loop that updates the position of all elements
        /// and checks for collisions.
        /// </summary>
        private void gameUpdateLoop_Tick(object sender, EventArgs e)
        {


            #region update ball position

   

            #endregion
            if (ballMoveRight == true)
            {
                ball.X = ball.X + BALL_SPEED;

            }
            else
            {
                ball.X = ball.X - BALL_SPEED;
            }

            if (ballMoveDown == true)
            {
                ball.Y = ball.Y + BALL_SPEED;

            }
            else
            {
                ball.Y = ball.Y - BALL_SPEED;
            }

            #region update paddle positions

          
            #endregion

            // Player 1 paddle 
            if (aKeyDown == true && p1.Y > 0)
            {
                p1.Y = p1.Y - PADDLE_SPEED;

            }

            if (zKeyDown == true && p1.Y < this.Height - p1.Height)
            {
                p1.Y = p1.Y + PADDLE_SPEED;

            }

            // Player 2 paddle 
            if (jKeyDown == true && p2.Y > 0)
            {
                p2.Y = p2.Y - PADDLE_SPEED;

            }

            if (mKeyDown == true && p2.Y < this.Height - p2.Height)
            {
                p2.Y = p2.Y + PADDLE_SPEED;

            }

            #region ball collision with top and bottom lines

            

            #endregion
            if (ball.Y <= 0)
            {
                ballMoveDown = true;
                collisionSound.Play();
            }

            if (ball.Y >= this.Height)
            {
                ballMoveDown = false;
                collisionSound.Play();
            }


            #region ball collision with paddles

            // TODO create if statment that checks p1 collides with ball and if it does
            // --- play a "paddle hit" sound and
            // --- use ballMoveRight boolean to change direction

            // TODO create if statment that checks p2 collides with ball and if it does
            // --- play a "paddle hit" sound and
            // --- use ballMoveRight boolean to change direction

            /*  ENRICHMENT
             *  Instead of using two if statments as noted above see if you can create one
             *  if statement with multiple conditions to play a sound and change direction
             */

            #endregion


            if (p1.IntersectsWith(ball))
            {
                ballMoveRight = true;
                collisionSound.Play();
            }

            if (p2.IntersectsWith(ball))
            {
                ballMoveRight = false;
                collisionSound.Play();
            }

            #region ball collision with side walls (point scored)


         

            //  same as above but this time check for collision with the right wall

            #endregion
            // Player score labels 


            if (ball.X <= 0)
            {
                player2Score++;
                SetParameters();
            }
            

            else if (ball.X >= this.Width - ball.Width)
            {
                player1Score++;
                SetParameters();
            }

            if(player1Score == gameWinScore)
            {

                GameOver("Player 1 Wins " + "Press SPACE to Play again");
            }

            if (player2Score == gameWinScore)
            {

                GameOver("Player 2 Wins " + "Press SPACE to Play again");
            }

            //refresh the screen, which causes the Form1_Paint method to run
            this.Refresh();
        }

        /// <summary>
        /// Displays a message for the winner when the game is over and allows the user to either select
        /// to play again or end the program
        /// </summary>
        /// <param name="winner">The player name to be shown as the winner</param>

        private void GameOver(string winner)
        {
            newGameOk = true;
            gameUpdateLoop.Stop();
            startLabel.Visible = true;
            startLabel.Text = winner;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // draw paddles using FillRectangle
            e.Graphics.FillRectangle(whiteBrush, p1);
            e.Graphics.FillRectangle(whiteBrush, p2);

            // draw ball using FillRectangle
            e.Graphics.FillRectangle(whiteBrush, ball);

            //  draw scores to the screen using DrawString
            e.Graphics.DrawString("Player 1: " + player1Score, drawFont, whiteBrush, 20, 20);
            e.Graphics.DrawString("Player 2: " + player2Score, drawFont, whiteBrush, this.Width - 250, 20);
        }

    }
}
