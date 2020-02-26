#include "Common.h"
/* All models in this file are derived from the villager model. */

static struct ModelPart head, nose, torso, robe, leftLeg, rightLeg;

static void Villager_MakeCommon(void) {
	BoxDesc_BuildBox(&head, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
			BoxDesc_Box(-4, 24, -4, 4, 34, 4),
			BoxDesc_Rot(0, 24, 0),
	});

	BoxDesc_BuildBox(&nose, &(struct BoxDesc) {
		BoxDesc_Tex(24, 0),
			BoxDesc_Box(1, 27, -3.5f, -1, 23, -5.5f),
			BoxDesc_Rot(0, 24, 0),
	});

	BoxDesc_BuildBox(&torso, &(struct BoxDesc) {
		BoxDesc_Tex(16, 20),
			BoxDesc_Box(-4, 12, -3, 4, 24, 3)
	});

	BoxDesc_BuildBox(&robe, &(struct BoxDesc) {
		BoxDesc_Tex(0, 38),
			BoxDesc_Dims(-4, 6, -3, 4, 24, 3),
			BoxDesc_Bounds(-4.5f, 4.5f, -3.5f, 4.5f, 24.5f, 3.5f)
	});

	BoxDesc_BuildBox(&leftLeg, &(struct BoxDesc) {
		BoxDesc_Tex(0, 22),
			BoxDesc_Box(0, 0, -2, -4, 12, 2),
			BoxDesc_Rot(0, 12, 0)
	});

	BoxDesc_BuildBox(&rightLeg, &(struct BoxDesc) {
		BoxDesc_Tex(0, 22),
			BoxDesc_Box(0, 0, -2, 4, 12, 2),
			BoxDesc_Rot(0, 12, 0)
	});
}

static void Villager_DrawCommon(struct Entity *e) {
	Model_DrawPart(&torso);
	Model_DrawPart(&robe);

	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &head, true);
	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &nose, true);
	Model_DrawRotate(e->Anim.LeftLegX / 2, 0, 0, &leftLeg, false);
	Model_DrawRotate(e->Anim.RightLegX / 2, 0, 0, &rightLeg, false);
}

/* VILLAGER MODEL */

static struct ModelPart leftArm, rightArm, hands;
static struct ModelVertex vertices[MODEL_BOX_VERTICES * 9];

static void VillagerModel_MakeParts(void) {
	Villager_MakeCommon();

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

static float VillagerModel_GetNameY(struct Entity *e) { e; return 34 / 16.0f; }
static float VillagerModel_GetEyeY(struct Entity *e) { e; return 26 / 16.0f; }
static void VillagerModel_GetSize(struct Entity *e) { _SetSize(8.6f, 28.1f, 8.6f); }
static void VillagerModel_GetBounds(struct Entity *e) { _SetBounds(-4, 0, -4, 4, 32, 4); }

static void VillagerModel_Draw(struct Entity *e) {
	Model_ApplyTexture(e);

	Villager_DrawCommon(e);

	Model_DrawRotate(45 * MATH_DEG2RAD, 0, 0, &leftArm, false);
	Model_DrawRotate(45 * MATH_DEG2RAD, 0, 0, &rightArm, false);
	Model_DrawRotate(45 * MATH_DEG2RAD, 0, 0, &hands, false);

	Model_UpdateVB();
}	

static struct Model villager = { 
	"villager", vertices, &villager_tex,
	VillagerModel_MakeParts, VillagerModel_Draw,
	VillagerModel_GetNameY,  VillagerModel_GetEyeY,
	VillagerModel_GetSize,   VillagerModel_GetBounds
};

struct Model* VillagerModel_GetInstance(void) {
	Model_Init(&villager);
	villager.calcHumanAnims = false;
	return &villager;
}

/* ZOMBIE VILLAGER MODEL */

static struct ModelPart zLeftArm, zRightArm;
static struct ModelVertex zvertices[MODEL_BOX_VERTICES * 9];

static void ZombieVillagerModel_MakeParts(void) {
	Villager_MakeCommon();

	BoxDesc_BuildBox(&zLeftArm, &(struct BoxDesc) {
		BoxDesc_Tex(44, 38),
			BoxDesc_Box(-4, 12, -2, -8, 24, 2),
			BoxDesc_Rot(-6, 22, 0)
	});

	BoxDesc_BuildBox(&zRightArm, &(struct BoxDesc) {
		BoxDesc_Tex(44, 38),
			BoxDesc_Box(4, 12, -2, 8, 24, 2),
			BoxDesc_Rot(6, 22, 0)
	});

}

static void ZombieVillagerModel_Draw(struct Entity *e) {
	Model_ApplyTexture(e);

	Villager_DrawCommon(e);

	Model_DrawRotate(90 * MATH_DEG2RAD, 0, e->Anim.LeftArmZ, &zLeftArm, false);
	Model_DrawRotate(90 * MATH_DEG2RAD, 0, e->Anim.RightArmZ, &zRightArm, false);

	Model_UpdateVB();
}

static struct Model zvillager;
struct Model* ZombieVillagerModel_GetInstance(void) {
	zvillager = villager;
	
	zvillager.name = "zombievillager";
	zvillager.vertices = zvertices;
	zvillager.MakeParts = ZombieVillagerModel_MakeParts;
	zvillager.Draw = ZombieVillagerModel_Draw;
	zvillager.defaultTex = &zombieVillager_tex;

	return &zvillager;
}