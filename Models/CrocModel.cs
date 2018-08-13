using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.GraphicsAPI;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
    class CrocModel : IModel {
        public CrocModel(Game game) : base(game) {
            SurivalScore = 80;
            Bobbing = false;
        }

        public override void CreateParts() {
            vertices = new ModelVertex[boxVertices * boxesBuilt];

            snout     = BuildRotatedBox(MakeRotatedBoxBounds(-3, 0, -19, 3, 3, -12).TexOrigin(30, 0) .RotOrigin(0, 2, -13));
            head      = BuildRotatedBox(MakeRotatedBoxBounds(-4, 0, -12, 4, 4, -7) .TexOrigin(0, 23) .RotOrigin(0, 2, -7));
            frontTail = BuildRotatedBox(MakeRotatedBoxBounds(-4, 0, 9, 4, 4, 14)   .TexOrigin(24, 23).RotOrigin(0, 2, 9));
            midTail   = BuildRotatedBox(MakeRotatedBoxBounds(-3, 0, 13, 3, 3, 18)  .TexOrigin(30, 10).RotOrigin(0, 2, 13));
            backTail  = BuildRotatedBox(MakeRotatedBoxBounds(-2, 0, 17, 2, 2, 22)  .TexOrigin(48, 0) .RotOrigin(0, 1, 17));            

            body = BuildRotatedBox(MakeRotatedBoxBounds(-5, 0, -8, 5, 5, 10) .TexOrigin(0, 0));

            leftLegFront = BuildBox(MakeBoxBounds(-8, 0, -7, -5, 3, -4).MirrorX().TexOrigin(48, 7).RotOrigin(-6, 2, -5));
            leftLegBack  = BuildBox(MakeBoxBounds(-8, 0, 6, -5, 3, 9)  .MirrorX().TexOrigin(48, 7).RotOrigin(-6, 2, 7));

            rightLegFront = BuildBox(MakeBoxBounds(5, 0, -7, 8, 3, -4).TexOrigin(48, 7).RotOrigin(6, 2, -5));
            rightLegBack  = BuildBox(MakeBoxBounds(5, 0, 6, 8, 3, 9)  .TexOrigin(48, 7).RotOrigin(6, 2, 7));
        }

        public override float NameYOffset { get { return 0.5f; } }

        public override float GetEyeY(Entity entity) { return 0.25f; }

        public override Vector3 CollisionSize { get { return new Vector3(1.9385f, 5f / 16f, 1.9375f); } }

        public override AABB PickingBounds { get { return new AABB(-8f / 16f, 0f, -19 / 16f, 8f / 16f, 5f / 16f, 22f / 16f); } }

        public override void DrawModel(Entity p) {
            ApplyTexture(p);

            float walkRot = (float)Math.Sin(p.anim.walkTime) * (float)Math.PI / -16f;
            float walkRotPhase1 = (float)Math.Sin(p.anim.walkTime - Math.PI / 8f) * (float)Math.PI / -12f;
            float walkRotPhase2 = (float)Math.Sin(p.anim.walkTime - Math.PI / 4f) * (float)Math.PI / -8f;

            DrawPart(body);

            DrawRotate(0f, -walkRot / 2f, 0f, head, false);
            DrawRotate(0f, walkRot, 0f, frontTail, false);
            DrawRotate(walkRot * 2f, 0f, 0f, leftLegFront, false);
            DrawRotate(walkRot * -2f, 0f, 0f, rightLegFront, false);
            DrawRotate(walkRot * -2f, 0f, 0f, leftLegBack, false);
            DrawRotate(walkRot * 2f, 0f, 0f, rightLegBack, false);

            UpdateVB();

            Translate(p, (float)Math.Sin(walkRot / 2f) * 6f / 16f, 0f, ((float)Math.Cos(walkRot / 2f) - 1f) * -6f / 16f);
            DrawRotate(0f, -walkRot / 2f, 0f, snout, false);

            UpdateVB();

            Translate(p, (float)Math.Sin(walkRot) * 5f / 16f, 0f, ((float)Math.Cos(walkRot / 2f) - 1f) * 5f / 16f);
            DrawRotate(0f, walkRotPhase1, 0f, midTail, false);

            UpdateVB();

            Translate(p, (float)(Math.Sin(walkRot) + Math.Sin(walkRotPhase1)) * 5f / 16f, 0f, ((float)Math.Cos(walkRot) + (float)Math.Cos(walkRotPhase1) - 2f) * 5f / 16f);
            DrawRotate(0f, walkRotPhase2, 0f, backTail, false);

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

        private ModelPart snout, head, body, leftLegFront, rightLegFront, leftLegBack, rightLegBack, frontTail, midTail, backTail;

        private const int boxesBuilt = 10;
    }
}