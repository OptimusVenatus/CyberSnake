using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SnakeMobile
{
    public class player
    {
        public static int point;

        public int DeltaPoint;
        private readonly List<Vector2> history = new List<Vector2>();
        
        public bool living { get; set; }

        public double speed { get; set; } = 2;

        public player()
        {
            //creating the first player rings
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);

            living = true;
            speed = 2;
        }

        

        //return player X coordinates with last ring
        public double X()
        {
            return history[history.Count - 1].X;
        }

        //same but for Y
        public double y()
        {
            return history[history.Count - 1].Y;
        }

        //Same but in Vector
        public Vector2 pos()
        {
            return new Vector2((float)X(), (float)y());
        }

        //function for adding new ring to the snake
        public void AddCoord(double x, double y)
        {
            //removing old ring (optimisation purposes)
            for (var i = 0; i < history.Count - (point + 3); i++) history.RemoveAt(i);


            history.Add(new Vector2((float)x, (float)y));
        }

        //function to get all player's rings in array
        public Vector2[] hist()
        {
            return history.ToArray();
        }

        //adding new ring capacity to the snake for expending it
        public void increment(int p = 1)
        {
            point = point + p;
        }

        //reset the snake
        public void reset()
        {
            point = 0;
            var history = new List<Vector2>();
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);
            AddCoord(_ref.xsize / 2, _ref.ysize / 2);
        }
    }
}