#include "Common.h"

/*public HoldingModel(Game game) : base(game) {
	MaxScale = (BlockInfo.Count + 2f);
}

public override void CreateParts() {
	HumanoidModel humanoid = (HumanoidModel)game.ModelCache.Models[0].Instance;
	vertices = humanoid.vertices;
	Set = humanoid.Set;
	Set64 = humanoid.Set64;
	SetSlim = humanoid.SetSlim;
}

public override Vector3 CollisionSize{ get { return new Vector3(8.6f / 16f, 23f / 16f, 8.6f / 16f); } }

public override AABB PickingBounds{ get { return new AABB(-0.5f, 0f, -0.25f, 0.5f, 2f, 0.25f); } }
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
	}/
}
*//*
static void HoldingModel_Draw(struct Entity* e) {
	//Model model = p.SkinType == SkinType.Type64x64Slim ? SetSlim : (p.SkinType == SkinType.Type64x64 ? Set64 : Set);

	Models.Rotation = ROTATE_ORDER_XZY;
	//Rotate = RotateOrder.XZY;

	float handBob = (float)Math_Sin(p.anim.walkTime * 2.0f) * p.anim.swing * MATH_PI / 16.0f;
	float handIdle = p.anim.rightArmX * (1.0f - p.anim.swing);
	Models.Human->vertices

	Model_ApplyTexture(e);
	Gfx_SetAlphaTest(false);
	//game.Graphics.AlphaTest = false;

	Model_DrawPart(model.Torso);

	Model_DrawRotate(-p.HeadXRadians, 0.0f, 0.0f, model.Head, true);
	Model_DrawRotate(p.anim.leftLegX, 0.0f, p.anim.leftLegZ, model.LeftLeg, false);
	Model_DrawRotate(p.anim.rightLegX, 0.0f, p.anim.rightLegZ, model.RightLeg, false);
	Model_DrawRotate(MATH_PI / 3.0f + handBob + handIdle, (handBob + handIdle) * -2.0f / 3.0f, MATH_PI / 8.0f, model.LeftArm, false);
	Model_DrawRotate(MATH_PI / 3.0f + handBob + handIdle, (handBob + handIdle) * 2.0f / 3.0f, MATH_PI / -8.0f, model.RightArm, false);

	Model_UpdateVB();

	Gfx_SetAlphaTest(true);

	if (e->SkinType != SKIN_64x32) {
		Model_DrawPart(model.TorsoLayer);

		Model_DrawRotate(p.anim.leftLegX, 0.0f, p.anim.leftLegZ, model.LeftLegLayer, false);
		Model_DrawRotate(p.anim.rightLegX, 0.0f, p.anim.rightLegZ, model.RightLegLayer, false);
		Model_DrawRotate(MATH_PI / 3.0f + handBob + handIdle, (handBob + handIdle) * -2.0f / 3.0f, MATH_PI / 8.0f, model.LeftArmLayer, false);
		Model_DrawRotate(MATH_PI / 3.0f + handBob + handIdle, (handBob + handIdle) * 2.0f / 3.0f, MATH_PI / -8.0f, model.RightArmLayer, false);
	}

	DrawRotate(-p.HeadXRadians, 0.0f, 0.0f, model.Hat, true);

	UpdateVB();

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

private ushort preventBlockChange = 0x8000;*/