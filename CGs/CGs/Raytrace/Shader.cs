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
    class Shader
    {
        Vector3 light;
        double ambient = 0.3;

        public Shader(Vector3 l)
        {
            light = l;
        }

        public Color shading(Vector3 v)
        {
            Color c = new Color((float)ambient + Vector3.Dot(light, v), 0, 0);
            return c;
        }
    }
}
