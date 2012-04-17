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

        public Ray getRay(int x, int y)
        {
            float dy = 2.0f / (float)resolutionX;
            float dx = 2.0f / (float)resolutionY;
            ray.Direction = new Vector3(-1.0f + (float)x * dx, -1.0f + (float)y * dy, 0) - ray.Position;
            ray.Direction.Normalize();
            return ray;
        }
    }
}
