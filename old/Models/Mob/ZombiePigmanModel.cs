using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class ZombiePigmanModel : IModel {	
		public ZombiePigmanModel(Game window) : base(window) { SurivalScore = 20; }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * 7];
			Head     = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4)
			                    .TexOrigin(0, 0)
			                    .RotOrigin(0, 24, 0));
			LeftLeg  = BuildBox(MakeBoxBounds(0, 0, -2, -4, 12, 2)
			                    .TexOrigin(0, 16)
			                    .RotOrigin(0, 12, 0));
			RightLeg = BuildBox(MakeBoxBounds(0, 0, -2, 4, 12, 2)
			                    .TexOrigin(0, 16)
			                    .RotOrigin(0, 12, 0));
			LeftArm  = BuildBox(MakeBoxBounds(-4, 12, -2, -8, 24, 2)
			                    .TexOrigin(40, 16)
			                    .RotOrigin(-6, 22, 0));
			RightArm = BuildBox(MakeBoxBounds(4, 12, -2, 8, 24, 2)
			                    .TexOrigin(40, 16)
			                    .RotOrigin(6, 22, 0));

            Torso = BuildBox(MakeBoxBounds(-4, 12, -2, 4, 24, 2)
                             .TexOrigin(16, 16));

            Hat = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4)
                           .TexOrigin(32, 0)
                           .RotOrigin(0, 24, 0)
                           .Expand(0.5f));
        }

		public override float NameYOffset { get { return 2.075f; } }

		public override float GetEyeY(Entity entity) { return 1.625f; }

		public override Vector3 CollisionSize { get { return new Vector3(8.6f / 16f, 28.1f / 16f, 8.6f / 16f); } }

		public override AABB PickingBounds { get { return new AABB(-0.25f, 0f, -0.25f, 0.25f, 2f, 0.25f); } }

		public override void DrawModel(Entity p) {
			ApplyTexture(p);

            DrawPart(Torso);

            DrawRotate(-p.HeadXRadians, 0f, 0f, Head, true);
			DrawRotate(p.anim.leftLegX, 0f, 0f, LeftLeg, false);
			DrawRotate(p.anim.rightLegX, 0f, 0f, RightLeg, false);
			DrawRotate(90f * Utils.Deg2Rad, 0f, p.anim.leftArmZ, LeftArm, false);
			DrawRotate(90f * Utils.Deg2Rad, 0f, p.anim.rightArmZ, RightArm, false);
			DrawRotate(-p.HeadXRadians, 0f, 0f, Hat, true);

			UpdateVB();
		}
		
		ModelPart Head, Hat, Torso, LeftLeg, RightLeg, LeftArm, RightArm;
	}
}