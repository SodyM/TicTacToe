﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe
{
    /// <summary>
    /// A single square for a tic-tac-toe board
    /// </summary>
    public class BoardSquare
    {
        SquareContents contents = SquareContents.Empty;

        // drawing support
        Texture2D squareSprite;
        Texture2D xSprite;
        Texture2D oSprite;
        Rectangle drawRectangle = new Rectangle();

        // assets
        const string SQUARE_IMAGE   = "square";
        const string X_IMAGE        = "x";
        const string O_IMAGE        = "o";

        /// <summary>
        /// Creates a new board square centered at the given location
        /// </summary>
        /// <param name="contentManager">the content manager to use</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        public BoardSquare(ContentManager contentManager, int x, int y)
        {
            LoadContent(contentManager);

            drawRectangle.X = x - drawRectangle.Width / 2;
            drawRectangle.Y = y - drawRectangle.Height / 2;
        }
       
        /// <summary>
        /// Gets and sets the contents of the square
        /// </summary>
        public SquareContents Contents
        {
            get { return contents; }
            set { contents = value; }
        }

        /// <summary>
        /// Gets whether or not the square is empty
        /// </summary>
        public bool Empty
        {
            get { return contents == SquareContents.Empty;  }
        }

        /// <summary>
        /// Updates the board square
        /// </summary>
        /// <param name="mouse">the current mouse state</param>
        /// <returns>true if the player placed an X, false otherwise</returns>
        public bool Update(MouseState mouse)
        {
            if (drawRectangle.Contains(mouse.X, mouse.Y) 
                && mouse.LeftButton == ButtonState.Pressed 
                && contents == SquareContents.Empty)
            {
                contents = SquareContents.X;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Draws the board square
        /// </summary>
        /// <param name="spriteBatch">the sprite batch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(squareSprite, drawRectangle, Color.White);                        

            if (contents == SquareContents.X)
            {
                spriteBatch.Draw(xSprite, drawRectangle, Color.White);
            }
            else if (contents == SquareContents.O)
            {
                spriteBatch.Draw(oSprite, drawRectangle, Color.White);
            }                      
        }


        /// <summary>
        /// Gets the side length for a board square
        /// </summary>
        /// <param name="contentManager">the content manager</param>
        /// <returns>the side length</returns>
        public static int GetSideLength(ContentManager contentManager)
        {
            BoardSquare tempSquare = new BoardSquare(contentManager, 0, 0);
            return tempSquare.drawRectangle.Width;
        }

        /// <summary>
        /// Loads the content for the board square
        /// </summary>
        /// <param name="contentManager">the content manager to use</param>
        private void LoadContent(ContentManager contentManager)
        {
            // load content and set size of draw rectangle
            squareSprite = contentManager.Load<Texture2D>(SQUARE_IMAGE);
            xSprite = contentManager.Load<Texture2D>(X_IMAGE);
            oSprite = contentManager.Load<Texture2D>(O_IMAGE);
            drawRectangle.Width = squareSprite.Width;
            drawRectangle.Height = squareSprite.Height;
        }
    }
}
