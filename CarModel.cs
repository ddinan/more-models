using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.GraphicsAPI;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels
{
    class CarModel : IModel 
    {
        public CarModel(Game game) : base(game)
        {
            Bobbing = false;
            GroundFriction = new Vector3(1.05f, 1.05f, 1.05f);
        }

        public override float GetEyeY(Entity e) { return 28f / 16f; }

        public override Vector3 CollisionSize { get { return new Vector3(4.75f, 2.125f, 4.75f); } }

        public override float NameYOffset { get { return 2.375f; } }

        public override AABB PickingBounds { get { return new AABB(-2.25f, 0f, -2.25f, 2.25f, 2.25f, 2.25f); } }

        public override void CreateParts() {
            vertices = new ModelVertex[boxVertices * boxesBuilt];

            top     = BuildRotatedBox(MakeRotatedBoxBounds(-20, 20, -16, 20, 36, 20).TexOrigin(120, 0));
            body    = BuildRotatedBox(MakeRotatedBoxBounds(-22, 4, -40, 22, 20, 40) .TexOrigin(0, 0));
            spoiler = BuildRotatedBox(MakeRotatedBoxBounds(-19, 27, 33, 19, 30, 42) .TexOrigin(120, 52));

            tireFrontLeft  = BuildBox(MakeBoxBounds(-24, 0, -28, -20, 10, -18) .MirrorX().TexOrigin(156, 64).RotOrigin(-22, 5, -23));
            tireBackLeft   = BuildBox(MakeBoxBounds(-24, 0, 18, -20, 10, 28)   .MirrorX().TexOrigin(156, 64).RotOrigin(-22, 5, 23));
            mirrorLeft     = BuildBox(MakeBoxBounds(-27, 20, -15, -20, 25, -18).MirrorX().TexOrigin(202, 52).RotOrigin(-20, 22, -15));
            spoilerLeft    = BuildBox(MakeBoxBounds(-15, 20, 31, -12, 30, 34)  .MirrorX().TexOrigin(184, 64).RotOrigin(-12, 20, 31));

            tireFrontRight = BuildBox(MakeBoxBounds(20, 0, -28, 24, 10, -18) .TexOrigin(156, 64).RotOrigin(22, 5, -23));
            tireBackRight  = BuildBox(MakeBoxBounds(20, 0, 18, 24, 10, 28)   .TexOrigin(156, 64).RotOrigin(22, 5, 23));
            mirrorRight    = BuildBox(MakeBoxBounds(20, 20, -15, 27, 25, -18).TexOrigin(202, 52).RotOrigin(20, 22, -15));
            spoilerRight   = BuildBox(MakeBoxBounds(12, 20, 31, 15, 30, 34)  .TexOrigin(184, 64).RotOrigin(12, 20, 31));

            frontLeft = BuildBox(MakeBoxBounds(-22, 4, -31, -20, 14, -15).MirrorX().TexOrigin(120, 64));
            backLeft  = BuildBox(MakeBoxBounds(-22, 4, 15, -20, 14, 31)  .MirrorX().TexOrigin(120, 64));

            frontRight = BuildBox(MakeBoxBounds(20, 4, -31, 22, 14, -15).TexOrigin(120, 64));
            backRight  = BuildBox(MakeBoxBounds(20, 4, 15, 22, 14, 31)  .TexOrigin(120, 64));            
        }

        public override void DrawModel(Entity p)
        {
            uScale = 1f / 256f;
            vScale = 1f / 128f;

            game.Graphics.BindTexture(GetTexture(p));

            DrawPart(top);
            DrawPart(body);
            DrawPart(frontLeft);
            DrawPart(frontRight);
            DrawPart(backLeft);
            DrawPart(backRight);
            DrawPart(spoiler);

            DrawRotate(-p.anim.walkTime, 0f, 0f, tireFrontLeft, false);
            DrawRotate(-p.anim.walkTime, 0f, 0f, tireFrontRight, false);
            DrawRotate(-p.anim.walkTime, 0f, 0f, tireBackLeft, false);
            DrawRotate(-p.anim.walkTime, 0f, 0f, tireBackRight, false);
            DrawRotate(0f, (float)Math.PI / 12f, 0f, mirrorLeft, false);
            DrawRotate(0f, (float)Math.PI / -12f, 0f, mirrorRight, false);
            DrawRotate((float)Math.PI / 6f, 0f, 0f, spoilerLeft, false);
            DrawRotate((float)Math.PI / 6f, 0f, 0f, spoilerRight, false);

            UpdateVB();
        }

        private ModelPart body, top, tireFrontLeft, tireFrontRight, tireBackLeft, tireBackRight, frontLeft, frontRight, backLeft, backRight, mirrorLeft, mirrorRight, spoilerLeft, spoilerRight, spoiler;

        private const int boxesBuilt = 15;
    }
}
