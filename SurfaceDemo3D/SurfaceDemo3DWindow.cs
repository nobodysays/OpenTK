﻿// unset

using Common;
using Common._3D_Objects;
using Common.Drawers.Settings;
using Common.Windows;
using GlmNet;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static System.MathF;

namespace SurfaceDemo
{
    public class SurfaceDemo3DWindow : Window3D
    {
        readonly Surface _dynamicSurface;
        public SurfaceDemo3DWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            _dynamicSurface = new Surface((x, z) => x * z / 5, new SurfaceDrawer3DSettings
            {
                MaxX = 4,
                MinX = -4,
                MaxZ = 4,
                MinZ = -4,
                NumberOfPartitions = 120
            });
            AddGrid();
            _dynamicSurface.Material.Color = new vec3(0.6f, 0.6f, 0);
            toDraw.Add(_dynamicSurface);
            //_dynamicSurface.ShowNormals = true;
            _dynamicSurface.ScaleWorld(new vec3(1, 0.5f, 1));
            var surface2 = new Surface((u, v) =>
                new vec3(
                    1 * Cos(u) * Cos(v),
                    1 * Sin(u) * Cos(v),
                    1 * Sin(v)
                    ), new SurfaceDrawer3DSettings
                    {
                        MaxX = 2 * PI,
                        MinX = 0,
                        MaxZ = PI / 2,
                        MinZ = -PI / 2,
                        NumberOfPartitions = 60
                    }, true);
            surface2.TranslateWorld(new vec3(5, 3, 0));
            surface2.Material.Color = new vec3(0.6f, 0, 0.6f);

            
            toDraw.Add(surface2);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            _dynamicSurface.SetNewFunction((x, z)=>Sin((float)GLFW.GetTime()*3f+x*z)/5f, false);
        }
        
    }
}