﻿// unset

using GlmNet;

namespace Common.Interfaces
{
    public interface IRayCasting
    {
        void CheckCollision(vec3 ray, vec3 cameraPosition);
    }
}