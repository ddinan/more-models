// Cape textures are flipped so you will need to flip both of your cape textures.
using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;

namespace MoreModels {
	public class CapeModel : HumanoidModel {
        public CapeModel(Game game) : base(game) { }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices];
			Cape = BuildBox(MakeBoxBounds(-5, 8, 3, 5, 24, 2)
							.TexOrigin(0, 0)
							.RotOrigin(0, 23, 0));
		}

		public override void DrawModel(Entity p) {
            game.ModelCache.Models[0].Instance.DrawModel(p);

			game.Graphics.BindTexture(game.ModelCache.Textures[texIndex].TexID);
			uScale = 1.0f / 64f; vScale = 1.0f / 32f;
			DrawRotate(p.anim.swing * ((float)Math.PI / -3f) + p.anim.leftArmZ * (1f - p.anim.swing), 0, 0, Cape, false);
			UpdateVB();
			game.Graphics.AlphaTest = true;
		}

        public override void DrawArm(Entity p) {
            game.ModelCache.Models[0].Instance.DrawArm(p);
        }
        ModelPart Cape;
	}
}
