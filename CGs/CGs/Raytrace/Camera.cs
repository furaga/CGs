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
    class Camera
    {
        public Vector3 from, to;
        public static int resolutionX = 320;
        public static int resolutionY = 320;

        public Camera(Vector3 pos, Vector3 direc)
        {
            from = pos;
            to = direc;
        }

        public void setFrom(int x, int y)
        {
            float d = 2.0f / (float)resolutionX;
            from.X = -1.0f + (float)x * d;
            from.Y = -1.0f + (float)y * d;
        }
    }
}
