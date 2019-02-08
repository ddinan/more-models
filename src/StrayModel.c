#include "Common.h"
static struct ModelPart head, torso, leftLeg, rightLeg, leftArm, rightArm;

static void StrayModel_MakeParts(void) {
	BoxDesc_BuildBox(&head, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-4,24,-4, 4,32,4),
		BoxDesc_Rot(0, 24, 0)
	});

	BoxDesc_BuildBox(&torso, &(struct BoxDesc) {
		BoxDesc_Tex(16, 16),
		BoxDesc_Box(-4,12,-2, 4,24,2)
	});

	BoxDesc_BuildBox(&leftLeg, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(-1,0,-1, -3,12,1),
		BoxDesc_Rot(0, 12, 0)
	});

	BoxDesc_BuildBox(&rightLeg, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(1,0,-1, 3,12,1),
		BoxDesc_Rot(0, 12, 0)
	});

	BoxDesc_BuildBox(&leftArm, &(struct BoxDesc) {
		BoxDesc_Tex(40, 16),
		BoxDesc_Box(-4,12,-1, -6,24,1),
		BoxDesc_Rot(-5, 23, 0)
	});

	BoxDesc_BuildBox(&rightArm, &(struct BoxDesc) {
		BoxDesc_Tex(40, 16),
		BoxDesc_Box(4,12,-1, 6,24,1),
		BoxDesc_Rot(5, 23, 0)
	});
}

static void StrayModel_Draw(struct Entity* e) {
	Model_ApplyTexture(e);

	Model_DrawPart(&torso);
	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &head, true);
	Model_DrawRotate(e->Anim.LeftLegX,  0, 0, &leftLeg, false);
	Model_DrawRotate(e->Anim.RightLegX, 0, 0, &rightLeg, false);
	Model_DrawRotate(90 * MATH_DEG2RAD, 0, e->Anim.LeftArmZ,  &leftArm, false);
	Model_DrawRotate(90 * MATH_DEG2RAD, 0, e->Anim.RightArmZ, &rightArm, false);

	Model_UpdateVB();
}	

static float StrayModel_GetNameY(struct Entity* e) { return 2.075f; }
static float StrayModel_GetEyeY(struct Entity* e)  { return 1.625f; }
static void StrayModel_GetSize(struct Entity* e)   { _SetSize(8,30,8); }
static void StrayModel_GetBounds(struct Entity* e) { _SetBounds(-4,0,-4, 4,32,4); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 6];
static struct Model model = { 
	"stray", vertices, &stray_tex,
	StrayModel_MakeParts, StrayModel_Draw,
	StrayModel_GetNameY,  StrayModel_GetEyeY,
	StrayModel_GetSize,   StrayModel_GetBounds
};

struct Model* StrayModel_GetInstance(void) {
	Model_Init(&model);
	return &model;
}