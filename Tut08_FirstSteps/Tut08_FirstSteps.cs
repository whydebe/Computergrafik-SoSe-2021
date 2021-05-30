using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.GUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuseeApp {
    [FuseeApplication(Name = "Tut08_FirstSteps", Description = "by whygameplays")]
    public class Tut08_FirstSteps : RenderCanvas {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private float _camAngle = 0;
        private Transform _cubeTransform;

        public override void Init() {
        
            RC.ClearColor = (float4) ColorUint.Black;
            _cubeTransform = new Transform {Translation = new float3(0, 0, 0)};
            var cubeShader = MakeEffect.FromDiffuseSpecular((float4)ColorUint.Red, float4.Zero);

            // Create a scene with a cube - The three components: one Transform, one ShaderEffect and the Mesh
            _cubeTransform = new Transform {Translation = new float3(0, 0, 0)};

            var cubeMesh1 = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));

            var cubeMesh2 = SimpleMeshes.CreateCuboid(new float3(1, 1, 1000));
            var cubeMesh3 = SimpleMeshes.CreateCuboid(new float3(1, 1000, 1));
            var cubeMesh4 = SimpleMeshes.CreateCuboid(new float3(1000, 1, 1));

            var cubeMesh5 = SimpleMeshes.CreateCuboid(new float3(15, 15, 0));
            var cubeMesh6 = SimpleMeshes.CreateCuboid(new float3(15, 0, 15));
            var cubeMesh7 = SimpleMeshes.CreateCuboid(new float3(0, 15, 15));

            // Assemble the cube node containing the three components
            var cubeNode = new SceneNode(); 
            cubeNode.Components.Add(_cubeTransform);
            cubeNode.Components.Add(cubeShader);
            cubeNode.Components.Add(cubeMesh1);
            cubeNode.Components.Add(cubeMesh2);
            cubeNode.Components.Add(cubeMesh3);
            cubeNode.Components.Add(cubeMesh4);
            cubeNode.Components.Add(cubeMesh5);
            cubeNode.Components.Add(cubeMesh6);
            cubeNode.Components.Add(cubeMesh7);

            // Create the scene containing the cube as the only object
            _scene = new SceneContainer(); 
            _scene.Children.Add(cubeNode);
            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }



        // RenderAFrame is called once a frame
        public override void RenderAFrame() {
            SetProjectionAndViewport();
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);
            // Animate the camera angle
            _camAngle = _camAngle + 90.0f * M.Pi/180.0f * DeltaTime;
            // Animate the cube
            _cubeTransform.Translation = new float3(0, 10 * M.Sin(1 * TimeSinceStart), 0);
            _cubeTransform.Rotation = new float3(0, -5 * M.Sin(-1 * TimeSinceStart), 0);
            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, 0, 50) * float4x4.CreateRotationY(_camAngle);
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }



        public void SetProjectionAndViewport() {
            // Set the rendering area to the entire window size
            RC.Viewport(0, 0, Width, Height);
            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }        
    }
}