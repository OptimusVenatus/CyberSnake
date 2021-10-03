using Java.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace SnakeMobile
{
    public class trap
    {
        private apple invocator;

        private readonly int size = 5;

        public trap(apple invocator)
        {
            this.invocator = invocator;
            reroll();
        }


        public int X { get; set; }
        public int Y { get; set; }

        public Vector2 pos()
        {
            return new Vector2(X, Y);
        }

        //this function is used to random place the trap
        public void reroll()
        {
            //get new coordinates
            var r = new Random();
            X = r.NextInt(_ref.xsize - 100) + 50;
            Y = r.NextInt(_ref.ysize - 100) + 50;
        }

        //This function is used to draw the trap on sprite batch
        public void draw(SpriteBatch _sb, GameTime _gt)
        {
            _sb.Begin();
            
            //drawing main square
            _sb.DrawLine(new Vector2(X - size, Y - size), new Vector2(X + 5, Y - 5), Color.Olive);
            _sb.DrawLine(new Vector2(X - size, Y - size), new Vector2(X - 5, Y + 5), Color.Olive);
            _sb.DrawLine(new Vector2(X + size, Y + size), new Vector2(X - 5, Y + 5), Color.Olive);
            _sb.DrawLine(new Vector2(X + size, Y + size), new Vector2(X + 5, Y - 5), Color.Olive);

            //drawing the triangle around
            _sb.DrawCircle(pos(), 50, 3, Color.DarkOliveGreen);
            _sb.End();
        }
    }
}