// Cape textures are flipped so you will need to flip both of your cape textures.
using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class CapeModel : HumanoidModel {
        public CapeModel(Game window) : base(window) {
            UsesHumanSkin = true;
            CalcHumanAnims = true;

            //HumanoidModel humanoid = (HumanoidModel)game.ModelCache.Models[0].Instance;
            //vertices = humanoid.vertices;
        }

		//public override float NameYOffset { get { return 2.03125f; } }
		//public override Vector3 CollisionSize { get { return new Vector3(0.5375f, 1.75625f, 0.5375f); } }
		//public override AABB PickingBounds { get { return new AABB(-0.5f, 0f, -0.25f, 0.5f, 2f, 0.25f); } }
		//public override float GetEyeY(Entity entity) { return 1.625f; }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices];
			Cape = BuildBox(MakeBoxBounds(-5, 8, 3, 5, 24, 2)
							.TexOrigin(0, 0)
							.RotOrigin(0, 23, 0));
		}

		public override void DrawModel(Entity p) {
            game.ModelCache.Models[0].Instance.DrawModel(p);

			game.Graphics.BindTexture(game.ModelCache.Textures[game.ModelCache.GetTextureIndex("cape.png")].TexID);
			uScale = 1.0f / 64f; vScale = 1.0f / 32f;
			DrawRotate(p.anim.swing * -((float)Math.PI / 3f) + p.anim.leftArmZ, 0, 0, Cape, false);
			UpdateVB();
			game.Graphics.AlphaTest = true;
		}

        public override void DrawArm(Entity p) {
            game.ModelCache.Models[0].Instance.DrawArm(p);
        }
        ModelPart Cape;
	}
}