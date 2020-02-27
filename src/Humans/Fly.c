#include "Common.h"

static void FlyModel_GetTransform(struct Entity *e, Vec3 pos, struct Matrix *m) {
	static struct Matrix t;

	pos.Y += 1.5f;
	Entity_GetTransform(e, pos, e->ModelScale, m);

	Matrix_RotateX(&t, e->Anim.Swing * MATH_PI / -2);
	Matrix_Mul(m, &t, m);

	Matrix_Translate(&t, 0, -1.5f, 0);
	Matrix_Mul(m, &t, m);
}

void FlyModel_Draw(struct Entity *e) {
	static float legRot, armRot;

	legRot = (float)(Math_Cos(e->Anim.WalkTime / 8) + 1) * e->Anim.Swing * MATH_PI / 64;
	armRot = (float)(Math_Sin(e->Anim.WalkTime / 8) + 1) * e->Anim.Swing * MATH_PI / 32;

	Model_SetupState(Models.Human, e);

	e->Anim.LeftArmX = e->Anim.LeftArmX * (1 - e->Anim.Swing);
	e->Anim.LeftArmZ = e->Anim.LeftArmZ * (1 - e->Anim.Swing) - armRot;

	e->Anim.RightArmX = e->Anim.RightArmX * (1 - e->Anim.Swing) + e->Anim.Swing * MATH_PI;
	e->Anim.RightArmZ = e->Anim.RightArmZ * (1 - e->Anim.Swing) + armRot;

	e->Anim.LeftLegX = (float)Math_Sin(Game.Time + e->Anim.WalkTime / 8) * MATH_PI / -64;
	e->Anim.LeftLegZ = e->Anim.LeftArmZ * (1 - e->Anim.Swing) - legRot;

	e->Anim.RightLegX = (float)Math_Sin(Game.Time + e->Anim.WalkTime / 8) * MATH_PI / 64;
	e->Anim.RightLegZ = e->Anim.RightArmZ * (1 - e->Anim.Swing) + legRot;

	Models.Human->Draw(e);
}

static struct Model model;
struct Model* FlyModel_GetInstance(void) {
	// copy everything from human model
	model = *Models.Human;

	model.name = "fly";
	model.MakeParts = nullfunc;
	model.Draw = FlyModel_Draw;
	model.GetTransform = FlyModel_GetTransform;
	model.bobbing = false;
	model.gravity = 0.04f;
	return &model;
}