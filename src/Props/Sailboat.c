#include "Common.h"
static struct ModelPart base, front, cabin, mast, horMast, frontSail, backSail;

static void SailBoatModel_MakeParts(void) {
	BoxDesc_BuildRotatedBox(&base, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-15, -2, -30, 15, 13, 35)
	});

	BoxDesc_BuildRotatedBox(&front, &(struct BoxDesc) {
		BoxDesc_Tex(90, 0),
		BoxDesc_Box(-6, 3, -40, 6, 18, -30)
	});

	BoxDesc_BuildRotatedBox(&cabin, &(struct BoxDesc) {
		BoxDesc_Tex(90, 40),
			BoxDesc_Box(-6, 13, -10, 6, 21, 10)
	});

	BoxDesc_BuildRotatedBox(&mast, &(struct BoxDesc) {
		BoxDesc_Tex(150, 0),
			BoxDesc_Box(-1, 13, -1, 1, 58, 1)
	});

	BoxDesc_BuildRotatedBox(&horMast, &(struct BoxDesc) {
		BoxDesc_Tex(150, 0),
			BoxDesc_Box(-1, 22, -40, 1, 23, 35)
	});

	BoxDesc_BuildRotatedBox(&frontSail, &(struct BoxDesc) {
		BoxDesc_Tex(0, 85),
		BoxDesc_Box(0, 13,-35, 0, 58,-1)
	});

	BoxDesc_BuildRotatedBox(&backSail, &(struct BoxDesc) {
		BoxDesc_Tex(100, 85),
		BoxDesc_Box(0, 13, 1, 0, 58, 35)
	});


}

static void SailBoatModel_Draw(struct Entity *e) {
	Model_ApplyTexture(e);
	Models.uScale = 1 / 256.0f;
	Models.vScale = 1 / 256.0f;

	Model_DrawPart(&base);
	Model_DrawPart(&front);
	Model_DrawPart(&cabin);
	Model_DrawPart(&mast);
	Model_DrawPart(&horMast);
	Model_DrawPart(&frontSail);
	Model_DrawPart(&backSail);

	Model_UpdateVB();
}

static float SailBoatModel_GetNameY(struct Entity *e) { e; return 2.375f; }
static float SailBoatModel_GetEyeY(struct Entity *e) { e; return 1.750f; }
static void SailBoatModel_GetSize(struct Entity *e)   { _SetSize(32,40,32); }
static void SailBoatModel_GetBounds(struct Entity *e) { _SetBounds(-15,-2,-40, 15,58,35); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 6];
static struct Model model = {
	"sailBoat", vertices, &sailBoat_tex,
	SailBoatModel_MakeParts, SailBoatModel_Draw,
	SailBoatModel_GetNameY,  SailBoatModel_GetEyeY,
	SailBoatModel_GetSize,   SailBoatModel_GetBounds
};

struct Model* SailBoatModel_GetInstance(void) {
	Model_Init(&model);
	model.bobbing = false;
	model.groundFriction = (Vec3) { 1.05f, 1.05f, 1.05f };
	return &model;
}