using ClassicalSharp.Entities;
using ClassicalSharp.GraphicsAPI;
using ClassicalSharp.Physics;
using OpenTK;

namespace ClassicalSharp.Model
{
    class CarModel : IModel 
    {
        public CarModel(Game game) : base(game)
        {
            Bobbing = false;
            GroundFriction = new Vector3(1.05f, 1.05f, 1.05f);
        }

        public override float GetEyeY(Entity e) { return 28f / 16f; }

        public override Vector3 CollisionSize { get { return new Vector3(2.9f, 1.9f, 2.9f); } }

        public override float NameYOffset { get { return 2.375f; } }

        public override AABB PickingBounds { get { return new AABB(-1.5f, 0f, -1.5f, 1.5f, 2f, 1.5f); } }

        public override void CreateParts() {
            vertices = new ModelVertex[boxVertices * boxesBuilt];

            top        = BuildRotatedBox(MakeRotatedBoxBounds(-4, 16 + 4, -4, 4, 20 + 4, 4).TexOrigin(0, 0) .RotOrigin(0, 16 + 4, 0));
            body       = BuildRotatedBox(MakeRotatedBoxBounds(-4, 0 + 4, -8, 4, 4 + 4, 8)  .TexOrigin(24, 0).RotOrigin(0, 0 + 4, 0));
                
            frontLeft  = BuildBox(MakeBoxBounds(-16 - 2, 2, -22, -12 - 2, 10, -14).TexOrigin(0, 12).RotOrigin(-16, 6, -18).Expand(2f));
            frontRight = BuildBox(MakeBoxBounds(12 + 2, 2, -22, 16 + 2, 10, -14)  .TexOrigin(0, 12).RotOrigin(16, 6, -18) .Expand(2f));
            backLeft   = BuildBox(MakeBoxBounds(-16 - 2, 2, 14, -12 - 2, 10, 22)  .TexOrigin(0, 12).RotOrigin(-16, 6, 18) .Expand(2f));
            backRight  = BuildBox(MakeBoxBounds(12 + 2, 2, 14, 16 + 2, 10, 22)    .TexOrigin(0, 12).RotOrigin(16, 6, 18)  .Expand(2f));
        }

        public override void DrawModel(Entity p)
        {
            game.Graphics.BindTexture(GetTexture(p));

            DrawScale(4f, top);
            DrawScale(4f, body);

            DrawRotate(-p.anim.walkTime, 0f, 0f, frontLeft, false);
            DrawRotate(-p.anim.walkTime, 0f, 0f, frontRight, false);
            DrawRotate(-p.anim.walkTime, 0f, 0f, backLeft, false);
            DrawRotate(-p.anim.walkTime, 0f, 0f, backRight, false);

            UpdateVB();
        }

        protected void DrawScale(float scale, ModelPart part)
        {
            VertexP3fT2fC4b vertex = default(VertexP3fT2fC4b);
            VertexP3fT2fC4b[] finVertices = game.ModelCache.vertices;

            for (int i = 0; i < part.Count; i++)
            {
                ModelVertex v = vertices[part.Offset + i];
                vertex.X = (v.X - part.RotX) * scale + part.RotX; vertex.Y = (v.Y - part.RotY) * scale + part.RotY; vertex.Z = (v.Z - part.RotZ) * scale + part.RotZ;
                vertex.Colour = cols[i >> 2];

                vertex.U = (v.U & UVMask) * uScale - (v.U >> UVMaxShift) * 0.01f * uScale;
                vertex.V = (v.V & UVMask) * vScale - (v.V >> UVMaxShift) * 0.01f * vScale;
                finVertices[index++] = vertex;
            }
        }
        private ModelPart body, top, frontLeft, frontRight, backLeft, backRight;

        private const int boxesBuilt = 6;
    }
}
