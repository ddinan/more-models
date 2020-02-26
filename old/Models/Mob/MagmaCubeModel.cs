using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class MagmaCubeModel : IModel {
		public MagmaCubeModel(Game game) : base(game) { SurivalScore = 16; }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * boxesBuilt];
			
			head     = BuildBox(MakeBoxBounds(4, 8, -4, -4, 0, 4).TexOrigin(0, 0).RotOrigin(0, 0, 0));
        }

		public override float NameYOffset { get { return 0.5f; } }

		public override float GetEyeY(Entity entity) { return 6f/16f; }

		public override Vector3 CollisionSize { get { return new Vector3(0.875f, 0.875f, 0.875f); } }

		public override AABB PickingBounds { get { return new AABB(-5f / 16f, 0f, -0.875f, 5f / 16f, 1f, 9f / 16f); }	}

		public override void DrawModel(Entity p) {
			ApplyTexture(p);

            DrawRotate(-p.HeadXRadians, 0f, 0f, head, true);

			UpdateVB();
		}

		private ModelPart head;

        private const int boxesBuilt = 1;
	}
}