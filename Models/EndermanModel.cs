using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class EndermanModel : IModel {		
		public EndermanModel(Game window) : base(window) { SurivalScore = 20; }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * 8];
			Head     = BuildBox(MakeBoxBounds(-4, 42, -4, 4, 50, 4)
			                    .TexOrigin(0, 0)
			                    .RotOrigin(0, 44, 0));
			Jaw      = BuildBox(MakeBoxBounds(-4, 42, -4, 4, 50, 4)
			                    .TexOrigin(0, 16)
			                    .RotOrigin(0, 44, 0));
			LeftArm  = BuildBox(MakeBoxBounds(-4, 16, -1, -6, 42, 1)
			                    .TexOrigin(56, 0)
						        .RotOrigin(-6, 44, 0));
			RightArm = BuildBox(MakeBoxBounds(4, 16, -1, 6, 42, 1)
			                    .TexOrigin(56, 0)
			                    .RotOrigin(6, 44, 0));
			LeftLeg  = BuildBox(MakeBoxBounds(-1, 0, -1, -3, 30, 1)
			                    .TexOrigin(56, 0)
			                    .RotOrigin(0, 33, 0));
			RightLeg = BuildBox(MakeBoxBounds(1, 0, -1, 3, 30, 1)
			                    .TexOrigin(56, 0)
						        .RotOrigin(0, 33, 0));

            Torso = BuildBox(MakeBoxBounds(-4, 30, -2, 4, 42, 2)
                             .TexOrigin(32, 16));

            Eyes = BuildBox(MakeBoxBounds(-4, 45, -4, 4, 46, -3)
                            .TexOrigin(7, 11)
                            .RotOrigin(0, 44, 0)
                            .Expand(0.25f));
        }

		public override float NameYOffset { get { return 3.25f; } }

		public override float GetEyeY(Entity entity) { return 47f / 16f; }

		public override Vector3 CollisionSize { get { return new Vector3(3f / 16f, 45f / 16f, 3 / 16f); } }

		public override AABB PickingBounds { get { return new AABB(-6f / 16f, 0f, -4f / 16f, 6f / 16f, 48f / 16f, 4f / 16f); } }

		public override void DrawModel(Entity p) {
			ApplyTexture(p);

            DrawPart(Torso);

            DrawRotate(-p.HeadXRadians, 0f, 0f, Head, true);
			DrawRotate(-p.HeadXRadians, 0f, 0f, Jaw, true);
			DrawRotate(p.anim.leftArmX, 0f, p.anim.leftArmZ, LeftArm, false);
			DrawRotate(p.anim.rightArmX, 0f, p.anim.rightArmZ, RightArm, false);
			DrawRotate(p.anim.leftLegX, 0f, 0f, LeftLeg, false);
			DrawRotate(p.anim.rightLegX, 0f, 0f, RightLeg, false);

            UpdateVB();
            index = 0;
            game.Graphics.BindTexture(game.ModelCache.Textures[game.ModelCache.GetTextureIndex("enderman_eyes.png")].TexID);

            DrawRotate(-p.HeadXRadians, 0f, 0f, Eyes, true);

            UpdateVB();

		}

		ModelPart Head, Jaw, Eyes, Torso, LeftArm, RightArm, LeftLeg, RightLeg;
	}
}
