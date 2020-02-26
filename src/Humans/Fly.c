#include "Common.h"

static void FlyModel_GetTransform(struct Entity *e, Vec3 pos, struct Matrix *m) {
	static float flyRot; static struct Matrix temp;

	flyRot = e->Anim.Swing * MATH_PI / -2;
	Entity_GetTransform(e, pos, e->ModelScale, m);

	Matrix_Translate(&temp, 0, 1.5f, 0);
	Matrix_Mul(m, &temp, m);

	Matrix_RotateX(&temp, flyRot);
	Matrix_Mul(m, &temp, m);

	Matrix_Translate(&temp, 0, -1.5f, 0);
	Matrix_Mul(m, &temp, m);
}

/*static float FlyModel_GetNameY(struct Entity *e) { e; return 32.5f / 16; }
static float FlyModel_GetEyeY(struct Entity *e) { e; return 26 / 16.0f; }
static void FlyModel_GetSize(struct Entity *e) { _SetSize(8.6f, 28.1f, 8.6f); }
static void FlyModel_GetBounds(struct Entity *e) { _SetBounds(-4, 0, -4, 4, 32, 4); }*/

//public override float GetEyeY(Entity entity) { return 26f / 16f; }

//public override Vector3 CollisionSize{ get { return new Vector3(8.6f / 16f, 28.1f / 16f, 8.6f / 16f); } }

//public override AABB PickingBounds{ get { return new AABB(-8f / 16f, 0f, -4f / 16f, 8f / 16f, 32f / 16f, 4f / 16f); } }

void FlyModel_Draw(struct Entity *e) {
	static float legRot, armRot;

	legRot = (float)(Math_Cos(e->Anim.WalkTime / 8) + 1) * e->Anim.Swing * MATH_PI / 64;
	armRot = (float)(Math_Sin(e->Anim.WalkTime / 8) + 1) * e->Anim.Swing * MATH_PI / 32;

	//if (e->Pitch > MATH_PI / 2 && e->Anim.Swing != 0) e->Pitch = (MATH_PI * 2 - e->Pitch) * (1 - e->Anim.Swing);
	//else e->Pitch = -e->Pitch;

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