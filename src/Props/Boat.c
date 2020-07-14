#include "Common.h"
static struct ModelPart base, front, cabin;

static void BoatModel_MakeParts(void) {
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
			BoxDesc_Box(-10, 13, 15, 10, 30, 30)
	});

}

static void BoatModel_Draw(struct Entity *e) {
	Model_ApplyTexture(e);
	Models.uScale = 1 / 256.0f;
	Models.vScale = 1 / 128.0f;

	Model_DrawPart(&base);
	Model_DrawPart(&front);
	Model_DrawPart(&cabin);

	Model_UpdateVB();
}

static float BoatModel_GetNameY(struct Entity *e) { e; return 2.375f; }
static float BoatModel_GetEyeY(struct Entity *e) { e; return 1.750f; }
static void BoatModel_GetSize(struct Entity *e) { _SetSize(32, 40, 32); }
static void BoatModel_GetBounds(struct Entity *e) { _SetBounds(-15, -2, -40, 15, 58, 35); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 6];
static struct Model model = {
	"boat", vertices, &boat_tex,
	BoatModel_MakeParts, BoatModel_Draw,
	BoatModel_GetNameY,  BoatModel_GetEyeY,
	BoatModel_GetSize,   BoatModel_GetBounds
};

struct Model* BoatModel_GetInstance(void) {
	Model_Init(&model);
	model.bobbing = false;
	model.groundFriction = (Vec3) { 1.05f, 1.05f, 1.05f };
	return &model;
}