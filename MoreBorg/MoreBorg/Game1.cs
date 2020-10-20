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

namespace MoreBorg
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D turret;
        Texture2D tubeDown;
        Texture2D tubeLeft;
        Texture2D tubeRight;
        Texture2D tubeUp;
        //Texture2D torpedoDown;
        Texture2D badguys;
        Texture2D torpedoUp;

        Rectangle turretRect;
        Rectangle tubeDownRect;
        Rectangle tubeLeftRect;
        Rectangle tubeRightRect;
        Rectangle tubeUpRect;
        //Rectangle torpedoDownRect;
        //Rectangle torpedoLeftRect;
        //Rectangle torpedoRightRect;
        Rectangle torpedoUpRect;
        Rectangle badGuyTorpedo;

        KeyboardState oldKB;

        Color colorUp;
        Color colorDown;
        Color colorLeft;
        Color colorRight;

        int xPos;
        int yPos;
      
        int badX = 350;
        int badY = 0;
        String directionOfBad;
        int timerForBad = 0;

        bool isFired;
        bool isBadFired;
        String facingDirection;
        String goingDirection;
        Rectangle badGuyRectangle;
        Texture2D badGuyTorpedoTexture;
        int tickForBadGuy = 240;

        int MJ = 100;
        int storedJ = -1;

        int torpWidth = 50;
        int torpLength = 50;

        int sizeToAdd = 0;

        int tickForCharge = 0;

        int xBadTorp = 400;
        int yBadTorp = 30;
        SpriteFont font1;
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
            // TODO: Add your initialization logic here
            turretRect = new Rectangle(230, 160, 300, 100);
            //
            tubeLeftRect = new Rectangle(250, 190, 100, 50);
            tubeRightRect = new Rectangle(460, 196, 100, 50);
            tubeUpRect = new Rectangle(380, 110, 50, 100);
            tubeDownRect = new Rectangle(380, 255, 50, 100);
            badGuyRectangle = new Rectangle(0, 0, 100, 93);
            badGuyTorpedo = new Rectangle(xBadTorp, yBadTorp, 50, 50);
            xPos = 380;
            yPos = 100;


            torpedoUpRect = new Rectangle(xPos, yPos, torpWidth, torpLength);

            facingDirection = "up";

            colorLeft = Color.Red;
            colorRight = Color.Red;
            colorDown = Color.Red;
            colorUp = Color.Green;

            
            isFired = false;
            isBadFired = false;
            sizeToAdd = 0;
            torpLength = 50;
            torpWidth = 50;
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
            turret = this.Content.Load<Texture2D>("Turret");
            tubeLeft = this.Content.Load<Texture2D>("Launch Tube Left");
            tubeRight = this.Content.Load<Texture2D>("Launch Tube Right");
            tubeUp = this.Content.Load<Texture2D>("Launch Tube Up");
            tubeDown = this.Content.Load<Texture2D>("Launch Tube Down");
            badGuyTorpedoTexture = this.Content.Load<Texture2D>("Torpedo Down");
            torpedoUp = this.Content.Load<Texture2D>("Torpedo Up");
            badguys = this.Content.Load<Texture2D>("badguyship");
            font1 = this.Content.Load<SpriteFont>("SpriteFont1");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            KeyboardState kb = Keyboard.GetState();
            if (isBadFired) {
                if (directionOfBad.Equals("up")) {
                    yBadTorp += 6;
                }
                else if (directionOfBad.Equals("down"))
                {
                    yBadTorp -= 6;
                }
                else if (directionOfBad.Equals("left"))
                {
                    xBadTorp += 6;
                }
                else if (directionOfBad.Equals("right"))
                {
                    xBadTorp -= 6;
                }
            }
            timerForBad++;
            if (timerForBad == tickForBadGuy)
            {
                Random random = new Random();
                int direction = random.Next(4);
                if (direction == 0)
                {
                    badX = 350;
                    badY = random.Next(30);
                    directionOfBad = "up";
                    badGuyTorpedoTexture = this.Content.Load<Texture2D>("Torpedo Down");
                }
                else if (direction == 1)
                {
                    badX = random.Next(150) + 520;
                    badY = 180;
                    directionOfBad = "right";
                    badGuyTorpedoTexture = this.Content.Load<Texture2D>("Torpedo Left");
                }
                else if (direction == 2)
                {
                    badX = 350;
                    badY = random.Next(200) + 300;
                    directionOfBad = "down";
                    badGuyTorpedoTexture = this.Content.Load<Texture2D>("Torpedo Up");
                }
                else if (direction == 3)
                {
                    badY = 180;
                    badX = random.Next(180);
                    directionOfBad = "left";
                    badGuyTorpedoTexture = this.Content.Load<Texture2D>("Torpedo Right");
                }
                xBadTorp = badX + 50;
                yBadTorp = badY + 30;
             
                tickForBadGuy = random.Next(181)+240;
                isBadFired = true;
                timerForBad = 0;
            }
            if (xBadTorp >= 270 && xBadTorp <= 490 && yBadTorp >= 170 && yBadTorp <= 250) {
                isBadFired = false;
                xBadTorp = badX + 50;
                yBadTorp = badY + 30;
            }
            badGuyTorpedo = new Rectangle(xBadTorp, yBadTorp, 50, 50);
            badGuyRectangle = new Rectangle(badX, badY, 100, 93);
            torpedoUpRect = new Rectangle(xPos, yPos, torpWidth, torpLength);

            tickForCharge++;
            if (tickForCharge == 60)
            {
                MJ += 3;
                if (MJ > 100)
                {
                    MJ = 100;
                }
                tickForCharge = 0;
            }
            if (storedJ == -1)
            {
                if (kb.IsKeyDown(Keys.NumPad0))
                {
                    storedJ = 0;
                }
                else if (kb.IsKeyDown(Keys.NumPad1))
                {
                    storedJ = 1;
                }
                else if (kb.IsKeyDown(Keys.NumPad2))
                {
                    storedJ = 2;
                }
                else if (kb.IsKeyDown(Keys.NumPad3))
                {
                    storedJ = 3;
                }
                else if (kb.IsKeyDown(Keys.NumPad4))
                {
                    storedJ = 4;
                }
                else if (kb.IsKeyDown(Keys.NumPad5))
                {
                    storedJ = 5;
                }
                else if (kb.IsKeyDown(Keys.NumPad6))
                {
                    storedJ = 6;
                }
                else if (kb.IsKeyDown(Keys.NumPad7))
                {
                    storedJ = 7;
                }
                else if (kb.IsKeyDown(Keys.NumPad8))
                {
                    storedJ = 8;
                }
                else if (kb.IsKeyDown(Keys.NumPad9))
                {
                    storedJ = 9;
                }
            }
            torpedoPosUpdate(kb, oldKB);
            if (isFired)
            {
                if (goingDirection.Equals("up"))
                {
                    torpLength = 50 + sizeToAdd;
                    yPos -= 6;

                }
                else if (goingDirection.Equals("down"))
                {
                    torpLength = 50 + sizeToAdd;
                    yPos += 6;
                }
                else if (goingDirection.Equals("left"))
                {
                    torpWidth = 50 + sizeToAdd;
                    xPos -= 6;
                }
                else if (goingDirection.Equals("right"))
                {
                    torpWidth = 50 + sizeToAdd;
                    xPos += 6;
                }
                // torpedoUpRect = new Rectangle(xPos, yPos, torpWidth, torpLength);
                if (yPos < -50 || yPos > 480 || xPos < -50 || xPos > 800)
                {
                    isFired = false;
                    storedJ = -1;
                    resetTorpedo();
                }
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            oldKB = kb;

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
            spriteBatch.Draw(turret, turretRect, Color.White);
            spriteBatch.Draw(tubeLeft, tubeLeftRect, colorLeft);
            spriteBatch.Draw(tubeRight, tubeRightRect, colorRight);
            spriteBatch.Draw(tubeUp, tubeUpRect, colorUp);
            spriteBatch.Draw(tubeDown, tubeDownRect, colorDown);
            spriteBatch.Draw(badguys, badGuyRectangle, Color.White);
            spriteBatch.Draw(badGuyTorpedoTexture, badGuyTorpedo, Color.White);
            spriteBatch.Draw(torpedoUp, torpedoUpRect, Color.White);

            spriteBatch.DrawString(font1, "MJ:" + MJ, new Vector2(510, 100), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void torpedoPosUpdate(KeyboardState kb, KeyboardState oldKB)
        {
            if (kb.IsKeyDown(Keys.Space) && !oldKB.IsKeyDown(Keys.Space) && !isFired)
            {
                isFired = true;
                goingDirection = facingDirection;
                if (storedJ == -1) { storedJ = 0; }
                if (storedJ > MJ)
                {
                    storedJ = MJ;
                }
                MJ -= storedJ;
                sizeToAdd = 10 * storedJ;
            }
            if (kb.IsKeyDown(Keys.Up) && !oldKB.IsKeyDown(Keys.Up))
            {
                colorUp = Color.Green;
                colorDown = Color.Red;
                colorLeft = Color.Red;
                colorRight = Color.Red;
                if (!isFired)
                {
                    torpedoUp = this.Content.Load<Texture2D>("Torpedo Up");
                }
                updateTorpedo(isFired, 380, 100, "up");
            }

            if (kb.IsKeyDown(Keys.Down) && !oldKB.IsKeyDown(Keys.Down))
            {
                colorDown = Color.Green;
                colorUp = Color.Red;
                colorLeft = Color.Red;
                colorRight = Color.Red;
                if (!isFired)
                {
                    torpedoUp = this.Content.Load<Texture2D>("Torpedo Down");
                }
                updateTorpedo(isFired, 380, 325, "down");
            }

            if (kb.IsKeyDown(Keys.Left) && !oldKB.IsKeyDown(Keys.Left))
            {
                colorLeft = Color.Green;
                colorDown = Color.Red;
                colorUp = Color.Red;
                colorRight = Color.Red;
                if (!isFired)
                {
                    torpedoUp = this.Content.Load<Texture2D>("Torpedo Left");
                }
                updateTorpedo(isFired, 250, 190, "left");
            }

            if (kb.IsKeyDown(Keys.Right) && !oldKB.IsKeyDown(Keys.Right))
            {
                colorRight = Color.Green;
                colorDown = Color.Red;
                colorLeft = Color.Red;
                colorUp = Color.Red;
                if (!isFired)
                {
                    torpedoUp = this.Content.Load<Texture2D>("Torpedo Right");
                }
                updateTorpedo(isFired, 510, 196, "right");
            }
        }
        public void updateTorpedo(Boolean hasBeenFired, int x, int y, String direction)
        {
            if (!hasBeenFired)
            {
                xPos = x;
                yPos = y;
            }
            torpedoUpRect = new Rectangle(xPos, yPos, torpWidth, torpLength);
            facingDirection = direction;
        }
        public void resetTorpedo()
        {
            torpWidth = 50;
            torpLength = 50;
            if (facingDirection.Equals("right"))
            {
                xPos = 510;
                yPos = 196;
                torpedoUp = this.Content.Load<Texture2D>("Torpedo Right");
            }
            else if (facingDirection.Equals("left"))
            {
                xPos = 250;
                yPos = 190;
                torpedoUp = this.Content.Load<Texture2D>("Torpedo Left");
            }
            else if (facingDirection.Equals("up"))
            {
                xPos = 380;
                yPos = 100;
                torpedoUp = this.Content.Load<Texture2D>("Torpedo Up");
            }
            else if (facingDirection.Equals("down"))
            {
                xPos = 380;
                yPos = 325;
                torpedoUp = this.Content.Load<Texture2D>("Torpedo Down");
            }

        }
    }
}
