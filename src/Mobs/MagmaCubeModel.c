#include "Common.h"
/* TEXTURE NEEDS FIX, OTHERWISE BAD LIGHTING. */
static struct ModelPart cube;

static void MagmaCubeModel_MakeParts(void) {
	BoxDesc_BuildBox(&cube, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(4,0,-4, -4,8,4) 
	});
}

static void MagmaCubeModel_Draw(struct Entity* e) {
	Model_ApplyTexture(e);
	Model_DrawPart(&cube);
	Model_UpdateVB();
}

static float MagmaCubeModel_GetNameY(struct Entity* e) { return 8 / 16.0f; }
static float MagmaCubeModel_GetEyeY(struct Entity* e) { return 6 / 16.0f; }
static void MagmaCubeModel_GetSize(struct Entity* e)   { _SetSize(14,14,14); }
static void MagmaCubeModel_GetBounds(struct Entity* e) { _SetBounds(-5,0,14, 5,16,9); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES];
static struct Model model = { 
	"magmacube", vertices, &magmaCube_tex,
	MagmaCubeModel_MakeParts, MagmaCubeModel_Draw,
	MagmaCubeModel_GetNameY,  MagmaCubeModel_GetEyeY,
	MagmaCubeModel_GetSize,   MagmaCubeModel_GetBounds
};

struct Model* MagmaCubeModel_GetInstance(void) {
	Model_Init(&model);
	return &model;
}