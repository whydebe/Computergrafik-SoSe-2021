using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Engine.Core.Effects;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.GUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut11_AssetsPicking", Description = "by whygameplays")]
    public class Tut11_AssetsPicking : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private ScenePicker _scenePicker;
        private PickResult _currentPick;
        private float4 _oldColor;
        private Transform LeftFrontWheel;
        private Transform RightFrontWheel;
        private Transform LeftRearWheel;
        private Transform RightRearWheel;


        private String[] segments = {"FrontAxle", "RearAxle", "LeftFrontWheel", "RightFrontWheel", "LeftRearWheel", "RightRearWheel", "Body", "Rotator", "Holder", "Rotating_Ring", "Gun_Holder_large", "Gun_large"};


        // Init is called on startup.
        public override void Init()
        {
            RC.ClearColor = new float4(((float4)ColorUint.Black));
            // _scene = AssetStorage.Get<SceneContainer>("Car1.fus");
            _scene = AssetStorage.Get<SceneContainer>("Car2.fus");
            // _scene = AssetStorage.Get<SceneContainer>("Car3.fus");

            LeftFrontWheel  = _scene.Children.FindNodes(node => node.Name == "LeftFrontWheel")?.FirstOrDefault()?.GetTransform();
            RightFrontWheel = _scene.Children.FindNodes(node => node.Name == "RightFrontWheel")?.FirstOrDefault()?.GetTransform();
            LeftRearWheel   = _scene.Children.FindNodes(node => node.Name == "LeftRearWheel")?.FirstOrDefault()?.GetTransform();
            RightRearWheel  = _scene.Children.FindNodes(node => node.Name == "RightRearWheel")?.FirstOrDefault()?.GetTransform();

            _scenePicker = new ScenePicker(_scene);

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, 0, 20) * float4x4.CreateRotationX(-(float) Math.Atan(15.0 / 40.0));

            RightRearWheel.Rotation  = LeftRearWheel.Rotation;
            LeftFrontWheel.Rotation  = LeftRearWheel.Rotation;
            RightFrontWheel.Rotation = LeftRearWheel.Rotation;

            if (Mouse.LeftButton)
            {
                float2 pickPosClip = Mouse.Position * new float2(2.0f / Width, -2.0f / Height) + new float2(-1, 1);
                PickResult newPick = _scenePicker.Pick(RC, pickPosClip).OrderBy(pr => pr.ClipPos.z).FirstOrDefault();

                if (newPick?.Node != _currentPick?.Node)
                {
                    if (_currentPick != null)
                    {
                        var ef = _currentPick.Node.GetComponent<DefaultSurfaceEffect>();
                        ef.SurfaceInput.Albedo = _oldColor;
                    }

                    if (newPick != null)
                    {
                        var ef = newPick.Node.GetComponent<SurfaceEffect>();
                        _oldColor = ef.SurfaceInput.Albedo;
                        ef.SurfaceInput.Albedo = (float4) ColorUint.Red;
                    }
                    _currentPick = newPick;
                }
            }

            if (_currentPick != null & (Mouse.LeftButton ^ Mouse.RightButton)) {
                Transform currentTransform = _currentPick.Node.GetTransform();

                

                if (Array.IndexOf(segments, _currentPick.Node.Name) > 0) {
                    currentTransform.Rotation.z -= 2 * Keyboard.LeftRightAxis * DeltaTime;
                }
            }

            if (_currentPick != null)
            {
                Transform currentTransform = _currentPick.Node.GetTransform();
                switch (_currentPick.Node.Name)
                {
                    default:
                        LeftRearWheel.Rotation = currentTransform.Rotation + new float3(Keyboard.WSAxis * DeltaTime * 3f, 0, 0);
                    break;

                    case "Rotator":
                        currentTransform.Rotation = currentTransform.Rotation + new float3(0, Keyboard.ADAxis * DeltaTime * 5, 0);
                    break;

                    case "FrontAxle":
                    break;

                    case "RearAxle":
                    break;

                    case "Body":
                    break;

                    case "Holder":
                    break;

                    case "Gun_Holder":
                        currentTransform.Rotation = currentTransform.Rotation + new float3(0, Keyboard.ADAxis * DeltaTime * 5, 0);
                    break;

                    case "Rotating_Ring":
                    break;

                    case "Gun_Holder_large":
                        currentTransform.Rotation = currentTransform.Rotation + new float3(0, Keyboard.ADAxis * DeltaTime * 5, 0);
                    break;

                    case "Gun_large":
                    break;
                }
            }

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }

        public void SetProjectionAndViewport()
        {
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