#include "Common.h"

static struct Model *block;

static void RecalcProperties(struct Entity *e) {
	BlockID block = (BlockID)((e->ModelScale.X - 0.9999f) * 1000);

	if (block > 0) {
		// Change the block that the player is holding
		if (block < BLOCK_COUNT) e->ModelBlock = block;
		else e->ModelBlock = BLOCK_AIR;

		Vec3_Set(e->ModelScale, 1, 1, 1);
	}
}

static void DrawBlockTransform(struct Entity *e, float dispX, float dispY, float dispZ, float scale) {
	static Vec3 pos;
	static struct Matrix m, temp;

	if (block) {
		pos = e->Position;
		pos.Y += e->Anim.BobbingModel;

		Entity_GetTransform(e, pos, e->ModelScale, &m);
		Matrix_Mul(&m, &m, &Gfx.View);
		Matrix_Translate(&temp, dispX, dispY, dispZ);
		Matrix_Mul(&m, &temp, &m);
		Matrix_Scale(&temp, scale, scale, scale);
		Matrix_Mul(&m, &temp, &m);

		Model_SetupState(block, e);
		Gfx_LoadMatrix(MATRIX_VIEW, &m);
		block->Draw(e);
	}
}

static void HoldModel_Draw(struct Entity *e) {
	static float handBob;
	static float handIdle;

	RecalcProperties(e);

	// Draw Human
	handBob = (float)Math_Sin(e->Anim.WalkTime * 2.0f) * e->Anim.Swing * MATH_PI / 16.0f;
	handIdle = e->Anim.RightArmX * (1.0f - e->Anim.Swing);

	e->Anim.LeftArmX = MATH_PI / 3.0f + handBob + handIdle;
	e->Anim.LeftArmZ = MATH_PI / 8.0f;

	e->Anim.RightArmX = MATH_PI / 3.0f + handBob + handIdle;
	e->Anim.RightArmZ = MATH_PI / -8.0f;

	Model_SetupState(Models.Human, e);
	Models.Human->Draw(e);
	
	DrawBlockTransform(e, 0, (MATH_PI / 3 + handBob + handIdle) * 10 / 16 + 0.5f, -9.0f / 16, 0.5f);
}

static struct Model model;
struct Model* HoldModel_GetInstance(void) {
	// copy everything from human model
	model = *Models.Human;

	model.name = "hold";
	model.MakeParts = nullfunc;
	model.Draw = HoldModel_Draw;
	block = Model_Get(&(String)String_FromConst("block"));
	return &model;
}