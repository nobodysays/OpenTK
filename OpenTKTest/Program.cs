﻿using System;
using System.Collections.Generic;
using System.Drawing;
using Common;
using GlmNet;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static System.MathF;
using static Utils.AppUtils;

namespace OpenTKTest
{
    internal class Program
    {
        class TestWindow : Window3D
        {
            public TestWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
            {
            }
        }
        private static void Main(string[] args)
        {
            var settings = GameWindowSettings.Default;
            var nativeWindowSettings = new NativeWindowSettings
            {
                Size = new Vector2i(800, 600),
                Title = "Surface"
            };

            using (var window = new TestWindow(settings, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}