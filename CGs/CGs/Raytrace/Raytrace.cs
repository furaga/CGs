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

namespace CGs.Raytrace
{
    class Raytrace : CGs.Renderer
    {
        public override void Initialize(int w, int h)
        {
            Camera.resolutionX = w;
            Camera.resolutionY = h;
        }

        override public bool Draw(Color[] target, int w, int h)
        {
            if (target.Length < w * h)
            {
                MessageBox.Show("invalid size");
                return false;
            }

            Vector3 p = new Vector3(0, 0, 0);
            Color c = new Color(1.0f, 0.0f, 1.0f);
            Sphere s = new Sphere(0.5, p, c);
            Shader shader = new Shader(new Vector3(1, -1, -1));
            Vector3 from = new Vector3(0, 0, 0);
            Vector3 to = new Vector3(0, 0, -1);
            Camera cam = new Camera(from, to);
            
            for (int i = 0; i < w * h; i++)
            {
                cam.setFrom(i % w, i / w);
                Vector3 v;
                if (s.isIntersect(cam.from, cam.to, out v))
                {
                    target[i] = shader.shading(v);
                }
                else
                {
                    target[i] = Color.Gray;
                }
            }
            return true;
        }
    }
}
