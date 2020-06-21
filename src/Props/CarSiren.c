#include "Common.h"
static struct ModelPart top, body, tireFrontLeft, tireFrontRight, tireBackLeft, tireBackRight, frontLeft, frontRight;
static struct ModelPart backLeft, backRight, mirrorLeft, mirrorRight, spoilerLeft, spoilerRight, spoiler, siren;

static void CarSirenModel_MakeParts(void) {
	BoxDesc_BuildRotatedBox(&top, &(struct BoxDesc) {
		BoxDesc_Tex(120, 0),
		BoxDesc_Box(-20,20,-16, 20,36,20)
	});

	BoxDesc_BuildRotatedBox(&body, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-22,4,-40, 22,20,40)
	});

	BoxDesc_BuildRotatedBox(&siren, &(struct BoxDesc) {
		BoxDesc_Tex(0, 116),
		BoxDesc_Box(-14, 36, -12, 14, 38, -7)
	});

	BoxDesc_BuildRotatedBox(&spoiler, &(struct BoxDesc) {
		BoxDesc_Tex(120, 52),
		BoxDesc_Box(-19,27,33, 19,30,42)
	});

	BoxDesc_BuildBox(&tireFrontLeft, &(struct BoxDesc) {
		BoxDesc_Tex(156, 64),
		BoxDesc_Box(-20,0,-28, -24,10,-18),
		BoxDesc_Rot(-22, 5, -23)
	});

	BoxDesc_BuildBox(&tireBackLeft, &(struct BoxDesc) {
		BoxDesc_Tex(156, 64),
		BoxDesc_Box(-20,0,18, -24,10,28),
		BoxDesc_Rot(-22, 5, 23)
	});

	BoxDesc_BuildBox(&mirrorLeft, &(struct BoxDesc) {
		BoxDesc_Tex(202, 52),
		BoxDesc_Box(-20,20,-15, -27,25,-18),
		BoxDesc_Rot(-20, 22, -15)
	});

	BoxDesc_BuildBox(&spoilerLeft, &(struct BoxDesc) {
		BoxDesc_Tex(184, 64),
		BoxDesc_Box(-12,20,31, -15,30,34),
		BoxDesc_Rot(-12, 20, 31)
	});

	BoxDesc_BuildBox(&tireFrontRight, &(struct BoxDesc) {
		BoxDesc_Tex(156, 64),
		BoxDesc_Box(20,0,-28, 24,10,-18),
		BoxDesc_Rot(22, 5, -23)
	});

	BoxDesc_BuildBox(&tireBackRight, &(struct BoxDesc) {
		BoxDesc_Tex(156, 64),
		BoxDesc_Box(20,0,18, 24,10,28),
		BoxDesc_Rot(22, 5, 23)
	});

	BoxDesc_BuildBox(&mirrorRight, &(struct BoxDesc) {
		BoxDesc_Tex(202, 52),
		BoxDesc_Box(20,20,-15, 27,25,-18),
		BoxDesc_Rot(20, 22, -15)
	});

	BoxDesc_BuildBox(&spoilerRight, &(struct BoxDesc) {
		BoxDesc_Tex(184, 64),
		BoxDesc_Box(12,20,31, 15,30,34),
		BoxDesc_Rot(12, 20, 31)
	});

	BoxDesc_BuildBox(&frontLeft, &(struct BoxDesc) {
		BoxDesc_Tex(120, 64),
		BoxDesc_Box(-20,4,-31, -22,14,-15)
	});

	BoxDesc_BuildBox(&backLeft, &(struct BoxDesc) {
		BoxDesc_Tex(120, 64),
		BoxDesc_Box(-20,4,15, -22,14,31)
	});

	BoxDesc_BuildBox(&frontRight, &(struct BoxDesc) {
		BoxDesc_Tex(120, 64),
		BoxDesc_Box(20,4,-31, 22,14,-15)
	});

	BoxDesc_BuildBox(&backRight, &(struct BoxDesc) {
		BoxDesc_Tex(120, 64),
		BoxDesc_Box(20,4,15, 22,14,31)
	});
}

static void CarSirenModel_Draw(struct Entity *e) {
	Model_ApplyTexture(e);
	Models.uScale = 1/256.0f;
	Models.vScale = 1/128.0f;

	Model_DrawPart(&top);
	Model_DrawPart(&body);
	Model_DrawPart(&frontLeft);
	Model_DrawPart(&frontRight);
	Model_DrawPart(&backLeft);
	Model_DrawPart(&backRight);
	Model_DrawPart(&spoiler);
	Model_DrawPart(&siren);

	Model_DrawRotate(-e->Anim.WalkTime, 0, 0, &tireFrontLeft,  false);
	Model_DrawRotate(-e->Anim.WalkTime, 0, 0, &tireFrontRight, false);
	Model_DrawRotate(-e->Anim.WalkTime, 0, 0, &tireBackLeft,   false);
	Model_DrawRotate(-e->Anim.WalkTime, 0, 0, &tireBackRight,  false);
	Model_DrawRotate(0,  MATH_PI / 12, 0, &mirrorLeft,   false);
	Model_DrawRotate(0, -MATH_PI / 12, 0, &mirrorRight,  false);
	Model_DrawRotate(MATH_PI / 6, 0, 0,   &spoilerLeft,  false);
	Model_DrawRotate(MATH_PI / 6, 0, 0,   &spoilerRight, false);

	Model_UpdateVB();
}	

static float CarSirenModel_GetNameY(struct Entity *e) { e; return 2.375f; }
static float CarSirenModel_GetEyeY(struct Entity *e) { e; return 1.750f; }
static void CarSirenModel_GetSize(struct Entity *e) { _SetSize(32, 31, 32); }
static void CarSirenModel_GetBounds(struct Entity *e) { _SetBounds(-22, 0, -40, 22, 38, 40); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 15];
static struct Model model = { 
	"carSiren", vertices, &carSiren_tex,
	CarSirenModel_MakeParts, CarSirenModel_Draw,
	CarSirenModel_GetNameY,  CarSirenModel_GetEyeY,
	CarSirenModel_GetSize,   CarSirenModel_GetBounds
};

struct Model* CarSirenModel_GetInstance(void) {
	Model_Init(&model);
	model.bobbing = false;
	model.groundFriction = (Vec3) { 1.05f, 1.05f, 1.05f };
	return &model;
}
