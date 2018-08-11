using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;
using BlockID = System.UInt16;

namespace MoreModels {
    class HoldingModel : HumanoidModel {
        public HoldingModel(Game game) : base(game) {
            MaxScale = (BlockInfo.Count + 2f);
        }

        public override void CreateParts() {
            HumanoidModel humanoid = (HumanoidModel)game.ModelCache.Models[0].Instance;
            vertices = humanoid.vertices;
            Set = humanoid.Set;
            Set64 = humanoid.Set64;
            SetSlim = humanoid.SetSlim;
        }

        public override Vector3 CollisionSize { get { return new Vector3(8.6f / 16f, 23f / 16f, 8.6f / 16f); } }

        public override AABB PickingBounds { get { return new AABB(-0.5f, 0f, -0.25f, 0.5f, 2f, 0.25f); } }
        public void resetMaxScale() { MaxScale = BlockInfo.Count + 2f; }

        public override void RecalcProperties(Entity p) {
            float scale = (float)Math.Floor(p.ModelScale.X);
            if (scale >= 2f) {
                // Change the block that the player is holding if it is signaled that the block should change.
                // Signal that the block should no longer be changed if the scale changes.
                if ((p.ModelBlock & preventBlockChange) != preventBlockChange) {
                    BlockID block = (BlockID)((int)scale - 2);
                    if (block < BlockInfo.Count) p.ModelBlock = block;
                    else p.ModelBlock = Block.Air;
                    p.ModelBlock |= preventBlockChange;
                    p.ModelScale = Vector3.One;
                }
            }
            /*else if (scale == 1f) {
                //Do nothing
            }
            else if (scale == 0f) {
                //Signal that the block should be changed when the scale changes
                p.ModelBlock &= (ushort)~preventBlockChange;
                p.ModelScale = Vector3.One;
            }*/
        }

        public override void DrawModel(Entity p) {
            ModelSet model = p.SkinType == SkinType.Type64x64Slim ? SetSlim : (p.SkinType == SkinType.Type64x64 ? Set64 : Set);

            Rotate = RotateOrder.XZY;

            float handBob = (float)Math.Sin(p.anim.walkTime * 2f) * p.anim.swing * (float)Math.PI / 16f;
            float handIdle = p.anim.rightArmX * (1f - p.anim.swing);

            ApplyTexture(p);
            game.Graphics.AlphaTest = false;

            DrawPart(model.Torso);

            DrawRotate(-p.HeadXRadians, 0f, 0f, model.Head, true);
            DrawRotate(p.anim.leftLegX, 0f, p.anim.leftLegZ, model.LeftLeg, false);
            DrawRotate(p.anim.rightLegX, 0f, p.anim.rightLegZ, model.RightLeg, false);
            DrawRotate((float)Math.PI / 3f + handBob + handIdle, (handBob + handIdle) * -2f / 3f, (float)Math.PI / 8f, model.LeftArm, false);
            DrawRotate((float)Math.PI / 3f + handBob + handIdle, (handBob + handIdle) * 2f / 3f, (float)Math.PI / -8f, model.RightArm, false);

            UpdateVB();
            index = 0;
            game.Graphics.AlphaTest = true;

            if (p.SkinType != SkinType.Type64x32) {
                DrawPart(model.TorsoLayer);

                DrawRotate(p.anim.leftLegX, 0f, p.anim.leftLegZ, model.LeftLegLayer, false);
                DrawRotate(p.anim.rightLegX, 0f, p.anim.rightLegZ, model.RightLegLayer, false);
                DrawRotate((float)Math.PI / 3f + handBob + handIdle, (handBob + handIdle) * -2f / 3f, (float)Math.PI / 8f, model.LeftArmLayer, false);
                DrawRotate((float)Math.PI / 3f + handBob + handIdle, (handBob + handIdle) * 2f / 3f, (float)Math.PI / -8f, model.RightArmLayer, false);
            }

            DrawRotate(-p.HeadXRadians, 0f, 0f, model.Hat, true);

            UpdateVB();
            index = 0;

            BlockID prevBlock = p.ModelBlock;
            p.ModelBlock &= 0x7fff;

            DrawBlockTransform(p, 0f, ((float)Math.PI / 3f + handBob + handIdle) * 10f / 16f + 8f / 16f, -9f / 16f, 0.5f);

            p.ModelBlock = prevBlock;
        }
        private void DrawBlockTransform(Entity p, float dispX, float dispY, float dispZ, float scale) {
            IModel blockModel = game.ModelCache.Models[9].Instance; // Get block model
            if (blockModel != null) {
                Vector3 pos = p.Position;
                if (Bobbing) pos.Y += p.anim.bobbingModel;

                Matrix4 matrix = TransformMatrix(p, pos), temp;
                Matrix4.Mult(out matrix, ref matrix, ref game.Graphics.View);
                Matrix4.Translate(out temp, dispX, dispY, dispZ);
                Matrix4.Mult(out matrix, ref temp, ref matrix);
                Matrix4.Scale(out temp, scale, scale, scale);
                Matrix4.Mult(out matrix, ref temp, ref matrix);

                game.Graphics.LoadMatrix(ref matrix);
                blockModel.DrawModel(p);
            }
        }

        private ushort preventBlockChange = 0x8000;
    }
}