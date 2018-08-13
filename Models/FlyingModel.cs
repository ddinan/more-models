using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.GraphicsAPI;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace MoreModels {
    class FlyingModel : HumanoidModel {
        public FlyingModel(Game game) : base(game) {
            Bobbing = false;
            Gravity = 0.04f;
        }

        public override void CreateParts() {
            HumanoidModel humanoid = (HumanoidModel)game.ModelCache.Models[0].Instance;
            vertices = humanoid.vertices;
            Set = humanoid.Set;
            Set64 = humanoid.Set64;
            SetSlim = humanoid.SetSlim;
        }

        public override float NameYOffset { get { return 32.5f / 16f; } }

        public override float GetEyeY(Entity entity) { return 26f / 16f; }

        public override Vector3 CollisionSize { get { return new Vector3(8.6f / 16f, 28.1f / 16f, 8.6f / 16f); } }

        public override AABB PickingBounds { get { return new AABB(-8f / 16f, 0f, -4f / 16f, 8f / 16f, 32f / 16f, 4f / 16f); } }

        public override void DrawModel(Entity p) {
            ModelSet model = p.SkinType == SkinType.Type64x64Slim ? SetSlim : (p.SkinType == SkinType.Type64x64 ? Set64 : Set);

            float flyRot = p.anim.swing * (float)Math.PI / -2.0f;
            float legRot = (float)(Math.Cos(p.anim.walkTime / 8f) + 1f) * p.anim.swing * (float)Math.PI / 64f;
            float armRot = (float)(Math.Sin(p.anim.walkTime / 8f) + 1f) * p.anim.swing * (float)Math.PI / 32f;
            float headRot;
            if (p.HeadXRadians > (float)Math.PI / 2f && flyRot != 0f) headRot = ((float)Math.PI * 2f - p.HeadXRadians) * (1f - p.anim.swing);
            else headRot = -p.HeadXRadians;

            model.Torso.RotY = 24f / 16f;

            Rotate = RotateOrder.XZY;

            ApplyTexture(p);
            game.Graphics.AlphaTest = false;

            DrawRotate(headRot, 0f, 0f, model.Head, true);
            DrawRotate(flyRot, 0f, 0f, model.Torso, false);
            DrawRotate(flyRot, -armRot, p.anim.leftArmZ * (1f - p.anim.swing), model.LeftArm, false);
            DrawRotate(-flyRot, armRot - (float)Math.PI * 3f / 64f, p.anim.rightArmZ * (1f - p.anim.swing), model.RightArm, false);

            UpdateVB();

            Translate(p, 0f, ((float)Math.Cos(flyRot) - 1f) * -12f / 16f, (float)Math.Sin(flyRot) * -12f / 16f);
            DrawRotate(flyRot, -legRot, p.anim.leftArmZ * (1f - p.anim.swing), model.LeftLeg, false);
            DrawRotate(flyRot, legRot, p.anim.rightArmZ * (1f - p.anim.swing), model.RightLeg, false);

            UpdateVB();
            game.Graphics.AlphaTest = true;

            if (p.SkinType != SkinType.Type64x32) {
                model.TorsoLayer.RotY = 24f / 16f;

                ResetTransform(p);

                DrawRotate(flyRot, 0f, 0f, model.TorsoLayer, false);
                DrawRotate(flyRot, -armRot, p.anim.leftArmZ * (1f - p.anim.swing), model.LeftArmLayer, false);
                DrawRotate(-flyRot, armRot - (float)Math.PI * 3f / 64f, p.anim.rightArmZ * (1f - p.anim.swing), model.RightArmLayer, false);

                UpdateVB();

                Translate(p, 0f, ((float)Math.Cos(flyRot) - 1f) * -12f / 16f, (float)Math.Sin(flyRot) * -12f / 16f);
                DrawRotate(flyRot, -legRot, p.anim.leftArmZ * (1f - p.anim.swing), model.LeftLegLayer, false);
                DrawRotate(flyRot, legRot, p.anim.rightArmZ * (1f - p.anim.swing), model.RightLegLayer, false);

                UpdateVB();

                model.TorsoLayer.RotY = 0f;
            }

            ResetTransform(p);

            DrawRotate(headRot, 0f, 0f, model.Hat, true);

            UpdateVB();

            model.Torso.RotY = 0f;
        }

        private void Translate(Entity p, float dispX, float dispY, float dispZ) {
            Vector3 pos = p.Position;
            if (Bobbing) pos.Y += p.anim.bobbingModel;

            Matrix4 matrix = TransformMatrix(p, pos), temp;
            Matrix4.Mult(out matrix, ref matrix, ref game.Graphics.View);
            Matrix4.Translate(out temp, dispX, dispY, dispZ);
            Matrix4.Mult(out matrix, ref temp, ref matrix);

            game.Graphics.LoadMatrix(ref matrix);
        }

        protected void ResetTransform(Entity p) {
            Vector3 pos = p.Position;
            if (Bobbing) pos.Y += p.anim.bobbingModel;

            Matrix4 matrix = TransformMatrix(p, pos);
            Matrix4.Mult(out matrix, ref matrix, ref game.Graphics.View);

            game.Graphics.LoadMatrix(ref matrix);
        }
    }
}