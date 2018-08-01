using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
    public class HeadlessModel : HumanoidModel {
		public HeadlessModel(Game game) : base(game) { }

        public override void CreateParts()
        {
            base.CreateParts();
        }

        public override float NameYOffset{ get { return 1.625f; } }

        public override Vector3 CollisionSize
        {
            get { return new Vector3(8 / 16f + 0.6f / 16f, 23f / 16f, 8 / 16f + 0.6f / 16f); }
        }

        public override AABB PickingBounds
        {
            get { return new AABB(-8 / 16f, 0, -4 / 16f, 8 / 16f, 24 / 16f, 4 / 16f); }
        }

        protected override void RenderParts(Entity p)
        {
            SkinType skinType = p.SkinType;
            ModelSet model = skinType == SkinType.Type64x64Slim ? SetSlim : (skinType == SkinType.Type64x64 ? Set64 : Set);

            DrawPart(model.Torso);

            Rotate = RotateOrder.ZYX;

            DrawRotate(p.anim.leftLegX, 0, p.anim.leftLegZ, model.LeftLeg, false);
            DrawRotate(p.anim.rightLegX, 0, p.anim.rightLegZ, model.RightLeg, false);

            Rotate = RotateOrder.XZY;

            DrawRotate(p.anim.leftArmX, 0, p.anim.leftArmZ, model.LeftArm, false);
            DrawRotate(p.anim.rightArmX, 0, p.anim.rightArmZ, model.RightArm, false);

            UpdateVB();
            index = 0;

            game.Graphics.AlphaTest = true;

            if (skinType != SkinType.Type64x32)
            {
                DrawPart(model.TorsoLayer);

                Rotate = RotateOrder.ZYX;

                DrawRotate(p.anim.leftLegX, 0, p.anim.leftLegZ, model.LeftLegLayer, false);
                DrawRotate(p.anim.rightLegX, 0, p.anim.rightLegZ, model.RightLegLayer, false);

                Rotate = RotateOrder.XZY;

                DrawRotate(p.anim.leftArmX, 0, p.anim.leftArmZ, model.LeftArmLayer, false);
                DrawRotate(p.anim.rightArmX, 0, p.anim.rightArmZ, model.RightArmLayer, false);
            }

            UpdateVB();
        }
    }
}
