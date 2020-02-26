using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class VillagerModel : IModel {	
		public VillagerModel(Game window) : base(window) { SurivalScore = 20; }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * 7];
			Head = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 34, 4)
				.TexOrigin(0, 0)
				.RotOrigin(0, 24, 0));
			Nose = BuildBox(MakeBoxBounds(1, 27, -4, -1, 23, -5)
				.TexOrigin(24, 0)
				.RotOrigin(0, 24, 0));
			LeftLeg = BuildBox(MakeBoxBounds(0, 0, -2, -4, 12, 2)
				.TexOrigin(0, 22)
				.RotOrigin(0, 12, 0));
			RightLeg = BuildBox(MakeBoxBounds(0, 0, -2, 4, 12, 2)
				.TexOrigin(0, 22)
				.RotOrigin(0, 12, 0));
			Arms = BuildBox(MakeBoxBounds(-8, 15, -3, 8, 23, 2)
				.TexOrigin(28, 51)
				.RotOrigin(0, 21, -1));
            Torso = BuildBox(MakeBoxBounds(-4, 12, -2, 4, 24, 2)
            	.TexOrigin(16, 20));
			Robe = BuildBox(MakeBoxBounds(-4, 4, 3, 4, 24, -3)
            	.TexOrigin(0, 38)
				.Expand(0.5f));
        }

		public override float NameYOffset { get { return 2.125f; } }

		public override float GetEyeY(Entity entity) { return 1.625f; }

		public override Vector3 CollisionSize { get { return new Vector3(8.6f / 16f, 28.1f / 16f, 8.6f / 16f); } }

		public override AABB PickingBounds { get { return new AABB(-0.25f, 0f, -0.25f, 0.25f, 2f, 0.25f); } }

		public override void DrawModel(Entity p) {
			// Remove arm animation
			p.anim.leftArmX = 0; p.anim.rightArmX = 0;
			p.anim.leftArmZ = 0; p.anim.rightArmZ = 0;
			
			ApplyTexture(p);

            DrawPart(Torso);
            DrawPart(Robe);

            DrawRotate(-p.HeadXRadians, 0f, 0f, Head, true);
            DrawRotate(-p.HeadXRadians, 0f, 0f, Nose, true);
			DrawRotate(p.anim.leftLegX, 0f, 0f, LeftLeg, false);
			DrawRotate(p.anim.rightLegX, 0f, 0f, RightLeg, false);
			DrawRotate(45f * Utils.Deg2Rad, 0f, p.anim.leftArmZ, Arms, false);

			UpdateVB();
		}
		
		ModelPart Head, Nose, Torso, Robe, LeftLeg, RightLeg, Arms;
	}
}