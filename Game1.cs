using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FlappyF
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int W, H;
        KeyboardState currentKBState;
        KeyboardState previousKBState;
        bool Bosscome;
        bool isEnd;
        bool isOver;

        //Keys for pass each level
        int keys;

        SpriteFont font;

        int _elapedTime;

        // Player
        Player player1;
        Player player2;

        //Background
        //MovingBackground background0;
        //MovingBackground background1;
        MovingBackground background2;
        Texture2D bg2;

        //Enemy
        List<Enemy> enemies;
        Random random;
        Texture2D enemyTexture;
        TimeSpan enemySpawnTime;
        TimeSpan previousSpawnTime;

        //Boss
        //Boss boss1 = new Boss();
        Boss boss1;
        Texture2D boss1Texture;
        TimeSpan boss1enemySpawnTime;
        TimeSpan boss1previousSpawnTime;

        //Item
        List<Item> dash;
        Texture2D DashTexture;
        
        Animation boss1Animation = new Animation();
        //Wall
        ///Up wall
        List<Wall> walls1;
        Texture2D  WallTexture1;
        TimeSpan previouswallSpawnTime1;
        TimeSpan wallSpawnTime1;

        ///Down wall
        List<Wall> walls2;
        Texture2D WallTexture2;
        TimeSpan previouswallSpawnTime2;
        TimeSpan wallSpawnTime2;


        // Bullects
        List<Bullet> bullets1;
        List<Bullet> bullets2;
        Texture2D bulletTexture1;
        Texture2D bulletTexture2;
        TimeSpan fireTime;
        TimeSpan fireTime2;
        TimeSpan previousFireTime;
        TimeSpan previousFireTime2;

        //Song and sound
        SoundEffect HitSound;
        Song gameSong;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = W = 850;
            graphics.PreferredBackBufferHeight = H = 530;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            random = new Random();
            Bosscome = false;
            isEnd = false;
            isOver = false;

            keys = 0;

            _elapedTime = 0;

            player1 = new Player();
            player2 = new Player();


            boss1 = new Boss();
            boss1previousSpawnTime = TimeSpan.Zero;
            boss1enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            background2 = new MovingBackground();

            enemies = new List<Enemy>();
            previousSpawnTime = TimeSpan.FromSeconds(5.0f);
            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            bullets1 = new List<Bullet>();
            bullets2 = new List<Bullet>();
            fireTime = TimeSpan.FromSeconds(0.09f);
            previousFireTime = TimeSpan.Zero;


            walls1 = new List<Wall>();
            previouswallSpawnTime1 = TimeSpan.FromSeconds(5.0f);
            wallSpawnTime1 = TimeSpan.FromSeconds(1.5f);

            walls2 = new List<Wall>();
            previouswallSpawnTime2 = TimeSpan.FromSeconds(5.0f);
            wallSpawnTime2 = TimeSpan.FromSeconds(1.8f);

            dash = new List<Item>();

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

            // TODO: use this.Content to load your game content here
            Vector2 playerPos1 = new Vector2(30.0f, (H / 2) - 100);
            Animation playerAni1 = new Animation();

            Vector2 playerPos2 = new Vector2(30.0f, (H / 2) - 80);
            Animation playerAni2 = new Animation();

            Vector2 bossPos1 = new Vector2(650, (H / 2) - 100);
            Animation bossAni1 =  new Animation();

            HitSound = Content.Load<SoundEffect>("tama2");
            //gameSong = Content.Load<Song>("g1");

            //SoundEffect.MasterVolume = 0.05f;
            //MediaPlayer.Play(gameSong);
            //MediaPlayer.IsRepeating = true;
            //MediaPlayer.Volume = 0.15f;

            font = Content.Load<SpriteFont>("gameFont");

            enemyTexture = Content.Load<Texture2D>("spider");
            bulletTexture1 = Content.Load<Texture2D>("spell");
            bulletTexture2 = Content.Load<Texture2D>("power");

            WallTexture1 = Content.Load<Texture2D>("tree1");
            WallTexture2 = Content.Load<Texture2D>("tree3");

            DashTexture = Content.Load<Texture2D>("egg");

            playerAni1.Initialize(Content.Load<Texture2D>("Fairy"), Vector2.Zero, 125, 95, 4, 100, true);
            player1.Initialize(playerAni1, playerPos1);

            playerAni2.Initialize(Content.Load<Texture2D>("pony"), Vector2.Zero, 110, 95, 2, 100, true);
            player2.Initialize(playerAni2, playerPos2);

            bossAni1.Initialize(Content.Load<Texture2D>("ghost"), Vector2.Zero, 135, 150, 3, 100, true);
            boss1.Initialize(bossAni1, bossPos1, 200, 20, 5.0f);

            //boss1Texture = Content.Load<Texture2D>("pony");

            //background0.Initialize(Content.Load<Texture2D>("mainbackground"), 0, W, H);
            //background1.Initialize(Content.Load<Texture2D>("bgLayer1"), -1, W, H);
            background2.Initialize(Content.Load<Texture2D>("bg1"), -2, W, H);
            bg2 = Content.Load<Texture2D>("bg2");


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

            // TODO: Add your update logic here
            currentKBState = Keyboard.GetState();
            //P1
            if (currentKBState.IsKeyDown(Keys.W))
            {
                //player.Position.Y = player.Position.Y - player.Speed;
                player1.MoveUp();
            }
            else if (currentKBState.IsKeyUp(Keys.W))
            {
                player1.MoveDown();
            }
            //P2
            if (currentKBState.IsKeyDown(Keys.Up))
            {
                player2.MoveUp();
            }
            else if (currentKBState.IsKeyUp(Keys.Up))
            {
                player2.MoveDown();
            }

            player1.Position.X = MathHelper.Clamp(player1.Position.X, 0, W - player1.Width);
            player1.Position.Y = MathHelper.Clamp(player1.Position.Y, 0, H - player1.Height);
            player1.Update(gameTime);

            player2.Position.X = MathHelper.Clamp(player2.Position.X, 0, W - player2.Width);
            player2.Position.Y = MathHelper.Clamp(player2.Position.Y, 0, H - player2.Height);
            player2.Update(gameTime);

            boss1.Position.X = MathHelper.Clamp(boss1.Position.X, 0, W - boss1.Width);
            boss1.Position.Y = MathHelper.Clamp(boss1.Position.Y, 0, H - boss1.Height);
            boss1.Update(gameTime);

            if (gameTime.TotalGameTime - previousFireTime > fireTime)
            {
                if (currentKBState.IsKeyDown(Keys.Space))
                {
                    Vector2 bullet_posision1 = new Vector2(player1.Position.X + player1.Width, player1.Position.Y + player1.Height / 2);
                    AddBullet1(bullet_posision1);
                }
                if (currentKBState.IsKeyDown(Keys.RightControl))
                {
                    Vector2 bullet_posision2 = new Vector2(player2.Position.X + player2.Width, player2.Position.Y + player2.Height / 2);
                    AddBullet2(bullet_posision2);
                }
                previousFireTime = gameTime.TotalGameTime;
            }

            for (int i = bullets1.Count - 1; i >= 0; i--)
            {
                bullets1[i].Update();
                if (bullets1[i].isActive == false)
                {
                    bullets1.RemoveAt(i);
                }
            }
            for (int i = bullets2.Count - 1; i >= 0; i--)
            {
                bullets2[i].Update();
                if (bullets2[i].isActive == false)
                {
                    bullets2.RemoveAt(i);
                }
            }

            UpdateCollision();

            int spawnChance = random.Next(0, 1000);

            if (spawnChance < 5)
            {
             //spawn dash
                AddItem();
            }

            if (keys == 5)
            {
                Bosscome = true;
            }

            //else if (keys  == 10)
            //{
            //    Bosscome = true;
            //}


            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {
                AddEnemy();
                previousSpawnTime = gameTime.TotalGameTime;
            }

            if (Bosscome == false)
            {
                //Spawn Up Wall
                if (gameTime.TotalGameTime - previouswallSpawnTime1 > wallSpawnTime1)
                {
                    AddWall1();
                    previouswallSpawnTime1 = gameTime.TotalGameTime;
                }

                //Spawn Down Wall
                if (gameTime.TotalGameTime - previouswallSpawnTime2 > wallSpawnTime2)
                {
                    AddWall2();
                    previouswallSpawnTime2 = gameTime.TotalGameTime;
                }

            }

            if (player1.Health <= 0)
            {
                player1.isActive = false;
                if (player1.Health <= 0 && player2.Health <= 0)
                {
                    isOver = true;
                }
            }

            else if (player2.Health <= 0)
            {
                player2.isActive = false;
                if (player1.Health <= 0 && player2.Health <= 0)
                {
                    isOver = true;
                }
            }

            //if (player1.Health <= 0 && player2.Health <= 0)
            //{
            //    isOver = true;
            //}

            if (boss1.Health <= 0)
            {
                Bosscome = false;
                //Spawn Up Wall
                if (gameTime.TotalGameTime - previouswallSpawnTime1 > wallSpawnTime1)
                {
                    AddWall21();
                    previouswallSpawnTime1 = gameTime.TotalGameTime;
                }

                //Spawn Down Wall
                if (gameTime.TotalGameTime - previouswallSpawnTime2 > wallSpawnTime2)
                {
                    AddWall22();
                    previouswallSpawnTime2 = gameTime.TotalGameTime;
                }

                //background2.Initialize(Content.Load<Texture2D>("bg2"), -2, W, H);
                background2.Texture = bg2;
                background2.Speed = -10;

                if (keys == 10)
                {
                    Bosscome = true;
                    boss1.Health = 500;
                    keys = 11;
                }

                if (boss1.Health <= 0 && keys == 11)
                    {
                        isEnd = true;
                        Bosscome = false;
                    }
                

            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);

                if (enemies[i].isActive == false)
                {
                    enemies.RemoveAt(i);
                }
            }

            for (int i = walls1.Count - 1; i >= 0; i--)
            {
                    walls1[i].Update(gameTime);
            }

            for (int i = walls2.Count - 1; i >= 0; i--)
            {
                    walls2[i].Update(gameTime);
            }


            for (int i = dash.Count - 1; i >= 0; i--)
            {
                dash[i].Update(gameTime);
                if (dash[i].isActive == false)
                {
                    dash.RemoveAt(i);
                }
            }

            //background0.Update();
            //background1.Update();
            background2.Update();


            _elapedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (Bosscome == true)
            {
                background2.Draw(spriteBatch);

                player1.Draw(spriteBatch);
                player2.Draw(spriteBatch);

                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Draw(spriteBatch);
                }

                for (int i = 0; i < bullets1.Count; i++)
                {
                    bullets1[i].Draw(spriteBatch);
                }
                for (int i = 0; i < bullets2.Count; i++)
                {
                    bullets2[i].Draw(spriteBatch);
                }

                for (int i = 0; i < walls1.Count; i++)
                {
                    walls1[i].Draw(spriteBatch);
                }

                for (int i = 0; i < walls2.Count; i++)
                {
                    walls2[i].Draw(spriteBatch);
                }

                boss1.Draw(spriteBatch);

                if(player1.Health > 0)
                    {
                    spriteBatch.DrawString(font, "P1 lives: " + player1.Health, new Vector2(10, 500), Color.SkyBlue);
                }
                if (player2.Health > 0)
                {
                    spriteBatch.DrawString(font, "P2 lives: " + player2.Health, new Vector2(200, 500), Color.HotPink);
                }
                spriteBatch.DrawString(font, "Eggs: " + keys, new Vector2(10, 10), Color.Yellow);

                spriteBatch.DrawString(font, "Boss lives: " + boss1.Health, new Vector2(W - 300, 10), Color.Red);

            }

            else if (isEnd == true)
            {
                spriteBatch.DrawString(font, "YOU WIN", new Vector2(W / 2 - 40, H / 2), Color.Yellow);
            }

            else if (isOver == true)
            {
                spriteBatch.DrawString(font, "GAME OVER", new Vector2(W / 2 - 40, H / 2), Color.OrangeRed);
            }

            else if (Bosscome == false)
            {

                background2.Draw(spriteBatch);

                player1.Draw(spriteBatch);
                player2.Draw(spriteBatch);


                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Draw(spriteBatch);
                }

                for (int i = 0; i < bullets1.Count; i++)
                {
                    bullets1[i].Draw(spriteBatch);
                }
                for (int i = 0; i < bullets2.Count; i++)
                {
                    bullets2[i].Draw(spriteBatch);
                }


                for (int i = 0; i < walls1.Count; i++)
                {
                    walls1[i].Draw(spriteBatch);
                }

                for (int i = 0; i < walls2.Count; i++)
                {
                    walls2[i].Draw(spriteBatch);
                }

                for (int i = 0; i < dash.Count; i++)
                {
                    dash[i].Draw(spriteBatch);
                }

                if (player1.Health > 0)
                {
                    spriteBatch.DrawString(font, "P1 lives: " + player1.Health, new Vector2(10, 480), Color.SkyBlue);
                }
                if (player2.Health > 0)
                {
                    spriteBatch.DrawString(font, "P2 lives: " + player2.Health, new Vector2(200, 480), Color.HotPink);
                }

                spriteBatch.DrawString(font, "Eggs: " + keys, new Vector2(10, 10), Color.Yellow);

            }

            //else if (isEnd == true)
            //{
            //    spriteBatch.DrawString(font, "YOU WIN", new Vector2(W / 2 - 50, H / 2), Color.Yellow);
            //}

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void AddEnemy()
        {
            Animation enemyAnimation = new Animation();
            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 120, 61, 2, 100, true);

            Vector2 position = new Vector2();
            position.X = W;
            position.Y = random.Next(100, H - 100);

            Enemy enemy = new Enemy();
            enemy.Initialize(enemyAnimation, position);

            enemies.Add(enemy);
        }


        private void AddWall1()
        {
            Vector2 position = new Vector2();
            position.X = W;
            position.Y = random.Next(H - 700,H - 650);

            Wall wall1 = new Wall();
            wall1.Intialize(WallTexture1, position, W, 5.0f);

            walls1.Add(wall1);
        }

        private void AddWall2()
        {
            Vector2 position = new Vector2();
            position.X = W;
            position.Y = random.Next(H - 200, H - 150);

            Wall wall2 = new Wall();
            wall2.Intialize(WallTexture2, position, W, 5.0f);

            walls2.Add(wall2);
        }

        private void AddWall21()
        {
            Vector2 position = new Vector2();
            position.X = W;
            position.Y = random.Next(H - 700, H - 550);

            Wall wall1 = new Wall();
            wall1.Intialize(WallTexture1, position, W, 4.0f);

            walls1.Add(wall1);
        }

        private void AddWall22()
        {
            Vector2 position = new Vector2();
            position.X = W;
            position.Y = random.Next(H - 200, H - 50);

            Wall wall2 = new Wall();
            wall2.Intialize(WallTexture2, position, W, 4.0f);

            walls2.Add(wall2);
        }

        private void AddItem()
        {

            Vector2 position = new Vector2();
            //position.X = W;
            //position.Y = random.Next(20, H);

            //Item item = new Item();
            //item.Intialize(DashTexture, position, W);

            //dash.Add(item);

            position.X = W;
            position.Y = random.Next(100,H - 100);

            Item item = new Item();
            if  (item.ItemTexture == DashTexture);
            {
            item.Intialize(DashTexture, position);
            dash.Add(item);
            }
        }


        private void AddBullet1(Vector2 position)
        {
                Bullet bullet1 = new Bullet();
                bullet1.Intialize(bulletTexture1, position, W);
                bullets1.Add(bullet1);
        }

        private void AddBullet2(Vector2 position)
        {
            Bullet bullet2 = new Bullet();
            bullet2.Intialize(bulletTexture2, position, W);
            bullets2.Add(bullet2);
        }


        private void UpdateCollision()
        {
            Rectangle rectp1;
            Rectangle rectp2;
            Rectangle recte1;
            Rectangle rectb1;
            Rectangle rectb2;
            Rectangle rectboss1;
            Rectangle rectWup;
            Rectangle rectWdown;
            Rectangle rectdash;

            rectp1 = new Rectangle((int)player1.Position.X, (int)player1.Position.Y, player1.Width, player1.Height);
            rectp2 = new Rectangle((int)player2.Position.X, (int)player2.Position.Y, player2.Width, player2.Height);

            for (int i = 0; i < enemies.Count; i++)
            {
                // player collision
                recte1 = new Rectangle((int)enemies[i].Position.X, (int)enemies[i].Position.Y, enemies[i].Width, enemies[i].Height);

                if (rectp1.Intersects(recte1))
                {
                    player1.Health -= enemies[i].Damage;
                    enemies[i].Health = 0;
                    HitSound.Play();
                    if (player1.Health <= 0)
                    {
                        player1.isActive = false;
                    }
                }
                if (rectp2.Intersects(recte1))
                {
                    player2.Health -= enemies[i].Damage;
                    enemies[i].Health = 0;
                    HitSound.Play();
                    if (player2.Health <= 0)
                    {
                        player2.isActive = false;
                    }
                }

            }
            // bullet collision
            for (int i = 0; i < bullets1.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    rectb1 = new Rectangle((int)bullets1[i].Position.X, (int)bullets1[i].Position.Y, bullets1[i].Texture.Width, bullets1[i].Texture.Height);
                    recte1 = new Rectangle((int)enemies[j].Position.X, (int)enemies[j].Position.Y, enemies[j].Width, enemies[j].Height);
                    rectboss1 = new Rectangle((int)boss1.Position.X, (int)boss1.Position.Y, boss1.Width, boss1.Height);

                    if (rectb1.Intersects(recte1))
                    {
                        enemies[j].Health -= bullets1[i].Damage;
                        bullets1[i].isActive = false;
                    }
                

                    if (rectb1.Intersects(rectboss1) && Bosscome == true)
                    {
                        boss1.Health -= bullets1[i].Damage;
                        bullets1[i].isActive = false;
                    }
                }
            }
            for (int i = 0; i < bullets2.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    rectb2 = new Rectangle((int)bullets2[i].Position.X, (int)bullets2[i].Position.Y, bullets2[i].Texture.Width, bullets2[i].Texture.Height);                    
                    recte1 = new Rectangle((int)enemies[j].Position.X, (int)enemies[j].Position.Y, enemies[j].Width, enemies[j].Height);
                    rectboss1 = new Rectangle((int)boss1.Position.X, (int)boss1.Position.Y, boss1.Width, boss1.Height);

                    if (rectb2.Intersects(recte1))
                    {
                        enemies[j].Health -= bullets2[i].Damage;
                        bullets2[i].isActive = false;
                    }

                    if (rectb2.Intersects(rectboss1) && Bosscome == true)
                    {
                        boss1.Health -= bullets2[i].Damage;
                        bullets2[i].isActive = false;
                    }
                }



            }

            // wall collision
            ///up
            for (int i = 0; i < walls1.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    rectWup = new Rectangle((int)walls1[i].Position.X, (int)walls1[i].Position.Y, WallTexture1.Width, WallTexture1.Height);

                    if (rectp1.Intersects(rectWup))
                    {
                        player1.Health -= walls1[i].Damage;
                        walls1[i].Damage = 0;
                    }
                    if (rectp2.Intersects(rectWup))
                    {
                        player2.Health -= walls1[i].Damage;
                        walls1[i].Damage = 0;
                    }
                }
            }
            ///down
            for (int i = 0; i < walls2.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    rectWdown = new Rectangle((int)walls2[i].Position.X, (int)walls2[i].Position.Y, WallTexture2.Width, WallTexture2.Height);

                    if (rectp1.Intersects(rectWdown))
                    {
                        player1.Health -= walls2[i].Damage;
                        walls2[i].Damage = 0;
                    }
                    if (rectp2.Intersects(rectWdown))
                    {
                        player2.Health -= walls2[i].Damage;
                        walls2[i].Damage = 0;
                    }
                }
            }

            // Item collision
            ///Dash Item
            for (int i = 0; i < dash.Count; i++)
            {
                rectdash = new Rectangle((int)dash[i].Position.X, (int)dash[i].Position.Y, DashTexture.Width, DashTexture.Height);

                if (rectp1.Intersects(rectdash) && Bosscome == false)
                {
                    keys += dash[i].Impact;
                    dash[i].Impact = 0;
                }

                if (rectp2.Intersects(rectdash) && Bosscome == false)
                {
                    keys += dash[i].Impact;
                    dash[i].Impact = 0;
                }
            }

        }

    }
}
