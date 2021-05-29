// Felix Kakuschke, 18.05.2021
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

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut08_FirstSteps", Description = "Yet another FUSEE App.")]
    public class Tut08_FirstSteps : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private float _camAngle = 0;
        private Cube[] cubes;

        private int arrsize = 5;
        private Random rnd = new Random();

        public override void Init()
        {
            
            RC.ClearColor = (float4) ColorUint.DarkGreen;
            _scene = new SceneContainer();

            cubes = new Cube[arrsize];
            var maxsize = 5;
            var prevy = -maxsize * arrsize + maxsize*1.5f;
            for (var i = 0; i < arrsize; i++){
                float edgelength = (i+1) * ((float) maxsize/arrsize);
                float3 size = new float3(edgelength);
                var newcube = new Cube(new float3(1,1,1), new float3(0,prevy + 2 * size.y,0), (float4)ColorUint.Blue, size, edgelength);
                cubes[i] = newcube;
                _scene.Children.Add(cubes[i].node);
                prevy = (int) newcube.trans.Translation.y;
            }
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        public override void RenderAFrame()
        {
            SetProjectionAndViewport();
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            for (var i = 0; i < cubes.Length; i++){
                float4 rgb = HSLtoRGB(Time.TimeSinceStart * 180 + i * 360/arrsize, 1f, 0.5f);
                cubes[i].changeColor(rgb);

                cubes[i].rotate((45f * M.Pi/180f) * Time.DeltaTime);
                cubes[i].setTranslate((float) Math.Cos(2 * Time.TimeSinceStart) * cubes[i].size * 3);
                cubes[i].setScale(Math.Abs(cubes[i].trans.Translation.x) / (cubes[i].size * 3));
            }
            
            RC.View = float4x4.CreateTranslation(0, 0, 50) * float4x4.CreateRotationY(_camAngle);

            _sceneRenderer.Render(RC);
            Present();
        }

        public void SetProjectionAndViewport()
        {
            RC.Viewport(0, 0, Width, Height);
            var aspectRatio = Width / (float)Height;
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }        

        public float4 HSLtoRGB(float _h, float _s, float _l)
        {
            // Mathe von https://www.niwa.nu/2013/05/math-behind-colorspace-conversions-rgb-hsl/
            float normH = (_h % 360) / 360.0f;
            float x = _l + _s - _l * _s;
            float y = 2 * _l - x;
            

            float[] temp_RGB = {(normH + 1/3f)%1, (normH), (normH - 1/3f)%1};
            float[] rgb_arr = new float[3];

            for (var i = 0; i < 3; i++) {
                if (6 * temp_RGB[i] < 1) {
                    rgb_arr[i] = y + (x - y) * 6 * temp_RGB[i];
                } else if (2 * temp_RGB[i] < 1) {
                    rgb_arr[i] = x;
                } else if (3 * temp_RGB[i] < 2) {
                    rgb_arr[i] = y + (x - y) * (2/3f - temp_RGB[i]) * 6;
                } else {
                    rgb_arr[i] = y;
                }
            }
            float4 rgb = new float4(rgb_arr[0], rgb_arr[1], rgb_arr[2], 1);
            return rgb;
        }

    }

    public class Cube {

        public Transform trans;
        public Fusee.Engine.Core.Effects.DefaultSurfaceEffect shade;
        public Mesh mesh;

        public SceneNode node;

        public float size;

        public Cube(float3 _sc, float3 _tr, float4 _col, float3 _dims, float _size) {
            this.trans = new Transform{Scale = _sc, Translation = _tr};
            this.shade = MakeEffect.FromDiffuseSpecular(_col, float4.Zero);
            this.mesh = SimpleMeshes.CreateCuboid(_dims);
            this.node = new SceneNode();
            this.node.Components.Add(this.trans);
            this.node.Components.Add(this.shade);
            this.node.Components.Add(this.mesh);
            this.size = _size;
        }

        public void changeColor(float4 _newcol) {
            this.shade.SurfaceInput.Albedo = _newcol;
        }

        public void rotate(float _angle) {
            this.trans.Rotation.y += _angle;
        }

        public void setTranslate(float _x) {
            this.trans.Translation.x = _x;
        }

        public void setScale(float _scale) {
            this.trans.Scale.x = _scale;
        }
    }
}