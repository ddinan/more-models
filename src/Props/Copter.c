#include "Common.h"
static struct ModelPart body, boom, skidLeft, skidRight, baseLeft, baseRight, mast, tail, tMast;
static struct ModelPart blade1, blade2, tBlade1, tBlade2;

static void CopterModel_MakeParts(void) {
	BoxDesc_BuildRotatedBox(&body, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-20,8,-40, 20,40,-10)
	});

	BoxDesc_BuildRotatedBox(&boom, &(struct BoxDesc) {
		BoxDesc_Tex(145, 0),
		BoxDesc_Box(-4,8,-10, 4,18,40)
	});

	BoxDesc_BuildRotatedBox(&skidLeft, &(struct BoxDesc) {
		BoxDesc_Tex(0, 80),
		BoxDesc_Box(-20,0,-40,-15,1,5)
	});

	BoxDesc_BuildBox(&skidRight, &(struct BoxDesc) {
		BoxDesc_Tex(1, 82),
		BoxDesc_Box(15, 0, -40, 20, 1, 5)
	});

	BoxDesc_BuildBox(&baseLeft, &(struct BoxDesc) {
		BoxDesc_Tex(14, 81),
		BoxDesc_Box(-20, 1, -30, -15, 8, -20)
	});

	BoxDesc_BuildBox(&baseRight, &(struct BoxDesc) {
		BoxDesc_Tex(14, 81),
		BoxDesc_Box(15, 1, -30, 20, 8, -20)
	});

	BoxDesc_BuildBox(&mast, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-1, 40, -26, 1, 43, -24)
	});

	BoxDesc_BuildBox(&tail, &(struct BoxDesc) {
		BoxDesc_Tex(80, 80),
		BoxDesc_Box(-1, 18, 34, 1, 30, 40)
	});

	BoxDesc_BuildBox(&blade1, &(struct BoxDesc) {
		//BoxDesc_Tex(0, 62),
		//BoxDesc_Box(-30, 43, -28, 30, 44, -22),
		BoxDesc_Tex(111, 71),
		BoxDesc_Box(-3, 43, -50, 3, 44, 0),
		BoxDesc_Rot(-0, 43, -25)
	});


	BoxDesc_BuildBox(&blade2, &(struct BoxDesc) {
		BoxDesc_Tex(0, 62),
		BoxDesc_Box(-30, 43, -28, 30, 44, -22),
		BoxDesc_Rot(-0, 43, -25)
	});

	BoxDesc_BuildBox(&tMast, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-5, 12, 36, -4, 13, 37)
	});

	BoxDesc_BuildBox(&tBlade1, &(struct BoxDesc) {
		BoxDesc_Tex(12, 101),
		BoxDesc_Box(-6, 11, 29, -5, 14, 44),
		BoxDesc_Rot(-6, 13, 37)
	});


	BoxDesc_BuildBox(&tBlade2, &(struct BoxDesc) {
		BoxDesc_Tex(12, 101),
		BoxDesc_Box(-6, 5, 35, -5, 20, 38),
		BoxDesc_Rot(-6, 13, 37)
	});
}


static void CopterModel_Draw(struct Entity *e) {
	Model_ApplyTexture(e);
	Models.uScale = 1/256.0f;
	Models.vScale = 1/128.0f;

	Model_DrawPart(&body);
	Model_DrawPart(&boom);
	Model_DrawPart(&skidLeft);
	Model_DrawPart(&skidRight);
	Model_DrawPart(&baseLeft);
	Model_DrawPart(&baseRight);
	Model_DrawPart(&mast);
	Model_DrawPart(&tMast);
	Model_DrawPart(&tail);


	Model_DrawRotate(0, -e->Anim.WalkTime, 0, &blade1, false);
	Model_DrawRotate(0, -e->Anim.WalkTime, 0, &blade2, false);
	Model_DrawRotate(-e->Anim.WalkTime, 0, 0, &tBlade1, false);
	Model_DrawRotate(-e->Anim.WalkTime, 0, 0, &tBlade2, false);
	//Model_DrawRotate(-e->Anim.WalkTime + (MATH_PI / 2), 0, 0, &tBlade2, false);

	Model_UpdateVB();
}	

static float CopterModel_GetNameY(struct Entity *e) { e; return 2.375f; }
static float CopterModel_GetEyeY(struct Entity *e) { e; return 1.750f; }
static void CopterModel_GetSize(struct Entity *e) { _SetSize(32, 31, 32); }
static void CopterModel_GetBounds(struct Entity *e) { _SetBounds(-22, 0, -40, 22, 36, 40); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 15];
static struct Model model = { 
	"copter", vertices, &copter_tex,
	CopterModel_MakeParts, CopterModel_Draw,
	CopterModel_GetNameY,  CopterModel_GetEyeY,
	CopterModel_GetSize,   CopterModel_GetBounds
};

struct Model* CopterModel_GetInstance(void) {
	Model_Init(&model);
	model.bobbing = false;
	model.groundFriction = (Vec3) { 1.05f, 1.05f, 1.05f };
	return &model;
}
