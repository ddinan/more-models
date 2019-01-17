#include "Common.h"
struct ModelPart head, hat, torso, leftLeg, rightLeg, leftArm, rightArm;

void ZombiePigmanModel_MakeParts(void) {
	BoxDesc_BuildBox(&head, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-4,24,-4, 4,32,4),
		BoxDesc_Rot(0, 24, 0),
	});

	BoxDesc_BuildBox(&torso, &(struct BoxDesc) {
		BoxDesc_Tex(16, 16),
		BoxDesc_Box(-4,12,-2, 4,24,2)
	});

	BoxDesc_BuildBox(&leftLeg, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(0,0,-2, -4,12,2),
		BoxDesc_Rot(0, 12, 0)
	});

	BoxDesc_BuildBox(&rightLeg, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(0,0,-2, 4,12,2),
		BoxDesc_Rot(0, 12, 0)
	});

	BoxDesc_BuildBox(&leftArm, &(struct BoxDesc) {
		BoxDesc_Tex(40, 16),
		BoxDesc_Box(-4,12,-2, -8,24,2),
		BoxDesc_Rot(-6, 22, 0)
	});

	BoxDesc_BuildBox(&rightArm, &(struct BoxDesc) {
		BoxDesc_Tex(40, 16),
		BoxDesc_Box(4,12,-2, 8,24,2),
		BoxDesc_Rot(6, 22, 0)
	});

	BoxDesc_BuildBox(&hat, &(struct BoxDesc) {
		BoxDesc_Tex(32, 0),
		BoxDesc_Dims(-4,24,-4, 4,32,4),
		BoxDesc_Bounds(-4.5f,23.5f,-4.5f, 4.5f,32.5f,4.5f),
		BoxDesc_Rot(0, 24, 0)
	});
}

void ZombiePigmanModel_Draw(struct Entity* e) {
	Model_ApplyTexture(e);

	Model_DrawPart(&torso);
	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &head, true);
	Model_DrawRotate(e->Anim.LeftLegX,  0, 0, &leftLeg, false);
	Model_DrawRotate(e->Anim.RightLegX, 0, 0, &rightLeg, false);
	Model_DrawRotate(90 * MATH_DEG2RAD, 0, e->Anim.LeftArmZ,  &leftArm, false);
	Model_DrawRotate(90 * MATH_DEG2RAD, 0, e->Anim.RightArmZ, &rightArm, false);
	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &hat, true);

	Model_UpdateVB();
}	

float ZombiePigmanModel_GetNameY(struct Entity* e) { return 2.075f; }
float ZombiePigmanModel_GetEyeY(struct Entity* e)  { return 1.875f; }
void ZombiePigmanModel_GetSize(struct Entity* e)   { _SetSize(8.6f,28.1f,8.6f); }
void ZombiePigmanModel_GetBounds(struct Entity* e) { _SetBounds(-4,0,-4, 4,32,4); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 7];
static struct Model model = { 
	"zombiepigman", vertices, &zombiePigman_tex,
	ZombiePigmanModel_MakeParts, ZombiePigmanModel_Draw,
	ZombiePigmanModel_GetNameY,  ZombiePigmanModel_GetEyeY,
	ZombiePigmanModel_GetSize,   ZombiePigmanModel_GetBounds
};

struct Model* ZombiePigmanModel_GetInstance(void) {
	Model_Init(&model);
	return &model;
}