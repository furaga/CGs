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
        Shader shader;

        public override void Initialize(int w, int h)
        {
            Camera.resolutionX = w;
            Camera.resolutionY = h;

            actors = new List<Actor>();
            
            var p = new Vector3(-0.40f, 0, -1);
            var c = new Color(0.1f, 0.1f, 0.1f);
            actors.Add(new Sphere(0.4, p, c));

            p = new Vector3(0.40f, 0f, 0.3f);
            c = new Color(0.0f, 1.0f, 0.0f);
            actors.Add(new Sphere(0.4, p, c));

            var offset = new Vector3(0, 0, -10);
            var vx = new Vector3(0.1f, 0.0f, 0);
            var vy = new Vector3(0.0f, 0.1f, 0);
            actors.Add(new Plane(offset, vx, vy, Color.Red));

            shader = new Shader(new Vector3(0, 0, -1));

            var from = new Vector3(0, 0, 5);
            var dir = new Vector3(0, 0, -1);
            cam = new Camera(new Ray(from, dir));
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
                target[i] = new Color(trace(cam.getRay(i % w, i / h), null));
            }
            return true;
        }

        Vector3 trace(Ray ray, Actor sender)
        {
            var nearestNorm = Vector3.Zero;
            var nearestPos = Vector3.Zero;
            Actor nearestActor = null;
            var color = Vector3.Zero;

            foreach (var actor in actors)
            {
                if (actor == sender) continue;

                var norm = Vector3.Zero;
                var pos = Vector3.Zero;
                if (actor.isIntersect(ray, out norm, out pos))
                {
                    var d1 = pos - ray.Position;
                    var d2 = nearestPos - ray.Position;
                    if (nearestActor == null)
                    {
                        nearestActor = actor;
                        nearestNorm = norm;
                        nearestPos = pos;
                    }
                    else if (d1.LengthSquared() < d2.LengthSquared())
                    {
                        nearestActor = actor;
                        nearestNorm = norm;
                        nearestPos = pos;
                    }
                }
            }

            if (nearestActor == null)
            {
                return Vector3.Zero;
            }

            var newRay = new Ray(nearestPos, nearestNorm);

            var reflect = trace(newRay, nearestActor);

            return shader.shading(nearestNorm, nearestActor.color) + reflect;
        }
    }
}
