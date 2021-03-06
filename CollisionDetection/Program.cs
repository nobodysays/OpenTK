﻿using Common;
using Common.Colliders;
using Common.Windows;
using GlmNet;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CollisionDetection
{
    internal class CollisionDetectionWindow3D : Window3D
    {
        private readonly Sphere _sphere;
        private SphereCollider sphereCollider;
        private SphereCollider sphereCollider2;
        private InfinitePlaneCollider _infinitePlaneCollider;

        public CollisionDetectionWindow3D(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            AddGrid();
            AddMainCoordinatesAxis();

            _sphere = new Sphere(0.45f);
            sphereCollider = new SphereCollider(0.5f);
            sphereCollider.AttachTo(_sphere);
            _sphere.TranslateWorld(new vec3(0, 1, 1));

            var sphere2 = new Sphere(1);
            sphere2.Material.Color = new vec3(0.4f, 0, 0);
            sphere2.TranslateWorld(new vec3(-2, 1, 1));
            sphereCollider2 = new SphereCollider(1);
            sphereCollider2.AttachTo(sphere2);
            toDraw.Add(sphereCollider2);
            toDraw.Add(_sphere);
            toDraw.Add(sphereCollider);
            toDraw.Add(sphere2);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                _sphere.TranslateWorld(_sphere.WorldPosition + new vec3(0, (float)args.Time, 0));
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                _sphere.TranslateWorld(_sphere.WorldPosition - new vec3(0, (float)args.Time, 0));
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                _sphere.TranslateWorld(_sphere.WorldPosition + new vec3((float)args.Time, 0, 0));
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                _sphere.TranslateWorld(_sphere.WorldPosition - new vec3((float)args.Time, 0, 0));
            }
            if (KeyboardState.IsKeyDown(Keys.Q))
            {
                _sphere.TranslateWorld(new vec3(0));
            }
            if (KeyboardState.IsKeyDown(Keys.E))
            {
                _sphere.TranslateLocal(new vec3(0));
            }
            var V = _sphere.WorldPosition - lastPosition;
            if (sphereCollider.IsIntersectsSphere(sphereCollider2, out var result))
            {
                sphereCollider.Parent.Material.Color = new vec3(0, 1, 0);
                var reflected = 2 * result.Normal * (glm.dot(result.Normal, V));
                /*var line = new Line3D(new Line()
                {
                    Point = result.Point, Direction = result.Normal
                });
                line.Material.Color = new vec3(1, 0, 0);
                toDraw.Add(line);*/
                _sphere.TranslateWorld(_sphere.WorldPosition - reflected);
            }
            else
                sphereCollider.Parent.Material.Color = new vec3(0.5f);
            lastPosition = _sphere.WorldPosition;
        }

        private vec3 lastPosition = new vec3(0);
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var settings = GameWindowSettings.Default;
            var nativeWindowSettings = new NativeWindowSettings
            {
                Size = new Vector2i(800, 600),
                Title = "Collision Detection"
            };

            using (var window = new CollisionDetectionWindow3D(settings, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}