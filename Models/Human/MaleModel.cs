using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using ClassicalSharp.GraphicsAPI;
using OpenTK;

namespace MoreModels {
    class MaleModel : IModel {
        public MaleModel(Game game) : base(game) { }

        public override void CreateParts() {
            vertices = new ModelVertex[boxVertices * boxesBuilt];

            BoxDesc headDesc, torsoDesc, leftUpperArmDesc, rightUpperArmDesc, leftLowerArmDesc, rightLowerArmDesc, leftUpperLegDesc, rightUpperLegDesc, leftLowerLegDesc, rightLowerLegDesc;

            headDesc          = MakeBoxBounds(-4, 24, -4, 4, 32, 4) .RotOrigin(0, 24, 0);
            torsoDesc         = MakeBoxBounds(-4, 12, -2, 4, 24, 2) .RotOrigin(0, 12, 0);
            rightUpperArmDesc = MakeBoxBounds(4, 18, -2, 7, 24, 2)  .RotOrigin(6, 22, 0);            
            rightLowerArmDesc = MakeBoxBounds(4, 12, -1, 7, 18, 2)  .RotOrigin(4, 18, 0);            
            rightUpperLegDesc = MakeBoxBounds(0, 6, -2, 4, 12, 2)   .RotOrigin(2, 12, 0);
            rightLowerLegDesc = MakeBoxBounds(1, 0, -2, 4, 6, 2)    .RotOrigin(2, 6, 0);
            leftUpperArmDesc  = MakeBoxBounds(-7, 18, -2, -4, 24, 2).RotOrigin(-6, 22, 0);
            leftLowerArmDesc  = MakeBoxBounds(-7, 12, -1, -4, 18, 2).RotOrigin(-4, 18, 0);
            leftUpperLegDesc  = MakeBoxBounds(-4, 6, -2, 0, 12, 2)  .RotOrigin(-2, 12, 0);
            leftLowerLegDesc  = MakeBoxBounds(-4, 0, -2, -1, 6, 2)   .RotOrigin(-2, 6, 0);

            head          = BuildBox(headDesc         .TexOrigin(0, 0));            
            torso         = BuildBox(torsoDesc        .TexOrigin(0, 32));
            rightUpperArm = BuildBox(rightUpperArmDesc.TexOrigin(32, 0));
            rightLowerArm = BuildBox(rightLowerArmDesc.TexOrigin(32, 10));
            rightUpperLeg = BuildBox(rightUpperLegDesc.TexOrigin(32, 20));
            rightLowerLeg = BuildBox(rightLowerLegDesc.TexOrigin(32, 30));
            leftUpperArm  = BuildBox(leftUpperArmDesc .TexOrigin(48, 0));
            leftLowerArm  = BuildBox(leftLowerArmDesc .TexOrigin(48, 10));    
            leftUpperLeg  = BuildBox(leftUpperLegDesc .TexOrigin(48, 20));            
            leftLowerLeg  = BuildBox(leftLowerLegDesc .TexOrigin(48, 30));

            hat              = BuildBox(headDesc         .TexOrigin(0, 16) .Expand(0.5f));
            jacket           = BuildBox(torsoDesc        .TexOrigin(0, 48) .Expand(0.5f));
            rightUpperSleeve = BuildBox(rightUpperArmDesc.TexOrigin(64, 0) .Expand(0.5f));
            rightLowerSleeve = BuildBox(rightLowerArmDesc.TexOrigin(64, 10).Expand(0.5f));
            rightUpperPant   = BuildBox(rightUpperLegDesc.TexOrigin(64, 20).Expand(0.5f));
            rightLowerPant   = BuildBox(rightLowerLegDesc.TexOrigin(64, 30).Expand(0.5f));
            leftUpperSleeve  = BuildBox(leftUpperArmDesc .TexOrigin(80, 0) .Expand(0.5f));
            leftLowerSleeve  = BuildBox(leftLowerArmDesc .TexOrigin(80, 10).Expand(0.5f));
            leftUpperPant    = BuildBox(leftUpperLegDesc .TexOrigin(80, 20).Expand(0.5f));
            leftLowerPant    = BuildBox(leftLowerLegDesc .TexOrigin(80, 30).Expand(0.5f));

            cape = BuildBox(MakeBoxBounds(-6, 1, 2, 6, 24, 3).TexOrigin(24, 40).RotOrigin(0, 23, 3));
        }

        public override float NameYOffset { get { return 2.075f; } }

        public override float GetEyeY(Entity entity) { return 1.625f; }

        public override Vector3 CollisionSize { get { return new Vector3(8.6f / 16f, 28.1f / 16f, 8.6f / 16f); } }

        public override AABB PickingBounds { get { return new AABB(-0.25f, 0f, -0.25f, 0.25f, 2f, 0.25f); } }

        public override void DrawModel(Entity p) {
            Rotate = RotateOrder.XZY;

            float lowerRightLegRot = ((float)Math.Cos(p.anim.walkTime + (float)Math.PI / 2f) + 1f) / 2f * -p.anim.swing * (float)Math.PI / 2f + p.anim.rightLegX;
            float lowerLeftLegRot = ((float)Math.Cos(p.anim.walkTime - (float)Math.PI / 2f) + 1f) / 2f * -p.anim.swing * (float)Math.PI / 2f + p.anim.leftLegX;

            float lowerLeftArmRot = p.anim.swing * (float)Math.PI / 3f + p.anim.leftArmX;
            float lowerRightArmRot = p.anim.swing * (float)Math.PI / 3f + p.anim.rightArmX;

            float breath = 0.25f * p.anim.rightArmZ * (1f - p.anim.swing) + 1f;
            float breathDisp = 0.75f * (breath - 1f);

            float lowerLeftArmX = 0.25f * (float)Math.Sin(p.anim.leftArmZ);
            float lowerLeftArmY = 0.25f - 0.25f * (float)Math.Cos(p.anim.leftArmX) + breathDisp;
            float lowerLeftArmZ = 0.25f * (float)Math.Sin(-p.anim.leftArmX) - 0.5f / 16f;
            float lowerRightArmX = 0.25f * (float)Math.Sin(p.anim.rightArmZ);
            float lowerRightArmY = 0.25f - 0.25f * (float)Math.Cos(p.anim.rightArmX) + breathDisp;
            float lowerRightArmZ = 0.25f * (float)Math.Sin(-p.anim.rightArmX) - 0.5f / 16f;
            float lowerLeftLegY = 0.375f - 0.375f * (float)Math.Cos(p.anim.leftLegX);
            float lowerLeftLegZ = 0.375f * (float)Math.Sin(-p.anim.leftLegX);
            float lowerRightLegY = 0.375f - 0.375f * (float)Math.Cos(p.anim.rightLegX);
            float lowerRightLegZ = 0.375f * (float)Math.Sin(-p.anim.rightLegX);

            ApplyTexture(p);
            uScale = 1f / 128f;
            vScale = 1f / 64f;
            game.Graphics.AlphaTest = false;

            DrawRotate(p.anim.leftLegX, 0f, p.anim.leftLegZ, leftUpperLeg, false);
            DrawRotate(p.anim.rightLegX, 0f, p.anim.rightLegZ, rightUpperLeg, false);
            UpdateVB();

            TransformAt(p, 0f, 1.5f, 0f, 0f, breathDisp, 0f, 0.9f, 0.9f, 0.9f);
            DrawRotate(-p.HeadXRadians, 0f, 0f, head, true);
            UpdateVB();

            Transform(p, 0f, breathDisp, 0f, 1f, 1f, 1f);
            DrawRotate(p.anim.leftArmX, 0f, p.anim.leftArmZ, leftUpperArm, false);
            DrawRotate(p.anim.rightArmX, 0f, p.anim.rightArmZ, rightUpperArm, false);
            UpdateVB();

            TransformAt(p, 0f, 0.75f, 0f, 0f, 0f, 0f, breath, breath, breath);
            DrawPart(torso);
            UpdateVB();

            Transform(p, lowerLeftArmX, lowerLeftArmY, lowerLeftArmZ, 1f, 1f, 1f);
            DrawRotate(lowerLeftArmRot, 0f, 0f, leftLowerArm, false);
            UpdateVB();

            Transform(p, lowerRightArmX, lowerRightArmY, lowerRightArmZ, 1f, 1f, 1f);
            DrawRotate(lowerRightArmRot, 0f, 0f, rightLowerArm, false);
            UpdateVB();

            Transform(p, 0.5f / 16f, lowerLeftLegY, lowerLeftLegZ, 1f, 1f, 1f);
            DrawRotate(lowerLeftLegRot, 0f, 0f, leftLowerLeg, false);
            UpdateVB();

            Transform(p, -0.5f / 16f, lowerRightLegY, lowerRightLegZ, 1f, 1f, 1f);
            DrawRotate(lowerRightLegRot, 0f, 0f, rightLowerLeg, false);
            UpdateVB();

            game.Graphics.AlphaTest = true;
            ResetTransform(p);

            DrawRotate(p.anim.leftLegX, 0f, p.anim.leftLegZ, leftUpperPant, false);
            DrawRotate(p.anim.rightLegX, 0f, p.anim.rightLegZ, rightUpperPant, false);
            DrawRotate((float)Math.PI * p.anim.swing / -3f, 0f, 0f, cape, false);
            UpdateVB();

            TransformAt(p, 0f, 1.5f, 0f, 0f, breathDisp, 0f, 0.9f, 0.9f, 0.9f);
            DrawRotate(-p.HeadXRadians, 0f, 0f, hat, true);
            UpdateVB();

            Transform(p, 0f, breathDisp, 0f, 1f, 1f, 1f);
            DrawRotate(p.anim.leftArmX, 0f, p.anim.leftArmZ, leftUpperSleeve, false);
            DrawRotate(p.anim.rightArmX, 0f, p.anim.rightArmZ, rightUpperSleeve, false);
            UpdateVB();

            TransformAt(p, 0f, 0.75f, 0f, 0f, 0f, 0f, breath, breath, breath);
            DrawPart(jacket);
            UpdateVB();

            Transform(p, lowerLeftArmX, lowerLeftArmY, lowerLeftArmZ, 1f, 1f, 1f);
            DrawRotate(lowerLeftArmRot, 0f, 0f, leftLowerSleeve, false);
            UpdateVB();

            Transform(p, lowerRightArmX, lowerRightArmY, lowerRightArmZ, 1f, 1f, 1f);
            DrawRotate(lowerRightArmRot, 0f, 0f, rightLowerSleeve, false);
            UpdateVB();

            Transform(p, 0f, lowerLeftLegY, lowerLeftLegZ, 1f, 1f, 1f);
            DrawRotate(lowerLeftLegRot, 0f, 0f, leftLowerPant, false);
            UpdateVB();

            Transform(p, 0f, lowerRightLegY, lowerRightLegZ, 1f, 1f, 1f);
            DrawRotate(lowerRightLegRot, 0f, 0f, rightLowerPant, false);
            UpdateVB();
        }

        public override void DrawArm(Entity p) {
            uScale = 1f / 128f;
            vScale = 1f / 64f;
            DrawArmPart(rightLowerArm);
            DrawArmPart(rightLowerSleeve);
            UpdateVB();
        }

        protected void Transform(Entity p, float dispX, float dispY, float dispZ, float scaleX, float scaleY, float scaleZ) {
            Vector3 pos = p.Position;
            if (Bobbing) pos.Y += p.anim.bobbingModel;

            Matrix4 matrix = TransformMatrix(p, pos), temp;
            Matrix4.Mult(out matrix, ref matrix, ref game.Graphics.View);
            Matrix4.Scale(out temp, scaleX, scaleY, scaleZ);
            Matrix4.Mult(out matrix, ref temp, ref matrix);
            Matrix4.Translate(out temp, dispX, dispY, dispZ);
            Matrix4.Mult(out matrix, ref temp, ref matrix);

            game.Graphics.LoadMatrix(ref matrix);
        }

        protected void TransformAt(Entity p, float x, float y, float z, float dispX, float dispY, float dispZ, float scaleX, float scaleY, float scaleZ) {
            Vector3 pos = p.Position;
            if (Bobbing) pos.Y += p.anim.bobbingModel;

            Matrix4 matrix = TransformMatrix(p, pos), temp;
            Matrix4.Mult(out matrix, ref matrix, ref game.Graphics.View);
            Matrix4.Translate(out temp, x, y, z);
            Matrix4.Mult(out matrix, ref temp, ref matrix);
            Matrix4.Scale(out temp, scaleX, scaleY, scaleZ);
            Matrix4.Mult(out matrix, ref temp, ref matrix);
            Matrix4.Translate(out temp, dispX, dispY, dispZ);
            Matrix4.Mult(out matrix, ref temp, ref matrix);
            Matrix4.Translate(out temp, -x, -y, -z);
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

        private ModelPart head, hat, torso, leftUpperArm, rightUpperArm, leftLowerArm, rightLowerArm, leftUpperLeg, rightUpperLeg, leftLowerLeg, rightLowerLeg;
        private ModelPart jacket, leftUpperSleeve, rightUpperSleeve, leftLowerSleeve, rightLowerSleeve, leftUpperPant, rightUpperPant, leftLowerPant, rightLowerPant, cape;

        private const int boxesBuilt = 21;
    }
}