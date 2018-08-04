using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class CaveSpiderModel : IModel {
		public CaveSpiderModel(Game window) : base(window) { SurivalScore = 105; }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * 5];
			Head     = BuildBox(MakeBoxBounds(-4, 4, -11, 4, 12, -3)
			                    .TexOrigin(32, 4)
			                    .RotOrigin(0, 8, -3));
			Link     = BuildBox(MakeBoxBounds(-3, 5, 3, 3, 11, -3)
			                    .TexOrigin(0, 0));
			End      = BuildBox(MakeBoxBounds(-5, 4, 3, 5, 12, 15)
			                    .TexOrigin(0, 12));
			LeftLeg  = BuildBox(MakeBoxBounds(-19, 7, -1, -3, 9, 1)
			                    .TexOrigin(18, 0)
			                    .RotOrigin(-3, 8, 0));
			RightLeg = BuildBox(MakeBoxBounds(3, 7, -1, 19, 9, 1)
			                    .TexOrigin(18, 0)
			                    .RotOrigin(3, 8, 0));
		}

		public override float NameYOffset { get { return 1.0125f; } }

		public override float GetEyeY(Entity entity) { return 8/16f; }

		public override Vector3 CollisionSize { get { return new Vector3(15f / 16f, 0.75f, 15f / 16f); } }

		public override AABB PickingBounds { get { return new AABB(-5f / 16f, 0f, -11f / 16f, 5f / 16f, 0.75f, 15f / 16f); } }

		const float quarterPi = (float)(Math.PI / 4f);
		const float eighthPi = (float)(Math.PI / 8f);

		public override void DrawModel(Entity p) {
            ApplyTexture(p);

			DrawRotate(-p.HeadXRadians, 0f, 0f, Head, true);

			DrawPart(Link);
			DrawPart(End);

			float rotX = (float)(Math.Sin(p.anim.walkTime) * p.anim.swing * Math.PI);
			float rotZ = (float)(Math.Cos(p.anim.walkTime * 2f) * p.anim.swing * Math.PI / 16f);
			float rotY = (float)(Math.Sin(p.anim.walkTime * 2f) * p.anim.swing * Math.PI / 32f);

			Rotate = RotateOrder.XZY;

			DrawRotate(rotX, quarterPi + rotY,  eighthPi + rotZ, LeftLeg, false);
			DrawRotate(-rotX, eighthPi + rotY, eighthPi + rotZ, LeftLeg, false);
			DrawRotate(rotX, -eighthPi - rotY, eighthPi - rotZ, LeftLeg, false);
			DrawRotate(-rotX, -quarterPi - rotY, eighthPi - rotZ, LeftLeg, false);
			DrawRotate(rotX, -quarterPi + rotY, -eighthPi + rotZ, RightLeg, false);
			DrawRotate(-rotX, -eighthPi + rotY, -eighthPi + rotZ, RightLeg, false);
            DrawRotate(rotX, eighthPi - rotY, -eighthPi - rotZ, RightLeg, false);
			DrawRotate(-rotX, quarterPi - rotY, -eighthPi - rotZ, RightLeg, false);

			Rotate = RotateOrder.ZYX;

			UpdateVB();
		}		
		ModelPart Head, Link, End, LeftLeg, RightLeg;
	}
}