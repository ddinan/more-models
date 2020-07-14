#include "Common.h"
static struct ModelPart top, body, tireFrontLeft, tireFrontRight, tireBackLeft, tireBackRight, frontLeft, frontRight;
static struct ModelPart backLeft, backRight, mirrorLeft, mirrorRight, box;

static void TruckModel_MakeParts(void) {
	
	BoxDesc_BuildRotatedBox(&top, &(struct BoxDesc) {
		BoxDesc_Tex(120, 0),
			BoxDesc_Box(-20, 20, -16, 20, 36, 0)
	});

	BoxDesc_BuildRotatedBox(&box, &(struct BoxDesc) {
		BoxDesc_Tex(120, 32),
			BoxDesc_Box(-20, 20, 0, 20, 36, 40)
	});

	BoxDesc_BuildRotatedBox(&body, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-22,4,-40, 22,20,40)
	});

	BoxDesc_BuildRotatedBox(&body, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
			BoxDesc_Box(-22, 4, -40, 22, 20, 40)
	});

	BoxDesc_BuildBox(&tireFrontLeft, &(struct BoxDesc) {
		BoxDesc_Tex(156, 64+38),
		BoxDesc_Box(-20,0,-28, -24,10,-18),
		BoxDesc_Rot(-22, 5, -23)
	});

	BoxDesc_BuildBox(&tireBackLeft, &(struct BoxDesc) {
		BoxDesc_Tex(156, 64+38),
		BoxDesc_Box(-20,0,18, -24,10,28),
		BoxDesc_Rot(-22, 5, 23)
	});

	BoxDesc_BuildBox(&mirrorLeft, &(struct BoxDesc) {
		BoxDesc_Tex(202, 52+38),
		BoxDesc_Box(-20,20,-15, -27,25,-18),
		BoxDesc_Rot(-20, 22, -15)
	});

	BoxDesc_BuildBox(&tireFrontRight, &(struct BoxDesc) {
		BoxDesc_Tex(156, 64+38),
		BoxDesc_Box(20,0,-28, 24,10,-18),
		BoxDesc_Rot(22, 5, -23)
	});

	BoxDesc_BuildBox(&tireBackRight, &(struct BoxDesc) {
		BoxDesc_Tex(156, 64+38),
		BoxDesc_Box(20,0,18, 24,10,28),
		BoxDesc_Rot(22, 5, 23)
	});

	BoxDesc_BuildBox(&mirrorRight, &(struct BoxDesc) {
		BoxDesc_Tex(202, 52+38),
		BoxDesc_Box(20,20,-15, 27,25,-18),
		BoxDesc_Rot(20, 22, -15)
	});

	BoxDesc_BuildBox(&frontLeft, &(struct BoxDesc) {
		BoxDesc_Tex(120, 64+38),
		BoxDesc_Box(-20,4,-31, -22,14,-15)
	});

	BoxDesc_BuildBox(&backLeft, &(struct BoxDesc) {
		BoxDesc_Tex(120, 64+38),
		BoxDesc_Box(-20,4,15, -22,14,31)
	});

	BoxDesc_BuildBox(&frontRight, &(struct BoxDesc) {
		BoxDesc_Tex(120, 64+38),
		BoxDesc_Box(20,4,-31, 22,14,-15)
	});

	BoxDesc_BuildBox(&backRight, &(struct BoxDesc) {
		BoxDesc_Tex(120, 64+38),
		BoxDesc_Box(20,4,15, 22,14,31)
	});
}

static void TruckModel_Draw(struct Entity *e) {
	Model_ApplyTexture(e);
	Models.uScale = 1/256.0f;
	Models.vScale = 1/128.0f;

	Model_DrawPart(&top);
	Model_DrawPart(&body);
	Model_DrawPart(&frontLeft);
	Model_DrawPart(&frontRight);
	Model_DrawPart(&backLeft);
	Model_DrawPart(&backRight);	
	Model_DrawPart(&box);

	Model_DrawRotate(-e->Anim.WalkTime, 0, 0, &tireFrontLeft,  false);
	Model_DrawRotate(-e->Anim.WalkTime, 0, 0, &tireFrontRight, false);
	Model_DrawRotate(-e->Anim.WalkTime, 0, 0, &tireBackLeft,   false);
	Model_DrawRotate(-e->Anim.WalkTime, 0, 0, &tireBackRight,  false);
	Model_DrawRotate(0,  MATH_PI / 12, 0, &mirrorLeft,   false);
	Model_DrawRotate(0, -MATH_PI / 12, 0, &mirrorRight,  false);

	Model_UpdateVB();
}	

static float TruckModel_GetNameY(struct Entity *e) { e; return 2.375f; }
static float TruckModel_GetEyeY(struct Entity *e) { e; return 1.750f; }
static void TruckModel_GetSize(struct Entity *e) { _SetSize(32, 31, 32); }
static void TruckModel_GetBounds(struct Entity *e) { _SetBounds(-22, 0, -40, 22, 36, 40); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 15];
static struct Model model = { 
	"truck", vertices, &truck_tex,
	TruckModel_MakeParts, TruckModel_Draw,
	TruckModel_GetNameY,  TruckModel_GetEyeY,
	TruckModel_GetSize,   TruckModel_GetBounds
};

struct Model* TruckModel_GetInstance(void) {
	Model_Init(&model);
	model.bobbing = false;
	model.groundFriction = (Vec3) { 1.05f, 1.05f, 1.05f };
	return &model;
}
