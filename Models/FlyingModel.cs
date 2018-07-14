using System;
using ClassicalSharp.Entities;
using ClassicalSharp.Physics;
using OpenTK;

namespace ClassicalSharp.Model {

	public class FlyingModel : IModel {
		
		public FlyingModel(Game window) : base(window) {}
		
		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * 7];
			Head = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4)
			                .TexOrigin(0, 0)
			                .RotOrigin(0, 24, 0));
			Hat = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4)
			                .TexOrigin(32, 0)
			                .RotOrigin(0, 24, 0).Expand(0.5f));
			Torso = BuildBox(MakeBoxBounds(-4, 12, -2, 4, 24, 2)
			                .TexOrigin(16, 16)
							.RotOrigin(0, 22, 0));
			LeftLeg = BuildBox(MakeBoxBounds(0, 0, -2, -4, 12, 2)
			                .TexOrigin(0, 16)
			                .RotOrigin(0, 22, 0));
			RightLeg = BuildBox(MakeBoxBounds(0, 0, -2, 4, 12, 2)
			                .TexOrigin(0, 16)
		                    .RotOrigin(0, 22, 0));
			LeftArm = BuildBox(MakeBoxBounds(-4, 12, -2, -8, 24, 2)
			                .TexOrigin(40, 16)
			                .RotOrigin(-6, 22, 0));
			RightArm = BuildBox(MakeBoxBounds(4, 12, -2, 8, 24, 2)
			                .TexOrigin(40, 16)
			                .RotOrigin(6, 22, 0));
		}

		public override float NameYOffset { get { return 2.075f; } }

            public override float GetEyeY(Entity entity) { return 26 / 16f; }

            public override Vector3 CollisionSize
            {
                get { return new Vector3(8 / 16f + 0.6f / 16f, 28.1f / 16f, 8 / 16f + 0.6f / 16f); }
            }

            public override AABB PickingBounds
            {
                get { return new AABB(-4 / 16f, 0, -4 / 16f, 4 / 16f, 32 / 16f, 4 / 16f); }
            }

            const float nOnePi = (float)(Math.PI / -1);
            const float nTwelvePi = (float)(Math.PI / -12);
            const float nEightPi = (float)(Math.PI / -8);

            const float onePi = (float)(Math.PI / 1);
            const float twelvePi = (float)(Math.PI / 12);
            const float eightPi = (float)(Math.PI / 8);

            private const float piOver2 = (float)(Math.PI / 2.0);
            private const float piOver128 = (float)(Math.PI / 128.0);

            public override void DrawModel(Entity p)
            {
            	
                float rotX = p.anim.swing * piOver2;
                float rotZ = (float)(Math.Cos(p.anim.walkTime / 4) * Math.Cos(p.anim.walkTime / 4) * p.anim.swing * Math.PI / 16.0f);
                float rotY = (float)(Math.Sin(p.anim.walkTime * 2) * p.anim.swing * Math.PI / 32f);
                Rotate = RotateOrder.XZY;
                
                game.Graphics.BindTexture(GetTexture(p));
                DrawRotate(-p.HeadXRadians, 0, 0, Head, true);
                DrawRotate(-rotX, 0, 0, Torso, false);
                DrawRotate(-rotX, -rotZ / 8f, p.anim.leftArmZ / 4f + p.anim.swing / 32f, LeftLeg, false); 
                DrawRotate(-rotX, rotZ / 8f, p.anim.rightArmZ / 4f - p.anim.swing / 32f, RightLeg, false); 
                DrawRotate(-rotX, rotY, p.anim.leftArmZ, LeftArm, false);
                DrawRotate(rotX, rotY, p.anim.rightArmZ, RightArm, false);
                DrawRotate(-p.HeadXRadians, 0, 0, Hat, true);
                UpdateVB();
            }

            private ModelPart Head, Torso, LeftArm, RightArm, LeftLeg, RightLeg, Hat;
        }
    }
