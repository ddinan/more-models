#include "Common.h"

/* MakeParts, Draw, and DrawArm functions feature some code from the game's source. */

static struct ModelPart head, torso, hat, jacket;
static struct ModelLimbs {
	struct ModelPart
		leftUpperLeg, leftLowerLeg, rightUpperLeg, rightLowerLeg, leftUpperArm, leftLowerArm, rightUpperArm, rightLowerArm,
		leftUpperPant, leftLowerPant, rightUpperPant, rightLowerPant, leftUpperSleeve, leftLowerSleeve, rightUpperSleeve,
		rightLowerSleeve;
} limbs[3];

static void Human2Model_MakeParts(void) {
	struct BoxDesc
		headDesc  = { BoxDesc_Tex( 0, 0), BoxDesc_Box(-4,24,-4,  4,32, 4), BoxDesc_Rot(0,24, 0) },
		torsoDesc = { BoxDesc_Tex(16,16), BoxDesc_Box(-4,12,-2,  4,24, 2), BoxDesc_Rot(0,12, 0) },

		leftArmDesc  = { BoxDesc_Tex(40,16), BoxDesc_Box(-4,12,-2, -8,24, 2), BoxDesc_Rot(-6,22, 0) },
		rightArmDesc = { BoxDesc_Tex(40,16), BoxDesc_Box( 4,12,-2,  8,24, 2), BoxDesc_Rot( 6,22, 0) },
		leftLegDesc  = { BoxDesc_Tex( 0,16), BoxDesc_Box( 0, 0,-2, -4,12, 2), BoxDesc_Rot(-2,12, 0) },
		rightLegDesc = { BoxDesc_Tex( 0,16), BoxDesc_Box( 0, 0,-2,  4,12, 2), BoxDesc_Rot( 2,12, 0) },

		leftArm64Desc = { BoxDesc_Tex(32,48), BoxDesc_Box(-8,12,-2, -4,24, 2), BoxDesc_Rot(-6,22, 0) },
		leftLeg64Desc = { BoxDesc_Tex(16,48), BoxDesc_Box(-4, 0,-2,  0,12, 2), BoxDesc_Rot(-2,12, 0) },

		leftArmThinDesc  = { BoxDesc_Tex(32,48), BoxDesc_Box(-7,12,-2, -4,24, 2), BoxDesc_Rot(-6,22, 0) },
		rightArmThinDesc = { BoxDesc_Tex(40,16), BoxDesc_Box( 4,12,-2,  7,24, 2), BoxDesc_Rot( 6,22, 0) },
		leftLegThinDesc  = { BoxDesc_Tex(16,48), BoxDesc_Box(-4, 0,-2,  0,12, 2), BoxDesc_Rot(-2,12, 0) },
		rightLegThinDesc = { BoxDesc_Tex( 0,16), BoxDesc_Box( 0, 0,-2,  4,12, 2), BoxDesc_Rot( 2,12, 0) };

	BoxDesc_Rebound(
		headDesc,
		-4 * 0.875f, ((24 - 24) * 0.875f + 24), -4 * 0.875f,
		 4 * 0.875f, ((32 - 24) * 0.875f + 24),  4 * 0.875f
	);
	BoxDesc_BuildBox(&head,  &headDesc);
	BoxDesc_BuildBox(&torso, &torsoDesc);

	BoxDesc_BuildBendyBox(&limbs[SKIN_64x32].leftUpperArm,  &limbs[SKIN_64x32].leftLowerArm,  &leftArmDesc,  0.125f);
	BoxDesc_BuildBendyBox(&limbs[SKIN_64x32].rightUpperArm, &limbs[SKIN_64x32].rightLowerArm, &rightArmDesc, 0.125f);
	BoxDesc_BuildBendyBox(&limbs[SKIN_64x32].leftUpperLeg,  &limbs[SKIN_64x32].leftLowerLeg,  &leftLegDesc,  0);
	BoxDesc_BuildBendyBox(&limbs[SKIN_64x32].rightUpperLeg, &limbs[SKIN_64x32].rightLowerLeg, &rightLegDesc, 0);

	BoxDesc_BuildBendyBox(&limbs[SKIN_64x64].leftUpperArm,  &limbs[SKIN_64x64].leftLowerArm,  &leftArm64Desc, 0.125f);
	BoxDesc_BuildBendyBox(&limbs[SKIN_64x64].rightUpperArm, &limbs[SKIN_64x64].rightLowerArm, &rightArmDesc,  0.125f);
	BoxDesc_BuildBendyBox(&limbs[SKIN_64x64].leftUpperLeg,  &limbs[SKIN_64x64].leftLowerLeg,  &leftLeg64Desc, 0);
	BoxDesc_BuildBendyBox(&limbs[SKIN_64x64].rightUpperLeg, &limbs[SKIN_64x64].rightLowerLeg, &rightLegDesc,  0);

	BoxDesc_BuildBendyBox(
		&limbs[SKIN_64x64_SLIM].leftUpperArm,  &limbs[SKIN_64x64_SLIM].leftLowerArm,  &leftArmThinDesc,  0.125f
	);
	BoxDesc_BuildBendyBox(
		&limbs[SKIN_64x64_SLIM].rightUpperArm, &limbs[SKIN_64x64_SLIM].rightLowerArm, &rightArmThinDesc, 0.125f
	);
	BoxDesc_BuildBendyBox(
		&limbs[SKIN_64x64_SLIM].leftUpperLeg,  &limbs[SKIN_64x64_SLIM].leftLowerLeg,  &leftLegThinDesc,  0
	);
	BoxDesc_BuildBendyBox(
		&limbs[SKIN_64x64_SLIM].rightUpperLeg, &limbs[SKIN_64x64_SLIM].rightLowerLeg, &rightLegThinDesc, 0
	);

	headDesc.texX  = 32;
	BoxDesc_Rebound(
		headDesc,
		-4.5f * 0.875f, (23.5f - 24) * 0.875f + 24, -4.5f * 0.875f,
		 4.5f * 0.875f, (32.5f - 24) * 0.875f + 24,  4.5f * 0.875f
	);

	torsoDesc.texY = 32; BoxDesc_Rebound(torsoDesc, -4.5f,11.5f,-2.5f,  4.5f,24.5f, 2.5f);

	leftArm64Desc.texX = 48; BoxDesc_Rebound(leftArm64Desc, -8.5f,11.5f,-2.5f, -3.5f,24.5f, 2.5f);
	rightArmDesc.texY  = 32; BoxDesc_Rebound(rightArmDesc,   3.5f,11.5f,-2.5f,  8.5f,24.5f, 2.5f);
	leftLeg64Desc.texX =  0; BoxDesc_Rebound(leftLeg64Desc, -4.5f,-0.5f,-2.5f,  0.5f,12.5f, 2.5f);
	rightLegDesc.texY  = 32; BoxDesc_Rebound(rightLegDesc,  -0.5f,-0.5f,-2.5f,  4.5f,12.5f, 2.5f);

	leftArmThinDesc.texX  = 48; BoxDesc_Rebound(leftArmThinDesc,  -7.5f,11.5f,-2.5f, -3.5f,24.5f, 2.5f);
	rightArmThinDesc.texY = 32; BoxDesc_Rebound(rightArmThinDesc,  3.5f,11.5f,-2.5f,  7.5f,24.5f, 2.5f);
	leftLegThinDesc.texX  =  0; BoxDesc_Rebound(leftLegThinDesc,  -4.5f,-0.5f,-2.5f,  0.5f,12.5f, 2.5f);
	rightLegThinDesc.texY = 32; BoxDesc_Rebound(rightLegThinDesc, -0.5f,-0.5f,-2.5f,  4.5f,12.5f, 2.5f);

	BoxDesc_BuildBox(&hat,    &headDesc);
	BoxDesc_BuildBox(&jacket, &torsoDesc);

	BoxDesc_BuildBendyBox(&limbs[SKIN_64x64].leftUpperSleeve,  &limbs[SKIN_64x64].leftLowerSleeve,  &leftArm64Desc, 0.125f);
	BoxDesc_BuildBendyBox(&limbs[SKIN_64x64].rightUpperSleeve, &limbs[SKIN_64x64].rightLowerSleeve, &rightArmDesc,  0.125f);
	BoxDesc_BuildBendyBox(&limbs[SKIN_64x64].leftUpperPant,    &limbs[SKIN_64x64].leftLowerPant,    &leftLeg64Desc, 0);
	BoxDesc_BuildBendyBox(&limbs[SKIN_64x64].rightUpperPant,   &limbs[SKIN_64x64].rightLowerPant,   &rightLegDesc,  0);

	BoxDesc_BuildBendyBox(
		&limbs[SKIN_64x64_SLIM].leftUpperSleeve,  &limbs[SKIN_64x64_SLIM].leftLowerSleeve,  &leftArmThinDesc,  0.125f
	);
	BoxDesc_BuildBendyBox(
		&limbs[SKIN_64x64_SLIM].rightUpperSleeve, &limbs[SKIN_64x64_SLIM].rightLowerSleeve, &rightArmThinDesc, 0.125f
	);
	BoxDesc_BuildBendyBox(
		&limbs[SKIN_64x64_SLIM].leftUpperPant,    &limbs[SKIN_64x64_SLIM].leftLowerPant,    &leftLegThinDesc,  0
	);
	BoxDesc_BuildBendyBox(
		&limbs[SKIN_64x64_SLIM].rightUpperPant,   &limbs[SKIN_64x64_SLIM].rightLowerPant,   &rightLegThinDesc, 0
	);
}

static float Human2Model_GetNameY(struct Entity *e) { e; return 2.075f; }
static float Human2Model_GetEyeY(struct Entity *e) { e; return 1.625f; }

static void Human2Model_GetSize(struct Entity *e) { _SetSize(8.6f, 28.1f, 8.6f); }
static void Human2Model_GetBounds(struct Entity *e) { _SetBounds(-4, 0, -4, 4, 32, 4); }

static void Human2Model_GetTransform(struct Entity *e, Vec3 pos, struct Matrix *m) {
	static struct Matrix mat;
	Entity_GetTransform(e, pos, e->ModelScale, m);
	Matrix_RotateX(&mat, MATH_PI / -18 * e->Anim.Swing);
	Matrix_Mul(m, &mat, m);
}
static void Translate(struct Entity *e, float dispX, float dispY, float dispZ) {
	Vec3 pos = e->Position;
	struct Matrix matrix, temp;
	pos.Y += e->Anim.BobbingModel;
	Human2Model_GetTransform(e, pos, &matrix);
	Matrix_Mul(&matrix, &matrix, &Gfx.View);
	Matrix_Translate(&temp, dispX, dispY, dispZ);
	Matrix_Mul(&matrix, &temp, &matrix);

	Gfx_LoadMatrix(MATRIX_VIEW, &matrix);
}
static void ScaleAt(struct Entity *e, float x, float y, float z, float scale) {
	Vec3 pos = e->Position;
	struct Matrix matrix, temp;

	pos.Y += e->Anim.BobbingModel;
	Human2Model_GetTransform(e, pos, &matrix);
	Matrix_Mul(&matrix, &matrix, &Gfx.View);
	Matrix_Translate(&temp, x, y, z);
	Matrix_Mul(&matrix, &temp, &matrix);
	Matrix_Scale(&temp, scale, scale, scale);
	Matrix_Mul(&matrix, &temp, &matrix);
	Matrix_Translate(&temp, -x, -y, -z);
	Matrix_Mul(&matrix, &temp, &matrix);

	Gfx_LoadMatrix(MATRIX_VIEW, &matrix);
}
static void Human2Model_Draw(struct Entity *e) {
	int type;
	float lowerLeftLegRot, lowerRightLegRot, lowerLeftArmRot, lowerRightArmRot, breath, breathDisp, lowerArmX, lowerArmY,
		lowerArmZ, lowerLegY, lowerLegZ;
	struct ModelLimbs *l;

	lowerLeftLegRot  = ((float)Math_Cos(e->Anim.WalkTime - MATH_PI / 2) + 1) / 2 * -e->Anim.Swing * MATH_PI / 2 +
		e->Anim.LeftLegX;
	lowerRightLegRot = ((float)Math_Cos(e->Anim.WalkTime + MATH_PI / 2) + 1) / 2 * -e->Anim.Swing * MATH_PI / 2 +
		e->Anim.RightLegX;

	lowerLeftArmRot  = e->Anim.Swing * MATH_PI / 3 + e->Anim.LeftArmX;
	lowerRightArmRot = e->Anim.Swing * MATH_PI / 3 + e->Anim.RightArmX;

	breath = 0.25f * e->Anim.RightArmZ * (1 - e->Anim.Swing) + (type == SKIN_64x64_SLIM ? 0.8f : 1);
	breathDisp = 0.75f * (breath - 1);

	lowerArmX = 0.00f + 0.25f * (float)Math_Sin( e->Anim.RightArmZ);
	lowerArmY = 0.25f - 0.25f * (float)Math_Cos( e->Anim.RightArmX) + breathDisp;
	lowerArmZ = 0.00f + 0.25f * (float)Math_Sin(-e->Anim.RightArmX);

	lowerLegY = 0.375f - 0.375f * (float)Math_Cos( e->Anim.RightLegX);
	lowerLegZ = 0.000f + 0.375f * (float)Math_Sin(-e->Anim.RightLegX);

	Model_ApplyTexture(e);

	type = Models.skinType & 3;
	l = limbs + type;

	Model_DrawRotate(e->Anim.LeftLegX,  0, e->Anim.LeftLegZ,  &l->leftUpperLeg,  false);
	Model_DrawRotate(e->Anim.RightLegX, 0, e->Anim.RightLegZ, &l->rightUpperLeg, false);
	if (type != SKIN_64x32) {
		Model_DrawRotate(e->Anim.LeftLegX,  0, e->Anim.LeftLegZ,  &l->leftUpperPant,  false);
		Model_DrawRotate(e->Anim.RightLegX, 0, e->Anim.RightLegZ, &l->rightUpperPant, false);
	}

	Model_UpdateVB();
	Translate(e, 0, breathDisp, 0);

	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &head, true);
	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &hat,  true);

	Models.Rotation = ROTATE_ORDER_XZY;
	Model_DrawRotate(e->Anim.LeftArmX,  0, e->Anim.LeftArmZ,  &l->leftUpperArm,  false);
	Model_DrawRotate(e->Anim.RightArmX, 0, e->Anim.RightArmZ, &l->rightUpperArm, false);
	if (type != SKIN_64x32) {
		Model_DrawRotate(e->Anim.LeftArmX,  0, e->Anim.LeftArmZ,  &l->leftUpperSleeve,  false);
		Model_DrawRotate(e->Anim.RightArmX, 0, e->Anim.RightArmZ, &l->rightUpperSleeve, false);
	} Models.Rotation = ROTATE_ORDER_ZYX;

	Model_UpdateVB();
	ScaleAt(e, 0, 0.75f, 0, breath);

	Model_DrawPart(&torso);
	if (type != SKIN_64x32) Model_DrawPart(&jacket);

	Model_UpdateVB();
	Translate(e, -lowerArmX, lowerArmY, -lowerArmZ);

	Models.Rotation = ROTATE_ORDER_XZY;

	Model_DrawRotate(lowerLeftArmRot, 0, 0, &l->leftLowerArm, false);
	if (type != SKIN_64x32) Model_DrawRotate(lowerLeftArmRot, 0, 0, &l->leftLowerSleeve, false);

	Model_UpdateVB();
	Translate(e, lowerArmX, lowerArmY, lowerArmZ);

	Model_DrawRotate(lowerRightArmRot, 0, 0, &l->rightLowerArm, false);
	if (type != SKIN_64x32) Model_DrawRotate(lowerRightArmRot, 0, 0, &l->rightLowerSleeve, false);

	Models.Rotation = ROTATE_ORDER_ZYX;
	Model_UpdateVB();
	Translate(e, 0, lowerLegY, -lowerLegZ);

	Model_DrawRotate(lowerLeftLegRot, 0, 0, &l->leftLowerLeg, false);
	if (type != SKIN_64x32) Model_DrawRotate(lowerLeftLegRot, 0, 0, &l->leftLowerPant, false);

	Model_UpdateVB();
	Translate(e, 0, lowerLegY, lowerLegZ);

	Model_DrawRotate(lowerRightLegRot, 0, 0, &l->rightLowerLeg, false);
	if (type != SKIN_64x32) Model_DrawRotate(lowerRightLegRot, 0, 0, &l->rightLowerPant, false);

	Model_UpdateVB();
}
static void Human2Model_DrawArm(struct Entity *e) {
	struct ModelLimbs *l;
	int type;

	e;
	type = Models.skinType & 3;
	l = limbs + type;
	Model_DrawArmPart(&l->rightUpperArm);
	Model_DrawArmPart(&l->rightLowerArm);
	if (type != SKIN_64x32) {
		Model_DrawArmPart(&l->rightUpperSleeve);
		Model_DrawArmPart(&l->rightLowerSleeve);
	}

	Model_UpdateVB();
}

static struct ModelVertex vertices[MODEL_BOX_VERTICES * (4 + 3 * 16)];
static struct Model model = {
	"humanoid2", vertices, NULL,
	Human2Model_MakeParts, Human2Model_Draw,
	Human2Model_GetNameY,  Human2Model_GetEyeY,
	Human2Model_GetSize,   Human2Model_GetBounds
};

struct Model* Human2Model_GetInstance(void) {
	Model_Init(&model);
	model.defaultTex = Models.Human->defaultTex;
	model.usesSkin = model.usesHumanSkin = true;
	model.DrawArm = Human2Model_DrawArm;
	model.GetTransform = Human2Model_GetTransform;
	return &model;
}
