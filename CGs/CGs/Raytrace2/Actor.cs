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
    public class Material
    {
        public Vector3 amb, dif, spec;
        public float specPower;
        public float reflection, refraction, diffuse;
        public Material(Vector3 amb, Vector3 dif, Vector3 spec, float specPower, float reflection = 0.0f, float refraction = 0.0f, float diffuse = 1.0f)
        {
            this.amb = amb;
            this.dif = dif;
            this.spec = spec;
            this.specPower = specPower;
            this.reflection = reflection;
            this.refraction = refraction;
            this.diffuse = diffuse;
        }
    }
    abstract class Actor
    {
        public Material material;
        abstract public bool isIntersect(Ray ray, out Vector3 norm, out Vector3 pos);

        virtual public Vector3 GetColor(float ln, float hn, Vector3 hitPos)
        {
            return material.amb + ln * material.dif+ (float)Math.Pow(hn, material.specPower) * material.spec;
        }
    }
}
