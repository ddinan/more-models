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

            DrawTranslateAndRotate((float)Math.Sin(walkRot / 2f) * 6f / 16f, 0f, (float)Math.Cos(walkRot / 2f) * -6f / 16f + 6f / 16f, 0f, -walkRot / 2f, 0f, snout);
            DrawTranslateAndRotate((float)Math.Sin(walkRot) * 5f / 16f, 0f, (float)Math.Cos(walkRot / 2f) * 5f / 16f - 5f / 16f, 0f, walkRotPhase1, 0f, midTail);
            DrawTranslateAndRotate((float)Math.Sin(walkRot) * 5f / 16f + (float)Math.Sin(walkRotPhase1) * 5f / 16f, 0f, (float)Math.Cos(walkRot) * 5f / 16f - 5f / 16f + (float)Math.Cos(walkRotPhase1) * 5f / 16f - 5f / 16f, 0f, walkRotPhase2, 0f, backTail);

            UpdateVB();
        }

        private void DrawTranslateAndRotate(float dispX, float dispY, float dispZ, float rotX, float rotY, float rotZ, ModelPart part) {
            float cosX = (float)Math.Cos(-rotX), sinX = (float)Math.Sin(-rotX);
            float cosY = (float)Math.Cos(-rotY), sinY = (float)Math.Sin(-rotY);
            float cosZ = (float)Math.Cos(-rotZ), sinZ = (float)Math.Sin(-rotZ);

            VertexP3fT2fC4b vertex = default(VertexP3fT2fC4b);
            VertexP3fT2fC4b[] finVertices = game.ModelCache.vertices;

            for (int i = 0; i < part.Count; i++) {
                ModelVertex v = vertices[part.Offset + i];

                // Prepare the vertex coordinates for rotation
                v.X -= part.RotX; v.Y -= part.RotY; v.Z -= part.RotZ;
                float t = 0f;

                // Rotate locally.
                if (Rotate == RotateOrder.ZYX) {
                    t = cosZ * v.X + sinZ * v.Y; v.Y = -sinZ * v.X + cosZ * v.Y; v.X = t; // Inlined RotZ
                    t = cosY * v.X - sinY * v.Z; v.Z = sinY * v.X + cosY * v.Z; v.X = t; // Inlined RotY
                    t = cosX * v.Y + sinX * v.Z; v.Z = -sinX * v.Y + cosX * v.Z; v.Y = t; // Inlined RotX
                }
                else if (Rotate == RotateOrder.XZY) {
                    t = cosX * v.Y + sinX * v.Z; v.Z = -sinX * v.Y + cosX * v.Z; v.Y = t; // Inlined RotX
                    t = cosZ * v.X + sinZ * v.Y; v.Y = -sinZ * v.X + cosZ * v.Y; v.X = t; // Inlined RotZ
                    t = cosY * v.X - sinY * v.Z; v.Z = sinY * v.X + cosY * v.Z; v.X = t; // Inlined RotY
                }
                else if (Rotate == RotateOrder.YZX) {
                    t = cosY * v.X - sinY * v.Z; v.Z = sinY * v.X + cosY * v.Z; v.X = t; // Inlined RotY
                    t = cosZ * v.X + sinZ * v.Y; v.Y = -sinZ * v.X + cosZ * v.Y; v.X = t; // Inlined RotZ
                    t = cosX * v.Y + sinX * v.Z; v.Z = -sinX * v.Y + cosX * v.Z; v.Y = t; // Inlined RotX
                }

                vertex.X = v.X + part.RotX; vertex.Y = v.Y + part.RotY; vertex.Z = v.Z + part.RotZ;
                // Translate part
                vertex.X += dispX; vertex.Y += dispY; vertex.Z += dispZ;

                vertex.Col = cols[i >> 2];

                vertex.U = (v.U & UVMask) * uScale - (v.U >> UVMaxShift) * 0.01f * uScale;
                vertex.V = (v.V & UVMask) * vScale - (v.V >> UVMaxShift) * 0.01f * vScale;
                finVertices[index++] = vertex;
            }
        }

        private ModelPart snout, head, body, leftLegFront, rightLegFront, leftLegBack, rightLegBack, frontTail, midTail, backTail;

        private const int boxesBuilt = 10;
    }
}