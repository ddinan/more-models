#include "Common.h"
static struct ModelPart head, rightHorn, leftHorn, torso, udder, leftLegFront, rightLegFront, leftLegBack, rightLegBack;

static void CowModel_MakeParts(void) {
	BoxDesc_BuildBox(&head, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-4,16,-14,4,24,-8),
		BoxDesc_Rot(0, 20, -6),
	});

	BoxDesc_BuildBox(&rightHorn, &(struct BoxDesc) {
		BoxDesc_Tex(22, 0),
		BoxDesc_Box(-5,22,-13, -4,25,-12),
		BoxDesc_Rot(0, 20, -6),
	});

	BoxDesc_BuildBox(&leftHorn, &(struct BoxDesc) {
		BoxDesc_Tex(22, 0),
		BoxDesc_Box(4,22,-13, 5,25,-12),
		BoxDesc_Rot(0, 20, -6),
	});

	BoxDesc_BuildBox(&leftLegFront, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(-2,0,-8, -6,12,-4),
		BoxDesc_Rot(0, 12, -6),
	});

	BoxDesc_BuildBox(&rightLegFront, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(2,0,-8, 6,12,-4),
		BoxDesc_Rot(0, 12, -6),
	});

	BoxDesc_BuildBox(&leftLegBack, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(-2,0,5, -6,12,9),
		BoxDesc_Rot(0, 12, 7),
	});

	BoxDesc_BuildBox(&rightLegBack, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(2,0,5, 6,12,9),
		BoxDesc_Rot(0, 12, 7),
	});

	BoxDesc_BuildRotatedBox(&torso, &(struct BoxDesc) {
		BoxDesc_Tex(18, 4),
		BoxDesc_Box(-6,12,-8, 6,22,10)
	});

	BoxDesc_BuildRotatedBox(&udder, &(struct BoxDesc) {
		BoxDesc_Tex(52, 0),
		BoxDesc_Box(-2,11,4, 2,12,10)
	});
}

static void CowModel_Draw(struct Entity* e) {
	Model_ApplyTexture(e);

	Model_DrawPart(&torso);
	Model_DrawPart(&udder);

	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &head,      true);
	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &leftHorn,  true);
	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &rightHorn, true);

	Model_DrawRotate(e->Anim.LeftLegX,  0, 0, &leftLegFront,  false);
	Model_DrawRotate(e->Anim.RightLegX, 0, 0, &rightLegFront, false);
	Model_DrawRotate(e->Anim.RightLegX, 0, 0, &leftLegBack,   false);
	Model_DrawRotate(e->Anim.LeftLegX,  0, 0, &rightLegBack,  false);

	Model_UpdateVB();
}

static float CowModel_GetNameY(struct Entity* e) { return 24 / 16.0f; }
static float CowModel_GetEyeY(struct Entity* e) { return 20 / 16.0f; }
static void CowModel_GetSize(struct Entity* e)   { _SetSize(14,14,14); }
static void CowModel_GetBounds(struct Entity* e) { _SetBounds(-5,0,-14, 5,16,9); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 9];
static struct Model model = { 
	"cow", vertices, &cow_tex,
	CowModel_MakeParts, CowModel_Draw,
	CowModel_GetNameY,  CowModel_GetEyeY,
	CowModel_GetSize,   CowModel_GetBounds
};

struct Model* CowModel_GetInstance(void) {
	Model_Init(&model);
	return &model;
}