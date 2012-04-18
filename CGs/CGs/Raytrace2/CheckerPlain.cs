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
    class CheckerPlain : Actor
    {
        Vector3 pos;
        Vector3 norm, v1, v2;
        Material material2;

        public CheckerPlain(Vector3 p, Vector3 norm, Material mat, Material mat2)
        {
            norm.Normalize();
            this.pos = p;
            this.norm = norm;
            this.material = mat;
            this.material2 = mat2;
            v1 = new Vector3(norm.Z, 0, -norm.X);
            v2 = new Vector3(0, norm.Z, -norm.Y);
            v1.Normalize();
            v2.Normalize();
        }

        override public bool isIntersect(Ray ray, out Vector3 normal, out Vector3 position)
        {
            var from = ray.Position;
            var dir = ray.Direction;

            normal = position = Vector3.Zero;

            var dot = Vector3.Dot(dir, norm);
            if (dot >= -0.00001f)
            {
                return false;
            }

            var t = Vector3.Dot(pos - from, norm) / dot;
            if (t < 0)
            {
                return false;
            }

            position = from + t * dir;
            normal = norm;

            return true;
        }

        override public Vector3 GetColor(float ln, float hn, Vector3 hitPos)
        {
            var v = hitPos - pos;
            var s = (int)Math.Floor(4 * v.X);
            var t = (int)Math.Floor(1 * v.Z);
            var mat = material;
            if ((s + t) % 2 == 0)
            {
                mat = material2;
            }
            return mat.amb + ln * mat.dif + (float)Math.Pow(hn, mat.specPower) * mat.spec;
        }
    }
}