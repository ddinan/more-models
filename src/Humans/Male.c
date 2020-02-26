#include "Common.h"

static struct ModelPart head, hat, torso, leftUpperArm, rightUpperArm, leftLowerArm, rightLowerArm, leftUpperLeg, rightUpperLeg, leftLowerLeg, rightLowerLeg,
				jacket, leftUpperSleeve, rightUpperSleeve, leftLowerSleeve, rightLowerSleeve, leftUpperPant, rightUpperPant, leftLowerPant, rightLowerPant, cape;
static void MaleModel_MakeParts(void) {
	struct BoxDesc
		headDesc =			{ BoxDesc_Tex( 0, 0), BoxDesc_Box(-4,24,-4,  4,32, 4), BoxDesc_Rot( 0,24, 0) },
		torsoDesc =			{ BoxDesc_Tex( 0,32), BoxDesc_Box(-4,12,-2,  4,24, 2), BoxDesc_Rot( 0,12, 0) },
		leftUpperArmDesc =	{ BoxDesc_Tex(48, 0), BoxDesc_Box(-8,18,-2, -4,24, 2), BoxDesc_Rot(-6,22, 0) },
		rightUpperArmDesc =	{ BoxDesc_Tex(32, 0), BoxDesc_Box( 4,18,-2,  8,24, 2), BoxDesc_Rot( 6,22, 0) },
		leftLowerArmDesc =	{ BoxDesc_Tex(48,10), BoxDesc_Box(-8,12,-2, -4,18, 2), BoxDesc_Rot(-4,18, 0) },
		rightLowerArmDesc =	{ BoxDesc_Tex(32,10), BoxDesc_Box( 4,12,-2,  8,18, 2), BoxDesc_Rot( 4,18, 0) },
		leftUpperLegDesc =	{ BoxDesc_Tex(48,20), BoxDesc_Box(-4, 6,-2,  0,12, 2), BoxDesc_Rot(-2,12, 0) },
		rightUpperLegDesc =	{ BoxDesc_Tex(32,20), BoxDesc_Box( 0, 6,-2,  4,12, 2), BoxDesc_Rot( 2,12, 0) },
		leftLowerLegDesc =	{ BoxDesc_Tex(48,30), BoxDesc_Box(-4, 0,-2,  0, 6, 2), BoxDesc_Rot(-2, 6, 0) },
		rightLowerLegDesc = { BoxDesc_Tex(32,30), BoxDesc_Box( 0, 0,-2,  4, 6, 2), BoxDesc_Rot( 2, 6, 0) };

	BOXDESC_REBOUND(headDesc, -4*0.875f, ((24-24)*0.875f+24), -4*0.875f, 4*0.875f, ((32-24)*0.875f+24), 4*0.875f);
	BoxDesc_BuildBox(&head, &headDesc);
	BoxDesc_BuildBox(&torso, &torsoDesc);
	BoxDesc_BuildBox(&leftUpperArm, &leftUpperArmDesc);
	BoxDesc_BuildBox(&rightUpperArm, &rightUpperArmDesc);
	BoxDesc_BuildBox(&leftLowerArm, &leftLowerArmDesc);
	BoxDesc_BuildBox(&rightLowerArm, &rightLowerArmDesc);
	BoxDesc_BuildBox(&leftUpperLeg, &leftUpperLegDesc);
	BoxDesc_BuildBox(&rightUpperLeg, &rightUpperLegDesc);
	BoxDesc_BuildBox(&leftLowerLeg, &leftLowerLegDesc);
	BoxDesc_BuildBox(&rightLowerLeg, &rightLowerLegDesc);

	headDesc.texY = 16; BOXDESC_REBOUND(headDesc, -4.5f*0.875f,(23.5f-24)*0.875f+24,-4.5f*0.875f, 4.5f*0.875f, (32.5f-24)*0.875f+24, 4.5f*0.875f);

	torsoDesc.texY		   = 48; BOXDESC_REBOUND(torsoDesc,			-4.5f,11.5f,-2.5f,	4.5f, 24.5f, 2.5f);
	leftUpperArmDesc.texX  = 80; BOXDESC_REBOUND(leftUpperArmDesc,	-8.5f,17.5f,-2.5f, -3.5f, 24.5f, 2.5f);
	rightUpperArmDesc.texX = 64; BOXDESC_REBOUND(rightUpperArmDesc,	 3.5f,17.5f,-2.5f,	8.5f, 24.5f, 2.5f);
	leftLowerArmDesc.texX  = 80; BOXDESC_REBOUND(leftLowerArmDesc,	-8.5f,11.5f,-2.5f, -3.5f, 18.5f, 2.5f);
	rightLowerArmDesc.texX = 64; BOXDESC_REBOUND(rightLowerArmDesc,	 3.5f,11.5f,-2.5f,	8.5f, 18.5f, 2.5f);
	leftUpperLegDesc.texX  = 80; BOXDESC_REBOUND(leftUpperLegDesc,	-4.5f, 5.5f,-2.5f,	0.5f, 12.5f, 2.5f);
	rightUpperLegDesc.texX = 64; BOXDESC_REBOUND(rightUpperLegDesc,	-0.5f, 5.5f,-2.5f,	4.5f, 12.5f, 2.5f);
	leftLowerLegDesc.texX  = 80; BOXDESC_REBOUND(leftLowerLegDesc,	-4.5f,-0.5f,-2.5f,	0.5f,  6.5f, 2.5f);
	rightLowerLegDesc.texX = 64; BOXDESC_REBOUND(rightLowerLegDesc,	-0.5f,-0.5f,-2.5f,	4.5f,  6.5f, 2.5f);

	BoxDesc_BuildBox(&hat, &headDesc);
	BoxDesc_BuildBox(&jacket, &torsoDesc);
	BoxDesc_BuildBox(&leftUpperSleeve, &leftUpperArmDesc);
	BoxDesc_BuildBox(&rightUpperSleeve, &rightUpperArmDesc);
	BoxDesc_BuildBox(&leftLowerSleeve, &leftLowerArmDesc);
	BoxDesc_BuildBox(&rightLowerSleeve, &rightLowerArmDesc);
	BoxDesc_BuildBox(&leftUpperPant, &leftUpperLegDesc);
	BoxDesc_BuildBox(&rightUpperPant, &rightUpperLegDesc);
	BoxDesc_BuildBox(&leftLowerPant, &leftLowerLegDesc);
	BoxDesc_BuildBox(&rightLowerPant, &rightLowerLegDesc);

	BoxDesc_BuildBox(&cape, &(const struct BoxDesc){ BoxDesc_Tex(24, 40), BoxDesc_Box(-6, 1, 2, 6, 24, 3), BoxDesc_Rot(0, 23, 3) });
}

static float MaleModel_GetNameY(struct Entity *e) { e; return 2.075f; }
static float MaleModel_GetEyeY(struct Entity *e) { e; return 1.625f; }

static void MaleModel_GetSize(struct Entity *e) { _SetSize(8.6f, 28.1f, 8.6f); }
static void MaleModel_GetBounds(struct Entity *e) { _SetBounds(-4, 0, -4, 4, 32, 4); }

static void MaleModel_GetTransform(struct Entity *e, Vec3 pos, struct Matrix *m) {
	static struct Matrix mat;
	Entity_GetTransform(e, pos, e->ModelScale, m);
	Matrix_RotateX(&mat, MATH_PI / -18 * e->Anim.Swing);
	Matrix_Mul(m, &mat, m);
}
static void Translate(struct Entity *e, float dispX, float dispY, float dispZ) {
	Vec3 pos = e->Position;
	struct Matrix matrix, temp;
	pos.Y += e->Anim.BobbingModel;
	MaleModel_GetTransform(e, pos, &matrix);
	Matrix_Mul(&matrix, &matrix, &Gfx.View);
	Matrix_Translate(&temp, dispX, dispY, dispZ);
	Matrix_Mul(&matrix, &temp, &matrix);

	Gfx_LoadMatrix(MATRIX_VIEW, &matrix);
}
static void ScaleAt(struct Entity *e, float x, float y, float z, float scale) {
	Vec3 pos = e->Position;
	struct Matrix matrix, temp;

	pos.Y += e->Anim.BobbingModel;
	MaleModel_GetTransform(e, pos, &matrix);
	Matrix_Mul(&matrix, &matrix, &Gfx.View);
	Matrix_Translate(&temp, x, y, z);
	Matrix_Mul(&matrix, &temp, &matrix);
	Matrix_Scale(&temp, scale, scale, scale);
	Matrix_Mul(&matrix, &temp, &matrix);
	Matrix_Translate(&temp, -x, -y, -z);
	Matrix_Mul(&matrix, &temp, &matrix);

	Gfx_LoadMatrix(MATRIX_VIEW, &matrix);
}
static void MaleModel_Draw(struct Entity *e) {
	Models.Rotation = ROTATE_ORDER_XZY;

	float lowerRightLegRot = ((float)Math_Cos(e->Anim.WalkTime + MATH_PI/2) + 1) / 2 * -e->Anim.Swing * MATH_PI/2 + e->Anim.RightLegX;
	float lowerLeftLegRot = ((float)Math_Cos(e->Anim.WalkTime - MATH_PI/2) + 1) / 2 * -e->Anim.Swing * MATH_PI/2 + e->Anim.LeftLegX;

	float lowerLeftArmRot = e->Anim.Swing * MATH_PI/3 + e->Anim.LeftArmX;
	float lowerRightArmRot = e->Anim.Swing * MATH_PI/3 + e->Anim.RightArmX;

	float breath = 0.25f * e->Anim.RightArmZ * (1 - e->Anim.Swing) + 1;
	float breathDisp = 0.75f * (breath - 1);

	float lowerArmX = 0.25f * (float)Math_Sin(e->Anim.RightArmZ);
	float lowerArmY = 0.25f - 0.25f * (float)Math_Cos(e->Anim.RightArmX) + breathDisp;
	float lowerArmZ = 0.25f * (float)Math_Sin(-e->Anim.RightArmX);
	float lowerLegY = 0.375f - 0.375f * (float)Math_Cos(e->Anim.RightLegX);
	float lowerLegZ = 0.375f * (float)Math_Sin(-e->Anim.RightLegX);

	Model_ApplyTexture(e);
	Models.uScale = 1/128.0f;
	Models.vScale = 1/64.0f;

	Model_DrawRotate(e->Anim.LeftLegX, 0, e->Anim.LeftLegZ, &leftUpperLeg, false);
	Model_DrawRotate(e->Anim.RightLegX, 0, e->Anim.RightLegZ, &rightUpperLeg, false);
	Model_DrawRotate(e->Anim.LeftLegX, 0, e->Anim.LeftLegZ, &leftUpperPant, false);
	Model_DrawRotate(e->Anim.RightLegX, 0, e->Anim.RightLegZ, &rightUpperPant, false);
	Model_DrawRotate(MATH_PI * e->Anim.Swing / -3, 0, 0, &cape, false);
	Model_UpdateVB();

	Translate(e, 0, breathDisp, 0);

	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &head, true);
	Model_DrawRotate(e->Anim.LeftArmX, 0, e->Anim.LeftArmZ, &leftUpperArm, false);
	Model_DrawRotate(e->Anim.RightArmX, 0, e->Anim.RightArmZ, &rightUpperArm, false);
	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &hat, true);
	Model_DrawRotate(e->Anim.LeftArmX, 0, e->Anim.LeftArmZ, &leftUpperSleeve, false);
	Model_DrawRotate(e->Anim.RightArmX, 0, e->Anim.RightArmZ, &rightUpperSleeve, false);
	Model_UpdateVB();

	ScaleAt(e, 0, 0.75f, 0, breath);

	Model_DrawPart(&torso);
	Model_DrawPart(&jacket);
	Model_UpdateVB();

	Translate(e, -lowerArmX, lowerArmY, -lowerArmZ);

	Model_DrawRotate(lowerLeftArmRot, 0, 0, &leftLowerArm, false);
	Model_DrawRotate(lowerLeftArmRot, 0, 0, &leftLowerSleeve, false);
	Model_UpdateVB();

	Translate(e, lowerArmX, lowerArmY, lowerArmZ);

	Model_DrawRotate(lowerRightArmRot, 0, 0, &rightLowerArm, false);
	Model_DrawRotate(lowerRightArmRot, 0, 0, &rightLowerSleeve, false);
	Model_UpdateVB();

	Translate(e, 0, lowerLegY, -lowerLegZ);

	Model_DrawRotate(lowerLeftLegRot, 0, 0, &leftLowerLeg, false);
	Model_DrawRotate(lowerLeftLegRot, 0, 0, &leftLowerPant, false);
	Model_UpdateVB();

	Translate(e, 0, lowerLegY, lowerLegZ);

	Model_DrawRotate(lowerRightLegRot, 0, 0, &rightLowerLeg, false);
	Model_DrawRotate(lowerRightLegRot, 0, 0, &rightLowerPant, false);
	Model_UpdateVB();
}
static void MaleModel_DrawArm(struct Entity *e) {
	e;
	Models.uScale = 1/128.0f;
	Models.vScale = 1/64.0f;
	Model_DrawArmPart(&rightLowerArm);
	Model_DrawArmPart(&rightLowerSleeve);
	Model_DrawArmPart(&rightUpperArm);
	Model_DrawArmPart(&rightUpperSleeve);
	Model_UpdateVB();
}
static struct ModelVertex vertices[MODEL_BOX_VERTICES * 21];
static struct Model model = {
	"male", vertices, &char_tex,
	MaleModel_MakeParts, MaleModel_Draw,
	MaleModel_GetNameY,  MaleModel_GetEyeY,
	MaleModel_GetSize,   MaleModel_GetBounds
};

struct Model* MaleModel_GetInstance(void) {
	Model_Init(&model);
	model.DrawArm = MaleModel_DrawArm;
	model.GetTransform = MaleModel_GetTransform;
	return &model;
}
