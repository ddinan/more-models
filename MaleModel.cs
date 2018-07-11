using System;
using ClassicalSharp.Entities;
using ClassicalSharp.Physics;
using ClassicalSharp.GraphicsAPI;
using OpenTK;

namespace ClassicalSharp.Model
{
    class MaleModel : IModel
    {
        public MaleModel(Game game) : base(game) { UsesHumanSkin = true; }

        public override void CreateParts()
        {
            vertices = new ModelVertex[boxVertices * numParts];

            head          = BuildBox(MakeBoxBounds(-4, 24, -4, 4, 32, 4) .TexOrigin(0, 0)  .RotOrigin(0, 24, 0));
            torso         = BuildBox(MakeBoxBounds(-4, 12, -2, 4, 24, 2) .TexOrigin(16, 16).RotOrigin(0, 12, 0));
            leftUpperArm  = BuildBox(MakeBoxBounds(-4, 18, -2, -8, 24, 2).TexOrigin(40, 16).RotOrigin(-6, 22, 0));
            rightUpperArm = BuildBox(MakeBoxBounds(4, 18, -2, 8, 24, 2)  .TexOrigin(40, 16).RotOrigin(6, 22, 0));
            leftLowerArm  = BuildBox(MakeBoxBounds(-4, 12, -2, -8, 18, 2).TexOrigin(40, 22).RotOrigin(-4, 18, 0));
            rightLowerArm = BuildBox(MakeBoxBounds(4, 12, -2, 8, 18, 2)  .TexOrigin(40, 22).RotOrigin(4, 18, 0));
            leftUpperLeg  = BuildBox(MakeBoxBounds(0, 6, -2, -4, 12, 2)  .TexOrigin(0, 16) .RotOrigin(-2, 12, 0));
            rightUpperLeg = BuildBox(MakeBoxBounds(0, 6, -2, 4, 12, 2)   .TexOrigin(0, 16) .RotOrigin(2, 12, 0));
            leftLowerLeg  = BuildBox(MakeBoxBounds(0, 0, -2, -4, 6, 2)   .TexOrigin(0, 22) .RotOrigin(-2, 6, 0));
            rightLowerLeg = BuildBox(MakeBoxBounds(0, 0, -2, 4, 6, 2)    .TexOrigin(0, 22) .RotOrigin(2, 6, 0));

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

            game.Graphics.BindTexture(GetTexture(p));

            DrawRotate(p.anim.leftLegX, 0f, p.anim.leftLegZ, leftUpperLeg, false);
            DrawRotate(p.anim.rightLegX, 0f, p.anim.rightLegZ, rightUpperLeg, false);

            DrawTransform(0f, breathDisp, 0f,-p.HeadXRadians, 0f, 0f, .9f, head, true);
            DrawTransform(0f, breathDisp, 0f, - p.HeadXRadians, 0f, 0f, .9f, hat, true);

            DrawTransform(0f, breathDisp, 0f, p.anim.leftArmX, 0f, p.anim.leftArmZ, 1f, leftUpperArm, false);
            DrawTransform(0f, breathDisp, 0f, p.anim.rightArmX, 0f, p.anim.rightArmZ, 1f, rightUpperArm, false);
            DrawTransform(0f, 0f, 0f, 0f, 0f, 0f, breath, torso, false);
            DrawTransform(lowerLeftArmX, lowerLeftArmY, lowerLeftArmZ, lowerLeftArmRot, 0f, 0f, 1f, leftLowerArm, false);
            DrawTransform(lowerRightArmX, lowerRightArmY, lowerRightArmZ, lowerRightArmRot, 0f, 0f, 1f, rightLowerArm, false);
            DrawTransform(0f, lowerLeftLegY, lowerLeftLegZ, lowerLeftLegRot, 0f, 0f, 1f, leftLowerLeg, false);
            DrawTransform(0f, lowerRightLegY, lowerRightLegZ, lowerRightLegRot, 0f, 0f, 1f, rightLowerLeg, false);

            UpdateVB();
        }

        protected void DrawTransform(float dispX, float dispY, float dispZ, float rotX, float rotY, float rotZ, float scale, ModelPart part, bool head)
        {
            float cosX = (float)Math.Cos(-rotX), sinX = (float)Math.Sin(-rotX);
            float cosY = (float)Math.Cos(-rotY), sinY = (float)Math.Sin(-rotY);
            float cosZ = (float)Math.Cos(-rotZ), sinZ = (float)Math.Sin(-rotZ);

            //Pivot coordinates x,y,z
            //float x = part.RotX, y = part.RotY, z = part.RotZ;

            //What is this
            VertexP3fT2fC4b vertex = default(VertexP3fT2fC4b);
            //Make a pointer to the vertices available in the model cache
            VertexP3fT2fC4b[] finVertices = game.ModelCache.vertices;
                
            for (int i = 0; i < part.Count; i++)
            {
                ModelVertex v = vertices[part.Offset + i];

                // Prepare the vertex coordinates for rotation
                v.X -= part.RotX; v.Y -= part.RotY; v.Z -= part.RotZ;
                float t = 0;

                //v.X = cosX;


                // Rotate locally.
                if (Rotate == RotateOrder.ZYX)
                {
                    t = cosZ * v.X + sinZ * v.Y; v.Y = -sinZ * v.X + cosZ * v.Y; v.X = t; // Inlined RotZ
                    t = cosY * v.X - sinY * v.Z; v.Z = sinY * v.X + cosY * v.Z; v.X = t; // Inlined RotY
                    t = cosX * v.Y + sinX * v.Z; v.Z = -sinX * v.Y + cosX * v.Z; v.Y = t; // Inlined RotX
                }
                else if (Rotate == RotateOrder.XZY)
                {
                    t = cosX * v.Y + sinX * v.Z; v.Z = -sinX * v.Y + cosX * v.Z; v.Y = t; // Inlined RotX
                    t = cosZ * v.X + sinZ * v.Y; v.Y = -sinZ * v.X + cosZ * v.Y; v.X = t; // Inlined RotZ
                    t = cosY * v.X - sinY * v.Z; v.Z = sinY * v.X + cosY * v.Z; v.X = t; // Inlined RotY
                }
                else if (Rotate == RotateOrder.YZX)
                {
                    t = cosY * v.X - sinY * v.Z; v.Z = sinY * v.X + cosY * v.Z; v.X = t; // Inlined RotY
                    t = cosZ * v.X + sinZ * v.Y; v.Y = -sinZ * v.X + cosZ * v.Y; v.X = t; // Inlined RotZ
                    t = cosX * v.Y + sinX * v.Z; v.Z = -sinX * v.Y + cosX * v.Z; v.Y = t; // Inlined RotX
                }

                // Rotate globally
                if (head)
                {
                    t = cosHead * v.X - sinHead * v.Z; v.Z = sinHead * v.X + cosHead * v.Z; v.X = t; // Inlined RotY
                }
                //Scale box at pivot
                v.X *= scale; v.Y *= scale; v.Z *= scale;

                vertex.X = v.X + part.RotX; vertex.Y = v.Y + part.RotY; vertex.Z = v.Z + part.RotZ;
                // Translate part
                vertex.X += dispX; vertex.Y += dispY; vertex.Z += dispZ;

                vertex.Colour = cols[i >> 2];

                vertex.U = (v.U & UVMask) * uScale - (v.U >> UVMaxShift) * 0.01f * uScale;
                vertex.V = (v.V & UVMask) * vScale - (v.V >> UVMaxShift) * 0.01f * vScale;
                finVertices[index++] = vertex;
            }
        }

        private ModelPart head, hat, torso, leftUpperArm, rightUpperArm, leftLowerArm, rightLowerArm, leftUpperLeg, rightUpperLeg, leftLowerLeg, rightLowerLeg;

        private const int numParts = 11;
    }
}
