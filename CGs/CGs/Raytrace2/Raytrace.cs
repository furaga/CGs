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
    class Raytrace : CGs.Renderer
    {
        Camera cam;
        List<Actor> actors;
        Sphere s;
        Shader shader;

        public override void Initialize(int w, int h)
        {
            Camera.resolutionX = w;
            Camera.resolutionY = h;

            actors = new List<Actor>();

            for (int i = 0; i < 15; i++)
            {
                var p = new Vector3(0.7f * (float)Math.Sin(MathHelper.ToRadians(24 * i)), 0.7f * (float)Math.Cos(MathHelper.ToRadians(24 * i)), 0);
                var c = new Color(1.0f / 15 * i, 0.0f, 0.0f);
                actors.Add(new Sphere(0.1, p, c));
            }

            shader = new Shader(new Vector3(1, 1, -1));

            var from = new Vector3(0, 0, 10);
            var to = new Vector3(0, 0, -1);
            cam = new Camera(new Ray(from, to));
        }

        override public bool Draw(Color[] target, int w, int h)
        {
            if (target.Length < w * h)
            {
                MessageBox.Show("invalid size");
                return false;
            }
            for (int i = 0; i < w * h; i++)
            {
                target[i] = trace(i % w, i / w);
            }
            return true;
        }

        Color trace(int x, int y)
        {
            cam.setRay(x, y);
            var v = Vector3.Zero;
            foreach (var actor in actors)
            {
                if (actor.isIntersect(cam.ray, out v))
                {
                    return shader.shading(v, actor.color);
                }
            }
            return Color.Gray;
        }
    }
}
