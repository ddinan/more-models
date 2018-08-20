using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class TableModel : IModel {
		public TableModel(Game game) : base(game) { SurivalScore = 0; }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * boxesBuilt];
			
			Base = BuildBox(MakeBoxBounds(-8, 16, -8, 8, 18, 8).TexOrigin(0, 0));
			LeftLegFront = BuildBox(MakeBoxBounds(-6, 15, -6, -8, 0, -8).TexOrigin(0, 0));
			RightLegFront = BuildBox(MakeBoxBounds(8, 15, -6, 6, 0, -8).TexOrigin(0, 0));
			LeftLegBack = BuildBox(MakeBoxBounds(-6, 15, 8, -8, 0, 6).TexOrigin(0, 0));
			RightLegBack = BuildBox(MakeBoxBounds(8, 15, 6, 6, 0, 8).TexOrigin(0, 0));
        }

		public override float NameYOffset { get { return 1.5f; } }

		public override float GetEyeY(Entity entity) { return 1.25f; }

		public override Vector3 CollisionSize { get { return new Vector3(0.875f, 0.875f, 0.875f); } }

		public override AABB PickingBounds { get { return new AABB(-5f / 16f, 0f, -0.875f, 5f / 16f, 1f, 9f / 16f); }	}

		public override void DrawModel(Entity p) {
			ApplyTexture(p);
			uScale = 1.0f / 16f; vScale = 1.0f / 16f;

            DrawPart(Base);
            DrawPart(LeftLegFront);
            DrawPart(RightLegFront);
			DrawPart(LeftLegBack);
            DrawPart(RightLegBack);
            
			UpdateVB();
		}

		private ModelPart Base, LeftLegFront, RightLegFront, LeftLegBack, RightLegBack;

        private const int boxesBuilt = 5;
	}
}