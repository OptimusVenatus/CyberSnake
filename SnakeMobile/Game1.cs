using System;
using System.Collections.Generic;
using Android.Content.Res;
using Java.Lang;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using Xamarin.Essentials;
using DisplayOrientation = Microsoft.Xna.Framework.DisplayOrientation;
using Math = System.Math;

namespace SnakeMobile
{
    public class Game1 : Game
    {
        // get screen size
        public static int xsize;
        public static int ysize;

        //the state value is used for alternating between game and game over menu
        public static string State;

        //cretion of stars list 
        //stars are used for animating the menu of the game
        private static List<star> sl;

        //angle is the angle used by the snake for movement
        public static double angle = 2 * Math.PI / 3;
        
        //creation of player
        private player _pl;
        
        //private int deathtime;

        private Song eatS;

        private Texture2D LogoT;
        private Texture2D PlayT;
        private Texture QuitT;
        private Texture2D ReplayT;
        private apple target;
        
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.SupportedOrientations = DisplayOrientation.Portrait;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        { 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loading textures sprites
            LogoT = Content.Load<Texture2D>("logo");
            PlayT = Content.Load<Texture2D>("play");
            ReplayT = Content.Load<Texture2D>("PlayAgain");
            QuitT = Content.Load<Texture2D>("quit");

            //List of stars for main menu
            sl = new List<star>();


            //set the state of the game to menu
            State = "menu";
            
            //getting the screen size
            var metrics = Resources.System.DisplayMetrics;
            ysize = metrics.HeightPixels;
            xsize = metrics.WidthPixels;
            _ref.xsize = xsize;
            _ref.ysize = ysize;

            //creating the main menu stars (stars are the sqare background animations)
            for (var i = 0; i < 1000; i++)
            {
                sl.Add(new star());
            };

            _pl = new player();
            //the apple is called target
            target = new apple();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        private void ChangeState()
        {
            if (State == "menu")
            {
                _pl = new player();
                _ref.tl = new List<trap>();
                State = "game";
            }
            else if (State == "game")
            {
                State = "menu";
            }
        }

        protected override void Update(GameTime gameTime)
        {
            //if the player is exiting the game with the return button of android
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed && State == "game")
            {
                _pl.living = false;

                angle = 2 * Math.PI / 3;
                _pl = new player();
                _pl.reset();
                _ref.tl = new List<trap>();
                State = "menu";
                Thread.Sleep(10);
            }

            //Game update loop
            if (State == "game")
            {
                //this condition is executed every 50 millis
                if (gameTime.TotalGameTime.Milliseconds % 50 == 0 && _pl.living)
                {
                    //next coordinates of the snake depending on the move angle 
                    var nx = (float)_pl.X() + (float)Math.Cos(angle) * 20;
                    var ny = (float)_pl.y() + (float)Math.Sin(angle) * 20;


                    //this stand here for cross the screen boundaries 
                    if (nx >= xsize)
                        nx = nx - xsize;
                    else if (nx < 0) nx = nx + xsize;

                    if (ny >= ysize)
                        ny = ny - ysize;
                    else if (ny < 0) ny = ysize;

                    //add coordinates of new "circle" of the snake
                    _pl.AddCoord(nx, ny);
                }

                //calculating the distance between player and apple for collision detection
                //unfortunately the hitbox is round shaped but it does the job
                if (_ref.distance(_pl.pos(), target.pos()) <= 20)
                    target.eat(_pl);

                //trap life cycle
                foreach (var t in _ref.tl)
                {
                    //if trap is in collison of player and player is living kill the player
                    //basically distance based hit detection
                    //unfortunately the hitbox is round shaped but it does the job
                    if (_ref.distance(t.pos(), _pl.pos()) <= 20 && _pl.living)
                    {
                        //vibrate phone
                        var duration = TimeSpan.FromSeconds(1);
                        Vibration.Vibrate(duration);
                        
                        //kill player
                        _pl.living = false;
                        
                        //reset all values
                        angle = 2 * Math.PI / 3;  //reset angle
                        _pl = new player();       //create new player
                        _pl.reset();              //and reset it
                        _ref.tl = new List<trap>();//reset traps
                        
                        //auto replay game
                        State = "game";

                        //waiting for one second for auto replay and correct vibration
                        Thread.Sleep(1000);
                    }
                }

                //getting the first touch input on the screen
                var touchCollection = TouchPanel.GetState();
                if (touchCollection.Count > 0)
                {
                    var tl = touchCollection[0];
                    
                    //Calculating the movement angle of the snake
                    if (tl.Position.X > xsize / 2)
                    {
                        angle = Math.Atan(
                            (tl.Position.Y - (ysize - ysize / 5)) /
                            (tl.Position.X - xsize / 2)
                        );
                    }
                    else
                    {
                        angle = Math.Atan(
                            (tl.Position.Y - (ysize - ysize / 5)) /
                            (tl.Position.X - xsize / 2)
                        ) + Math.PI;
                    }
                }
            }

            //Menu update loop
            if (State == "menu")
            {
                //update star position
                foreach (var s in sl)
                {
                    s.live(_spriteBatch);
                }

                //if the touch screen have an input -> change the game state to actual game
                var touchCollection = TouchPanel.GetState();
                if (touchCollection.Count > 0) ChangeState();
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            //Game drawing loop
            if (State == "game")
            {
                _spriteBatch.Begin();
                GraphicsDevice.Clear(Color.Black);


                //drawing all the square of the snake
                foreach (var v in _pl.hist()) _spriteBatch.DrawCircle(v, 10, 100, Color.Aquamarine);


                //drawing the joystick borders
                _spriteBatch.DrawCircle(
                    new Vector2((float)(xsize / 2 + Math.Cos(angle) * xsize / 10),
                        (float)(ysize - ysize / 5 + Math.Sin(angle) * xsize / 10)), xsize / 10, 100, Color.DarkBlue,
                    5);
                //drawing the joystick center
                _spriteBatch.DrawCircle(new Vector2(xsize / 2, ysize - ysize / 5), xsize / 5, 100, Color.Aqua, 5);


                _spriteBatch.End();
                
                //drawing the apple
                target.draw(_spriteBatch, gameTime);
                
                //drawing traps
                foreach (var t in _ref.tl) t.draw(_spriteBatch, gameTime);

                base.Draw(gameTime);
            }
            else if (State == "menu")
            {
                GraphicsDevice.Clear(Color.Black);

                foreach (star v in sl)
                {
                    v.draw(_spriteBatch);
                }
                _spriteBatch.Begin();

                //drawing the cybersnake logo
                _spriteBatch.Draw(LogoT, new Rectangle(0, 0, _ref.xsize, _ref.ysize / 5), Color.White);
                
                //drawing the replay button
                _spriteBatch.Draw(PlayT,
                    new Rectangle(100, (int)Math.Round(_ref.ysize / 2.5), _ref.xsize - 200, _ref.ysize / 5),
                    Color.White);
                _spriteBatch.End();
                
                base.Draw(gameTime);
            }
        }
    }
}