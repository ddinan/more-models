#include "Common.h"
static struct ModelPart base, leftLegFront, rightLegFront, leftLegBack, rightLegBack;

static void TableModel_MakeParts(void) {
	// all the parts have a TexOrigin of 0,0
	struct BoxDesc box_base   = { 0,0, BoxDesc_Box(-9,14,-9,  9,16, 9) };
	struct BoxDesc box_lFront = { 0,0, BoxDesc_Box(-6, 0,-6, -8,14,-8) }; 
	struct BoxDesc box_rFront = { 0,0, BoxDesc_Box( 6, 0,-6,  8,14,-8) }; 
	struct BoxDesc box_lBack  = { 0,0, BoxDesc_Box(-6, 0, 6, -8,14, 8) }; 
	struct BoxDesc box_rBack  = { 0,0, BoxDesc_Box( 6, 0, 6,  8,14, 8) };
	
	BoxDesc_BuildBox(&base,          &box_base);
	BoxDesc_BuildBox(&leftLegFront,  &box_lFront);
	BoxDesc_BuildBox(&rightLegFront, &box_rFront);
	BoxDesc_BuildBox(&leftLegBack,   &box_lBack);
	BoxDesc_BuildBox(&rightLegBack,  &box_rBack);
}

static void TableModel_Draw(struct Entity* entity) {
	Model_ApplyTexture(entity);
	Models.uScale = 1/16.0f; Models.vScale = 1/16.0f;

	Model_DrawPart(&base);
	Model_DrawPart(&leftLegFront);
	Model_DrawPart(&rightLegFront);
	Model_DrawPart(&leftLegBack);
	Model_DrawPart(&rightLegBack);

	Model_UpdateVB();
}

static float TableModel_GetNameY(struct Entity* e) { return 1.50f; }
static float TableModel_GetEyeY(struct Entity* e) { return 0.875f; }
static void TableModel_GetSize(struct Entity* e)   { _SetSize(14,15,14); }
static void TableModel_GetBounds(struct Entity* e) { _SetBounds(-8,0,8, -8,16,8); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 5];
static struct Model model = {
	"table", vertices, &wood_tex,
	TableModel_MakeParts, TableModel_Draw,
	TableModel_GetNameY,  TableModel_GetEyeY,
	TableModel_GetSize,   TableModel_GetBounds
};

struct Model* TableModel_GetInstance(void) {
	Model_Init(&model);
	return &model;
}