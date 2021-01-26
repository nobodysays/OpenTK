﻿// unset

using GlmNet;
using OpenTK.Graphics.OpenGL4;
using OpenTKProject;
using System.Collections.Generic;

namespace Common._3D_Objects
{
    public class Grid3D : SceneObject
    {
        public Grid3D(int horizontalDivisions, int verticalDivisions)
        {
            shader = new Shader(@"Shaders\3D\parametricFunction.vert", @"Shaders\3D\parametricFunction.frag");
            float cellSize = 1;

            float width = horizontalDivisions * cellSize;
            float height = verticalDivisions * cellSize;

            float xMin = -width / 2;
            float xMax =  width / 2;
            
            float yMin = -width / 2;
            float yMax =  width / 2;
            var vertices = new List<vec3>();
            var indices = new List<uint>();
            var horizontalStep = (xMax - xMin) / (horizontalDivisions - 1);
            var verticalStep = (yMax - yMin) / (verticalDivisions - 1);
            float x = xMin;
            for (int i = 0; i < horizontalDivisions; i++)
            {
                vertices.Add(new vec3(x, 0, yMin));
                vertices.Add(new vec3(x, 0, yMax));
                indices.Add((uint)i*2);
                indices.Add((uint)i*2 + 1);
                x += horizontalStep;
            }
            
            float y = yMin;
            for (int i = 0; i < horizontalDivisions; i++)
            {
                vertices.Add(new vec3(xMin, 0, y));
                vertices.Add(new vec3(xMax, 0, y));
                indices.Add((uint)(i+horizontalDivisions)*2);
                indices.Add((uint)(i+horizontalDivisions)*2 + 1);
                y += verticalStep;
            }
            
            InitializeVAO_VBO_EBO(vertices.ToSingleArray(), indices.ToArray());
        }
        
        public override void Draw(ref mat4 view, ref mat4 projection)
        {
            if (vao == null)
            {
                return;
            }
            vao.Bind();
            UpdateDefaultShader(ref view, ref projection);
            GL.DrawElements(PrimitiveType.Lines, indicesCount, DrawElementsType.UnsignedInt, 0);
        }
    }
}