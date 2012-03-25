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
    class Camera
    {
        public Ray ray;
        public static int resolutionX = 40;
        public static int resolutionY = 40;

        public Camera(Ray r)
        {
            ray = r;
        }

        public void setRay(int x, int y)
        {
            float d = 2.0f / (float)resolutionX;
            ray.Position.X = -1.0f + (float)x * d;
            ray.Position.Y = -1.0f + (float)y * d;
        }
    }
}
