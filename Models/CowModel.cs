using ClassicalSharp.Entities;
using ClassicalSharp.Physics;
using OpenTK;

namespace ClassicalSharp.Model {

	public class CowModel : IModel {
		
		public CowModel(Game game) : base(game) { SurivalScore = 10; }
	
		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * boxesBuilt];

			head          = BuildBox(MakeBoxBounds(-4, 16, -14, 4, 24, -8)  .TexOrigin(0, 0) .RotOrigin(0, 20, -6));
			rightHorn     = BuildBox(MakeBoxBounds(-5, 22, -13, -4, 25, -12).TexOrigin(22, 0).RotOrigin(0, 20, -6));
			leftHorn      = BuildBox(MakeBoxBounds(4, 22, -13, 5, 25, -12)  .TexOrigin(22, 0).RotOrigin(0, 20, -6));
			leftLegFront  = BuildBox(MakeBoxBounds(-6, 0, -8, -2, 12, -4)   .TexOrigin(0, 16).RotOrigin(0, 12, -5));
			rightLegFront = BuildBox(MakeBoxBounds(2, 0, -8, 6, 12, -4)     .TexOrigin(0, 16).RotOrigin(0, 12, -5));
			leftLegBack   = BuildBox(MakeBoxBounds(-6, 0, 5, -2, 12, 9)     .TexOrigin(0, 16).RotOrigin(0, 12, 7));
			rightLegBack  = BuildBox(MakeBoxBounds(2, 0, 5, 6, 12, 9)       .TexOrigin(0, 16).RotOrigin(0, 12, 7));

            torso = BuildRotatedBox(MakeRotatedBoxBounds(-6, 12, -8, 6, 22, 10) .TexOrigin(18, 4));
            udder = BuildRotatedBox(MakeRotatedBoxBounds(-2, 11, 4, 2, 12, 10)  .TexOrigin(52, 0));
        }
		
		public override float NameYOffset { get { return 1.5f; } }

		public override float GetEyeY(Entity entity) { return 12f/16f; }

		public override Vector3 CollisionSize {
			get { return new Vector3(14f / 16f, 14f / 16f, 14f / 16f); }
		}

		public override AABB PickingBounds {
			get { return new AABB(-5f / 16f, 0f, -14f / 16f, 5f / 16f, 16f / 16f, 9f / 16f); }
		}

		public override void DrawModel(Entity p) {
			game.Graphics.BindTexture(GetTexture(p));

            DrawPart(torso);
            DrawPart(udder);

            DrawRotate(-p.HeadXRadians, 0f, 0f, head, true);
			DrawRotate(-p.HeadXRadians, 0f, 0f, leftHorn, true);
			DrawRotate(-p.HeadXRadians, 0f, 0f, rightHorn, true);

			DrawRotate(p.anim.leftLegX, 0f, 0f, leftLegFront, false);
			DrawRotate(p.anim.rightLegX, 0f, 0f, rightLegFront, false);
			DrawRotate(p.anim.rightLegX, 0f, 0f, leftLegBack, false);
			DrawRotate(p.anim.leftLegX, 0f, 0f, rightLegBack, false);

			UpdateVB();
		}
		
		private ModelPart head, rightHorn, leftHorn, torso, udder, leftLegFront, rightLegFront, leftLegBack, rightLegBack;

        private const int boxesBuilt = 9;
	}
}