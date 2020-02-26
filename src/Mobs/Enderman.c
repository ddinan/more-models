#include "Common.h"

static struct ModelPart head, jaw, eyes, torso, leftArm, rightArm, leftLeg, rightLeg;

static void EndermanModel_MakeParts(void) {
	BoxDesc_BuildBox(&head, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-4, 42, -4, 4, 50, 4),
		BoxDesc_Rot(0, 44, 0)
	});

	BoxDesc_BuildBox(&jaw, &(struct BoxDesc) {
		BoxDesc_Tex(0, 16),
		BoxDesc_Box(-4, 42, -4, 4, 50, 4),
		BoxDesc_Rot(0, 44, 0)
	});

	BoxDesc_BuildBox(&leftArm, &(struct BoxDesc) {
		BoxDesc_Tex(56, 0),
		BoxDesc_Box(-4, 16, -1, -6, 42, 1),
		BoxDesc_Rot(-6, 44, 0)
	});

	BoxDesc_BuildBox(&rightArm, &(struct BoxDesc) {
		BoxDesc_Tex(56, 0),
		BoxDesc_Box(4, 16, -1, 6, 42, 1),
		BoxDesc_Rot(6, 44, 0)
	});

	BoxDesc_BuildBox(&leftLeg, &(struct BoxDesc) {
		BoxDesc_Tex(56, 0),
		BoxDesc_Box(-1, 0, -1, -3, 30, 1),
		BoxDesc_Rot(0, 33, 0)
	});

	BoxDesc_BuildBox(&rightLeg, &(struct BoxDesc) {
		BoxDesc_Tex(56, 0),
		BoxDesc_Box(1, 0, -1, 3, 30, 1),
		BoxDesc_Rot(0, 33, 0)
	});

	BoxDesc_BuildBox(&torso, &(struct BoxDesc) {
		BoxDesc_Tex(32, 16),
		BoxDesc_Box(-4, 30, -2, 4, 42, 2)
	});

	BoxDesc_BuildBox(&eyes, &(struct BoxDesc) {
		BoxDesc_Tex(7, 11),
		BoxDesc_Dims(-4, 45, -4, 4, 46, -3),
		BoxDesc_Bounds(-4.0625f, 44.9375f, -4.0625f, 4.0625f, 46.0625f, -2.9375f),
		BoxDesc_Rot(0, 44, 0)
	});
}

static float EndermanModel_GetNameY(struct Entity *e) { e; return 3.25f; }
static float EndermanModel_GetEyeY(struct Entity *e) { e; return 45 / 16.0f; }
static void EndermanModel_GetSize(struct Entity *e) { _SetSize(3, 45, 3); }
static void EndermanModel_GetBounds(struct Entity *e) { _SetBounds(-6, 0, -4, 6, 48, 4); }

static void EndermanModel_Draw(struct Entity *e) {
	Model_ApplyTexture(e);
	Model_DrawPart(&torso);

	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &head, true);
	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &jaw, true);
	Model_DrawRotate(e->Anim.LeftArmX / 2, 0, e->Anim.LeftArmZ, &leftArm, false);
	Model_DrawRotate(e->Anim.RightArmX / 2, 0, e->Anim.RightArmZ, &rightArm, false);
	Model_DrawRotate(e->Anim.LeftLegX / 2, 0, 0, &leftLeg, false);
	Model_DrawRotate(e->Anim.RightLegX / 2, 0, 0, &rightLeg, false);

	Model_UpdateVB();
	Gfx_BindTexture(endermanEyes_tex.texID);

	Models.Cols[FACE_ZMIN] = (PackedCol)PackedCol_Make(0xff, 0xff, 0xff, 0x10);
	Gfx_SetAlphaBlending(true);

	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &eyes, true);
	Model_UpdateVB();
	Gfx_SetAlphaBlending(false);
}

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 8];
static struct Model model = {
	"enderman", vertices, &enderman_tex,
	EndermanModel_MakeParts, EndermanModel_Draw,
	EndermanModel_GetNameY,  EndermanModel_GetEyeY,
	EndermanModel_GetSize,   EndermanModel_GetBounds
};
struct Model* EndermanModel_GetInstance(void) {
	Model_Init(&model);
	return &model;
}