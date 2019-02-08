#include "Common.h"
static struct ModelPart head, link, end, leftLeg, rightLeg;

static void CaveSpiderModel_MakeParts(void) {
	BoxDesc_BuildBox(&head, &(struct BoxDesc) {
		BoxDesc_Tex(32, 4),
		BoxDesc_Box(-4,4,-11, 4,12,-3),
		BoxDesc_Rot(0, 8, -3),
	});

	BoxDesc_BuildBox(&link, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-3,5,3, 3,11,-3)
	});

	BoxDesc_BuildBox(&end, &(struct BoxDesc) {
		BoxDesc_Tex(0, 12),
		BoxDesc_Box(-5,4,3, 5,12,15)
	});

	BoxDesc_BuildBox(&leftLeg, &(struct BoxDesc) {
		BoxDesc_Tex(18, 0),
		BoxDesc_Box(-19,7,-1, -3,9,1),
		BoxDesc_Rot(-3, 8, 0)
	});

	BoxDesc_BuildBox(&rightLeg, &(struct BoxDesc) {
		BoxDesc_Tex(18, 0),
		BoxDesc_Box(3,7,-1, 19,9,1),
		BoxDesc_Rot(3, 8, 0)
	});
}

#define quarterPi (MATH_PI / 4.0f)
#define eighthPi  (MATH_PI / 8.0f)

static void CaveSpiderModel_Draw(struct Entity* e) {
	Model_ApplyTexture(e);

	Model_DrawRotate(-e->HeadX * MATH_DEG2RAD, 0, 0, &head, true);
	Model_DrawPart(&link);
	Model_DrawPart(&end);

	float rotX = (float)Math_Sin(e->Anim.WalkTime)     * e->Anim.Swing * MATH_PI;
	float rotZ = (float)Math_Cos(e->Anim.WalkTime * 2) * e->Anim.Swing * MATH_PI / 16.0f;
	float rotY = (float)Math_Sin(e->Anim.WalkTime * 2) * e->Anim.Swing * MATH_PI / 32.0f;

	Models.Rotation = ROTATE_ORDER_XZY;

	Model_DrawRotate(rotX,  quarterPi + rotY,   eighthPi + rotZ, &leftLeg,  false);
	Model_DrawRotate(-rotX, eighthPi  + rotY,   eighthPi + rotZ, &leftLeg,  false);
	Model_DrawRotate(rotX,  -eighthPi - rotY,   eighthPi - rotZ, &leftLeg,  false);
	Model_DrawRotate(-rotX, -quarterPi - rotY,  eighthPi - rotZ, &leftLeg,  false);
	Model_DrawRotate(rotX,  -quarterPi + rotY, -eighthPi + rotZ, &rightLeg, false);
	Model_DrawRotate(-rotX, -eighthPi + rotY,  -eighthPi + rotZ, &rightLeg, false);
	Model_DrawRotate(rotX,  eighthPi - rotY,   -eighthPi - rotZ, &rightLeg, false);
	Model_DrawRotate(-rotX, quarterPi - rotY,  -eighthPi - rotZ, &rightLeg, false);

	Models.Rotation = ROTATE_ORDER_ZYX;
	Model_UpdateVB();
}	

static float CaveSpiderModel_GetNameY(struct Entity* e) { return 1.0125f; }
static float CaveSpiderModel_GetEyeY(struct Entity* e)  { return 0.5000f; }
static void CaveSpiderModel_GetSize(struct Entity* e)   { _SetSize(15,12,15); }
static void CaveSpiderModel_GetBounds(struct Entity* e) { _SetBounds(-5,0,-11, 5,12,15); }

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 7];
static struct Model model = { 
	"cavespider", vertices, &caveSpider_tex,
	CaveSpiderModel_MakeParts, CaveSpiderModel_Draw,
	CaveSpiderModel_GetNameY,  CaveSpiderModel_GetEyeY,
	CaveSpiderModel_GetSize,   CaveSpiderModel_GetBounds
};

struct Model* CaveSpiderModel_GetInstance(void) {
	Model_Init(&model);
	return &model;
}