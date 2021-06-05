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
    [FuseeApplication(Name = "Tut09_HierarchyAndInput", Description = "by whygameplays")]




    /* ---------------------------------------------
                !    HOW TO USE     !
            ROTATE:     L-MOUSE
            OPEN/CLOSE: SPACE
            UP/DOWN:    UP/DOWN ARROW-KEY
            LEFT/RIGHT: LEFT/RIGHT ARROW-KEY
    --------------------------------------------- */




    public class Tut09_HierarchyAndInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private float _camAngle = 0;
        private Transform _baseTransform;
        private Transform _bodyTransform;
        private Transform _upperArmTransform;
        private Transform _foreArmTransform;
        private Transform _grip1Transform;
        private Transform _grip2Transform;
        private Transform _grip3Transform;
        private Transform _grip4Transform;
        private float gripAngle = 0;
        private Boolean gripOpen = true;
        private Boolean spaceActive = false;


        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };


            _bodyTransform = new Transform
            {
                Rotation = new float3(0, 0.5f, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 6, 0)
            };

            _upperArmTransform = new Transform
            {
                Rotation = new float3(-0.5f, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 4, 0)
            };

            _foreArmTransform = new Transform
            {
                Rotation = new float3(-0.5f, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 8, 0)
            };


            _grip1Transform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 1, 0)
            };

            _grip2Transform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-2, 1, 0)
            };

            _grip3Transform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 1, 2)
            };


            _grip4Transform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 1, -2)
            };



            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNode>
                {
                    // BASE - GREY
                    new SceneNode
                    {
                        Components = new List<SceneComponent>
                        {
                            // TRANSFORM COMPONENT
                            _baseTransform,
                            // SHADER EFFECT COMPONENT
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightGrey, float4.Zero),
                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        },

                        Children =
                        {
                            // BODY - RED
                            new SceneNode
                            {
                                Components=
                                {
                                    _bodyTransform,
                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.Red, float4.Zero),
                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                },

                                Children =
                                {
                                    // UPPERARM
                                    new SceneNode
                                    {
                                        Components =
                                        {
                                            _upperArmTransform,
                                        },

                                        Children =
                                        {
                                            new SceneNode
                                            {
                                                Components =
                                                {
                                                    new Transform{Translation = new float3(0, 4, 0)},
                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.Green, float4.Zero),
                                                    SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                }
                                            },

                                            // FOREARM
                                            new SceneNode
                                            {
                                                Components =
                                                {
                                                    _foreArmTransform,
                                                },

                                                Children =
                                                {
                                                   new SceneNode
                                                   {
                                                        Components =
                                                        {
                                                            new Transform{Translation = new float3(-2, 4, 0)},
                                                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.Blue, float4.Zero),
                                                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                        },

                                                        Children =
                                                        {
                                                            // GRIP BASE
                                                            new SceneNode
                                                            {
                                                                Components=
                                                                {
                                                                    new Transform{Translation = new float3(0, 5, 0)},
                                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.Blue, float4.Zero),
                                                                    SimpleMeshes.CreateCuboid(new float3(6, 2, 6)),
                                                                },

                                                                Children =
                                                                {
                                                                    // GRIP 1
                                                                    new SceneNode
                                                                    {
                                                                        Components = {
                                                                            _grip1Transform,
                                                                        },

                                                                        Children =
                                                                        {
                                                                            new SceneNode
                                                                            {
                                                                                Components =
                                                                                {
                                                                                    new Transform{Translation = new float3(1, 1.5f, 0)},
                                                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.White, float4.Zero),
                                                                                    SimpleMeshes.CreateCuboid(new float3(1, 4, 2))
                                                                                }
                                                                            }
                                                                        }
                                                                    },

                                                                    // GRIP 2
                                                                    new SceneNode
                                                                    {
                                                                        Components =
                                                                        {
                                                                            _grip2Transform
                                                                        },

                                                                        Children=
                                                                        {
                                                                            new SceneNode
                                                                            {
                                                                                Components =
                                                                                {
                                                                                    new Transform{Translation = new float3(-1, 1.5f, 0)},
                                                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.White, float4.Zero),
                                                                                    SimpleMeshes.CreateCuboid(new float3(1, 4, 2))
                                                                                }
                                                                            }
                                                                        }
                                                                    },

                                                                    // GRIP 3
                                                                    new SceneNode
                                                                    {
                                                                        Components =
                                                                        {
                                                                            _grip3Transform,
                                                                        },

                                                                        Children=
                                                                        {
                                                                            new SceneNode
                                                                            {
                                                                                Components =
                                                                                    {
                                                                                    new Transform{Translation = new float3(0, 1.5f, 1)},
                                                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.White, float4.Zero),
                                                                                    SimpleMeshes.CreateCuboid(new float3(2, 4, 1))
                                                                                }
                                                                            }
                                                                        }
                                                                    },

                                                                    // GRIP 4
                                                                    new SceneNode
                                                                    {
                                                                        Components=
                                                                        {
                                                                            _grip4Transform,
                                                                        },

                                                                        Children=
                                                                        {
                                                                            new SceneNode
                                                                            {
                                                                                Components =
                                                                                {
                                                                                    new Transform{Translation = new float3(0, 1.5f, -1)},
                                                                                    MakeEffect.FromDiffuseSpecular((float4) ColorUint.White, float4.Zero),
                                                                                    SimpleMeshes.CreateCuboid(new float3(2, 4, 1))
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        },
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }


        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor =new float4(((float4)ColorUint.Black));

            _scene = CreateScene();
            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            if(Mouse.LeftButton)
            {
                _camAngle += Mouse.Velocity.x * Time.DeltaTime * -0.001f;
            }

            _bodyTransform.Rotation.y += Time.DeltaTime * Keyboard.LeftRightAxis;
            _upperArmTransform.Rotation.x += Time.DeltaTime * Keyboard.UpDownAxis;
            _foreArmTransform.Rotation.x += Time.DeltaTime * Keyboard.WSAxis;
            
            /*
            if (gripAngle >= -0.65f && gripAngle <= 0.92f)
            {
                _grip1Transform.Rotation.z = -gripAngle;
                _grip2Transform.Rotation.z = gripAngle;
                _grip3Transform.Rotation.x = gripAngle;
                _grip4Transform.Rotation.x = -gripAngle;
            }
            */
            
            _grip1Transform.Rotation.z = -gripAngle;
            _grip2Transform.Rotation.z = gripAngle;
            _grip3Transform.Rotation.x = gripAngle;
            _grip4Transform.Rotation.x = -gripAngle;

            if (gripOpen)
            {
                if (gripAngle < 0.65f)
                {
                    gripAngle += 0.92f * DeltaTime;
                }

            }
            
            else
            {
                if (gripAngle > -0.65f) {
                    gripAngle -= 0.92f * DeltaTime;
                }
            }

            if (Keyboard.GetKey(KeyCodes.Space))
            {
                if (!spaceActive)
                {
                    gripOpen = !gripOpen;
                }
                spaceActive = true;
            }
            
            else
            {
                spaceActive = false;
            }


            /*
            else if (gripAngle < -0.65f)
            {
                gripAngle = -0.65f;
            }
            
            else if (gripAngle > 0.92f)
            {
                gripAngle = 0.92f;
            }
            */

            SetProjectionAndViewport();
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);
            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, -15, 50) * float4x4.CreateRotationY(_camAngle);
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
            // 0.25*PI Rad -> 45° gripOpen angle along the vertical direction. Horizontal gripOpen angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}