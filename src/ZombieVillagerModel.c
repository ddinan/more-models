#include "Common.h"
static struct ModelPart head, nose, hat, torso, leftLeg, rightLeg, leftArm, rightArm;

static void ZombieVillagerModel_MakeParts(void) {
	BoxDesc_BuildBox(&head, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-4,24,-4, 4,34,4),
		BoxDesc_Rot(0, 24, 0),
	});

	BoxDesc_BuildBox(&nose, &(struct BoxDesc) {
		BoxDesc_Tex(24, 32),
		BoxDesc_Box(1,27,-4, -1,23,-5),
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

static void ZombieVillagerModel_Draw(struct Entity* e) {
	Model_ApplyTexture(e);

	Model_DrawPart(&torso);
	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &head, true);
	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &nose, true);
	Model_DrawRotate(e->Anim.LeftLegX,  0, 0, &leftLeg, false);
	Model_DrawRotate(e->Anim.RightLegX, 0, 0, &rightLeg, false);
	Model_DrawRotate(90 * MATH_DEG2RAD, 0, e->Anim.LeftArmZ,  &leftArm, false);
	Model_DrawRotate(90 * MATH_DEG2RAD, 0, e->Anim.RightArmZ, &rightArm, false);
	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &hat, true);

	Model_UpdateVB();
}	

static float ZombieVillagerModel_GetNameY(struct Entity* e) { return 34/16.0f; }
static float ZombieVillagerModel_GetEyeY(struct Entity* e)  { return 26/16.0f; }
static void ZombieVillagerModel_GetSize(struct Entity* e)   { _SetSize(8.6f,28.1f,8.6f); }
static void ZombieVillagerModel_GetBounds(struct Entity* e) { _SetBounds(-4,0,-4, 4,32,4); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 8];
static struct Model model = { 
	"zombievillager", vertices, &zombieVillager_tex,
	ZombieVillagerModel_MakeParts, ZombieVillagerModel_Draw,
	ZombieVillagerModel_GetNameY,  ZombieVillagerModel_GetEyeY,
	ZombieVillagerModel_GetSize,   ZombieVillagerModel_GetBounds
};

struct Model* ZombieVillagerModel_GetInstance(void) {
	Model_Init(&model);
	return &model;
}