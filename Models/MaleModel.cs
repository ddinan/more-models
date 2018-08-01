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
            rightUpperArmDesc = MakeBoxBounds(4, 18, -2, 8, 24, 2)  .RotOrigin(6, 22, 0);            
            rightLowerArmDesc = MakeBoxBounds(4, 12, -2, 8, 18, 2)  .RotOrigin(4, 18, 0);            
            rightUpperLegDesc = MakeBoxBounds(0, 6, -2, 4, 12, 2)   .RotOrigin(2, 12, 0);
            rightLowerLegDesc = MakeBoxBounds(0, 0, -2, 4, 6, 2)    .RotOrigin(2, 6, 0);
            leftUpperArmDesc  = MakeBoxBounds(-8, 18, -2, -4, 24, 2).RotOrigin(-6, 22, 0);
            leftLowerArmDesc  = MakeBoxBounds(-8, 12, -2, -4, 18, 2).RotOrigin(-4, 18, 0);
            leftUpperLegDesc  = MakeBoxBounds(-4, 6, -2, 0, 12, 2)  .RotOrigin(-2, 12, 0);
            leftLowerLegDesc  = MakeBoxBounds(-4, 0, -2, 0, 6, 2)   .RotOrigin(-2, 6, 0);

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
            float lowerLeftArmZ = 0.25f * (float)Math.Sin(-p.anim.leftArmX);
            float lowerRightArmX = 0.25f * (float)Math.Sin(p.anim.rightArmZ);
            float lowerRightArmY = 0.25f - 0.25f * (float)Math.Cos(p.anim.rightArmX) + breathDisp;
            float lowerRightArmZ = 0.25f * (float)Math.Sin(-p.anim.rightArmX);
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

            DrawTransform(0f, breathDisp, 0f,-p.HeadXRadians, 0f, 0f, .9f, head, true);
            DrawTransform(0f, breathDisp, 0f, p.anim.leftArmX, 0f, p.anim.leftArmZ, 1f, leftUpperArm, false);
            DrawTransform(0f, breathDisp, 0f, p.anim.rightArmX, 0f, p.anim.rightArmZ, 1f, rightUpperArm, false);
            DrawTransform(0f, 0f, 0f, 0f, 0f, 0f, breath, torso, false);
            DrawTransform(lowerLeftArmX, lowerLeftArmY, lowerLeftArmZ, lowerLeftArmRot, 0f, 0f, 1f, leftLowerArm, false);
            DrawTransform(lowerRightArmX, lowerRightArmY, lowerRightArmZ, lowerRightArmRot, 0f, 0f, 1f, rightLowerArm, false);
            DrawTransform(0f, lowerLeftLegY, lowerLeftLegZ, lowerLeftLegRot, 0f, 0f, 1f, leftLowerLeg, false);
            DrawTransform(0f, lowerRightLegY, lowerRightLegZ, lowerRightLegRot, 0f, 0f, 1f, rightLowerLeg, false);

            UpdateVB();
            index = 0;
            game.Graphics.AlphaTest = true;

            DrawRotate(p.anim.leftLegX, 0f, p.anim.leftLegZ, leftUpperPant, false);
            DrawRotate(p.anim.rightLegX, 0f, p.anim.rightLegZ, rightUpperPant, false);
            DrawRotate((float)Math.PI * p.anim.swing / -3f, 0f, 0f, cape, false);

            DrawTransform(0f, breathDisp, 0f, -p.HeadXRadians, 0f, 0f, 0.9f, hat, true);
            DrawTransform(0f, breathDisp, 0f, p.anim.leftArmX, 0f, p.anim.leftArmZ, 1f, leftUpperSleeve, false);
            DrawTransform(0f, breathDisp, 0f, p.anim.rightArmX, 0f, p.anim.rightArmZ, 1f, rightUpperSleeve, false);
            DrawTransform(0f, 0f, 0f, 0f, 0f, 0f, breath, jacket, false);
            DrawTransform(lowerLeftArmX, lowerLeftArmY, lowerLeftArmZ, lowerLeftArmRot, 0f, 0f, 1f, leftLowerSleeve, false);
            DrawTransform(lowerRightArmX, lowerRightArmY, lowerRightArmZ, lowerRightArmRot, 0f, 0f, 1f, rightLowerSleeve, false);
            DrawTransform(0f, lowerLeftLegY, lowerLeftLegZ, lowerLeftLegRot, 0f, 0f, 1f, leftLowerPant, false);
            DrawTransform(0f, lowerRightLegY, lowerRightLegZ, lowerRightLegRot, 0f, 0f, 1f, rightLowerPant, false);

            UpdateVB();
        }

        public override void DrawArm(Entity p)
        {
            uScale = 1f / 128f;
            vScale = 1f / 64f;
            DrawArmPart(rightLowerArm);
            DrawArmPart(rightLowerSleeve);
            UpdateVB();
        }

        protected void DrawTransform(float dispX, float dispY, float dispZ, float rotX, float rotY, float rotZ, float scale, ModelPart part, bool head) {
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

                // Rotate globally
                if (head) {
                    t = cosHead * v.X - sinHead * v.Z; v.Z = sinHead * v.X + cosHead * v.Z; v.X = t; // Inlined RotY
                }
                //Scale box at pivot
                v.X *= scale; v.Y *= scale; v.Z *= scale;

                vertex.X = v.X + part.RotX; vertex.Y = v.Y + part.RotY; vertex.Z = v.Z + part.RotZ;
                // Translate part
                vertex.X += dispX; vertex.Y += dispY; vertex.Z += dispZ;

                vertex.Col = cols[i >> 2];

                vertex.U = (v.U & UVMask) * uScale - (v.U >> UVMaxShift) * 0.01f * uScale;
                vertex.V = (v.V & UVMask) * vScale - (v.V >> UVMaxShift) * 0.01f * vScale;
                finVertices[index++] = vertex;
            }
        }

        private ModelPart head, hat, torso, leftUpperArm, rightUpperArm, leftLowerArm, rightLowerArm, leftUpperLeg, rightUpperLeg, leftLowerLeg, rightLowerLeg;
        private ModelPart jacket, leftUpperSleeve, rightUpperSleeve, leftLowerSleeve, rightLowerSleeve, leftUpperPant, rightUpperPant, leftLowerPant, rightLowerPant, cape;

        private const int boxesBuilt = 21;
    }
}