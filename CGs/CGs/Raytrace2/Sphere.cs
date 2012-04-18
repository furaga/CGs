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
using System.Windows.Forms;

namespace CGs.Raytrace2
{
    class Sphere : Actor
    {
        double r;
        Vector3 center;

        public Sphere(double rad, Vector3 pos, Material mat)
        {
            r = rad;
            center = pos;
            material = mat;
        }

        override public bool isIntersect(Ray ray, out Vector3 norm, out Vector3 pos)
        {
            var from = ray.Position;
            var to = ray.Direction;

            norm = Vector3.Zero;
            pos = Vector3.Zero;
            
            var v = from - center;
            var b = Vector3.Dot(to, v);
            var c = v.LengthSquared() - r * r;
            var d = b * b - c;

            if (d < 0) return false;

            var det = Math.Sqrt(d);
            var t1 = - b + det;
            var t2 = - b - det;

            var t = 0.0;
            if (t1 >= 0 && t2 < 0) t = t1;
            else if (t2 >= 0) t = t2;
            else return false;

            pos = (float)t * to + from;
            norm = pos - center;
            norm.Normalize();
            return true;
        }
    }
}
