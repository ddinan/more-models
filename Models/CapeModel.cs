// Cape textures are flipped so you will need to flip both of your cape textures.
using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class CapeModel : IModel {
		public CapeModel(Game window) : base(window) { UsesHumanSkin = true; }

		public override float NameYOffset { get { return 2.03125f; } }
		public override Vector3 CollisionSize { get { return new Vector3(0.5375f, 1.75625f, 0.5375f); } }
		public override AABB PickingBounds { get { return new AABB(-0.5f, 0f, -0.25f, 0.5f, 2f, 0.25f); } }
		public override float GetEyeY(Entity entity) { return 1.625f; }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * 8];
			Head = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4)
							.TexOrigin(0, 0)
							.RotOrigin(0, 24, 0));
			Torso = BuildBox(MakeBoxBounds(-4, 12, -2, 4, 24, 2)
							.TexOrigin(16, 16)
							.RotOrigin(0, 0, 0));
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
			Cape = BuildBox(MakeBoxBounds(-5, 8, 3, 5, 24, 2)
							.TexOrigin(0, 0)
							.RotOrigin(0, 23, 0));
			Hat = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4)
			                .TexOrigin(32, 0)
			                .RotOrigin(0, 24, 0)
			                .Expand(0.5f));
		}

		public override void DrawModel(Entity p) {
			ApplyTexture(p);

			game.Graphics.AlphaTest = false;
			DrawRotate(0 - p.HeadXRadians, 0, 0, Head, true);
			UpdateVB();
			
			game.Graphics.AlphaTest = true;
			DrawRotate(0 - p.HeadXRadians, 0, 0, Hat, true);
			UpdateVB();
			
			game.Graphics.AlphaTest = false;
			DrawPart(Torso);
			DrawRotate(p.anim.leftLegX, 0, 0, LeftLeg, false);
			DrawRotate(p.anim.rightLegX, 0, 0, RightLeg, false);
			DrawRotate(p.anim.leftArmX, 0, p.anim.leftArmZ, LeftArm, false);
			DrawRotate(p.anim.rightArmX, 0, p.anim.rightArmZ, RightArm, false);
			UpdateVB();

			game.Graphics.BindTexture(game.ModelCache.Textures[game.ModelCache.GetTextureIndex("cape.png")].TexID);
			uScale = 1.0f / 64f; vScale = 1.0f / 32f;
			DrawRotate(p.anim.swing * -((float)Math.PI / 3f) + p.anim.leftArmZ, 0, 0, Cape, false);
			UpdateVB();
			game.Graphics.AlphaTest = true;
		}
		ModelPart Head, Hat, Torso, LeftArm, RightArm, LeftLeg, RightLeg, Cape;
	}
}