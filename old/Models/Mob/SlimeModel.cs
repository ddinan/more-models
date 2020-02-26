using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class SlimeModel : IModel {
		public SlimeModel(Game game) : base(game) { SurivalScore = 10; }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * boxesBuilt];
			
			headInner     = BuildBox(MakeBoxBounds(-3, 7, -3, 3, 1, 3).TexOrigin(0, 16).RotOrigin(0, 0, 0));
			headOuter     = BuildBox(MakeBoxBounds(4, 8, -4, -4, 0, 4).TexOrigin(0, 0).RotOrigin(0, 0, 0));
        }

		public override float NameYOffset { get { return 0.5f; } }

		public override float GetEyeY(Entity entity) { return 6f/16f; }

		public override Vector3 CollisionSize { get { return new Vector3(0.875f, 0.875f, 0.875f); } }

		public override AABB PickingBounds { get { return new AABB(-5f / 16f, 0f, -0.875f, 5f / 16f, 1f, 9f / 16f); }	}

		public override void DrawModel(Entity p) {
			ApplyTexture(p);

            DrawRotate(-p.HeadXRadians, 0f, 0f, headInner, true);
			DrawRotate(-p.HeadXRadians, 0f, 0f, headOuter, true);

			game.Graphics.AlphaBlending = true;
			
			UpdateVB();
			
			game.Graphics.AlphaBlending = false;
		}

		private ModelPart headInner, headOuter;

        private const int boxesBuilt = 2;
	}
}