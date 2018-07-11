using System;
using ClassicalSharp.Entities;
using ClassicalSharp.Physics;
using OpenTK;

namespace ClassicalSharp.Model
{
    class HoldingModel : IModel
    {
        public HoldingModel(Game game) : base(game) { UsesHumanSkin = true; }

        public override void CreateParts()
        {
            vertices = new ModelVertex[boxVertices * 7];
            Head = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4)     .TexOrigin(0, 0)    .RotOrigin(0, 24, 0));
            Hat = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4)      .TexOrigin(32, 0)   .RotOrigin(0, 24, 0).Expand(0.5f));
            Torso = BuildBox(MakeBoxBounds(-4, 12, -2, 4, 24, 2)    .TexOrigin(16, 16));
            LeftLeg = BuildBox(MakeBoxBounds(0, 0, -2, -4, 12, 2)   .TexOrigin(0, 16)   .RotOrigin(-2, 12, 0));
            RightLeg = BuildBox(MakeBoxBounds(0, 0, -2, 4, 12, 2)   .TexOrigin(0, 16)   .RotOrigin(2, 12, 0));
            LeftArm = BuildBox(MakeBoxBounds(-4, 12, -2, -8, 24, 2) .TexOrigin(40, 16)  .RotOrigin(-4, 22, 0));
            RightArm = BuildBox(MakeBoxBounds(4, 12, -2, 8, 24, 2)  .TexOrigin(40, 16)  .RotOrigin(4, 22, 0));
        }

        public override float NameYOffset { get { return 2.075f; } }

        public override float GetEyeY(Entity entity) { return 26 / 16f; }

        public override Vector3 CollisionSize
        {
            get { return new Vector3(8 / 16f + 0.6f / 16f, 28.1f / 16f, 8 / 16f + 0.6f / 16f); }
        }

        public override AABB PickingBounds
        {
            get { return new AABB(-4 / 16f, 0, -4 / 16f, 4 / 16f, 32 / 16f, 4 / 16f); }
        }

        public override void DrawModel(Entity p)
        {   
            Rotate = RotateOrder.XZY;

            float handBob = (float)Math.Sin(p.anim.walkTime * 2f) * p.anim.swing * (float)Math.PI / 16f;
            float handIdle = p.anim.rightArmX * (1f - p.anim.swing);

            game.Graphics.BindTexture(GetTexture(p));
            DrawRotate(-p.HeadXRadians, 0f, 0f, Head, true);
            DrawPart(Torso);
            DrawRotate(p.anim.leftLegX, 0f, p.anim.leftLegZ, LeftLeg, false);
            DrawRotate(p.anim.rightLegX, 0f, p.anim.rightLegZ, RightLeg, false);
            DrawRotate((float)Math.PI / 3f + handBob + handIdle, (handBob + handIdle) * -2f / 3f, (float)Math.PI / 8f, LeftArm, false);
            DrawRotate((float)Math.PI / 3f + handBob + handIdle, (handBob + handIdle) * 2f / 3f, (float)Math.PI / -8f, RightArm, false);
            DrawRotate(-p.HeadXRadians, 0f, 0f, Hat, true);
            UpdateVB();
        }
        private ModelPart Head, Torso, LeftArm, RightArm, LeftLeg, RightLeg, Hat;
    }
}