using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CGs.Raytrace
{
    class Sphere
    {
        double r;
        Vector3 p;
        Color c;

        public Sphere(double rad, Vector3 pos, Color col)
        {
            r = rad;
            p = pos;
            c = col;
        }
        
        //---------------------------------------------------------------------
        // 球とベクトルto-fromが交差しているか。交差してたら交点の法線ベクトルをvに入れる
        //---------------------------------------------------------------------
        public bool isIntersect(Vector3 from, Vector3 to, out Vector3 v)
        {
            v = Vector3.Zero;
            Vector3 vec = from - p;
            double b = Vector3.Dot(to, vec);
            double c = vec.LengthSquared() - r * r;
            double d = b * b - c;
            if (d < 0) return false;
            double det = Math.Sqrt(d);
            double t = -b + det;
            if (t < 0) return false;
            Vector3 v1 = (float)t * to + from;
            v1 = v1 - p;
            v1.Normalize();
            v = v1;
            return true;
        }
    }
}
