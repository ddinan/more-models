// This model should be used on servers that support entity rotation (/entrot) to get the full effect.
// /entrot [username] x 90
// /entrot [bot] [bot name] x 90

using System;
using ClassicalSharp.Entities;
using ClassicalSharp.Physics;
using OpenTK;

namespace ClassicalSharp.Model {

	public class FlyingModel : IModel {
		
		public FlyingModel(Game window) : base(window) { SurivalScore = 80; }
		
		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * 7];
			Head = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4)
			                .TexOrigin(0, 0)
			                .RotOrigin(0, 24, 0));
			Hat = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4)
			                .TexOrigin(32, 0)
			                .RotOrigin(0, 24, 0).Expand(0.5f));
			Torso = BuildBox(MakeBoxBounds(-4, 12, -2, 4, 24, 2)
			                  .TexOrigin(16, 16));
			LeftLeg = BuildBox(MakeBoxBounds(0, 0, -2, -4, 12, 2)
			                    .TexOrigin(0, 16)
			                    .RotOrigin(0, 12, 0));
			RightLeg = BuildBox(MakeBoxBounds(0, 0, -2, 4, 12, 2)
			                    .TexOrigin(0, 16)
			                    .RotOrigin(0, 12, 0));
			LeftArm = BuildBox(MakeBoxBounds(-4, 12, -2, -8, 24, 2)
			                   .TexOrigin(40, 16)
			                   .RotOrigin(-6, 22, 0));
			RightArm = BuildBox(MakeBoxBounds(4, 12, -2, 8, 24, 2)
			                    .TexOrigin(40, 16)
			                    .RotOrigin(6, 22, 0));
		}

		public override float NameYOffset { get { return 2.075f; } }
		
		public override float GetEyeY(Entity entity) { return 26/16f; }
		
		public override Vector3 CollisionSize {
			get { return new Vector3(8/16f + 0.6f/16f, 28.1f/16f, 8/16f + 0.6f/16f); }
		}
		
		public override AABB PickingBounds {
			get { return new AABB(-4/16f, 0, -4/16f, 4/16f, 32/16f, 4/16f); }
		}
		
		const float nOnePi = (float)(Math.PI / -1);
		const float nTwelvePi = (float)(Math.PI / -12);
		const float nEightPi = (float)(Math.PI / -8);
		
		const float onePi = (float)(Math.PI /  1);
		const float twelvePi = (float)(Math.PI / 12);
		const float eightPi = (float)(Math.PI / 8);
		
		public override void DrawModel(Entity p) {
			
			float rotX = (float)(Math.Sin(p.anim.walkTime) * p.anim.swing * Math.PI);
			float rotZ = (float)(Math.Cos(p.anim.walkTime * 2) * p.anim.swing * Math.PI / 16f);
			float rotY = (float)(Math.Sin(p.anim.walkTime * 2) * p.anim.swing * Math.PI / 32f);
			Rotate = RotateOrder.XZY;
			
			p.anim.leftArmZ = 0;
			p.anim.swing = 0;
		    p.anim.walkTime = 0;
			
			game.Graphics.BindTexture(GetTexture(p));
			DrawRotate(-p.HeadXRadians, 0, 0, Head, true);
			DrawPart(Torso);
			DrawRotate(rotX, nOnePi + rotY, nTwelvePi + rotZ, LeftLeg, false);
			DrawRotate(rotX, onePi + rotY, twelvePi + rotZ, RightLeg, false);
			DrawRotate(180 * Utils.Deg2Rad, 0, p.anim.leftArmZ, LeftArm, false);
			DrawRotate(rotX, nOnePi + rotY, nEightPi + rotZ, RightArm, false);
			DrawRotate(-p.HeadXRadians, 0, 0, Hat, true);
			UpdateVB();
		}
		
		ModelPart Head, Hat, Torso, LeftLeg, RightLeg, LeftArm, RightArm;
	}
}