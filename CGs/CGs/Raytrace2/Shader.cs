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
    class Shader
    {
        public Vector3 light;
        Vector3 lightColor;
        Vector3 ambient;

        public Shader(Vector3 l)
        {
            light = l;
            ambient = new Vector3(0.2f, 0.2f, 0.2f);
        }

        public Shader(Vector3 lightPos, Vector3 lightColor)
        {
            this.light = lightPos;
            this.lightColor = lightColor;
        }

        public Vector3 shading(Vector3 v, Color col)
        {
            // 
            float dot = MathHelper.Clamp(-Vector3.Dot(light, v), 0, 1);
            float r = col.R / 255.0f * dot;
            float g = col.G / 255.0f * dot;
            float b = col.B / 255.0f * dot;
            var c = new Vector3(r, g, b) + ambient;
            return c;
        }
        public Vector3 shading(Vector3 pos, Vector3 norm, Ray ray, Actor actor, bool isShade)
        {
            // 点光源
            var l = light - pos;
            var l2 = l.LengthSquared();
            l.Normalize();

            // 視線
            var dir = ray.Position - pos;
            dir.Normalize();

            // ハーフベクトル
            var h = dir + l;
            h.Normalize();

            var ln = Vector3.Dot(l, norm);
            var hn = Vector3.Dot(h, norm);
            if (ln < 0) ln = 0;
            if (hn < 0) hn = 0;

            var dst = actor.GetColor(isShade ? 0.0f : ln, hn, pos);
            
            // 光源の色の反映
            dst.X *= lightColor.X;
            dst.Y *= lightColor.Y;
            dst.Z *= lightColor.Z;

            // 光の強さの適当な補正
            //dst *= Math.Min(1.5f, 500000.0f / (10000.0f + l2));
            //dst *= Math.Min(1.0f, l.Y + 0.1f);

            return dst;
        }
    }
}
