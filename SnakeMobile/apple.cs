using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using Xamarin.Essentials;
using Random = Java.Util.Random;

namespace SnakeMobile
{
    public class apple
    {
        private readonly int size = 5;
        public int X { get; set; }
        public int Y { get; set; }

        public apple()
        {
            reroll();
        }

        public Vector2 pos()
        {
            return new Vector2(X, Y);
        }

        //function when apple is ate 
        public void eat(player pl)
        {
            //enlarge the plater
            pl.increment(3);
            
            //give him a  point
            pl.DeltaPoint++;
            
            //reset apple position 
            reroll();
            
            //creating new trap
            _ref.tl.Add(new trap(this));
            
            //vibrate
            var duration = TimeSpan.FromSeconds(0.25);
            Vibration.Vibrate(duration);
        }

        private void reroll()
        {
            //reseting coordinates
            var r = new Random();
            X = r.NextInt(_ref.xsize - 100) + 50;
            Y = r.NextInt(_ref.ysize - 100) + 50;
            
            //remove near traps for avoiding imposible situations
            for (var i = 0; i < _ref.tl.Count; i++)
                if (_ref.distance(pos(), _ref.tl[i].pos()) <= 50)
                    _ref.tl.RemoveAt(i);
        }

        //Drawing the apple with specified spritebatch
        public void draw(SpriteBatch _sb, GameTime _gt)
        {
            _sb.Begin();
            //drawing the core square
            _sb.DrawLine(new Vector2(X - size, Y - size), new Vector2(X + 5, Y - 5), Color.Red);
            _sb.DrawLine(new Vector2(X - size, Y - size), new Vector2(X - 5, Y + 5), Color.Red);
            _sb.DrawLine(new Vector2(X + size, Y + size), new Vector2(X - 5, Y + 5), Color.Red);
            _sb.DrawLine(new Vector2(X + size, Y + size), new Vector2(X + 5, Y - 5), Color.Red);
            
            //and the surrounding hexagon 
            _sb.DrawCircle(pos(), 50, 8, Color.IndianRed);
            _sb.End();
        }
    }
}