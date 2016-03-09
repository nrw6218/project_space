using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GroupProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //for deconstructing a magnatude when using a 45degree angle int componets
        const double ANGLE_MULTIPLIER = 0.70710678118;

        enum GameState { Game };
        GameState gameState;

        double fps;
        double timePerFrame;

        KeyboardState currentKb;
        KeyboardState previousKb;

        Texture2D tilesheet;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gameState = GameState.Game;
            fps = 60.0;
            timePerFrame = 1.0 / fps;
            currentKb = Keyboard.GetState();
            previousKb = currentKb;
            MapManager.Instance.NewMap("map.data");
            PlayerManager.Instance.CreatePlayer();

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

            tilesheet = Content.Load<Texture2D>("tilesheet");
            Texture2D bot = Content.Load<Texture2D>("botcombat");
            PlayerManager.Instance.Player.SetTexture(bot);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKb = currentKb;
            currentKb = Keyboard.GetState();

            switch (gameState)
            {
                case GameState.Game:
                    PlayerManager.Instance.Player.PreviousX = PlayerManager.Instance.Player.X;
                    PlayerManager.Instance.Player.PreviousY = PlayerManager.Instance.Player.Y;
                    bool movingVertically = false;
                    bool movingHorizontally = false;
                    int moveBy = 0;

                    /*
                    if (currentKb.IsKeyDown(Keys.W) != currentKb.IsKeyDown(Keys.S))
                        movingVertically = true;
                    if (currentKb.IsKeyDown(Keys.A) != currentKb.IsKeyDown(Keys.D))
                        movingHorizontally = true;

                    //if the player is moving change the moveby
                    if (movingVertically || movingHorizontally)
                        moveBy = MovementDistance(PlayerManager.Instance.Player.Speed, movingVertically && movingHorizontally);

                    //move the player vertically in the correct direction
                    if (movingVertically && currentKb.IsKeyDown(Keys.W))
                        PlayerManager.Instance.Player.Y -= moveBy;
                    else if (movingVertically && currentKb.IsKeyDown(Keys.S))
                        PlayerManager.Instance.Player.Y += moveBy;

                    //move the player horizontally in the correct direction
                    if (movingHorizontally && currentKb.IsKeyDown(Keys.A))
                        PlayerManager.Instance.Player.X -= moveBy;
                    else if (movingHorizontally && currentKb.IsKeyDown(Keys.D))
                        PlayerManager.Instance.Player.X += moveBy;
                    */

                    
                    if (currentKb.IsKeyDown(Keys.A))
                        PlayerManager.Instance.Player.X--;
                    if (currentKb.IsKeyDown(Keys.D))
                        PlayerManager.Instance.Player.X++;

                    //handles wall collision
                    List<Wall> collidingWalls = MapManager.Instance.CurrentSubMap.CollidingWalls();
                    
                    foreach(Wall w in collidingWalls)
                    {
                        //if the player is moving to the left, hitting a block on its right side
                        if (PlayerManager.Instance.Player.X < PlayerManager.Instance.Player.PreviousX)
                            PlayerManager.Instance.Player.X = w.Rectangle.X + w.Rectangle.Width;
                        //if the player is moving to the right, hitting a block on its left side
                        else if (PlayerManager.Instance.Player.X > PlayerManager.Instance.Player.PreviousX)
                            PlayerManager.Instance.Player.X = w.Rectangle.X - PlayerManager.Instance.Player.Rectangle.Width;
                    }

                    if (currentKb.IsKeyDown(Keys.W))
                        PlayerManager.Instance.Player.Y--;
                    if (currentKb.IsKeyDown(Keys.S))
                        PlayerManager.Instance.Player.Y++;

                    collidingWalls = MapManager.Instance.CurrentSubMap.CollidingWalls();

                    foreach (Wall w in collidingWalls)
                    {
                        //if the player is moving up, hitting a block on its bottom
                        if (PlayerManager.Instance.Player.Y < PlayerManager.Instance.Player.PreviousY)
                            PlayerManager.Instance.Player.Y = w.Rectangle.Y + w.Rectangle.Height;
                        //if the player is moving down, hitting a block on its top
                        else if (PlayerManager.Instance.Player.Y > PlayerManager.Instance.Player.PreviousY)
                            PlayerManager.Instance.Player.Y = w.Rectangle.Y - PlayerManager.Instance.Player.Rectangle.Height;
                    }
                    break;
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

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            MapManager.Instance.CurrentSubMap.Draw(spriteBatch, tilesheet);
            PlayerManager.Instance.Player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Returns distance that comething should travel this frame based on how much 
        /// it should move per frame and if it is moving diagonally or not
        /// 
        /// if an object is moveing on an anfle then it must 
        /// move int both the x and y directions by the returned value
        /// </summary>
        /// <param name="speed">how many pixes th eobject should move in a frame</param>
        /// <param name="onAngle">true if the object is moving diagonally</param>
        /// <returns></returns>
        public int MovementDistance(double speed, bool onAngle)
        {
            //speed * timePerFrame = how many pixels it should move this game frame
            //speed * timePerFrame * ANGLE_MULTIPLIER -> so the object doesn't move faster when moving on an angle

            if (onAngle)
            {
                //the object must be moved in both the x and y axies by the returned value
                return (int)(Math.Round(speed * timePerFrame * ANGLE_MULTIPLIER));
            }
            else
            {
                //object is moving on one axis
                return (int)(Math.Round(speed * timePerFrame));
            }
        }
    }
}
