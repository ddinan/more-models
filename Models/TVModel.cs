using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class TVModel : IModel {
		public TVModel(Game game) : base(game) { SurivalScore = 10; }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * boxesBuilt];
			
			Screen     = BuildBox(MakeBoxBounds(-24, 10, -1, 24, 35, 1).TexOrigin(0, 0).RotOrigin(0, 0, 0));
			Stem     = BuildBox(MakeBoxBounds(-4, 4, -1, 4, 10, 1).TexOrigin(7, 34).RotOrigin(0, 0, 0));
			Base     = BuildBox(MakeBoxBounds(-10, 0, -3, 10, 4, 3).TexOrigin(30, 31).RotOrigin(0, 0, 0));
        }

		public override float NameYOffset { get { return 2.25f; } }

		public override float GetEyeY(Entity entity) { return 1.5f; }

		public override Vector3 CollisionSize { get { return new Vector3(0.875f, 0.875f, 0.875f); } }

		public override AABB PickingBounds { get { return new AABB(-5f / 16f, 0f, -0.875f, 5f / 16f, 1f, 9f / 16f); }	}

		public override void DrawModel(Entity p) {
			ApplyTexture(p);
			uScale = 1.0f / 128f; vScale = 1.0f / 128f;

            DrawPart(Screen);
            DrawPart(Stem);
            DrawPart(Base);
			
			UpdateVB();
		}

		private ModelPart Screen, Stem, Base;

        private const int boxesBuilt = 3;
	}
}