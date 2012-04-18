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
    class TriangleMesh : Actor
    {
        Vector3 p0, p1, p2;
        Vector3 norm;

        public TriangleMesh(Vector3 p0, Vector3 p1, Vector3 p2, Material mat)
        {
            this.p0 = p0;
            this.p1 = p1;
            this.p2 = p2;
            this.material = mat;
            this.norm = Vector3.Cross(p2 - p0, p1 - p0);
            this.norm.Normalize();
        }

        //---------------------------------------------------------------------
        // 球とベクトルto-fromが交差しているか。交差してたら交点の法線ベクトルをvに入れる
        //---------------------------------------------------------------------
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

            var t = Vector3.Dot(p0 - from, norm) / dot;
            if (t < 0)
            {
                return false;
            }

            var p = from + t * dir;
            var d0 = p - p0;
            var d1 = p1 - p0;
            var c = Vector3.Cross(d0, d1);
            if (Vector3.Dot(c, norm) < 0)
            {
                return false;
            }

            d0 = p - p1;
            d1 = p2 - p1;
            c = Vector3.Cross(d0, d1);
            if (Vector3.Dot(c, norm) < 0)
            {
                return false;
            }

            d0 = p - p2;
            d1 = p0 - p2;
            c = Vector3.Cross(d0, d1);
            if (Vector3.Dot(c, norm) < 0)
            {
                return false;
            }

            position = p;
            normal = norm;

            return true;
        }
    }
}