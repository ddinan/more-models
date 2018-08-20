using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class ChairModel : IModel {
		public ChairModel(Game game) : base(game) { SurivalScore = 0; }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * boxesBuilt];
			
			Base = BuildBox(MakeBoxBounds(-8, 12, -8, 8, 14, 8).TexOrigin(0, 0));
			Back = BuildBox(MakeBoxBounds(8, 14, 8, -8, 30, 6).TexOrigin(0, 0));
			LeftLegFront = BuildBox(MakeBoxBounds(-6, 12, -6, -8, 0, -8).TexOrigin(0, 0));
			RightLegFront = BuildBox(MakeBoxBounds(8, 12, -6, 6, 0, -8).TexOrigin(0, 0));
			LeftLegBack = BuildBox(MakeBoxBounds(-6, 12, 8, -8, 0, 6).TexOrigin(0, 0));
			RightLegBack = BuildBox(MakeBoxBounds(8, 12, 6, 6, 0, 8).TexOrigin(0, 0));
        }

		public override float NameYOffset { get { return 2f; } }

		public override float GetEyeY(Entity entity) { return 1.25f; }

		public override Vector3 CollisionSize { get { return new Vector3(0.875f, 0.875f, 0.875f); } }

		public override AABB PickingBounds { get { return new AABB(-5f / 16f, 0f, -0.875f, 5f / 16f, 1f, 9f / 16f); }	}

		public override void DrawModel(Entity p) {
			ApplyTexture(p);
			uScale = 1.0f / 16f; vScale = 1.0f / 16f;

            DrawPart(Base);
            DrawPart(Back);
            DrawPart(LeftLegFront);
            DrawPart(RightLegFront);
			DrawPart(LeftLegBack);
            DrawPart(RightLegBack);
            
			UpdateVB();
		}

		private ModelPart Base, Back, LeftLegFront, RightLegFront, LeftLegBack, RightLegBack;

        private const int boxesBuilt = 6;
	}
}