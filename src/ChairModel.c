#include "Common.h"
static struct ModelPart base, back, leftLegFront, rightLegFront, leftLegBack, rightLegBack;

static void ChairModel_MakeParts(void) {
	// all the parts have a TexOrigin of 0,0
	struct BoxDesc box_base   = { 0,0, BoxDesc_Box(-8,12,-8,  8,14, 8) };
	struct BoxDesc box_back   = { 0,0, BoxDesc_Box( 8,14, 8, -8,30, 6) }; 
	struct BoxDesc box_lFront = { 0,0, BoxDesc_Box(-6,12,-6, -8, 0,-8) }; 
	struct BoxDesc box_rFront = { 0,0, BoxDesc_Box( 8,12,-6,  6, 0,-8) }; 
	struct BoxDesc box_lBack  = { 0,0, BoxDesc_Box(-6,12, 8, -8, 0, 6) }; 
	struct BoxDesc box_rBack  = { 0,0, BoxDesc_Box( 8,12, 6,  6, 0, 8) };
	
	BoxDesc_BuildBox(&base,          &box_base);
	BoxDesc_BuildBox(&back,          &box_back);
	BoxDesc_BuildBox(&leftLegFront,  &box_lFront);
	BoxDesc_BuildBox(&rightLegFront, &box_rFront);
	BoxDesc_BuildBox(&leftLegBack,   &box_lBack);
	BoxDesc_BuildBox(&rightLegBack,  &box_rBack);
}

static void ChairModel_Draw(struct Entity* entity) {
	Model_ApplyTexture(entity);
	Models.uScale = 1/16.0f; Models.vScale = 1/16.0f;

	Model_DrawPart(&base);
	Model_DrawPart(&back);
	Model_DrawPart(&leftLegFront);
	Model_DrawPart(&rightLegFront);
	Model_DrawPart(&leftLegBack);
	Model_DrawPart(&rightLegBack);

	Model_UpdateVB();
}

static float ChairModel_GetNameY(struct Entity* e) { return 2.00f; }
static float ChairModel_GetEyeY(struct Entity* e)  { return 1.25f; }
static void ChairModel_GetSize(struct Entity* e)   { _SetSize(14,14,14); }
static void ChairModel_GetBounds(struct Entity* e) { _SetBounds(-5,0,14, 5,16,9); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 6];
static struct Model model = {
	"chair", vertices, &wood_tex,
	ChairModel_MakeParts, ChairModel_Draw,
	ChairModel_GetNameY,  ChairModel_GetEyeY,
	ChairModel_GetSize,   ChairModel_GetBounds
};

struct Model* ChairModel_GetInstance(void) {
	Model_Init(&model);
	return &model;
}