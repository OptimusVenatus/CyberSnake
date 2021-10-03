using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace SnakeMobile
{
    public class star
    {
        
        private readonly int size = 5;

        public star()
        {
            var r = new Random();
            X = _ref.xsize / 2;
            Y = _ref.ysize / 2;
            angle = (float)(r.NextDouble() * (Math.PI * 2));
            speed = r.NextDouble() + 1;
            living = true;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public float angle { get; set; }

        public double speed { get; set; }

        public bool living { get; set; }

        //function to draw a star
        public void draw(SpriteBatch _sb)
        {
            _sb.Begin();
            //it's basically square drawing
            _sb.DrawLine(new Vector2((float)(X - size), (float)(Y - size)), new Vector2((float)(X + 5), (float)(Y - 5)),
                Color.White);
            _sb.DrawLine(new Vector2((float)(X - size), (float)(Y - size)), new Vector2((float)(X - 5), (float)(Y + 5)),
                Color.White);
            _sb.DrawLine(new Vector2((float)(X + size), (float)(Y + size)), new Vector2((float)(X - 5), (float)(Y + 5)),
                Color.White);
            _sb.DrawLine(new Vector2((float)(X + size), (float)(Y + size)), new Vector2((float)(X + 5), (float)(Y - 5)),
                Color.White);
            _sb.End();
        }

        //Moving the star
        public void live(SpriteBatch sb)
        {
            //Moving the star depending of the angle and his speed
            if (living)
            {
                X = (float)X + Math.Cos(angle) * speed;
                Y = (float)Y + Math.Sin(angle) * speed;
            }
            else
            {
                //if the star quit screen bounds reset it
                var r = new Random();
                X = _ref.xsize / 2;
                Y = _ref.ysize / 2;
                angle = (float)(r.NextDouble() * (Math.PI * 2));
                speed = r.NextDouble() + 1;
                living = true;
            }
            //killing the star if exit screen bounds
            if (X > _ref.xsize || X < 0) living = false;

            if (Y > _ref.ysize || Y < 0) living = false;
        }
    }
}