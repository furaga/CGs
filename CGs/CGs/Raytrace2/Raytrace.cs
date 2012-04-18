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
        const int MAX_DEPTH = 3;

        public override void Initialize(int w, int h)
        {
            Camera.resolutionX = w;
            Camera.resolutionY = h;

            actors = new List<Actor>();

            var offset = -3;
            var z0 = -5;

            // シェーディング

            shader = new Shader(new Vector3(0, -1, offset + 0.5f * z0), new Vector3(10, 9, 5));
            //shader = new Shader(Vector3.Zero, new Vector3(10, 9, 5));

            #region
            //--------------------------------------------------
            // 部屋を作成
            //--------------------------------------------------

            var material1 =
                new Material(
                    new Vector3(0.05f, 0.01f, 0.01f),
                    new Vector3(0.05f, 0.01f, 0.01f),
                    new Vector3(0, 0, 0),
                    32.0f
                );

            var material2 =
                new Material(
                    new Vector3(0.01f, 0.01f, 0.05f),
                    new Vector3(0.01f, 0.01f, 0.05f),
                    new Vector3(0, 0, 0),
                    32.0f
                );

            var material3 =
                new Material(
                    new Vector3(0.05f, 0.05f, 0.05f),
                    new Vector3(0.05f, 0.05f, 0.05f),
                    new Vector3(0, 0, 0),
                    32.0f
                );

            var material4 =
                new Material(
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0, 0, 0),
                    32.0f
                );

            var material5 =
                new Material(
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0, 0, 0),
                    32.0f,
                    0.9f,
                    0.0f,
                    0.1f
                );

            var material6 =
                new Material(
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0, 0, 0),
                    32.0f,
                    0.0f,
                    0.9f,
                    0.1f
                );

            // 左壁
            actors.Add(
                new TriangleMesh(
                    new Vector3(-1, 1, offset),
                    new Vector3(-1, -1, offset + z0),
                    new Vector3(-1, -1, offset),
                    material1
                )
            );

            actors.Add(
                new TriangleMesh(
                    new Vector3(-1, 1, offset),
                    new Vector3(-1, 1, offset + z0),
                    new Vector3(-1, -1, offset + z0),
                    material1
                )
            );

            // 右壁
            actors.Add(
                new TriangleMesh(
                    new Vector3(1, 1, offset),
                    new Vector3(1, -1, offset),
                    new Vector3(1, -1, offset + z0),
                    material2
                )
            );

            actors.Add(
                new TriangleMesh(
                    new Vector3(1, 1, offset),
                    new Vector3(1, -1, offset + z0),
                    new Vector3(1, 1, offset + z0),
                    material2
                )
            );

            // 奥壁
            actors.Add(
                new TriangleMesh(
                    new Vector3(-1, -1, offset + z0),
                    new Vector3(-1, 1, offset + z0),
                    new Vector3(1, 1, offset + z0),
                    material5
                )
            );

            actors.Add(
                new TriangleMesh(
                    new Vector3(-1, -1, offset + z0),
                    new Vector3(1, 1, offset + z0),
                    new Vector3(1, -1, offset + z0),
                    material5
                )
            );
            /*
            // 天井
            actors.Add(
                new TriangleMesh(
                    new Vector3(-1, -1, offset),
                    new Vector3(-1, -1, offset + z0),
                    new Vector3(1, -1, offset),
                    material3
                )
            );

            actors.Add(
                new TriangleMesh(
                    new Vector3(1, -1, offset),
                    new Vector3(-1, -1, offset + z0),
                    new Vector3(1, -1, offset + z0),
                    material3
                )
            );
             */

            // 床
            actors.Add(
                new TriangleMesh(
                    new Vector3(-1, 1, offset),
                    new Vector3(1, 1, offset),
                    new Vector3(-1, 1, offset + z0),
                    material3
                )
            );

            actors.Add(
                new TriangleMesh(
                    new Vector3(1, 1, offset),
                    new Vector3(1, 1, offset + z0),
                    new Vector3(-1, 1, offset + z0),
                    material3
                )
            );

            // 玉1
            actors.Add(
                new Sphere(
                    0.2f,
                    new Vector3(-0.3f, 0, offset + 0.5f * z0),
                    material5
                )
            );
            // 玉2
            actors.Add(
                new Sphere(
                    0.2f,
                    new Vector3(0.4f, 0.8f, offset + 0.5f * z0),
                    material6
                )
            );
            // 玉3
            actors.Add(
                new Sphere(
                    0.3f,
                    new Vector3(0.5f, 0.7f, offset + 0.8f * z0),
                    material4
                )
            );
            #endregion
            // カメラ

            var from = new Vector3(0, -0.8f, 15);
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
                target[i] = new Color(trace(cam.getRay(i % w, i / h), null, 0));
            }
            return true;
        }

        Vector3 trace(Ray ray, Actor sender, int depth)
        {
            if (depth > MAX_DEPTH) return Vector3.Zero; 

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

            
            var crl = nearestActor.material.reflection;
            var crr = nearestActor.material.refraction;
            var cr = nearestActor.material.refraction;
            
            // 反射ベクトル
            var v = ray.Direction - 2 * Vector3.Dot(nearestNorm, ray.Direction) * nearestNorm;
            v.Normalize();
            var reflectionRay = new Ray(nearestPos, v);

            // 屈折ベクトル
            var vn = Vector3.Dot(ray.Direction, nearestNorm);
            float eta = 1.5f; // 屈折率
            Vector3 t;
            if (vn < 0)
            {
                var d = 1 - (1 + vn) * (1 + vn) / (eta * eta);
                if (d < 0)
                {
                    t = Vector3.Zero;
                }
                else
                {
                    t = (-vn / eta - (float)Math.Sqrt(d)) * nearestNorm + (1 / eta) * ray.Direction;
                }
            }
            else
            {
                var d = (1 - vn) * (1 - vn) / (eta * eta);
                if (d < 0)
                {
                    t = Vector3.Zero;
                }
                else
                {
                    t = -(vn / eta - (float)Math.Sqrt(d)) * nearestNorm + eta * ray.Direction;
                }
            }

            t.Normalize();
            var refractionRay = new Ray(nearestPos, t);

            var reflect = nearestActor.material.reflection <= 0.00001f ? Vector3.Zero : trace(reflectionRay, nearestActor, depth + 1);
            var refraction = nearestActor.material.refraction <= 0.00001f ? Vector3.Zero : trace(refractionRay, nearestActor, depth + 1);

            bool shade = false;
            foreach (var actor in actors)
            {
                if (actor == nearestActor) continue;
                var norm = Vector3.Zero;
                var pos = Vector3.Zero; 
                var d = shader.light - nearestPos;
                d.Normalize();
                if (actor.isIntersect(new Ray(nearestPos, d), out norm, out pos))
                {
                    shade = true;
                    break;
                }
            }
            
            var diffuse = shader.shading(nearestPos, nearestNorm, ray, nearestActor, shade);

            return
                nearestActor.material.diffuse * diffuse +
                nearestActor.material.reflection * reflect +
                nearestActor.material.refraction * refraction;
        }
    }
}
