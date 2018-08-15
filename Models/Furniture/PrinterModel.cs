using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.GraphicsAPI;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
    class PrinterModel : IModel {
        public PrinterModel(Game game) : base(game) { }

        public override void CreateParts() {
            vertices = new ModelVertex[boxVertices * boxesBuilt];

            bottom    = BuildRotatedBox(MakeRotatedBoxBounds(-8, 0, -8, 8, 1, 8) .TexOrigin(0, 0));
            left      = BuildRotatedBox(MakeRotatedBoxBounds(-7, 1, -7, -6, 6, 7).TexOrigin(34, 14));
            right     = BuildRotatedBox(MakeRotatedBoxBounds(6, 1, -7, 7, 6, 7)  .TexOrigin(34, 14));
            center    = BuildRotatedBox(MakeRotatedBoxBounds(-6, 2, -6, 6, 7, 5) .TexOrigin(0, 17));
            top       = BuildRotatedBox(MakeRotatedBoxBounds(-2, 7, -3, 2, 8, 3) .TexOrigin(46, 14));
            topLeft   = BuildRotatedBox(MakeRotatedBoxBounds(-5, 7, 0, -2, 8, 3) .TexOrigin(56, 14));
            topRight  = BuildRotatedBox(MakeRotatedBoxBounds(2, 7, 0, 5, 8, 3)   .TexOrigin(56, 14));
            lineLeft  = BuildRotatedBox(MakeRotatedBoxBounds(-4, 7, -7, -3, 8, 0).TexOrigin(60, 0));
            lineRight = BuildRotatedBox(MakeRotatedBoxBounds(3, 7, -7, 4, 8, 0)  .TexOrigin(60, 0));

            back  = BuildBox(MakeBoxBounds(-6, 1, 5, 6, 5, 6)  .TexOrigin(34, 9));
            front = BuildBox(MakeBoxBounds(-4, 2, -7, 4, 6, -6).TexOrigin(46, 21));

            tray = BuildBox(MakeBoxBounds(-6, 4, 4, 6, 12, 5).TexOrigin(34, 0).RotOrigin(0, 4, 4));
        }

        public override float GetEyeY(Entity e) { return 4f / 16f; }

        public override Vector3 CollisionSize { get { return new Vector3(.9375f, 0.75f, .9375f); } }

        public override float NameYOffset { get { return 0.75f; } }

        public override AABB PickingBounds { get { return new AABB(-0.5f, 0f, -0.5f, 0.5f, 0.5f, 0.5f); } }

        public override void DrawModel(Entity p) {
            vScale = 1f / 64f;

            ApplyTexture(p);

            DrawPart(bottom);
            DrawPart(left);
            DrawPart(right);
            DrawPart(topLeft);
            DrawPart(topRight);

            UpdateVB();

            Translate(p, 0f, 0f, 0.75f / 16f);
            DrawPart(back);
            UpdateVB();

            Translate(p, 0f, 0.5f / 16f, 0f);
            DrawPart(front);
            UpdateVB();

            Translate(p, 0f, 0f, -0.5f / 16f);
            DrawPart(center);
            UpdateVB();

            Translate(p, 0f, -0.5f / 16f, 0f);
            DrawPart(top);
            DrawPart(lineLeft);
            DrawPart(lineRight);
            UpdateVB();

            Translate(p, 0f, 0.5f / 16f, 0.75f / 16f);
            DrawRotate((float)Math.PI / 8f, 0f, 0f, tray, false);
            UpdateVB();
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

        protected const int boxesBuilt = 12;

        protected ModelPart bottom, back, tray, front, center, topLeft, topRight, lineLeft, lineRight, left, right, top;
    }
}