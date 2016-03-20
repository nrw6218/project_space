﻿using Microsoft.Xna.Framework;
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
        Texture2D testItem;

        //for deconstructing a magnatude when using a 45degree angle int componets
        const double ANGLE_MULTIPLIER = 0.70710678118;

        enum GameState { Game, Inventory };
        GameState gameState;

        static double FPS = 60.0;
        static double SECONDS_PER_FRAME = 1 / FPS;
        double framesThisGameFrame;

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
            graphics.PreferredBackBufferWidth = 12 * Block.BLOCK_SIZE;
            graphics.PreferredBackBufferHeight = 6 * Block.BLOCK_SIZE;
            graphics.ApplyChanges();

            gameState = GameState.Game;
            framesThisGameFrame = 0;
            currentKb = Keyboard.GetState();
            previousKb = currentKb;

            MapManager.Instance.NewMap("../../../Content/testWorld.map");
            PlayerManager.Instance.CreatePlayer();
            PlayerManager.Instance.CreatePlayerInventory();

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
            testItem = Content.Load<Texture2D>("imgres");
            PlayerManager.Instance.Player.SetTexture(bot);
            PlayerManager.Instance.PlayerInventory.AddToInventory(new Item(testItem, "test"));
            PlayerManager.Instance.PlayerInventory.AddToInventory(new Item(testItem, "test"));
            MapManager.Instance.CurrentSubMap.MapInventory.AddToInventory(new Item(testItem, "test", new Rectangle(100, 100, 50, 50)));
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
            framesThisGameFrame = gameTime.ElapsedGameTime.TotalSeconds / SECONDS_PER_FRAME; 

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKb = currentKb;
            currentKb = Keyboard.GetState();

            switch (gameState)
            {
                case GameState.Game:
                    PlayerManager.Instance.Player.PreviousX = PlayerManager.Instance.Player.X;
                    PlayerManager.Instance.Player.PreviousY = PlayerManager.Instance.Player.Y;

                    //if the player is moving change the moveby
                    int moveBy = MovementDistance(PlayerManager.Instance.Player.Speed, (currentKb.IsKeyDown(Keys.W) != currentKb.IsKeyDown(Keys.S)) && (currentKb.IsKeyDown(Keys.A) != currentKb.IsKeyDown(Keys.D)));

                    //move the player horizontally in the correct direction
                    if (currentKb.IsKeyDown(Keys.A))
                        PlayerManager.Instance.Player.X -= moveBy;
                    if (currentKb.IsKeyDown(Keys.D))
                        PlayerManager.Instance.Player.X += moveBy;

                    //handles wall collision
                    List<Block> collidingWalls = MapManager.Instance.CurrentSubMap.CollidingWalls();

                    foreach (Block w in collidingWalls)
                    {
                        //if the player is moving to the left, hitting a block on its right side
                        if (PlayerManager.Instance.Player.X < PlayerManager.Instance.Player.PreviousX)
                            PlayerManager.Instance.Player.X = w.Rectangle.X + w.Rectangle.Width;
                        //if the player is moving to the right, hitting a block on its left side
                        else if (PlayerManager.Instance.Player.X > PlayerManager.Instance.Player.PreviousX)
                            PlayerManager.Instance.Player.X = w.Rectangle.X - PlayerManager.Instance.Player.Rectangle.Width;
                    }

                    //move the player vertically in the correct direction
                    if (currentKb.IsKeyDown(Keys.W))
                        PlayerManager.Instance.Player.Y -= moveBy;
                    if (currentKb.IsKeyDown(Keys.S))
                        PlayerManager.Instance.Player.Y += moveBy;

                    collidingWalls = MapManager.Instance.CurrentSubMap.CollidingWalls();

                    foreach (Block w in collidingWalls)
                    {
                        //if the player is moving up, hitting a block on its bottom
                        if (PlayerManager.Instance.Player.Y < PlayerManager.Instance.Player.PreviousY)
                            PlayerManager.Instance.Player.Y = w.Rectangle.Y + w.Rectangle.Height;
                        //if the player is moving down, hitting a block on its top
                        else if (PlayerManager.Instance.Player.Y > PlayerManager.Instance.Player.PreviousY)
                            PlayerManager.Instance.Player.Y = w.Rectangle.Y - PlayerManager.Instance.Player.Rectangle.Height;
                    }


                    if (currentKb.IsKeyDown(Keys.I))
                    {
                        gameState = GameState.Inventory;
                        this.IsMouseVisible = true;
                    }
                    if (currentKb.IsKeyDown(Keys.Space))
                    {
                        if (MapManager.Instance.CurrentSubMap.MapInventory.CurrentInventory.Count != 0)
                        {
                            for (int i = 0; i < MapManager.Instance.CurrentSubMap.MapInventory.CurrentInventory.Count; i++)
                            {
                                if (PlayerManager.Instance.Player.Rectangle.Intersects(MapManager.Instance.CurrentSubMap.MapInventory.CurrentInventory[i].MapPosition))
                                {
                                    MapManager.Instance.CurrentSubMap.MapInventory.CurrentInventory[i].AddToPlayerInventory();
                                    MapManager.Instance.CurrentSubMap.MapInventory.RemoveFromInventory(MapManager.Instance.CurrentSubMap.MapInventory.CurrentInventory[i]);
                                }
                            }

                        }
                    }

                    if(PlayerManager.Instance.Player.X + PlayerManager.Instance.Player.Width/2 < 0)
                    {
                        MapManager.Instance.MoveSubmap(Direction.Left);
                        PlayerManager.Instance.Player.X = graphics.PreferredBackBufferWidth - PlayerManager.Instance.Player.Width / 2;
                    }
                    else if (PlayerManager.Instance.Player.X + PlayerManager.Instance.Player.Width / 2 > graphics.PreferredBackBufferWidth)
                    {
                        MapManager.Instance.MoveSubmap(Direction.Right);
                        PlayerManager.Instance.Player.X = PlayerManager.Instance.Player.Width / 2;
                    }
                    else if (PlayerManager.Instance.Player.Y + PlayerManager.Instance.Player.Height / 2 < 0)
                    {
                        MapManager.Instance.MoveSubmap(Direction.Up);
                        PlayerManager.Instance.Player.Y = graphics.PreferredBackBufferHeight - PlayerManager.Instance.Player.Height / 2;
                    }
                    else if (PlayerManager.Instance.Player.Y + PlayerManager.Instance.Player.Height / 2 > graphics.PreferredBackBufferHeight)
                    {
                        MapManager.Instance.MoveSubmap(Direction.Down);
                        PlayerManager.Instance.Player.Y = PlayerManager.Instance.Player.Height / 2;
                    }



                    break;
                case GameState.Inventory:
                    if (currentKb.IsKeyDown(Keys.O))
                    {
                        gameState = GameState.Game;
                        this.IsMouseVisible = false;
                    }

                    if (currentKb.IsKeyDown(Keys.U))
                    {
                        PlayerManager.Instance.PlayerInventory.AddToInventory(new Item(testItem, "test"));
                    }

                    if (currentKb.IsKeyDown(Keys.Y))
                    {
                        MapManager.Instance.CurrentSubMap.MapInventory.AddToInventory(new Item(testItem, "test", new Rectangle(100, 100, 50, 50)));
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
            switch (gameState)
            {
                case GameState.Game:

                    MapManager.Instance.CurrentSubMap.Draw(spriteBatch, tilesheet);
                    PlayerManager.Instance.Player.Draw(spriteBatch);
                    MapManager.Instance.CurrentSubMap.MapInventory.Draw(spriteBatch);
                    break;

                case GameState.Inventory:
                    PlayerManager.Instance.PlayerInventory.Draw(spriteBatch);
                    break;

            }
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
        public int MovementDistance(double speed, bool onAngle = false)
        {
            //speed * timePerFrame = how many pixels it should move this game frame
            //speed * timePerFrame * ANGLE_MULTIPLIER -> so the object doesn't move faster when moving on an angle

            if (onAngle)
                return (int)(Math.Round(speed * framesThisGameFrame * ANGLE_MULTIPLIER));
            else
                return (int)(Math.Round(speed * framesThisGameFrame));
        }
    }
}
