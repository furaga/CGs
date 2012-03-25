﻿using System;
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
        Vector3 light;
        Vector3 ambient;

        public Shader(Vector3 l)
        {
            light = l;
            ambient = new Vector3(0.3f, 0.3f, 0.3f);
        }

        public Color shading(Vector3 v, Color col)
        {
            float dot = Vector3.Dot(light, v);
            float r = col.R / 255.0f * dot;
            float g = col.G / 255.0f * dot;
            float b = col.B / 255.0f * dot;
            Color c = new Color(new Vector3(r, g, b) + ambient);
            return c;
        }
    }
}