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

            DrawTranslateAndRotate(0f, (float)Math.Cos(flyRot) * -12f / 16f + 12f / 16f, (float)Math.Sin(flyRot) * -12f / 16f, flyRot, -legRot, p.anim.leftArmZ * (1f - p.anim.swing), model.LeftLeg);
            DrawTranslateAndRotate(0f, (float)Math.Cos(flyRot) * -12f / 16f + 12f / 16f, (float)Math.Sin(flyRot) * -12f / 16f, flyRot, legRot, p.anim.rightArmZ * (1f - p.anim.swing), model.RightLeg);

            UpdateVB();
            index = 0;
            game.Graphics.AlphaTest = true;

            if (p.SkinType != SkinType.Type64x32) {
                model.TorsoLayer.RotY = 24f / 16f;
                DrawRotate(flyRot, 0f, 0f, model.TorsoLayer, false);
                DrawTranslateAndRotate(0f, (float)Math.Cos(flyRot) * -12f / 16f + 12f / 16f, (float)Math.Sin(flyRot) * -12f / 16f, flyRot, -legRot, p.anim.leftArmZ * (1f - p.anim.swing), model.LeftLegLayer);
                DrawTranslateAndRotate(0f, (float)Math.Cos(flyRot) * -12f / 16f + 12f / 16f, (float)Math.Sin(flyRot) * -12f / 16f, flyRot, legRot, p.anim.rightArmZ * (1f - p.anim.swing), model.RightLegLayer);
                DrawRotate(flyRot, -armRot, p.anim.leftArmZ * (1f - p.anim.swing), model.LeftArmLayer, false);
                DrawRotate(-flyRot, armRot - (float)Math.PI * 3f / 64f, p.anim.rightArmZ * (1f - p.anim.swing), model.RightArmLayer, false);
                model.TorsoLayer.RotY = 0f;
            }
            DrawRotate(headRot, 0f, 0f, model.Hat, true);

            UpdateVB();

            model.Torso.RotY = 0f;
        }

        private void DrawTranslateAndRotate(float dispX, float dispY, float dispZ, float rotX, float rotY, float rotZ, ModelPart part) {
            float cosX = (float)Math.Cos(-rotX), sinX = (float)Math.Sin(-rotX);
            float cosY = (float)Math.Cos(-rotY), sinY = (float)Math.Sin(-rotY);
            float cosZ = (float)Math.Cos(-rotZ), sinZ = (float)Math.Sin(-rotZ);

            VertexP3fT2fC4b vertex = default(VertexP3fT2fC4b);
            VertexP3fT2fC4b[] finVertices = game.ModelCache.vertices;

            for (int i = 0; i < part.Count; i++) {
                ModelVertex v = vertices[part.Offset + i];

                // Prepare the vertex coordinates for rotation
                v.X -= part.RotX; v.Y -= part.RotY; v.Z -= part.RotZ;
                float t = 0f;

                // Rotate locally.
                if (Rotate == RotateOrder.ZYX) {
                    t = cosZ * v.X + sinZ * v.Y; v.Y = -sinZ * v.X + cosZ * v.Y; v.X = t; // Inlined RotZ
                    t = cosY * v.X - sinY * v.Z; v.Z = sinY * v.X + cosY * v.Z; v.X = t; // Inlined RotY
                    t = cosX * v.Y + sinX * v.Z; v.Z = -sinX * v.Y + cosX * v.Z; v.Y = t; // Inlined RotX
                }
                else if (Rotate == RotateOrder.XZY) {
                    t = cosX * v.Y + sinX * v.Z; v.Z = -sinX * v.Y + cosX * v.Z; v.Y = t; // Inlined RotX
                    t = cosZ * v.X + sinZ * v.Y; v.Y = -sinZ * v.X + cosZ * v.Y; v.X = t; // Inlined RotZ
                    t = cosY * v.X - sinY * v.Z; v.Z = sinY * v.X + cosY * v.Z; v.X = t; // Inlined RotY
                }
                else if (Rotate == RotateOrder.YZX) {
                    t = cosY * v.X - sinY * v.Z; v.Z = sinY * v.X + cosY * v.Z; v.X = t; // Inlined RotY
                    t = cosZ * v.X + sinZ * v.Y; v.Y = -sinZ * v.X + cosZ * v.Y; v.X = t; // Inlined RotZ
                    t = cosX * v.Y + sinX * v.Z; v.Z = -sinX * v.Y + cosX * v.Z; v.Y = t; // Inlined RotX
                }

                vertex.X = v.X + part.RotX; vertex.Y = v.Y + part.RotY; vertex.Z = v.Z + part.RotZ;
                // Translate part
                vertex.X += dispX; vertex.Y += dispY; vertex.Z += dispZ;

                vertex.Col = cols[i >> 2];

                vertex.U = (v.U & UVMask) * uScale - (v.U >> UVMaxShift) * 0.01f * uScale;
                vertex.V = (v.V & UVMask) * vScale - (v.V >> UVMaxShift) * 0.01f * vScale;
                finVertices[index++] = vertex;
            }
        }
    }
}