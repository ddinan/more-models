using System;
using ClassicalSharp.Entities;

namespace ClassicalSharp.Model {

    class FlyingModel : HumanoidModel
    {
        public FlyingModel(Game game) : base(game)
        {
            Bobbing = false;
            Gravity = 0.04f;
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
                DrawRotate(-flyRot, legRot / 8f, p.anim.rightArmZ / 4f + p.anim.swing / 32f, model.RightLegLayer, false);
                DrawRotate(-flyRot, armRot, p.anim.leftArmZ, model.LeftArmLayer, false);
                DrawRotate(flyRot, armRot, p.anim.rightArmZ, model.RightArmLayer, false);
            }
            DrawRotate(-p.HeadXRadians, 0f, 0f, model.Hat, true);

            UpdateVB();
        }
    }
}