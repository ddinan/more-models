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
            vertices = new ModelVertex[boxVertices * numParts];

            torso = BuildBox(MakeBoxBounds(-4, 12, -2, 4, 24, 2).TexOrigin(16, 16));

            head     = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4) .TexOrigin(0, 0)  .RotOrigin(0, 24, 0));
            leftLeg  = BuildBox(MakeBoxBounds(0, 0, -2, -4, 12, 2)  .TexOrigin(0, 16) .RotOrigin(-2, 12, 0));
            rightLeg = BuildBox(MakeBoxBounds(0, 0, -2, 4, 12, 2)   .TexOrigin(0, 16) .RotOrigin(2, 12, 0));
            leftArm  = BuildBox(MakeBoxBounds(-4, 12, -2, -8, 24, 2).TexOrigin(40, 16).RotOrigin(-4, 22, 0));
            rightArm = BuildBox(MakeBoxBounds(4, 12, -2, 8, 24, 2)  .TexOrigin(40, 16).RotOrigin(4, 22, 0));

            hat = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4).TexOrigin(32, 0).RotOrigin(0, 24, 0).Expand(0.5f));
        }

        public override float NameYOffset { get { return 2.075f; } }

        public override float GetEyeY(Entity entity) { return 26f / 16f; }

        public override Vector3 CollisionSize
        {
            get { return new Vector3(8f / 16f + 0.6f / 16f, 28.1f / 16f, 8f / 16f + 0.6f / 16f); }
        }

        public override AABB PickingBounds
        {
            get { return new AABB(-4f / 16f, 0f, -4f / 16f, 4f / 16f, 32f / 16f, 4f / 16f); }
        }

        public override void DrawModel(Entity p)
        {   
            Rotate = RotateOrder.XZY;

            float handBob = (float)Math.Sin(p.anim.walkTime * 2f) * p.anim.swing * (float)Math.PI / 16f;
            float handIdle = p.anim.rightArmX * (1f - p.anim.swing);

            game.Graphics.BindTexture(GetTexture(p));

            DrawPart(torso);

            DrawRotate(-p.HeadXRadians, 0f, 0f, head, true);
            DrawRotate(-p.HeadXRadians, 0f, 0f, hat, true);

            DrawRotate(p.anim.leftLegX, 0f, p.anim.leftLegZ, leftLeg, false);
            DrawRotate(p.anim.rightLegX, 0f, p.anim.rightLegZ, rightLeg, false);
            DrawRotate((float)Math.PI / 3f + handBob + handIdle, (handBob + handIdle) * -2f / 3f, (float)Math.PI / 8f, leftArm, false);
            DrawRotate((float)Math.PI / 3f + handBob + handIdle, (handBob + handIdle) * 2f / 3f, (float)Math.PI / -8f, rightArm, false);

            UpdateVB();
        }
        private ModelPart head, torso, leftArm, rightArm, leftLeg, rightLeg, hat;

        private const int numParts = 7;
    }
}