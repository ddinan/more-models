using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
    public class HeadlessModel : HumanoidModel {
		public HeadlessModel(Game game) : base(game) { }

        public override void CreateParts() {
            HumanoidModel humanoid = (HumanoidModel)game.ModelCache.Models[0].Instance;
            vertices = humanoid.vertices;
            Set = humanoid.Set;
            Set64 = humanoid.Set64;
            SetSlim = humanoid.SetSlim;
        }

        public override float NameYOffset{ get { return 1.625f; } }

        public override Vector3 CollisionSize { get { return new Vector3(8.6f / 16f, 23f / 16f, 8.6f / 16f); } }

        public override AABB PickingBounds { get { return new AABB(-0.5f, 0f, -0.25f, 0.5f, 1.5f, 0.25f); } }

        public override void DrawModel(Entity p) {
            SkinType skinType = p.SkinType;
            ModelSet model = skinType == SkinType.Type64x64Slim ? SetSlim : (skinType == SkinType.Type64x64 ? Set64 : Set);

            ApplyTexture(p);
            game.Graphics.AlphaTest = false;

            DrawPart(model.Torso);

            Rotate = RotateOrder.ZYX;

            DrawRotate(p.anim.leftLegX, 0f, p.anim.leftLegZ, model.LeftLeg, false);
            DrawRotate(p.anim.rightLegX, 0f, p.anim.rightLegZ, model.RightLeg, false);

            Rotate = RotateOrder.XZY;

            DrawRotate(p.anim.leftArmX, 0f, p.anim.leftArmZ, model.LeftArm, false);
            DrawRotate(p.anim.rightArmX, 0f, p.anim.rightArmZ, model.RightArm, false);

            UpdateVB();
            index = 0;
            game.Graphics.AlphaTest = true;

            if (skinType != SkinType.Type64x32) {
                DrawPart(model.TorsoLayer);

                Rotate = RotateOrder.ZYX;

                DrawRotate(p.anim.leftLegX, 0f, p.anim.leftLegZ, model.LeftLegLayer, false);
                DrawRotate(p.anim.rightLegX, 0f, p.anim.rightLegZ, model.RightLegLayer, false);

                Rotate = RotateOrder.XZY;

                DrawRotate(p.anim.leftArmX, 0f, p.anim.leftArmZ, model.LeftArmLayer, false);
                DrawRotate(p.anim.rightArmX, 0f, p.anim.rightArmZ, model.RightArmLayer, false);
            }

            UpdateVB();
        }
    }
}
