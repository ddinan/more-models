#include "Common.h"
static struct ModelPart head, snout, body, leftLegFront, rightLegFront, leftLegBack, rightLegBack, frontTail, midTail, backTail;

static void Translate(struct Entity *e, float dispX, float dispY, float dispZ) {
	struct Matrix mat, temp;

	Entity_GetTransform(e, e->Position, e->ModelScale, &mat);
	Matrix_Mul(&mat, &mat, &Gfx.View);
	Matrix_Translate(&temp, dispX, dispY, dispZ);
	Matrix_Mul(&mat, &temp, &mat);

	Gfx_LoadMatrix(MATRIX_VIEW, &mat);
}

static void CrocModel_MakeParts(void) {
	BoxDesc_BuildRotatedBox(&snout, &(struct BoxDesc) {
		BoxDesc_Tex(30, 0),
		BoxDesc_Box(-3, 0, -19, 3, 3, -12),
		BoxDesc_Rot(0, 2, -13),
	});

	BoxDesc_BuildRotatedBox(&head, &(struct BoxDesc) {
		BoxDesc_Tex(0, 23),
		BoxDesc_Box(-4, 0, -12, 4, 4, -7),
		BoxDesc_Rot(0, 2, -7),
	});

	BoxDesc_BuildRotatedBox(&frontTail, &(struct BoxDesc) {
		BoxDesc_Tex(24, 23),
		BoxDesc_Box(-4, 0, 9, 4, 4, 14),
		BoxDesc_Rot(0, 2, 9),
	});

	BoxDesc_BuildRotatedBox(&midTail, &(struct BoxDesc) {
		BoxDesc_Tex(30, 10),
		BoxDesc_Box(-3, 0, 13, 3, 3, 18),
		BoxDesc_Rot(0, 2, 13),
	});

	BoxDesc_BuildRotatedBox(&backTail, &(struct BoxDesc) {
		BoxDesc_Tex(48, 0),
		BoxDesc_Box(-2, 0, 17, 2, 2, 22),
		BoxDesc_Rot(0, 1, 17),
	});

	BoxDesc_BuildRotatedBox(&body, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-5, 0, -8, 5, 5, 10),
	});

	BoxDesc_BuildBox(&leftLegFront, &(struct BoxDesc) {
		BoxDesc_Tex(48, 7),
		BoxDesc_Box(-8, 0, -7, -5, 3, -4),
		BoxDesc_Rot(-6, 2, -5),
	});

	BoxDesc_BuildBox(&leftLegBack, &(struct BoxDesc) {
		BoxDesc_Tex(48, 7),
		BoxDesc_Box(-8, 0, 6, -5, 3, 9),
		BoxDesc_Rot(-6, 2, 7),
	});

	BoxDesc_BuildBox(&rightLegFront, &(struct BoxDesc) {
		BoxDesc_Tex(48, 7),
		BoxDesc_Box(5, 0, -7, 8, 3, -4),
		BoxDesc_Rot(6, 2, -5),
	});

	BoxDesc_BuildBox(&rightLegBack, &(struct BoxDesc) {
		BoxDesc_Tex(48, 7),
		BoxDesc_Box(5, 0, 6, 8, 3, 9),
		BoxDesc_Rot(6, 2, 7),
	});
}

static float CrocModel_GetNameY(struct Entity *e) { e; return 0.5f; }
static float CrocModel_GetEyeY(struct Entity *e) { e; return 0.25f; }
static void CrocModel_GetSize(struct Entity *e) { _SetSize(10, 5, 10); }
static void CrocModel_GetBounds(struct Entity *e) { _SetBounds(-8, 0, -19, 8, 5, 22); }

static void CrocModel_Draw(struct Entity *e) {
	Model_ApplyTexture(e);

	float walkRot = (float)Math_Sin(e->Anim.WalkTime) * MATH_PI / -16.0f;
	float walkRotPhase1 = (float)Math_Sin(e->Anim.WalkTime - MATH_PI / 8.0f) * MATH_PI / -12.0f;
	float walkRotPhase2 = (float)Math_Sin(e->Anim.WalkTime - MATH_PI / 4.0f) * MATH_PI / -8.0f;

	Model_DrawPart(&body);
	
	Model_DrawRotate(0.0f, -walkRot / 2.0f, 0.0f, &head, false);
	Model_DrawRotate(0.0f, walkRot, 0.0f, &frontTail, false);
	Model_DrawRotate(walkRot * 2.0f, 0.0f, 0.0f, &leftLegFront, false);
	Model_DrawRotate(walkRot * -2.0f, 0.0f, 0.0f, &rightLegFront, false);
	Model_DrawRotate(walkRot * -2.0f, 0.0f, 0.0f, &leftLegBack, false);
	Model_DrawRotate(walkRot * 2.0f, 0.0f, 0.0f, &rightLegBack, false);

	Model_UpdateVB();

	Translate(e, (float)Math_Sin(walkRot / 2.0f) * 6.0f / 16.0f, 0.0f, ((float)Math_Cos(walkRot / 2.0f) - 1.0f) * -6.0f / 16.0f);
	Model_DrawRotate(0.0f, -walkRot / 2.0f, 0.0f, &snout, false);

	Model_UpdateVB();

	Translate(e, (float)Math_Sin(walkRot) * 5.0f / 16.0f, 0.0f, ((float)Math_Cos(walkRot / 2.0f) - 1.0f) * 5.0f / 16.0f);
	Model_DrawRotate(0.0f, walkRotPhase1, 0.0f, &midTail, false);

	Model_UpdateVB();

	Translate(e, (float)(Math_Sin(walkRot) + Math_Sin(walkRotPhase1)) * 5.0f / 16.0f, 0.0f, ((float)Math_Cos(walkRot) + (float)Math_Cos(walkRotPhase1) - 2.0f) * 5.0f / 16.0f);
	Model_DrawRotate(0.0f, walkRotPhase2, 0.0f, &backTail, false);

	Model_UpdateVB();
}

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 10];
static struct Model model = {
  "croc", vertices, &croc_tex,
  CrocModel_MakeParts, CrocModel_Draw,
  CrocModel_GetNameY,  CrocModel_GetEyeY,
  CrocModel_GetSize,   CrocModel_GetBounds
};

struct Model* CrocModel_GetInstance(void) {
	Model_Init(&model);
	model.bobbing = false;
	model.calcHumanAnims = false;
	return &model;
}