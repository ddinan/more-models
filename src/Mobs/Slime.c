#include "Common.h"
/* TEXTURE NEEDS FIX, OTHERWISE BAD LIGHTING. */
static struct ModelPart headInner, headOuter, leftEye, rightEye, mouth;

static void SlimeModel_MakeParts(void) {
	BoxDesc_BuildBox(&headInner, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(-3, 1, -3, 3, 7, 3)
	});
	BoxDesc_BuildBox(&headOuter, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-4, 0, -4, 4, 8, 4)
	});
	BoxDesc_BuildBox(&leftEye, &(struct BoxDesc) {
		BoxDesc_Tex(32, 4),
		BoxDesc_Box(-3.25, 4, -3.25f, -1.25, 6, -1.25f)
	});
	BoxDesc_BuildBox(&rightEye, &(struct BoxDesc) {
		BoxDesc_Tex(32, 0),
		BoxDesc_Box(1.25, 4, -3.25f, 3.25, 6, -1.25f)
	});
	BoxDesc_BuildBox(&mouth, &(struct BoxDesc){
		BoxDesc_Tex(32, 8),
		BoxDesc_Box(-1, 2, -3.25f, 0, 3, -2.25f)
	});
}
static void SlimeModel_Draw(struct Entity *e) {
	Model_ApplyTexture(e);
	/* This currently causes anything behind slime to be culled (except entities). */
	Gfx_SetAlphaBlending(true);

	Model_DrawPart(&headInner);
	Model_DrawPart(&leftEye);
	Model_DrawPart(&rightEye);
	Model_DrawPart(&mouth);

	Model_DrawPart(&headOuter);

	Model_UpdateVB();
	Gfx_SetAlphaBlending(false);
}	

static float SlimeModel_GetNameY(struct Entity *e) { return 8 / 16.0f; }
static float SlimeModel_GetEyeY(struct Entity *e) { return 6 / 16.0f; }
static void SlimeModel_GetSize(struct Entity *e)   { _SetSize(14, 14, 14); }
static void SlimeModel_GetBounds(struct Entity *e) { _SetBounds(-5,0,14, 5,16,9); }

/*static void SlimeModel_GetTransform(struct Entity* e, Vec3 pos, struct Matrix* m) {
	static struct Matrix mat;
	static float disp;
	pos.Y -= e->Anim.BobbingModel;
	pos.Y += e->Anim.BobbingModel * e->ModelScale.Y;
	Entity_GetTransform(e, pos, e->ModelScale, m);
	Matrix_Translate(&mat, 0, 0, (((disp = (float)Math_Sin(e->Anim.WalkTime - MATH_PI/2)) < 0 ? -disp : disp) - 0.5f) * e->Anim.Swing * -0.4f / e->ModelScale.Y);
	Matrix_Mul(m, &mat, m);
}*/
static struct ModelVertex vertices[MODEL_BOX_VERTICES * 5];
static struct Model model = { 
	"slime", vertices, &slime_tex,
	SlimeModel_MakeParts, SlimeModel_Draw,
	SlimeModel_GetNameY,  SlimeModel_GetEyeY,
	SlimeModel_GetSize,   SlimeModel_GetBounds
};

struct Model* SlimeModel_GetInstance(void) {
	Model_Init(&model);
	model.maxScale = 32;
	// model.GetTransform = SlimeModel_GetTransform;
	return &model;
}