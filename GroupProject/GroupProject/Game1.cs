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
        Texture2D health;
        Item HealthBox;

        //For use in main menu and inventory screens
        Texture2D menuWall;
        Texture2D mainLogo;
        Texture2D hud;
        Texture2D astro;
        Texture2D logo;
        Texture2D screen;

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

        //Keep track of level
        int currentLevel;


        //for deconstructing a magnatude when using a 45degree angle int componets
        const double ANGLE_MULTIPLIER = 0.70710678118;

        enum GameState { MainMenu, Help, Game, Inventory, Equipment, Puzzle, End, Hub, Death, Load };
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
            currentLevel = 1;

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

            Profiler.Instance.Initialize(500, spriteBatch, GraphicsDevice);

            tilesheet = Content.Load<Texture2D>("tilesheet");
            Texture2D bot = Content.Load<Texture2D>("botcombat");
            crate = Content.Load<Texture2D>("Crate");
            menuWall = Content.Load<Texture2D>("InventoryBack");
            mainLogo = Content.Load<Texture2D>("mainBack");
            basicFont = Content.Load<SpriteFont>("Audiowide");
            hud = Content.Load<Texture2D>("hudBack");
            astro = Content.Load<Texture2D>("astroFront");
            key = Content.Load<Texture2D>("Card");
            health = Content.Load<Texture2D>("Health");
            box = Content.Load<Texture2D>("Crate2");
            artifact = Content.Load<Texture2D>("Artifact2");
            logo = Content.Load<Texture2D>("CompanyLogo");
            enemy = Content.Load<Texture2D>("Electric Enemy");
            weaponRight = Content.Load<Texture2D>("energyBlastRight");
            weaponLeft = Content.Load<Texture2D>("energyBlastLeft");
            weaponUp = Content.Load<Texture2D>("energyBlastUp");
            weaponDown = Content.Load<Texture2D>("energyBlastDown");
            screen = Content.Load<Texture2D>("Screen");

            TextureManager.Instance.Textures.Add("crate", crate);
            TextureManager.Instance.Textures.Add("box", box);
            TextureManager.Instance.Textures.Add("artifact", artifact);
            TextureManager.Instance.Textures.Add("key", key);
            TextureManager.Instance.Textures.Add("health", health);
            TextureManager.Instance.Textures.Add("enemy", enemy);

            TextureManager.Instance.Textures.Add("tilesheet", tilesheet);

            TextureManager.Instance.Textures.Add("weaponUp", weaponUp);
            TextureManager.Instance.Textures.Add("weaponDown", weaponDown);
            TextureManager.Instance.Textures.Add("weaponLeft", weaponLeft);
            TextureManager.Instance.Textures.Add("weaponRight", weaponRight);

            MapManager.Instance.NewMap("../../../Content/Levels/Ship Hub");

            PlayerManager.Instance.Player.SetTexture(astro);
            PlayerManager.Instance.PlayerInventory.Inventory.Add(new Item(crate, "Crate"));
            PlayerManager.Instance.PlayerInventory.Inventory.Add(new Item(box, "Mysterious Box"));
            PlayerManager.Instance.PlayerInventory.Inventory.Add(new Item(artifact, "Advanced Camera"));

            //Add health to the player's inventory
            HealthBox = new Item(health, "Health");
            PlayerManager.Instance.PlayerEquipment.AddToEquipment(new Item(health,"Health"));
            PlayerManager.Instance.PlayerEquipment.AddToEquipment(new Item(health, "Health"));
            PlayerManager.Instance.PlayerEquipment.AddToEquipment(new Item(health, "Health"));
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
            //Profiler.Instance.StartTimer();

            framesThisGameFrame = gameTime.ElapsedGameTime.TotalSeconds / SECONDS_PER_FRAME;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKb = currentKb;
            currentKb = Keyboard.GetState();

            

            switch (gameState)
            {
                case GameState.MainMenu:
                    if (currentKb.IsKeyDown(Keys.Enter) && previousKb.IsKeyUp(Keys.Enter))
                        gameState = GameState.Hub;
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

                case GameState.Hub:
                    PlayerManager.Instance.Player.Update();

                    //if the player is moving change the moveby
                    int moveBy = 0;
                    if (PlayerManager.Instance.PlayerInventory.Inventory.Count < 15)
                        moveBy = MovementDistance(PlayerManager.Instance.Player.Speed, (currentKb.IsKeyDown(Keys.W) != currentKb.IsKeyDown(Keys.S)) && (currentKb.IsKeyDown(Keys.A) != currentKb.IsKeyDown(Keys.D)));

                    //handles wall collision
                    /**************************************************************************/

                    //move the player horizontally in the correct direction
                    int moveX = 0;
                    int moveY = 0;

                    if (currentKb.IsKeyDown(Keys.A))
                        moveX -= moveBy;
                    if (currentKb.IsKeyDown(Keys.D))
                        moveX += moveBy;

                    if (currentKb.IsKeyDown(Keys.W))
                        moveY -= moveBy;
                    if (currentKb.IsKeyDown(Keys.S))
                        moveY += moveBy;

                    MapManager.Instance.CurrentSubMap.CollidingWalls(PlayerManager.Instance.Player, moveX, moveY);


                    //End Wall Collision
                    /******************************************************************************/

                    //Check to see if player can unlock a door
                    if (previousKb.IsKeyUp(Keys.Space))
                        MapManager.Instance.DoorCheck(currentKb);

                    //Keys to open inventory and equipment screens
                    if (currentKb.IsKeyDown(Keys.L) && previousKb.IsKeyUp(Keys.L))
                    {
                        if (PlayerManager.Instance.Player.Rectangle.Intersects(new Rectangle(200, 20, 150, 50)) || PlayerManager.Instance.Player.Rectangle.Intersects(new Rectangle(417, 20, 150, 50)))
                        {
                            gameState = GameState.Load;
                            this.IsMouseVisible = true;
                        }
                    }

                    //inventory and equipment

                    if (currentKb.IsKeyDown(Keys.Space))
                    {
                        for (int i = MapManager.Instance.CurrentSubMap.Inventory.Count - 1; i >= 0; i--)
                        {
                            if (PlayerManager.Instance.Player.Rectangle.Intersects(MapManager.Instance.CurrentSubMap.Inventory[i].MapPosition))
                            {
                                PlayerManager.Instance.PlayerInventory.Inventory.Add(MapManager.Instance.CurrentSubMap.Inventory[i]);
                                MapManager.Instance.CurrentSubMap.Inventory.RemoveAt(i);
                            }
                        }
                        for (int i = MapManager.Instance.CurrentSubMap.Equipment.Count - 1; i >= 0; i--)
                        {
                            if (PlayerManager.Instance.Player.Rectangle.Intersects(MapManager.Instance.CurrentSubMap.Equipment[i].MapPosition))
                            {
                                PlayerManager.Instance.PlayerEquipment.AddToEquipment(MapManager.Instance.CurrentSubMap.Equipment[i]);
                                MapManager.Instance.CurrentSubMap.Equipment.RemoveAt(i);
                            }
                        }
                    }

                    //moves submap when player walks to an edge
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

                    break;

                case GameState.Game:
                    PlayerManager.Instance.Player.Update();

                    //If the player has no health, switch to the death state
                    if(PlayerManager.Instance.Player.Hp <= 0)
                    {
                        gameState = GameState.Death;
                    }

                    //if the player is moving change the moveby
                    moveBy = 0;
                    if (PlayerManager.Instance.PlayerInventory.Inventory.Count < 15)
                        moveBy = MovementDistance(PlayerManager.Instance.Player.Speed, (currentKb.IsKeyDown(Keys.W) != currentKb.IsKeyDown(Keys.S)) && (currentKb.IsKeyDown(Keys.A) != currentKb.IsKeyDown(Keys.D)));

                    //handles wall collision
                    /**************************************************************************/

                    //move the player horizontally in the correct direction
                    moveX = 0;
                    moveY= 0;

                    if (currentKb.IsKeyDown(Keys.A))
                        moveX -= moveBy;
                    if (currentKb.IsKeyDown(Keys.D))
                        moveX += moveBy;

                    if (currentKb.IsKeyDown(Keys.W))
                        moveY -= moveBy;
                    if (currentKb.IsKeyDown(Keys.S))
                        moveY += moveBy;

                    MapManager.Instance.CurrentSubMap.CollidingWalls(PlayerManager.Instance.Player, moveX, moveY);

                    
                    //End Wall Collision
                    /******************************************************************************/

                    //Check to see if player can unlock a door
                    if (previousKb.IsKeyUp(Keys.Space))
                        MapManager.Instance.DoorCheck(currentKb);

                    //Stop the player from moving if they have collected 15 artifacts
                    if (PlayerManager.Instance.PlayerInventory.Inventory.Count >= 15)
                    {
                        moveBy = 0;
                        if (currentKb.IsKeyDown(Keys.Enter) && previousKb.IsKeyUp(Keys.Enter))
                        {
                            gameState = GameState.End;
                            currentLevel++;
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

                    //inventory and equipment

                    if (currentKb.IsKeyDown(Keys.Space))
                    {
                        for(int i = MapManager.Instance.CurrentSubMap.Inventory.Count - 1; i >= 0; i--)
                        {
                            if (PlayerManager.Instance.Player.Rectangle.Intersects(MapManager.Instance.CurrentSubMap.Inventory[i].MapPosition))
                            {
                                PlayerManager.Instance.PlayerInventory.Inventory.Add(MapManager.Instance.CurrentSubMap.Inventory[i]);
                                MapManager.Instance.CurrentSubMap.Inventory.RemoveAt(i);
                            }
                        }
                        for (int i = MapManager.Instance.CurrentSubMap.Equipment.Count - 1; i >= 0; i--)
                        {
                            if (PlayerManager.Instance.Player.Rectangle.Intersects(MapManager.Instance.CurrentSubMap.Equipment[i].MapPosition))
                            {
                                PlayerManager.Instance.PlayerEquipment.AddToEquipment(MapManager.Instance.CurrentSubMap.Equipment[i]);
                                MapManager.Instance.CurrentSubMap.Equipment.RemoveAt(i);
                            }
                        }
                    }

                        //moves submap when player walks to an edge
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


                    //Player attacks
                    if (currentKb.IsKeyDown(Keys.Up))
                        PlayerManager.Instance.PlayerAttackManager.Attack(Direction.Up);
                    else if (currentKb.IsKeyDown(Keys.Down))
                        PlayerManager.Instance.PlayerAttackManager.Attack(Direction.Down);
                    else if (currentKb.IsKeyDown(Keys.Left))
                        PlayerManager.Instance.PlayerAttackManager.Attack(Direction.Left);
                    else if (currentKb.IsKeyDown(Keys.Right))
                        PlayerManager.Instance.PlayerAttackManager.Attack(Direction.Right);

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
                        PlayerManager.Instance.PlayerInventory.Inventory.Add(new Item(crate, "test"));
                    }

                    if (currentKb.IsKeyDown(Keys.Y) && previousKb.IsKeyUp(Keys.Y))
                    {
                        MapManager.Instance.CurrentSubMap.Inventory.Add(new Item(crate, "test", new Rectangle(100, 100, 50, 50)));
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
                    //If the player hits space and health is less than 100, check for any health packs and use one
                    if(currentKb.IsKeyDown(Keys.Space) && previousKb.IsKeyUp(Keys.Space) && PlayerManager.Instance.Player.Hp < 100)
                    {
                        if(PlayerManager.Instance.PlayerEquipment.RemoveHealth() == true)
                            PlayerManager.Instance.Player.Hp = 100;
                    }
                    break;

                case GameState.End:
                    if (currentKb.IsKeyDown(Keys.Enter) && previousKb.IsKeyUp(Keys.Enter))
                    {
                        gameState = GameState.Hub;
                        this.IsMouseVisible = false;
                    }
                    MapManager.Instance.NewMap("../../../Content/Levels/Ship Hub");
                    foreach(Item i in PlayerManager.Instance.PlayerInventory.Inventory)
                    {
                        MapManager.Instance.AddItemToInventory(0, 0, i);
                    }
                    PlayerManager.Instance.PlayerInventory.Inventory.Clear();
                    break;

                case GameState.Load:
                    if (currentKb.IsKeyDown(Keys.Enter) && previousKb.IsKeyUp(Keys.Enter))
                    {
                        gameState = GameState.Game;
                        this.IsMouseVisible = false;
                    }
                    MapManager.Instance.NewMap("../../../Content/Levels/Level " + currentLevel);
                    PlayerManager.Instance.PlayerInventory.Inventory.Clear();
                    break;
                case GameState.Death:
                    if (currentKb.IsKeyDown(Keys.Enter) && previousKb.IsKeyUp(Keys.Enter))
                    {
                        gameState = GameState.MainMenu;
                        this.IsMouseVisible = false;
                    }
                    break;
            }
            //Profiler.Instance.StopTimer();
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
                    PlayerManager.Instance.Player.Hp = 100;
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

                case GameState.Hub:
                    MapManager.Instance.CurrentSubMap.Draw(spriteBatch);
                    //Draw items to fill the area
                    spriteBatch.Draw(crate, new Rectangle(200, 200, 50, 50), Color.NavajoWhite);
                    spriteBatch.Draw(artifact, new Rectangle(400, 150, 80, 80), Color.NavajoWhite);
                    spriteBatch.Draw(box, new Rectangle(150, 70, 30, 30), Color.NavajoWhite);
                    spriteBatch.Draw(box, new Rectangle(500, 170, 30, 30), Color.NavajoWhite);
                    spriteBatch.Draw(box, new Rectangle(200, 100, 30, 30), Color.NavajoWhite);
                    spriteBatch.Draw(crate, new Rectangle(600, 250, 50, 50), Color.NavajoWhite);
                    spriteBatch.Draw(crate, new Rectangle(650, 200, 50, 50), Color.NavajoWhite);
                    spriteBatch.Draw(screen, new Rectangle(200, 20, 150, 70), Color.NavajoWhite);
                    spriteBatch.Draw(screen, new Rectangle(417, 20, 150, 70), Color.NavajoWhite);
                    if(PlayerManager.Instance.Player.Rectangle.Intersects(new Rectangle(200,20,150,50)) || PlayerManager.Instance.Player.Rectangle.Intersects(new Rectangle(417, 20, 150, 50)))
                    {
                            spriteBatch.Draw(hud, new Rectangle(208, 345, 350, 50), Color.NavajoWhite);
                            spriteBatch.DrawString(basicFont, "Press L to Launch to New Location", new Vector2(225, 350), Color.Black);
                    }
                    //Draw the player
                    PlayerManager.Instance.Player.Draw(spriteBatch);
                    spriteBatch.Draw(hud, new Rectangle(15, 15, 130, 90), Color.NavajoWhite);
                    spriteBatch.DrawString(basicFont, "Level: " + currentLevel, new Vector2(25, 20), Color.Black);
                    spriteBatch.DrawString(basicFont, "Health: " + PlayerManager.Instance.Player.Hp, new Vector2(25, 38), Color.Black);
                    //Change the hud depending on the player's current level
                    if(currentLevel == 1)
                    {
                        spriteBatch.DrawString(basicFont, "Welcome to your safehouse...", new Vector2(260, 275),Color.Black);
                        spriteBatch.DrawString(basicFont, "At each site, uncover 15 artifacts to discover the history of this lost planet.", new Vector2(50, 300), Color.Black);
                    }
                    
                    break;

                case GameState.Game:
                    MapManager.Instance.CurrentSubMap.Draw(spriteBatch);
                    /*/if (MapManager.Instance.CurrentSubMap == MapManager.Instance.CurrentMap.GetSubmap(0, 1))
                    {
                        spriteBatch.Draw(logo, new Rectangle(285, 150, 200, 100), Color.White);
                    }/*/

                    PlayerManager.Instance.Player.Draw(spriteBatch);
                    spriteBatch.Draw(hud, new Rectangle(15, 15, 130, 90), Color.NavajoWhite);
                    spriteBatch.DrawString(basicFont, "Artifacts: " + PlayerManager.Instance.PlayerInventory.Inventory.Count, new Vector2(25, 20), Color.Black);
                    spriteBatch.DrawString(basicFont, "Health: " + PlayerManager.Instance.Player.Hp, new Vector2(25, 38), Color.Black);
                    if (PlayerManager.Instance.PlayerInventory.Inventory.Count >= 15)
                    {
                        spriteBatch.DrawString(basicFont, "MISSION_COMPLETE", new Vector2(289, 170), Color.Black);
                        spriteBatch.DrawString(basicFont, "press enter", new Vector2(334, 190), Color.LightGreen);
                    }

                    //playerattach
                    if (currentKb.IsKeyDown(Keys.Up))
                        PlayerManager.Instance.PlayerAttackManager.Draw(spriteBatch, Direction.Up);
                    else if (currentKb.IsKeyDown(Keys.Down))
                        PlayerManager.Instance.PlayerAttackManager.Draw(spriteBatch, Direction.Down);
                    else if (currentKb.IsKeyDown(Keys.Left))
                        PlayerManager.Instance.PlayerAttackManager.Draw(spriteBatch, Direction.Left);
                    else if (currentKb.IsKeyDown(Keys.Right))
                        PlayerManager.Instance.PlayerAttackManager.Draw(spriteBatch, Direction.Right);

                    //End player attacks


                    break;

                case GameState.Inventory:
                    spriteBatch.Draw(menuWall, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    PlayerManager.Instance.PlayerInventory.Draw(spriteBatch);
                    spriteBatch.DrawString(basicFont, "inventory", new Vector2(GraphicsDevice.Viewport.Width / 2 - 55, 10), Color.Black);
                    spriteBatch.DrawString(basicFont, "to_help: H", new Vector2(600, 10), Color.White);
                    spriteBatch.DrawString(basicFont, "to_equipment: E", new Vector2(60, 10), Color.White);
                    foreach (Item i in PlayerManager.Instance.PlayerInventory.Inventory)
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
                    spriteBatch.DrawString(basicFont, "use_health: space", new Vector2(GraphicsDevice.Viewport.Width / 2 - 85, 350), Color.Black);
                    spriteBatch.DrawString(basicFont, "to_help: H", new Vector2(600, 10), Color.White);
                    spriteBatch.DrawString(basicFont, "to_inventory: I", new Vector2(60, 10), Color.White);
                    break;

                case GameState.End:
                    spriteBatch.Draw(menuWall, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.DrawString(basicFont, "returning to safehouse...", new Vector2(269, 170), Color.Black);
                    break;

                case GameState.Load:
                    spriteBatch.Draw(menuWall, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.DrawString(basicFont, "entering unknown location...", new Vector2(265, 170), Color.Black);
                    break;

                case GameState.Death:
                    spriteBatch.Draw(menuWall, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);
                    spriteBatch.DrawString(basicFont, "mission_failed", new Vector2(289, 170), Color.Black);
                    break;

            }
            //Profiler.Instance.Draw(50, 50);
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
