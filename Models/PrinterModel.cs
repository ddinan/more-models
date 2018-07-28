using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.GraphicsAPI;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels
{
    class PrinterModel : IModel
    {
        public PrinterModel(Game game) : base(game) { }

        public override float GetEyeY(Entity e) { return 4f / 16f; }

        public override Vector3 CollisionSize { get { return new Vector3(1f, 0.5f, 1f); } }

        public override float NameYOffset { get { return 0.75f; } }

        public override AABB PickingBounds { get { return new AABB(-0.5f, 0f, -0.5f, 0.5f, 0.5f, 1.5f); } }

        public override void CreateParts()
        {
            vertices = new ModelVertex[boxVertices * boxesBuilt];

            bottom    = BuildRotatedBox(MakeRotatedBoxBounds(-8, 0, -8, 8, 1, 8) .TexOrigin(0, 0));            
            front     = BuildRotatedBox(MakeRotatedBoxBounds(-4, 2, -7, 4, 6, 4) .TexOrigin(0, 33));
            left      = BuildRotatedBox(MakeRotatedBoxBounds(-7, 1, -7, -6, 6, 7).TexOrigin(34, 14));
            right     = BuildRotatedBox(MakeRotatedBoxBounds(6, 1, -7, 7, 6, 7)  .TexOrigin(34, 14));            
            center    = BuildRotatedBox(MakeRotatedBoxBounds(-6, 2, -6, 6, 7, 5) .TexOrigin(0, 17));
            top       = BuildRotatedBox(MakeRotatedBoxBounds(-2, 7, -3, 2, 8, 3) .TexOrigin(46, 14));
            topLeft   = BuildRotatedBox(MakeRotatedBoxBounds(-5, 7, 0, -2, 8, 3) .TexOrigin(56, 14));
            topRight  = BuildRotatedBox(MakeRotatedBoxBounds(2, 7, 0, 5, 8, 3)   .TexOrigin(56, 14));
            lineLeft  = BuildRotatedBox(MakeRotatedBoxBounds(-4, 7, -7, -3, 8, 2).TexOrigin(60, 0));
            lineRight = BuildRotatedBox(MakeRotatedBoxBounds(3, 7, -7, 4, 8, 2)  .TexOrigin(60, 0));

            back = BuildBox(MakeBoxBounds(-6, 1, 5, 6, 5, 6).TexOrigin(34, 9));

            tray = BuildBox(MakeBoxBounds(-6, 4, 4, 6, 12, 5).TexOrigin(34, 0).RotOrigin(0, 4, 4));            
        }

        public override void DrawModel(Entity p)
        {
            vScale = 1f / 64f;

            game.Graphics.BindTexture(GetTexture(p));

            DrawPart(bottom);
            DrawPart(left);
            DrawPart(right);
            DrawPart(topLeft);
            DrawPart(topRight);

            DrawTranslate(0f, 0f, 0.75f / 16f, back);
            DrawTranslate(0f, 0.5f / 16f, 0f, front);
            DrawTranslate(0f, 0f, -0.5f / 16f, center);
            DrawTranslate(0f, -0.5f / 16f, 0f, top);
            DrawTranslate(0f, -0.5f / 16f, 0f, lineLeft);
            DrawTranslate(0f, -0.5f / 16f, 0f, lineRight);            
            
            DrawTranslateAndRotate(0f, 0.5f / 16f, 0.75f / 16f, (float)Math.PI / 8f, 0f, 0f, tray);

            UpdateVB();
        }

        private void DrawTranslate(float dispX, float dispY, float dispZ, ModelPart part)
        {
            VertexP3fT2fC4b vertex = default(VertexP3fT2fC4b);
            VertexP3fT2fC4b[] finVertices = game.ModelCache.vertices;

            for (int i = 0; i < part.Count; i++)
            {
                ModelVertex v = vertices[part.Offset + i];
                vertex.X = v.X + dispX; vertex.Y = v.Y + dispY; vertex.Z = v.Z + dispZ;
                vertex.Col = cols[i >> 2];

                vertex.U = (v.U & UVMask) * uScale - (v.U >> UVMaxShift) * 0.01f * uScale;
                vertex.V = (v.V & UVMask) * vScale - (v.V >> UVMaxShift) * 0.01f * vScale;
                finVertices[index++] = vertex;
            }
        }

        private void DrawTranslateAndRotate(float dispX, float dispY, float dispZ, float rotX, float rotY, float rotZ, ModelPart part)
        {
            float cosX = (float)Math.Cos(-rotX), sinX = (float)Math.Sin(-rotX);
            float cosY = (float)Math.Cos(-rotY), sinY = (float)Math.Sin(-rotY);
            float cosZ = (float)Math.Cos(-rotZ), sinZ = (float)Math.Sin(-rotZ);

            VertexP3fT2fC4b vertex = default(VertexP3fT2fC4b);
            VertexP3fT2fC4b[] finVertices = game.ModelCache.vertices;

            for (int i = 0; i < part.Count; i++)
            {
                ModelVertex v = vertices[part.Offset + i];

                // Prepare the vertex coordinates for rotation
                v.X -= part.RotX; v.Y -= part.RotY; v.Z -= part.RotZ;
                float t = 0;

                // Rotate locally.
                if (Rotate == RotateOrder.ZYX)
                {
                    t = cosZ * v.X + sinZ * v.Y; v.Y = -sinZ * v.X + cosZ * v.Y; v.X = t; // Inlined RotZ
                    t = cosY * v.X - sinY * v.Z; v.Z = sinY * v.X + cosY * v.Z; v.X = t; // Inlined RotY
                    t = cosX * v.Y + sinX * v.Z; v.Z = -sinX * v.Y + cosX * v.Z; v.Y = t; // Inlined RotX
                }
                else if (Rotate == RotateOrder.XZY)
                {
                    t = cosX * v.Y + sinX * v.Z; v.Z = -sinX * v.Y + cosX * v.Z; v.Y = t; // Inlined RotX
                    t = cosZ * v.X + sinZ * v.Y; v.Y = -sinZ * v.X + cosZ * v.Y; v.X = t; // Inlined RotZ
                    t = cosY * v.X - sinY * v.Z; v.Z = sinY * v.X + cosY * v.Z; v.X = t; // Inlined RotY
                }
                else if (Rotate == RotateOrder.YZX)
                {
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

        protected int xTexIndex;

        protected const int boxesBuilt = 12;

        protected ModelPart bottom, back, tray, front, center, topLeft, topRight, lineLeft, lineRight;

        protected ModelPart left, right, top;
    }
}