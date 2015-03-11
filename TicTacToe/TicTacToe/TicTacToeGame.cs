using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TicTacToe
{
    /// <summary>
    /// Simple implementation of TicTacToe in XNA
    /// </summary>
    public class TicTacToeGame : Microsoft.Xna.Framework.Game
    {
        // system variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // game variables
        Board board;
        QuitButton quitButton;
        const int QUIT_BUTTON_Y = 520;

        bool playerTookTurn;

        // game state and turn tracking
        static GameState gameState = GameState.Play;
        PlayerType whoseTurn = PlayerType.Human;

        // computer player
        ComputerPlayer computer = new ComputerPlayer();

        // main constant values
        const int DISPLAY_WIDTH = 800;
        const int DISPLAY_HEIGHT = 600;

        /// <summary>
        /// Constructor
        /// </summary>
        public TicTacToeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = DISPLAY_WIDTH;
            graphics.PreferredBackBufferHeight = DISPLAY_HEIGHT;

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            board = new Board(this.Content, graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            quitButton = new QuitButton(this.Content, graphics.PreferredBackBufferWidth / 2, QUIT_BUTTON_Y, GameState.Quit);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // update based on game state
            MouseState mouse = Mouse.GetState();
            if (gameState == GameState.Play)
            {
                if (whoseTurn == PlayerType.Human)
                {
                    // IF IT'S THE PLAYER'S TURN, LET THE PLAYER TAKE A TURN
                    // SAVE THE BOOLEAN VALUE THE METHOD RETURNS FOR USE IN THE NEXT STEP
                    playerTookTurn = board.TakePlayerTurn(mouse);

                    // IF THE PLAYER TOOK A TURN, CHANGE WHOSE TURN IT IS
                    if (playerTookTurn)
                    {
                        ChangeTurn();
                    }
                }
                else
                {
                    // COMPUTER PLAYER TAKE A TURN AND CHANGE WHOSE TURN IT IS
                    computer.TakeTurn(board);
                    ChangeTurn();
                }

            }
            else if (gameState == GameState.GameOver)
            {
                // THE GAME IS OVER, SO UPDATE THE QUIT BUTTON
                quitButton.Update(mouse);
            }
            else
            {
                // game state is quit, so exit the game
                this.Exit();
            }

            // IF THE GAME IS OVER AND GAME STATE IS GameState.Play, MAKE THE QUIT 
            // BUTTON VISIBLE AND CHANGE gameState TO GameState.GameOver
            if (board.GameOver() && gameState == GameState.Play)
            {
                quitButton.Visible = true;
                gameState = GameState.GameOver;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();
            board.Draw(this.spriteBatch);

            if (gameState == GameState.GameOver)
            {
                quitButton.Draw(this.spriteBatch);
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Changes whose turn it is
        /// </summary>
        private void ChangeTurn()
        {
            if (whoseTurn == PlayerType.Human)
            {
                whoseTurn = PlayerType.Computer;
            }
            else
            {
                whoseTurn = PlayerType.Human;
            }
        }

        /// <summary>
        /// Changes the state of the game
        /// </summary>
        /// <param name="newState">the new game state</param>
        public static void ChangeState(GameState newState)
        {
            gameState = newState;
        }
    }
}