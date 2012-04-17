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
    class Plane : Actor
    {
        Vector3 offset, vx, vy;
        Vector3 norm;

        public Plane(Vector3 offset, Vector3 vx, Vector3 vy, Color color)
        {
            this.offset = offset;
            this.vx = vx;
            this.vx.Normalize();
            this.vy = vy;
            this.vy.Normalize();
            this.color = color;
            this.norm = Vector3.Cross(vx, vy);
            this.norm.Normalize();
        }

        //---------------------------------------------------------------------
        // 球とベクトルto-fromが交差しているか。交差してたら交点の法線ベクトルをvに入れる
        //---------------------------------------------------------------------
        override public bool isIntersect(Ray ray, out Vector3 norm, out Vector3 pos)
        {
            var from = ray.Position;
            var to = ray.Direction;

            norm = pos = Vector3.Zero;

            var b = (vy.X * vx.Z - vx.X * vy.Z) / (vx.Y * vy.Z - vy.Y * vx.Z);
            var c = (vy.X * vx.Y - vx.X * vy.Y) / (vx.Z * vy.Y - vy.Z * vx.Y);
            var d = offset.X + b * offset.Y + c * offset.Z;

            var de = to.X + b * to.Y + c * to.Z;
            if (de == 0) return false;

            var t = d - from.X - b * from.Y - c * from.Z;

            if (t < 0) return false;

            pos = t * to + from;
            norm = this.norm;

            

            return true;
        }
    }
}