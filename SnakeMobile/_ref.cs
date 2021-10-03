using System;
using System.Collections.Generic;
using Android.Media;
using Microsoft.Xna.Framework;

namespace SnakeMobile
{
    //references class
    public class _ref
    {
        //screen size
        public static int xsize;
        public static int ysize;

        //traps
        public static List<trap> tl = new List<trap>();


        //function to determine distance between two vectors
        public static double distance(Vector2 a, Vector2 b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}