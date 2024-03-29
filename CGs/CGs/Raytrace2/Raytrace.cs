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
    class Raytrace : CGs.Renderer
    {
        Camera cam;
        List<Actor> actors;
        Shader shader;
        const int MAX_DEPTH = 3;
        Material material1;
        Material material2;
        Material material3;
        Material material4;
        Material material5;
        Material material6;
        Material material7;

        public void InitializeMaterials()
        {
            // マテリアルを作成
            material1 =
                new Material(
                    new Vector3(0.05f, 0.01f, 0.01f),
                    new Vector3(0.05f, 0.01f, 0.01f),
                    new Vector3(0, 0, 0),
                    32.0f
                );

            material2 =
                new Material(
                    new Vector3(0.01f, 0.01f, 0.05f),
                    new Vector3(0.01f, 0.01f, 0.05f),
                    new Vector3(0, 0, 0),
                    32.0f
                );

            material3 =
                new Material(
                    new Vector3(0.05f, 0.05f, 0.05f),
                    new Vector3(0.05f, 0.05f, 0.05f),
                    new Vector3(0, 0, 0),
                    32.0f
                );

            material4 =
                new Material(
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0, 0, 0),
                    32.0f
                );

            material5 =
                new Material(
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0, 0, 0),
                    32.0f,
                    0.9f,
                    0.0f,
                    0.1f
                );

            material6 =
                new Material(
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0, 0, 0),
                    32.0f,
                    0.0f,
                    0.9f,
                    0.1f
                );

            material7 =
                new Material(
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0.01f, 0.05f, 0.01f),
                    new Vector3(0, 0, 0),
                    32.0f,
                    1,
                    0,
                    0
                );
        }

        public void InitializeWorld1()
        {
            var offset = -3;
            var z0 = -5;

            // カメラ
            var from = new Vector3(0, -0.5f, 15);
            var dir = new Vector3(0, 0, -1);
            cam = new Camera(new Ray(from, dir));

            // シェーディング
            shader = new Shader(new Vector3(0, -1, offset + 0.5f * z0), new Vector3(10, 10, 10));

            // 左壁
            actors.Add(
                new QuadMesh(
                    new Vector3(-1, 1, offset),
                    new Vector3(-1, 1, offset + z0),
                    new Vector3(-1, -1, offset + z0),
                    new Vector3(-1, -1, offset),
                    material1
                )
            );

            // 右壁
            actors.Add(
                new QuadMesh(
                    new Vector3(1, 1, offset),
                    new Vector3(1, -1, offset),
                    new Vector3(1, -1, offset + z0),
                    new Vector3(1, 1, offset + z0),
                    material2
                )
            );

            // 奥壁
            actors.Add(
                new QuadMesh(
                    new Vector3(-1, -1, offset + z0),
                    new Vector3(-1, 1, offset + z0),
                    new Vector3(1, 1, offset + z0),
                    new Vector3(1, -1, offset + z0),
                    material5
                )
            );

            // 床
            actors.Add(
                new QuadMesh(
                    new Vector3(-1, 1, offset),
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
                    new Vector3(-0.3f, 0.2f, offset + 0.5f * z0),
                    material5
                )
            );

            // 玉2
            actors.Add(
                new Sphere(
                    0.2f,
                    new Vector3(0.5f, 0.8f, offset + 0.2f * z0),
                    material6
                )
            );

            // 玉3
            actors.Add(
                new Sphere(
                    0.3f,
                    new Vector3(0, 0.7f, offset + 0.5f * z0),
                    material4
                )
            );
        }

        public void InitializeWorld2()
        {
            // カメラ
            var from = new Vector3(0, -1.5f, 15);
            var dir = new Vector3(0, 0, -1);
            cam = new Camera(new Ray(from, dir));

            var offset = -3;
            var z0 = -2;
            // シェーディング
            shader = new Shader(new Vector3(0, -1, 0), new Vector3(10, 10, 10));

            // 床
            actors.Add(
                new CheckerPlain(
                    new Vector3(0, 1, 0),
                    new Vector3(0, -1, 0),
                    material2,
                    material3
                )
            );


            // 左側の鏡
            var angle = MathHelper.ToRadians(45.0f);
            var dx = -0.5f * (float)Math.Cos(angle);
            var vertice = new[] {
                new Vector3(-0.5f, 0, 0),
                new Vector3(-0.5f, 1, 0),
                new Vector3(0.5f, 1, 0),
                new Vector3(0.5f, 0, 0)
            };
            var m = Matrix.CreateRotationY(angle);
            Vector3.Transform(vertice, ref m, vertice);
            m = Matrix.CreateTranslation(new Vector3(dx, 0, offset + z0));
            Vector3.Transform(vertice, ref m, vertice);

            actors.Add(
                new QuadMesh(
                    vertice[0],
                    vertice[1],
                    vertice[2],
                    vertice[3],
                    material7
                )
            );

            // 右側の鏡
            vertice = new[] {
                new Vector3(-0.5f, 0, 0),
                new Vector3(-0.5f, 1, 0),
                new Vector3(0.5f, 1, 0),
                new Vector3(0.5f, 0, 0)
            };
            m = Matrix.CreateRotationY(-angle);
            Vector3.Transform(vertice, ref m, vertice);
            m = Matrix.CreateTranslation(new Vector3(-dx, 0, offset + z0));
            Vector3.Transform(vertice, ref m, vertice);

            actors.Add(
                new QuadMesh(
                    vertice[0],
                    vertice[1],
                    vertice[2],
                    vertice[3],
                    material7
                )
            );

            // 球
            actors.Add(
                new Sphere(
                    0.3f,
                    new Vector3(0, 0.6f, offset),
                    material3
                )
            );
        }

        public override void Initialize(int w, int h)
        {
            Camera.resolutionX = w;
            Camera.resolutionY = h;

            actors = new List<Actor>();

            InitializeMaterials();
            InitializeWorld1();
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
