// This model should be used on servers that support entity rotation (/entrot) to get the full effect.
// /entrot [username] x 90
// /entrot [bot] [bot name] x 90

using System;
using ClassicalSharp.Entities;

namespace ClassicalSharp.Model {

    class FlyingModel : HumanoidModel
    {
        public FlyingModel(Game game) : base(game)
        {
            Bobbing = false;
            Gravity = 0.01f;
        }

        protected override void MakeDescriptions()
        {
            head = MakeBoxBounds(-4, 24, -4, 4, 32, 4).RotOrigin(0, 24, 0);
            torso = MakeBoxBounds(-4, 12, -2, 4, 24, 2).RotOrigin(0, 24, 0);
            lLeg = MakeBoxBounds(-4, 0, -2, 0, 12, 2).RotOrigin(0, 24, 0);
            rLeg = MakeBoxBounds(0, 0, -2, 4, 12, 2).RotOrigin(0, 24, 0);
            lArm = MakeBoxBounds(-8, 12, -2, -4, 24, 2).RotOrigin(-5, 22, 0);
            rArm = MakeBoxBounds(4, 12, -2, 8, 24, 2).RotOrigin(5, 22, 0);

        }

        /*public override void CreateParts()
        {
            vertices = new ModelVertex[boxVertices * numParts];

            head     = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4) .TexOrigin(0, 0)  .RotOrigin(0, 24, 0));
            torso    = BuildBox(MakeBoxBounds(-4, 12, -2, 4, 24, 2) .TexOrigin(16, 16).RotOrigin(0, 24, 0));
            leftLeg  = BuildBox(MakeBoxBounds(0, 0, -2, -4, 12, 2)  .TexOrigin(0, 16) .RotOrigin(0, 24, 0));
            rightLeg = BuildBox(MakeBoxBounds(0, 0, -2, 4, 12, 2)   .TexOrigin(0, 16) .RotOrigin(0, 24, 0));
            leftArm  = BuildBox(MakeBoxBounds(-4, 12, -2, -8, 24, 2).TexOrigin(40, 16).RotOrigin(-6, 22, 0));
            rightArm = BuildBox(MakeBoxBounds(4, 12, -2, 8, 24, 2)  .TexOrigin(40, 16).RotOrigin(6, 22, 0));

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
        */
        public override void DrawModel(Entity p)
        {
            ModelSet model = p.SkinType == SkinType.Type64x64Slim ? SetSlim : (p.SkinType == SkinType.Type64x64 ? Set64 : Set);

            float flyRot = p.anim.swing * (float)Math.PI / 2.0f;
            float legRot = (float)(Math.Cos(p.anim.walkTime / 4f) * Math.Cos(p.anim.walkTime / 4f) * p.anim.swing * Math.PI / 16.0f);
            float armRot = (float)(Math.Sin(p.anim.walkTime * 2f) * p.anim.swing * Math.PI / 32f);

            Rotate = RotateOrder.XZY;

            game.Graphics.BindTexture(GetTexture(p));
            game.Graphics.AlphaTest = false;

            DrawRotate(-p.HeadXRadians, 0f, 0f, model.Head, true);
            
            DrawRotate(-flyRot, 0f, 0f, model.Torso, false);
            DrawRotate(-flyRot, -legRot / 8f, p.anim.leftArmZ / 4f + p.anim.swing / 32f, model.LeftLeg, false);
            DrawRotate(-flyRot, legRot / 8f, p.anim.rightArmZ / 4f - p.anim.swing / 32f, model.RightLeg, false);
            DrawRotate(-flyRot, armRot, p.anim.leftArmZ, model.LeftArm, false);
            DrawRotate(flyRot, armRot, p.anim.rightArmZ, model.RightArm, false);

            UpdateVB();

            game.Graphics.AlphaTest = true;
            index = 0;

            if (p.SkinType != SkinType.Type64x32)
            {
                DrawRotate(-flyRot, 0f, 0f, model.TorsoLayer, false);
                DrawRotate(-flyRot, -legRot / 8f, p.anim.leftArmZ / 4f + p.anim.swing / 32f, model.LeftLegLayer, false);
                DrawRotate(-flyRot, -legRot / 8f, p.anim.leftArmZ / 4f + p.anim.swing / 32f, model.RightLegLayer, false);
                DrawRotate(-flyRot, armRot, p.anim.leftArmZ, model.LeftArmLayer, false);
                DrawRotate(flyRot, armRot, p.anim.rightArmZ, model.RightArmLayer, false);
            }
            DrawRotate(-p.HeadXRadians, 0f, 0f, model.Hat, true);

            UpdateVB();
        }

        //private ModelPart head, torso, leftArm, rightArm, leftLeg, rightLeg, hat;

        //private const int numParts = 7;
    }
}