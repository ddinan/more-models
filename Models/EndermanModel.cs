// Due to not supporting multiple image files per single model, you will need to add the eyes manually if you're using a Minecraft enderman skin.

using System;
using ClassicalSharp.Entities;
using ClassicalSharp.Physics;
using OpenTK;

namespace ClassicalSharp.Model {

	public class EndermanModel : IModel {
		
		public EndermanModel(Game window) : base(window) { SurivalScore = 20; }

		/// <inheritdoc/>		
		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * 7];
			Head = BuildBox(MakeBoxBounds(-4, 44, -4, 4, 52, 4)
			               .TexOrigin(0, 0)
			               .RotOrigin(0, 44, 0));
			Jaw = BuildBox(MakeBoxBounds(-4, 44, -4, 4, 52, 4)
			               .TexOrigin(0, 16)
			               .RotOrigin(0, 44, 0));
			Torso = BuildBox(MakeBoxBounds(-4, 32, -2, 4, 44, 2)
			               .TexOrigin(32, 16)
			               .RotOrigin(0, 0, 0));
			LeftArm = BuildBox(MakeBoxBounds(-4, 16, -1, -6, 44, 1)
			               .TexOrigin(56, 0)
						   .RotOrigin(-6, 44, 0));
			RightArm = BuildBox(MakeBoxBounds(4, 16, -1, 6, 44, 1)
			               .TexOrigin(56, 0)
			               .RotOrigin(6, 44, 0));
			LeftLeg = BuildBox(MakeBoxBounds(-1, 0, -1, -3, 32, 1)
			               .TexOrigin(56, 0)
			               .RotOrigin(0, 33, 0));
			RightLeg = BuildBox(MakeBoxBounds(1, 0, -1, 3, 32, 1)
			               .TexOrigin(56, 0)
						   .RotOrigin(0, 33, 0));
		}
		
		/// <inheritdoc/>
		public override float NameYOffset { get { return 3.25f; } }

		/// <inheritdoc/>
		public override float GetEyeY(Entity entity) { return 47f/16f; }

		/// <inheritdoc/>
		public override Vector3 CollisionSize {
			get { return new Vector3(14/16f, 14/16f, 14/16f); }
		}

		/// <inheritdoc/>
		public override AABB PickingBounds {
			get { return new AABB(-5/16f, 0, -14/16f, 5/16f, 16/16f, 9/16f); }
		}

		/// <inheritdoc/>
		public override void DrawModel(Entity p) {
			game.Graphics.BindTexture(GetTexture(p));
			DrawRotate(-p.HeadXRadians, 0, 0, Head, true);
			DrawRotate(-p.HeadXRadians, 0, 0, Jaw, true);
			
			DrawPart(Torso);
			DrawRotate(p.anim.leftArmX, 0, p.anim.leftArmZ, LeftArm, false);
			DrawRotate(p.anim.rightArmX, 0, p.anim.rightArmZ, RightArm, false);
			DrawRotate(p.anim.leftLegX, 0, 0, LeftLeg, false);
			DrawRotate(p.anim.rightLegX, 0, 0, RightLeg, false);
			UpdateVB();
		}
		
		ModelPart Head, Jaw, Torso, LeftArm, RightArm, LeftLeg, RightLeg;
	}
}
