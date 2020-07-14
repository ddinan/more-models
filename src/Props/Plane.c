#include "Common.h"
static struct ModelPart body, cabin, wing, wingUp, wingBackR, wingBackL, baseLeft, baseRight, baseBack, tail, axe;
static struct ModelPart blade1, blade2, tireLeft, tireRight, tireBack, mastFR, mastFL, mastRR, mastRL;

static void PlaneModel_MakeParts(void) {
	BoxDesc_BuildRotatedBox(&body, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-7, 8, -40, 7, 18, 40)
	});

	BoxDesc_BuildRotatedBox(&cabin, &(struct BoxDesc) {
		BoxDesc_Tex(50, 50),
		BoxDesc_Box(-6,11,-13, 6,25,7)
	});

	BoxDesc_BuildRotatedBox(&wing, &(struct BoxDesc) {
		BoxDesc_Tex(50, 0),
		BoxDesc_Box(-40, 9, -18, 40, 10, 2)
	});

	BoxDesc_BuildRotatedBox(&wingUp, &(struct BoxDesc) {
		BoxDesc_Tex(50, 25),
		BoxDesc_Box(-40, 25, -18, 40, 26, 2)
	});

	BoxDesc_BuildRotatedBox(&wingBackL, &(struct BoxDesc) {
		BoxDesc_Tex(135, 50),
		BoxDesc_Box(-15, 9, 31, -7, 10, 39)
	});

	BoxDesc_BuildRotatedBox(&wingBackR, &(struct BoxDesc) {
		BoxDesc_Tex(135, 60),
		BoxDesc_Box(7, 9, 31, 15, 10, 39)
	});

	BoxDesc_BuildBox(&baseLeft, &(struct BoxDesc) {
		BoxDesc_Tex(130, 70),
		BoxDesc_Box(-4, 2, -15, -3, 8, -14)
	});

	BoxDesc_BuildBox(&baseRight, &(struct BoxDesc) {
		BoxDesc_Tex(130, 70), 
		BoxDesc_Box(3, 2, -15, 4, 8, -14)
	});

	BoxDesc_BuildBox(&baseBack, &(struct BoxDesc) {
		BoxDesc_Tex(130, 70),
		BoxDesc_Box(0, 2, 34, 1, 8, 35)
	});

	BoxDesc_BuildBox(&tail, &(struct BoxDesc) {
		BoxDesc_Tex(110, 50),
		BoxDesc_Box(-1, 18, 31, 1, 27, 39)
	});

	BoxDesc_BuildBox(&axe, &(struct BoxDesc) {
		BoxDesc_Tex(170, 50),
		BoxDesc_Box(-1, 12, -44, 1, 14, -40),
	});

	BoxDesc_BuildBox(&blade1, &(struct BoxDesc) {
		BoxDesc_Tex(135, 70),
		BoxDesc_Box(-9, 12, -42, 9, 14, -41),
		BoxDesc_Rot(0, 13, -41)
	});

	BoxDesc_BuildBox(&blade2, &(struct BoxDesc) {
		BoxDesc_Tex(135, 75),
		BoxDesc_Box(-1, 4, -42, 1, 22, -41),
		BoxDesc_Rot(0, 13, -41)
	});

	BoxDesc_BuildBox(&tireLeft, &(struct BoxDesc) {
		BoxDesc_Tex(110, 70),
		BoxDesc_Box(-6, 0, -17, -4, 5, -12),
	});

	BoxDesc_BuildBox(&tireRight, &(struct BoxDesc) {
		BoxDesc_Tex(110, 70),
		BoxDesc_Box(4, 0, -17, 6, 5, -12),
	});

	BoxDesc_BuildBox(&tireBack, &(struct BoxDesc) {
		BoxDesc_Tex(110, 70),
		BoxDesc_Box(-2, 0, 32, 0, 5, 37),
	});

	BoxDesc_BuildBox(&mastFL, &(struct BoxDesc) {
		BoxDesc_Tex(160, 50),
		BoxDesc_Box(-38, 10, -16, -37, 25, -15)
	});

	BoxDesc_BuildBox(&mastFR, &(struct BoxDesc) {
		BoxDesc_Tex(160, 50),
		BoxDesc_Box(37, 10, -16, 38, 25, -15)
	});

	BoxDesc_BuildBox(&mastRL, &(struct BoxDesc) {
		BoxDesc_Tex(160, 50),
		BoxDesc_Box(-38, 10, -1, -37, 25, 0)
	});

	BoxDesc_BuildBox(&mastRR, &(struct BoxDesc) {
		BoxDesc_Tex(160, 50),
		BoxDesc_Box(37, 10, -1, 38, 25, 0)
	});
}


static void PlaneModel_Draw(struct Entity *e) {
	Model_ApplyTexture(e);
	Models.uScale = 1/256.0f;
	Models.vScale = 1/128.0f;

	Model_DrawPart(&body);
	Model_DrawPart(&cabin);
	Model_DrawPart(&wing);
	Model_DrawPart(&wingUp);
	Model_DrawPart(&wingBackL);
	Model_DrawPart(&wingBackR);
	Model_DrawPart(&baseLeft);
	Model_DrawPart(&baseRight);
	Model_DrawPart(&baseBack);
	Model_DrawPart(&tail);
	Model_DrawPart(&axe);
	Model_DrawPart(&tireLeft);
	Model_DrawPart(&tireRight);
	Model_DrawPart(&tireBack);
	Model_DrawPart(&mastFL);
	Model_DrawPart(&mastRL);
	Model_DrawPart(&mastFR);
	Model_DrawPart(&mastRR);

	Model_DrawRotate(0, 0, -e->Anim.WalkTime, &blade1, false);
	Model_DrawRotate(0, 0, -e->Anim.WalkTime, &blade2, false);
	//Model_DrawRotate(-e->Anim.WalkTime + (MATH_PI / 2), 0, 0, &tBlade2, false);

	Model_UpdateVB();
}	

static float PlaneModel_GetNameY(struct Entity *e) { e; return 2.375f; }
static float PlaneModel_GetEyeY(struct Entity *e) { e; return 1.750f; }
static void PlaneModel_GetSize(struct Entity *e) { _SetSize(32, 31, 32); }
static void PlaneModel_GetBounds(struct Entity *e) { _SetBounds(-40, 0, -40, 40, 30, 40); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 15];
static struct Model model = { 
	"Plane", vertices, &plane_tex,
	PlaneModel_MakeParts, PlaneModel_Draw,
	PlaneModel_GetNameY,  PlaneModel_GetEyeY,
	PlaneModel_GetSize,   PlaneModel_GetBounds
};

struct Model* PlaneModel_GetInstance(void) {
	Model_Init(&model);
	model.bobbing = false;
	model.groundFriction = (Vec3) { 1.05f, 1.05f, 1.05f };
	return &model;
}
