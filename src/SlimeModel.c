#include "Common.h"
static struct ModelPart headInner, headOuter;

static void SlimeModel_MakeParts(void) {
	BoxDesc_BuildBox(&headInner, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-3,7,3, 3,1,3)
	});

	BoxDesc_BuildBox(&headOuter, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(4,8,-4, -4,0,4)
	});
}

static void SlimeModel_Draw(struct Entity* e) {
	Model_ApplyTexture(e);
	Gfx_SetAlphaBlending(true);

	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &headInner, true);
	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &headOuter, true);

	Model_UpdateVB();
	Gfx_SetAlphaBlending(false);
}	

static float SlimeModel_GetNameY(struct Entity* e) { return 8/16.0f; }
static float SlimeModel_GetEyeY(struct Entity* e)  { return 6/16.0f; }
static void SlimeModel_GetSize(struct Entity* e)   { _SetSize(14, 14, 14); }
static void SlimeModel_GetBounds(struct Entity* e) { _SetBounds(-5,0,14, 5,16,9); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 2];
static struct Model model = { 
	"slime", vertices, &slime_tex,
	SlimeModel_MakeParts, SlimeModel_Draw,
	SlimeModel_GetNameY,  SlimeModel_GetEyeY,
	SlimeModel_GetSize,   SlimeModel_GetBounds
};

struct Model* SlimeModel_GetInstance(void) {
	Model_Init(&model);
	return &model;
}