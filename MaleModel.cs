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
            vertices = new ModelVertex[boxVertices * boxesBuilt];

            BoxDesc head, torso, leftUpperArm, rightUpperArm, leftLowerArm, rightLowerArm, leftUpperLeg, rightUpperLeg, leftLowerLeg, rightLowerLeg;

            head          = MakeBoxBounds(-4, 24, -4, 4, 32, 4) .TexOrigin(0, 0)  .RotOrigin(0, 24, 0);
            torso         = MakeBoxBounds(-4, 12, -2, 4, 24, 2) .TexOrigin(16, 16).RotOrigin(0, 12, 0);
            
            rightUpperArm = MakeBoxBounds(4, 18, -2, 8, 24, 2)  .TexOrigin(40, 16).RotOrigin(6, 22, 0);            
            rightLowerArm = MakeBoxBounds(4, 12, -2, 8, 18, 2)  .TexOrigin(40, 22).RotOrigin(4, 18, 0);            
            rightUpperLeg = MakeBoxBounds(0, 6, -2, 4, 12, 2)   .TexOrigin(0, 16) .RotOrigin(2, 12, 0);            
            rightLowerLeg = MakeBoxBounds(0, 0, -2, 4, 6, 2)    .TexOrigin(0, 22) .RotOrigin(2, 6, 0);

            leftUpperArm = MakeBoxBounds(-4, 18, -2, -8, 24, 2).MirrorX().TexOrigin(40, 16).RotOrigin(-6, 22, 0);
            leftLowerArm = MakeBoxBounds(-4, 12, -2, -8, 18, 2).MirrorX().TexOrigin(40, 22).RotOrigin(-4, 18, 0);
            leftUpperLeg = MakeBoxBounds(0, 6, -2, -4, 12, 2)  .MirrorX().TexOrigin(0, 16) .RotOrigin(-2, 12, 0);
            leftLowerLeg = MakeBoxBounds(0, 0, -2, -4, 6, 2)   .MirrorX().TexOrigin(0, 22) .RotOrigin(-2, 6, 0);

            norm = new ModelDesc();

            norm.head          = BuildBox(head);
            norm.torso         = BuildBox(torso);
            norm.rightUpperArm = BuildBox(rightUpperArm);
            norm.rightLowerArm = BuildBox(rightLowerArm);
            norm.rightUpperLeg = BuildBox(rightUpperLeg);
            norm.rightLowerLeg = BuildBox(rightLowerLeg);

            norm.leftUpperArm  = BuildBox(leftUpperArm.MirrorX());            
            norm.leftLowerArm  = BuildBox(leftLowerArm.MirrorX());            
            norm.leftUpperLeg  = BuildBox(leftUpperLeg.MirrorX());            
            norm.leftLowerLeg  = BuildBox(leftLowerLeg.MirrorX());            

            norm.hat = BuildBox(head.TexOrigin(32, 0).Expand(0.5f));

            leftUpperArm = leftUpperArm.MirrorX();
            leftLowerArm = leftLowerArm.MirrorX();
            leftUpperLeg = leftUpperLeg.MirrorX();
            leftLowerLeg = leftLowerLeg.MirrorX();

            hd = new ModelDesc();

            hd.head          = norm.head;
            hd.hat           = norm.hat;
            hd.torso         = norm.torso;
            hd.rightUpperArm = norm.rightUpperArm;
            hd.rightLowerArm = norm.rightLowerArm;
            hd.rightUpperLeg = norm.rightUpperLeg;
            hd.rightLowerLeg = norm.rightLowerLeg;

            hd.leftUpperArm = BuildBox(leftUpperArm.TexOrigin(32, 48));
            hd.leftLowerArm = BuildBox(leftLowerArm.TexOrigin(32, 54));    
            hd.leftUpperLeg = BuildBox(leftUpperLeg.TexOrigin(16, 48));            
            hd.leftLowerLeg = BuildBox(leftLowerLeg.TexOrigin(16, 54));

            hd.jacket           = BuildBox(torso        .TexOrigin(16, 32).Expand(0.5f));
            hd.leftUpperSleeve  = BuildBox(leftUpperArm .TexOrigin(48, 48).Expand(0.5f));
            hd.rightUpperSleeve = BuildBox(rightUpperArm.TexOrigin(40, 32).Expand(0.5f));
            hd.leftLowerSleeve  = BuildBox(leftLowerArm .TexOrigin(48, 54).Expand(0.5f));
            hd.rightLowerSleeve = BuildBox(rightLowerArm.TexOrigin(40, 38).Expand(0.5f));
            hd.leftUpperPant    = BuildBox(leftUpperLeg .TexOrigin(0, 48) .Expand(0.5f));
            hd.rightUpperPant   = BuildBox(rightUpperLeg.TexOrigin(0, 32) .Expand(0.5f));
            hd.leftLowerPant    = BuildBox(leftLowerLeg .TexOrigin(0, 54) .Expand(0.5f));
            hd.rightLowerPant   = BuildBox(rightLowerLeg.TexOrigin(0, 38) .Expand(0.5f));

            slim = new ModelDesc();

            slim.head           = hd.head;
            slim.hat            = hd.hat;
            slim.torso          = hd.torso;
            slim.jacket         = hd.jacket;
            slim.leftUpperLeg   = hd.leftUpperLeg;
            slim.rightUpperLeg  = hd.rightUpperLeg;
            slim.leftLowerLeg   = hd.leftLowerLeg;
            slim.rightLowerLeg  = hd.rightLowerLeg;
            slim.leftUpperPant  = hd.leftUpperPant;
            slim.rightUpperPant = hd.rightUpperPant;
            slim.leftLowerPant  = hd.leftLowerPant;
            slim.rightLowerPant = hd.rightLowerPant;

            leftUpperArm.BodyW -= 1;
            leftUpperArm.X1 += 0.0625f;
            rightUpperArm.BodyW -= 1;
            rightUpperArm.X1 += 0.0625f;
            leftLowerArm.BodyW -= 1;
            leftLowerArm.X1 += 0.0625f;
            rightLowerArm.BodyW -= 1;
            rightLowerArm.X1 += 0.0625f;

            slim.leftUpperArm = BuildBox(leftUpperArm.TexOrigin(32, 48));
            slim.rightUpperArm = BuildBox(rightUpperArm.TexOrigin(40, 16));
            slim.leftLowerArm = BuildBox(leftLowerArm.TexOrigin(32, 54));
            slim.rightLowerArm = BuildBox(rightLowerArm.TexOrigin(40, 22));

            slim.leftUpperSleeve = BuildBox(leftUpperArm.TexOrigin(48, 48).Expand(0.5f));
            slim.rightUpperSleeve = BuildBox(rightUpperArm.TexOrigin(40, 32).Expand(0.5f));
            slim.leftLowerSleeve = BuildBox(leftLowerArm.TexOrigin(48, 54).Expand(0.5f));
            slim.rightLowerSleeve = BuildBox(rightLowerArm.TexOrigin(40, 38).Expand(0.5f));
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
            game.Graphics.AlphaTest = false;

            ModelDesc model = p.SkinType == SkinType.Type64x64Slim ? slim : (p.SkinType == SkinType.Type64x64 ? hd : norm);

            DrawRotate(p.anim.leftLegX, 0f, p.anim.leftLegZ, model.leftUpperLeg, false);
            DrawRotate(p.anim.rightLegX, 0f, p.anim.rightLegZ, model.rightUpperLeg, false);

            DrawTransform(0f, breathDisp, 0f,-p.HeadXRadians, 0f, 0f, .9f, model.head, true);

            DrawTransform(0f, breathDisp, 0f, p.anim.leftArmX, 0f, p.anim.leftArmZ, 1f, model.leftUpperArm, false);
            DrawTransform(0f, breathDisp, 0f, p.anim.rightArmX, 0f, p.anim.rightArmZ, 1f, model.rightUpperArm, false);
            DrawTransform(0f, 0f, 0f, 0f, 0f, 0f, breath, model.torso, false);
            DrawTransform(lowerLeftArmX, lowerLeftArmY, lowerLeftArmZ, lowerLeftArmRot, 0f, 0f, 1f, model.leftLowerArm, false);
            DrawTransform(lowerRightArmX, lowerRightArmY, lowerRightArmZ, lowerRightArmRot, 0f, 0f, 1f, model.rightLowerArm, false);
            DrawTransform(0f, lowerLeftLegY, lowerLeftLegZ, lowerLeftLegRot, 0f, 0f, 1f, model.leftLowerLeg, false);
            DrawTransform(0f, lowerRightLegY, lowerRightLegZ, lowerRightLegRot, 0f, 0f, 1f, model.rightLowerLeg, false);

            UpdateVB();
            index = 0;

            game.Graphics.AlphaTest = true;

            if (p.SkinType != SkinType.Type64x32)
            {
                DrawTransform(0f, breathDisp, 0f, p.anim.leftArmX, 0f, p.anim.leftArmZ, 1f, model.leftUpperSleeve, false);
                DrawTransform(0f, breathDisp, 0f, p.anim.rightArmX, 0f, p.anim.rightArmZ, 1f, model.rightUpperSleeve, false);
                DrawTransform(0f, 0f, 0f, 0f, 0f, 0f, breath, model.jacket, false);
                DrawTransform(lowerLeftArmX, lowerLeftArmY, lowerLeftArmZ, lowerLeftArmRot, 0f, 0f, 1f, model.leftLowerSleeve, false);
                DrawTransform(lowerRightArmX, lowerRightArmY, lowerRightArmZ, lowerRightArmRot, 0f, 0f, 1f, model.rightLowerSleeve, false);
                DrawTransform(0f, lowerLeftLegY, lowerLeftLegZ, lowerLeftLegRot, 0f, 0f, 1f, model.leftLowerPant, false);
                DrawTransform(0f, lowerRightLegY, lowerRightLegZ, lowerRightLegRot, 0f, 0f, 1f, model.rightLowerPant, false);
            }
            DrawTransform(0f, breathDisp, 0f, -p.HeadXRadians, 0f, 0f, .9f, model.hat, true);

            UpdateVB();
        }

        protected void DrawTransform(float dispX, float dispY, float dispZ, float rotX, float rotY, float rotZ, float scale, ModelPart part, bool head)
        {
            float cosX = (float)Math.Cos(-rotX), sinX = (float)Math.Sin(-rotX);
            float cosY = (float)Math.Cos(-rotY), sinY = (float)Math.Sin(-rotY);
            float cosZ = (float)Math.Cos(-rotZ), sinZ = (float)Math.Sin(-rotZ);

            VertexP3fT2fC4b vertex = default(VertexP3fT2fC4b);
            VertexP3fT2fC4b[] finVertices = game.ModelCache.vertices;
                
            for (int i = 0; i < part.Count; i++)
            {
                ModelVertex v = vertices[part.Offset + i];

                // Prepare the vertex coordinates for rotation
                v.X -= part.RotX; v.Y -= part.RotY; v.Z -= part.RotZ;
                float t = 0;

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

        private ModelDesc norm, hd, slim;

        private class ModelDesc
        {
            public ModelPart head, hat, torso, leftUpperArm, rightUpperArm, leftLowerArm, rightLowerArm, leftUpperLeg, rightUpperLeg, leftLowerLeg, rightLowerLeg;
            public ModelPart jacket, leftUpperSleeve, rightUpperSleeve, leftLowerSleeve, rightLowerSleeve, leftUpperPant, rightUpperPant, leftLowerPant, rightLowerPant;
        }

        private const int boxesBuilt = 32;
    }
}