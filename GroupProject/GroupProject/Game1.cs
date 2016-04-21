using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Assets borrowed from the following sources:
//http://s267.photobucket.com/user/RandomDave15/media/PSP%20wallpapers/Games/Untitled.png.html
//http://www.puppygames.net/chaz/
//


namespace GroupProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //For use with inventory items
        Texture2D crate;
        Texture2D box;
        Texture2D artifact;
        Texture2D key;

        //For use in main menu and inventory screens
        Texture2D menuWall;
        Texture2D mainLogo;
        Texture2D hud;
        Texture2D astro;
        Texture2D logo;

        //Font for in-game text
        SpriteFont basicFont;

        //For use in tracking the mouse state
        MouseState ms;

        //Enemytexture
        Texture2D enemy;

        //attack
        Texture2D weaponRight;
        Texture2D weaponUp;
        Texture2D weaponLeft;
        Texture2D weaponDown;


        //for deconstructing a magnatude when using a 45degree angle int componets
        const double ANGLE_MULTIPLIER = 0.70710678118;

        enum GameState { MainMenu, Help, Game, Inventory, Equipment, Puzzle, End };
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

            gameState = GameState.MainMenu;
            framesThisGameFrame = 0;
            currentKb = Keyboard.GetState();
            previousKb = currentKb;

            MapManager.Instance.NewMap("../../../Content/Level 1.map");
            PlayerManager.Instance.CreatePlayer();
            PlayerManager.Instance.CreatePlayerInventory();
            PlayerManager.Instance.CreatePlayerEquipment();

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
            crate = Content.Load<Texture2D>("Crate");
            menuWall = Content.Load<Texture2D>("InventoryBack");
            mainLogo = Content.Load<Texture2D>("mainBack");
            basicFont = Content.Load<SpriteFont>("Audiowide");
            hud = Content.Load<Texture2D>("hudBack");
            astro = Content.Load<Texture2D>("astroFront");
            key = Content.Load<Texture2D>("Card");
            box = Content.Load<Texture2D>("Crate2");
            artifact = Content.Load<Texture2D>("Artifact2");
            logo = Content.Load<Texture2D>("CompanyLogo");
            enemy = Content.Load<Texture2D>("Hilary");
            weaponRight = Content.Load<Texture2D>("energyBlastRight");
            weaponLeft = Content.Load<Texture2D>("energyBlastLeft");
            weaponUp = Content.Load<Texture2D>("energyBlastUp");
            weaponDown = Content.Load<Texture2D>("energyBlastDown");


            PlayerManager.Instance.Player.SetTexture(astro);
            PlayerManager.Instance.PlayerInventory.AddToInventory(new Item(crate, "Crate"));
            PlayerManager.Instance.PlayerInventory.AddToInventory(new Item(box, "Mysterious Box"));
            PlayerManager.Instance.PlayerInventory.AddToInventory(new Item(artifact, "Advanced Camera"));
            MapManager.Instance.CurrentSubMap.MapInventory.AddToInventory(new Item(crate, "Crate", new Rectangle(300, 200, 50, 50)));
            MapManager.Instance.CurrentSubMap.MapEquipment.AddToEquipment(new Item(key, "Key", new Rectangle(200, 100, 25, 30)));

            MapManager.Instance.CurrentMap.GetSubmap(0, 2).MapInventory.AddToInventory(new Item(box, "Mysterious Box", new Rectangle(200, 200, 50, 50)));
            MapManager.Instance.CurrentMap.GetSubmap(0, 2).MapInventory.AddToInventory(new Item(artifact, "Advanced Camera", new Rectangle(200, 250, 50, 50)));
            MapManager.Instance.CurrentMap.GetSubmap(0, 2).MapInventory.AddToInventory(new Item(crate, "Crate", new Rectangle(320, 200, 50, 50)));
            MapManager.Instance.CurrentMap.GetSubmap(0, 2).MapEquipment.AddToEquipment(new Item(key, "Key", new Rectangle(100, 100, 25, 30)));

            MapManager.Instance.CurrentMap.GetSubmap(2, 0).MapInventory.AddToInventory(new Item(artifact, "Advanced Camera", new Rectangle(200, 250, 50, 50)));
            MapManager.Instance.CurrentMap.GetSubmap(2, 1).MapInventory.AddToInventory(new Item(crate, "Crate", new Rectangle(320, 200, 50, 50)));
            MapManager.Instance.CurrentMap.GetSubmap(3, 1).MapInventory.AddToInventory(new Item(crate, "Crate", new Rectangle(320, 200, 50, 50)));

            MapManager.Instance.CurrentMap.GetSubmap(4, 0).MapInventory.AddToInventory(new Item(crate, "Crate", new Rectangle(80, 80, 50, 50)));
            MapManager.Instance.CurrentMap.GetSubmap(4, 0).MapInventory.AddToInventory(new Item(crate, "Crate", new Rectangle(70, 200, 50, 50)));

            MapManager.Instance.CurrentMap.GetSubmap(4, 1).MapInventory.AddToInventory(new Item(crate, "Crate", new Rectangle(200, 200, 50, 50)));
            MapManager.Instance.CurrentMap.GetSubmap(4, 1).MapEquipment.AddToEquipment(new Item(key, "Key", new Rectangle(300, 150, 25, 30)));

            MapManager.Instance.CurrentMap.GetSubmap(4, 2).MapInventory.AddToInventory(new Item(crate, "Crate", new Rectangle(80, 80, 50, 50)));
            MapManager.Instance.CurrentMap.GetSubmap(4, 2).MapInventory.AddToInventory(new Item(crate, "Crate", new Rectangle(70, 200, 50, 50)));

            EnemyManager.Instance.CreateEnemy(200, 100, 50, 50, 3, 1, MapManager.Instance.CurrentMap.GetSubmap(0, 2), enemy);
            EnemyManager.Instance.CreateEnemy(400, 100, 50, 50, 3, 1, MapManager.Instance.CurrentMap.GetSubmap(0, 2), enemy);
            EnemyManager.Instance.CreateEnemy(200, 100, 50, 50, 3, 1, MapManager.Instance.CurrentMap.GetSubmap(0, 1), enemy);
            EnemyManager.Instance.CreateEnemy(200, 100, 50, 50, 3, 1, MapManager.Instance.CurrentMap.GetSubmap(2, 1), enemy);
            EnemyManager.Instance.CreateEnemy(200, 100, 50, 50, 3, 1, MapManager.Instance.CurrentMap.GetSubmap(3, 2), enemy);
            EnemyManager.Instance.CreateEnemy(200, 200, 50, 50, 3, 1, MapManager.Instance.CurrentMap.GetSubmap(3, 2), enemy);
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

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKb = currentKb;
            currentKb = Keyboard.GetState();

            switch (gameState)
            {
                case GameState.MainMenu:
                    if (currentKb.IsKeyDown(Keys.Enter))
                        gameState = GameState.Help;
                    break;
                case GameState.Help:
                    if (currentKb.IsKeyDown(Keys.Enter) && previousKb.IsKeyUp(Keys.Enter))
                    {
                        gameState = GameState.Game;
                        this.IsMouseVisible = false;
                    }
                    if (currentKb.IsKeyDown(Keys.H) && previousKb.IsKeyUp(Keys.H))
                    {
                        gameState = GameState.Game;
                        this.IsMouseVisible = false;
                    }
                    if (currentKb.IsKeyDown(Keys.E) && previousKb.IsKeyUp(Keys.E))
                    {
                        gameState = GameState.Equipment;
                        this.IsMouseVisible = true;
                    }
                    if (currentKb.IsKeyDown(Keys.I) && previousKb.IsKeyUp(Keys.I))
                    {
                        gameState = GameState.Inventory;
                        this.IsMouseVisible = true;
                    }
                    break;

                case GameState.Game:
                    PlayerManager.Instance.Player.PreviousX = PlayerManager.Instance.Player.X;
                    PlayerManager.Instance.Player.PreviousY = PlayerManager.Instance.Player.Y;

                    //if the player is moving change the moveby
                    int moveBy = 0;
                    if (PlayerManager.Instance.PlayerInventory.Count < 15)
                        moveBy = MovementDistance(PlayerManager.Instance.Player.Speed, (currentKb.IsKeyDown(Keys.W) != currentKb.IsKeyDown(Keys.S)) && (currentKb.IsKeyDown(Keys.A) != currentKb.IsKeyDown(Keys.D)));

                    //handles wall collision
                    /**************************************************************************/

                    //move the player horizontally in the correct direction
                    if (currentKb.IsKeyDown(Keys.A))
                        PlayerManager.Instance.Player.X -= moveBy;
                    if (currentKb.IsKeyDown(Keys.D))
                        PlayerManager.Instance.Player.X += moveBy;


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
                    //End Wall Collision
                    /******************************************************************************/

                    //Check to see if player can unlock a door
                    if (previousKb.IsKeyUp(Keys.Space))
                        MapManager.Instance.DoorCheck(currentKb);

                    //Stop the player from moving if they have collected 15 artifacts
                    if (PlayerManager.Instance.PlayerInventory.Count >= 15)
                    {
                        moveBy = 0;
                        if (currentKb.IsKeyDown(Keys.Enter) && previousKb.IsKeyUp(Keys.Enter))
                        {
                            gameState = GameState.End;
                            this.IsMouseVisible = true;
                        }
                    }

                    //Keys to open help, inventory and equipment screens
                    if (currentKb.IsKeyDown(Keys.H) && previousKb.IsKeyUp(Keys.H))
                    {
                        gameState = GameState.Help;
                        this.IsMouseVisible = true;
                    }
                    if (currentKb.IsKeyDown(Keys.I) && previousKb.IsKeyUp(Keys.I))
                    {
                        gameState = GameState.Inventory;
                        this.IsMouseVisible = true;
                    }
                    if (currentKb.IsKeyDown(Keys.E) && previousKb.IsKeyUp(Keys.E))
                    {
                        gameState = GameState.Equipment;
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
                        if (MapManager.Instance.CurrentSubMap.MapEquipment.CurrentEquipment.Count != 0)
                        {
                            for (int i = 0; i < MapManager.Instance.CurrentSubMap.MapEquipment.CurrentEquipment.Count; i++)
                            {
                                if (PlayerManager.Instance.Player.Rectangle.Intersects(MapManager.Instance.CurrentSubMap.MapEquipment.CurrentEquipment[i].MapPosition))
                                {
                                    MapManager.Instance.CurrentSubMap.MapEquipment.CurrentEquipment[i].AddToPlayerEquipment();
                                    MapManager.Instance.CurrentSubMap.MapEquipment.RemoveFromEquipment(MapManager.Instance.CurrentSubMap.MapEquipment.CurrentEquipment[i]);
                                }
                            }

                        }
                    }

                    if (PlayerManager.Instance.Player.X + PlayerManager.Instance.Player.Width / 2 < 0)
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


                    //Player attacks for now

                    if (currentKb.IsKeyDown(Keys.Right))
                    {
                        Attack pAttack = new Attack(new Rectangle(PlayerManager.Instance.Player.X + 50, PlayerManager.Instance.Player.Y + 20, 100, 30), weaponRight);
                        foreach (Enemy a in EnemyManager.Instance.EnemyList)
                        {
                            if (a.Location == MapManager.Instance.CurrentSubMap)
                            {
                                if (pAttack.MapPosition.Intersects(a.Rectangle))
                                    a.Hp -= 1;
                            }
                        }
                    }
                    if (currentKb.IsKeyDown(Keys.Up))
                    {
                        Attack pAttack = new Attack(new Rectangle(PlayerManager.Instance.Player.X + 20, PlayerManager.Instance.Player.Y - 90, 30, 100), weaponUp);
                        foreach (Enemy a in EnemyManager.Instance.EnemyList)
                        {
                            if (a.Location == MapManager.Instance.CurrentSubMap)
                            {
                                if (pAttack.MapPosition.Intersects(a.Rectangle))
                                    a.Hp -= 1;
                            }
                        }
                    }
                    if (currentKb.IsKeyDown(Keys.Left))
                    {
                        Attack pAttack = new Attack(new Rectangle(PlayerManager.Instance.Player.X - 85, PlayerManager.Instance.Player.Y + 20, 100, 30), weaponLeft);
                        foreach (Enemy a in EnemyManager.Instance.EnemyList)
                        {
                            if (a.Location == MapManager.Instance.CurrentSubMap)
                            {
                                if (pAttack.MapPosition.Intersects(a.Rectangle))
                                    a.Hp -= 1;
                            }
                        }
                    }
                    if (currentKb.IsKeyDown(Keys.Down))
                    {
                        Attack pAttack = new Attack(new Rectangle(PlayerManager.Instance.Player.X + 20, PlayerManager.Instance.Player.Y + 55, 30, 100), weaponDown);
                        foreach (Enemy a in EnemyManager.Instance.EnemyList)
                        {
                            if (a.Location == MapManager.Instance.CurrentSubMap)
                            {
                                if (pAttack.MapPosition.Intersects(a.Rectangle))
                                    a.Hp -= 1;
                            }
                        }
                    }
                    //End player attacks

                    //handles enemies
                    EnemyManager.Instance.Update();


                    break;
                case GameState.Inventory:
                    ms = Mouse.GetState();
                    if (currentKb.IsKeyDown(Keys.I) && previousKb.IsKeyUp(Keys.I))
                    {
                        gameState = GameState.Game;
                        this.IsMouseVisible = false;
                    }
                    if (currentKb.IsKeyDown(Keys.E) && previousKb.IsKeyUp(Keys.E))
                    {
                        gameState = GameState.Equipment;
                        this.IsMouseVisible = true;
                    }
                    if (currentKb.IsKeyDown(Keys.H) && previousKb.IsKeyUp(Keys.H))
                    {
                        gameState = GameState.Help;
                        this.IsMouseVisible = true;
                    }

                    if (currentKb.IsKeyDown(Keys.U) && previousKb.IsKeyUp(Keys.U))
                    {
                        PlayerManager.Instance.PlayerInventory.AddToInventory(new Item(crate, "test"));
                    }

                    if (currentKb.IsKeyDown(Keys.Y) && previousKb.IsKeyUp(Keys.Y))
                    {
                        MapManager.Instance.CurrentSubMap.MapInventory.AddToInventory(new Item(crate, "test", new Rectangle(100, 100, 50, 50)));
                    }
                    break;

                case GameState.Equipment:
                    if (currentKb.IsKeyDown(Keys.U) && previousKb.IsKeyUp(Keys.U))
                    {
                        PlayerManager.Instance.PlayerEquipment.AddToEquipment(new Item(crate, "test"));
                    }

                    if (currentKb.IsKeyDown(Keys.I) && previousKb.IsKeyUp(Keys.I))
                    {
                        gameState = GameState.Inventory;
                        this.IsMouseVisible = true;
                    }
                    if (currentKb.IsKeyDown(Keys.E) && previousKb.IsKeyUp(Keys.E))
                    {
                        gameState = GameState.Game;
                        this.IsMouseVisible = false;
                    }
                    if (currentKb.IsKeyDown(Keys.H) && previousKb.IsKeyUp(Keys.H))
                    {
                        gameState = GameState.Help;
                        this.IsMouseVisible = true;
                    }
                    break;
                case GameState.Puzzle:
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
                case GameState.MainMenu:
                    spriteBatch.Draw(mainLogo, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.DrawString(basicFont, "press enter", new Vector2(223, GraphicsDevice.Viewport.Height / 2 + 60), Color.White);
                    break;

                case GameState.Help:
                    spriteBatch.Draw(menuWall, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.MediumPurple);
                    spriteBatch.DrawString(basicFont, "how to play", new Vector2(GraphicsDevice.Viewport.Width / 2 - 55, 10), Color.Black);
                    spriteBatch.DrawString(basicFont, "controls", new Vector2(GraphicsDevice.Viewport.Width / 2 - 250, 100), Color.Black);
                    spriteBatch.DrawString(basicFont, "Move: ASDW", new Vector2(GraphicsDevice.Viewport.Width / 2 - 250, 140), Color.Black);
                    spriteBatch.DrawString(basicFont, "Grab Item: Space Bar", new Vector2(GraphicsDevice.Viewport.Width / 2 - 250, 155), Color.Black);
                    spriteBatch.DrawString(basicFont, "Open Inventory: I", new Vector2(GraphicsDevice.Viewport.Width / 2 - 250, 170), Color.Black);
                    spriteBatch.DrawString(basicFont, "Open Equipment: E", new Vector2(GraphicsDevice.Viewport.Width / 2 - 250, 185), Color.Black);
                    spriteBatch.DrawString(basicFont, "Open Help: H", new Vector2(GraphicsDevice.Viewport.Width / 2 - 250, 200), Color.Black);
                    spriteBatch.DrawString(basicFont, "Exit Game: Escape", new Vector2(GraphicsDevice.Viewport.Width / 2 - 250, 215), Color.Black);
                    spriteBatch.DrawString(basicFont, "press enter to continue to game", new Vector2(GraphicsDevice.Viewport.Width / 2 - 140, 300), Color.Black);
                    spriteBatch.DrawString(basicFont, "mission: uncover fifteen artifacts", new Vector2(GraphicsDevice.Viewport.Width / 2 - 145, 40), Color.White);
                    spriteBatch.Draw(astro, new Rectangle(500, 100, 130, 150), Color.White);
                    break;

                case GameState.Game:
                    MapManager.Instance.CurrentSubMap.Draw(spriteBatch, tilesheet);
                    if (MapManager.Instance.CurrentSubMap == MapManager.Instance.CurrentMap.GetSubmap(0, 1))
                    {
                        spriteBatch.Draw(logo, new Rectangle(285, 150, 200, 100), Color.White);
                    }
                    MapManager.Instance.CurrentSubMap.MapInventory.Draw(spriteBatch);
                    MapManager.Instance.CurrentSubMap.MapEquipment.Draw(spriteBatch);
                    PlayerManager.Instance.Player.Draw(spriteBatch);
                    EnemyManager.Instance.Draw(spriteBatch);
                    spriteBatch.Draw(hud, new Rectangle(15, 15, 130, 55), Color.NavajoWhite);
                    spriteBatch.DrawString(basicFont, "Artifacts: " + PlayerManager.Instance.PlayerInventory.Count, new Vector2(25, 20), Color.Black);
                    if (PlayerManager.Instance.PlayerInventory.Count >= 15)
                    {
                        spriteBatch.DrawString(basicFont, "MISSION_COMPLETE", new Vector2(289, 170), Color.Black);
                        spriteBatch.DrawString(basicFont, "press enter", new Vector2(334, 190), Color.LightGreen);
                    }

                    //playerattach
                    if (currentKb.IsKeyDown(Keys.Right))
                    {
                        spriteBatch.Draw(weaponRight, new Rectangle(PlayerManager.Instance.Player.X + 50, PlayerManager.Instance.Player.Y + 20, 100, 30), Color.White);
                    }
                    if (currentKb.IsKeyDown(Keys.Up))
                    {
                        spriteBatch.Draw(weaponUp, new Rectangle(PlayerManager.Instance.Player.X + 20, PlayerManager.Instance.Player.Y - 90, 30, 100), Color.White);
                    }
                    if (currentKb.IsKeyDown(Keys.Left))
                    {
                        spriteBatch.Draw(weaponLeft, new Rectangle(PlayerManager.Instance.Player.X - 85, PlayerManager.Instance.Player.Y + 20, 100, 30), Color.White);
                    }
                    if (currentKb.IsKeyDown(Keys.Down))
                    {
                        spriteBatch.Draw(weaponDown, new Rectangle(PlayerManager.Instance.Player.X + 20, PlayerManager.Instance.Player.Y + 55, 30, 100), Color.White);
                    }

                    //End player attacks


                    break;

                case GameState.Inventory:
                    spriteBatch.Draw(menuWall, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    PlayerManager.Instance.PlayerInventory.Draw(spriteBatch);
                    spriteBatch.DrawString(basicFont, "inventory", new Vector2(GraphicsDevice.Viewport.Width / 2 - 55, 10), Color.Black);
                    spriteBatch.DrawString(basicFont, "to_help: H", new Vector2(600, 10), Color.White);
                    spriteBatch.DrawString(basicFont, "to_equipment: E", new Vector2(60, 10), Color.White);
                    foreach (Item i in PlayerManager.Instance.PlayerInventory.Currinventory)
                    {
                        if (i.MapPosition.Contains(ms.Position))
                        {
                            i.Draw(spriteBatch, new Rectangle(200, 100, 300, 300), Color.Green);
                        }
                    }
                    break;

                case GameState.Equipment:
                    spriteBatch.Draw(menuWall, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Coral);
                    PlayerManager.Instance.PlayerEquipment.Draw(spriteBatch);
                    spriteBatch.DrawString(basicFont, "equipment", new Vector2(GraphicsDevice.Viewport.Width / 2 - 55, 10), Color.Black);
                    spriteBatch.DrawString(basicFont, "to_help: H", new Vector2(600, 10), Color.White);
                    spriteBatch.DrawString(basicFont, "to_inventory: I", new Vector2(60, 10), Color.White);
                    break;

                case GameState.End:
                    spriteBatch.Draw(menuWall, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.DrawString(basicFont, "thanks for playing", new Vector2(289, 170), Color.Black);
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
