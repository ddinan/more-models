using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
	public class FemaleModel : HumanoidModel {
		public FemaleModel(Game game) : base(game) { }

		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * 5];

            LeftBreast = BuildBox(MakeBoxBounds(-4, 18, -3, -1, 21, -2)
                            .TexOrigin(24, 21)
                            .RotOrigin(-2, 20, -2)
                            .Expand(0.25f));
            RightBreast = BuildBox(MakeBoxBounds(1, 18, -3, 4, 21, -2)
                            .TexOrigin(19, 21)
                            .RotOrigin(2, 20, -2)
                            .Expand(0.25f));
            Middle = BuildBox(MakeBoxBounds(-2, 18, -3, 2, 21, -1)
                            .TexOrigin(20, 20)
                            .RotOrigin(0, 20, -1));
                            //.Expand(-0.25f));
            LeftGlute = BuildBox(MakeBoxBounds(-4, 11, 2, 0, 14, 3)
                            .TexOrigin(26, 28));
            //.Expand(0.5f));
            RightGlute = BuildBox(MakeBoxBounds(0, 11, 2, 4, 14, 3)
                            .TexOrigin(30, 27));
                            //.Expand(0.5f));
		}

		public override void DrawModel(Entity p) {
            float breastBob = ((float)Math.Sin(p.anim.walkTime * 2f)) * p.anim.swing * 1f / 32f + 1f / 16f;

            game.ModelCache.Models[0].Instance.DrawModel(p);

			game.Graphics.AlphaTest = false;

            Translate(p, 0f, breastBob - 0.25f / 16f, 0.5f / 16f);
            DrawRotate((float)Math.PI / 16f, 0f, 0f, Middle, false);
            UpdateVB();

            Translate(p, 0.5f / 16f, breastBob, 0.5f / 16f);
            DrawRotate((float)Math.PI / 16f, 0f, 0f, LeftBreast, false);
            UpdateVB();

            Translate(p, -0.5f / 16f, breastBob, 0.5f / 16f);
            DrawRotate((float)Math.PI / 16f, 0f, 0f, RightBreast, false);
			UpdateVB();

            Translate(p, 0f, 0f, -0.5f / 16f);
            DrawPart(LeftGlute);
            DrawPart(RightGlute);
            UpdateVB();

            game.Graphics.AlphaTest = true;
        }

        public override void DrawArm(Entity p) {
            game.ModelCache.Models[0].Instance.DrawArm(p);
        }

        private void Translate(Entity p, float dispX, float dispY, float dispZ) {
            Vector3 pos = p.Position;
            if (Bobbing) pos.Y += p.anim.bobbingModel;

            Matrix4 matrix = TransformMatrix(p, pos), temp;
            Matrix4.Mult(out matrix, ref matrix, ref game.Graphics.View);
            Matrix4.Translate(out temp, dispX, dispY, dispZ);
            Matrix4.Mult(out matrix, ref temp, ref matrix);

            game.Graphics.LoadMatrix(ref matrix);
        }

        protected void ResetTransform(Entity p) {
            Vector3 pos = p.Position;
            if (Bobbing) pos.Y += p.anim.bobbingModel;

            Matrix4 matrix = TransformMatrix(p, pos);
            Matrix4.Mult(out matrix, ref matrix, ref game.Graphics.View);

            game.Graphics.LoadMatrix(ref matrix);
        }

        ModelPart LeftBreast, RightBreast, LeftGlute, RightGlute, Middle;
	}
}