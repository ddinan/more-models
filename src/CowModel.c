#include "Common.h"
struct ModelPart head, rightHorn, leftHorn, torso, udder, leftLegFront, rightLegFront, leftLegBack, rightLegBack;

void CowModel_MakeParts(void) {
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
		BoxDesc_Box(-6,0,-8, -2,12,-4),
		BoxDesc_Rot(0, 12, -5),
	});

	BoxDesc_BuildBox(&rightLegFront, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(2,0,-8, 6,12,-4),
		BoxDesc_Rot(0, 12, -5),
	});

	BoxDesc_BuildBox(&leftLegBack, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(-6,0,5, -2,12,9),
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

	BoxDesc_BuildBox(&udder, &(struct BoxDesc) {
		BoxDesc_Tex(52, 0),
		BoxDesc_Box(-2,11,4, 2,12,10)
	});
}

void CowModel_Draw(struct Entity* e) {
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

float CowModel_GetNameY(struct Entity* e) { return 24/16.0f; }
float CowModel_GetEyeY(struct Entity* e)  { return 12/16.0f; }
void CowModel_GetSize(struct Entity* e)   { _SetSize(14,14,14); }
void CowModel_GetBounds(struct Entity* e) { _SetBounds(-5,0,-14, 5,16,9); }

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