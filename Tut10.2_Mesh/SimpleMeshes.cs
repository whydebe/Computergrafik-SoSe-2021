using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Engine.Core.Effects;
using Fusee.Math.Core;
using Fusee.Serialization;

namespace FuseeApp
{
    public static class SimpleMeshes 
    {
        public static Mesh CreateCuboid(float3 size)
        {
            return new Mesh
            {
                Vertices = new[]
                {
                    new float3 {x = + 0.5f * size.x, y = - 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = + 0.5f * size.x, y = + 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = + 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = - 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = + 0.5f * size.x, y = - 0.5f * size.y, z = - 0.5f * size.z},
                    new float3 {x = + 0.5f * size.x, y = + 0.5f * size.y, z = - 0.5f * size.z},
                    new float3 {x = + 0.5f * size.x, y = + 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = + 0.5f * size.x, y = - 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = - 0.5f * size.y, z = - 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = + 0.5f * size.y, z = - 0.5f * size.z},
                    new float3 {x = + 0.5f * size.x, y = + 0.5f * size.y, z = - 0.5f * size.z},
                    new float3 {x = + 0.5f * size.x, y = - 0.5f * size.y, z = - 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = - 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = + 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = + 0.5f * size.y, z = - 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = - 0.5f * size.y, z = - 0.5f * size.z},
                    new float3 {x = + 0.5f * size.x, y = + 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = + 0.5f * size.x, y = + 0.5f * size.y, z = - 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = + 0.5f * size.y, z = - 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = + 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = + 0.5f * size.x, y = - 0.5f * size.y, z = - 0.5f * size.z},
                    new float3 {x = + 0.5f * size.x, y = - 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = - 0.5f * size.y, z = + 0.5f * size.z},
                    new float3 {x = - 0.5f * size.x, y = - 0.5f * size.y, z = - 0.5f * size.z}
                },

                Triangles = new ushort[]
                {
                    0, 2, 1, 0, 3, 2,       // Face: front
                    4, 6, 5, 4, 7, 6,       // Face: right
                    8, 10, 9, 8, 11, 10,    // Face: back
                    12, 14, 13, 12, 15, 14, // Face: left
                    16, 18, 17, 16, 19, 18, // Face: top
                    20, 22, 21, 20, 23, 22  // Face: bottom

                },

                Normals = new[]
                {
                    new float3(0, 0,  1),
                    new float3(0, 0,  1),
                    new float3(0, 0,  1),
                    new float3(0, 0,  1),
                    new float3(1, 0,  0),
                    new float3(1, 0,  0),
                    new float3(1, 0,  0),
                    new float3(1, 0,  0),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(0, 1,  0),
                    new float3(0, 1,  0),
                    new float3(0, 1,  0),
                    new float3(0, 1,  0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0)
                },

                UVs = new[]
                {
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0)
                },
                BoundingBox = new AABBf(-0.5f * size, 0.5f * size)
            };
        }

        public static SurfaceEffect MakeMaterial(float4 color)
        {
            return MakeEffect.FromDiffuseSpecular(
                albedoColor: color,
                emissionColor: float4.Zero,
                shininess: 25.0f,
                specularStrength: 1f);
        }

        public static Mesh CreateCylinderBottom(float radius, float height, int segments)
        {
            float3[] verts = new float3[segments + 1];
            float3[] norms = new float3[segments + 1];
            ushort[] tris = new ushort[segments * 3];

            /* TEST
            verts[0] = new float3(0,0,0); 
            verts[1] = new float3(radius,0,0); 
            verts[2] = new float3(0,0,radius); 
            norms[0] = new float3(0,1,0); 
            norms[1] = new float3(0,1,0); 
            norms[2] = new float3(0,1,0); 
            tris[0] = 0;
            tris[1] = 1;
            tris[2] = 2;
            */

            float delta = 2 * M.Pi / segments;

            verts[0] = new float3(radius, 0, 0);
            norms[0] = float3.UnitY;
            verts[segments] = new float3(0, 0, 0);
            norms[segments] = float3.UnitY;

            for (int i = 1; i < segments; i++)
            {
                verts[i] = new float3(radius * M.Cos(i * delta), 0, radius * M.Sin(i * delta));
                norms[i] = float3.UnitY;
                tris[3 * i - 1] = (ushort) segments; // Point: center
                tris[3 * i - 2] = (ushort) i;        // Point: current segment
                tris[3 * i - 3] = (ushort) (i - 1);  // Point: previous segment
            }

            tris[3 * segments - 1] = (ushort) segments;
            tris[3 * segments - 2] = 0;
            tris[3 * segments - 3] = (ushort) (segments -1);

            return new Mesh
            {
                Vertices = verts,
                Normals = norms,
                Triangles = tris,
            };
        }

        public static Mesh CreateCylinder(float radius, float height, int segments)
        {
            float delta = 2 * M.Pi / segments;

            float3[] verts = new float3[segments * 4 + 2];
            float3[] norms = new float3[segments * 4 + 2];
        	ushort[] tris  = new ushort[segments * 3 * 4];

            verts[0] = new float3(radius, height / 2,0);
            verts[1] = new float3(radius, height / 2,0);
            verts[2] = new float3(radius, - height / 2,0);
            verts[3] = new float3(radius, - height / 2,0);

            norms[0] = new float3(0, 1, 0);
            norms[1] = new float3(1, 0, 0);
            norms[2] = new float3(1, 0, 0);
            norms[3] = new float3(0, -1, 0);

            verts[segments * 4] = new float3(0, height / 2,0);
            verts[segments * 4 + 1] = new float3(0, - height / 2,0);

            norms[segments * 4] = new float3(0, 1, 0);
            norms[segments * 4 + 1] = new float3(0, -1, 0);

            for (int i = 1; i < segments; i++)
            {
                verts[i * 4 + 0] = new float3(radius * M.Cos(delta * i),  height / 2,radius * M.Sin(delta * i)); // Top: cover
                verts[i * 4 + 1] = new float3(radius * M.Cos(delta * i),  height / 2,radius * M.Sin(delta * i)); // Top: mantle
                verts[i * 4 + 2] = new float3(radius * M.Cos(delta * i), -height / 2,radius * M.Sin(delta * i)); // Bottom: mantle
                verts[i * 4 + 3] = new float3(radius * M.Cos(delta * i), -height / 2,radius * M.Sin(delta * i)); // Bottom: cover

                norms[i * 4 + 0] = new float3(0, 1, 0);
                norms[i * 4 + 1] = new float3(M.Cos(delta * i), 0, M.Sin(delta * i));
                norms[i * 4 + 2] = new float3(M.Cos(delta * i), 0, M.Sin(delta * i));
                norms[i * 4 + 3] = new float3(0, -1, 0);  


                // Top: tri
                tris[12 * (i - 1) + 0] = (ushort) (4 * segments);      // Point: top center
                tris[12 * (i - 1) + 1] = (ushort) (4 * i + 0);         // Point: current top segment
                tris[12 * (i - 1) + 2] = (ushort) (4 * (i - 1) + 0);   // Point: previous top segment

                // Side 1: tri
                tris[12 * (i - 1) + 3] = (ushort) (4 * (i - 1) + 2);   // Point: previous lower shell
                tris[12 * (i - 1) + 4] = (ushort) (4 * i + 1);         // Point: current lower shell
                tris[12 * (i - 1) + 5] = (ushort) (4 * i + 2);         // Point: current top shell

                 // Side 2: tri
                tris[12 * (i-1) + 6] = (ushort) (4 * (i - 1) + 2);     // Point: previous lower shell
                tris[12 * (i-1) + 7] = (ushort) (4 * (i - 1) + 1);     // Point: current top shell
                tris[12 * (i - 1) + 8] = (ushort) (4 * i + 1);         // Point: previous top shell

                // Bottom: tri
                tris[12 * (i - 1) + 9]  = (ushort) (4 * segments + 1); // Point: bottom center
                tris[12 * (i - 1) + 10] = (ushort) (4 * (i - 1) + 3);  // Point: current bottom segment
                tris[12 * (i - 1) + 11] = (ushort) (4 * i + 3);        // Point: previous bottom segment
            }

            tris[12 * segments - 12] = (ushort)(4 * segments);
            tris[12 * segments - 11] = (ushort)(0);  
            tris[12 * segments - 10] = (ushort)(4 * segments - 4);  

            tris[12 * segments - 9] = (ushort)(4 * segments - 2);
            tris[12 * segments - 8] = (ushort)(1);
            tris[12 * segments - 7] = (ushort)(2);

            tris[12 * segments - 6] = (ushort)(4 * segments - 2);
            tris[12 * segments - 5] = (ushort)(4 * segments - 3);
            tris[12 * segments - 4] = (ushort)(1);

            tris[12 * segments - 3] = (ushort)(4 * segments + 1);
            tris[12 * segments - 2] = (ushort)(4 * segments - 1);
            tris[12 * segments - 1] = (ushort)(3);    
          
            return new Mesh
            {
               Vertices  = verts,
               Normals   = norms,
               Triangles = tris,
            };
        }

        public static Mesh CreateCone(float radius, float height, int segments)
        {
            return CreateConeFrustum(radius, 0.0f, height, segments);
        }

        public static Mesh CreateConeFrustum(float radiuslower, float radiusupper, float height, int segments)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreatePyramid(float baselen, float height)
        {
            throw new NotImplementedException();
        }
        public static Mesh CreateTetrahedron(float edgelen)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreateTorus(float mainradius, float segradius, int segments, int slices)
        {
            throw new NotImplementedException();
        }

    }
}
