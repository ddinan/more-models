#include "Common.h"
static struct ModelPart head, nose, torso, robe, leftLeg, rightLeg, leftArm, rightArm, hands;

static void VillagerModel_MakeParts(void) {
	BoxDesc_BuildBox(&head, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-4,24,-4, 4,34,4),
		BoxDesc_Rot(0, 24, 0),
	});

	BoxDesc_BuildBox(&nose, &(struct BoxDesc) {
		BoxDesc_Tex(24, 0),
		BoxDesc_Box(1,27,-3.5, -1,23,-5.5),
		BoxDesc_Rot(0, 24, 0),
	});

	BoxDesc_BuildBox(&torso, &(struct BoxDesc) {
		BoxDesc_Tex(16, 20),
		BoxDesc_Box(-4,12,-3, 4,24,3)
	});

	BoxDesc_BuildBox(&robe, &(struct BoxDesc) {
		BoxDesc_Tex(0, 38),
		BoxDesc_Dims(-4,6,-3, 4,24,3),
		BoxDesc_Bounds(-4.5f,4.5f,-3.5f, 4.5f,24.5f,3.5f)
	});

	BoxDesc_BuildBox(&leftLeg, &(struct BoxDesc) {
		BoxDesc_Tex(0, 22),
		BoxDesc_Box(0,0,-2, -4,12,2),
		BoxDesc_Rot(0, 12, 0)
	});

	BoxDesc_BuildBox(&rightLeg, &(struct BoxDesc) {
		BoxDesc_Tex(0, 22),
		BoxDesc_Box(0,0,-2, 4,12,2),
		BoxDesc_Rot(0, 12, 0)
	});

	BoxDesc_BuildBox(&leftArm, &(struct BoxDesc) {
		BoxDesc_Tex(44, 22),
		BoxDesc_Box(-4,15,-3, -8,23, 1),
		BoxDesc_Rot(0, 21, -1)
	});
	BoxDesc_BuildBox(&rightArm, &(struct BoxDesc) {
		BoxDesc_Tex(44, 22),
		BoxDesc_Box(4, 15, -3, 8, 23, 1),
		BoxDesc_Rot(0, 21, -1)
	});
	BoxDesc_BuildBox(&hands, &(struct BoxDesc) {
		BoxDesc_Tex(40, 38),
		BoxDesc_Box(-4, 15, -3, 4, 19, 1),
		BoxDesc_Rot(0, 21, -1)
	});
}

static void VillagerModel_Draw(struct Entity* e) {
	Model_ApplyTexture(e);

	Model_DrawPart(&torso);
	Model_DrawPart(&robe);

	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &head, true);
	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &nose, true);
	Model_DrawRotate(e->Anim.LeftLegX / 2,  0, 0, &leftLeg,  false);
	Model_DrawRotate(e->Anim.RightLegX / 2, 0, 0, &rightLeg, false);
	Model_DrawRotate(45 * MATH_DEG2RAD, 0, 0, &leftArm, false);
	Model_DrawRotate(45 * MATH_DEG2RAD, 0, 0, &rightArm, false);
	Model_DrawRotate(45 * MATH_DEG2RAD, 0, 0, &hands, false);
	Model_UpdateVB();
}	

static float VillagerModel_GetNameY(struct Entity* e) { return 34 / 16.0f; }
static float VillagerModel_GetEyeY(struct Entity* e) { return 26 / 16.0f; }
static void VillagerModel_GetSize(struct Entity* e)   { _SetSize(8.6f,28.1f,8.6f); }
static void VillagerModel_GetBounds(struct Entity* e) { _SetBounds(-4,0,-4, 4,32,4); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 9];
static struct Model model = { 
	"villager", vertices, &villager_tex,
	VillagerModel_MakeParts, VillagerModel_Draw,
	VillagerModel_GetNameY,  VillagerModel_GetEyeY,
	VillagerModel_GetSize,   VillagerModel_GetBounds
};

struct Model* VillagerModel_GetInstance(void) {
	Model_Init(&model);
	model.calcHumanAnims = false;
	return &model;
}