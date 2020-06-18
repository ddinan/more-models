#include "Common.h"

struct ModelPart leftBreast, rightBreast, leftGlute, rightGlute, middle;

static void FemaleModel_MakeParts(void) {
	BoxDesc_BuildBox(&leftBreast, &(struct BoxDesc) {
		BoxDesc_Tex(24, 21),
		BoxDesc_Dims(-4, 18, 3, -1, 21, -2),
		BoxDesc_Bounds(-3.5f, 18, -2.5f, -0.5f, 21, -1.5f),
		BoxDesc_Rot(-2, 20, -2)
	});
	BoxDesc_BuildBox(&rightBreast, &(struct BoxDesc) {
		BoxDesc_Tex(19, 21),
		BoxDesc_Dims(1, 18, -3, 4, 21, -2),
		BoxDesc_Bounds(0.5f, 18, -2.5f, 3.5f, 21, -1.5f),
		BoxDesc_Rot(2, 20, -2)
	});
	BoxDesc_BuildBox(&middle, &(struct BoxDesc) {
		BoxDesc_Tex(20, 20),
		BoxDesc_Dims(-2, 18, -3, 2, 21, -1),
		BoxDesc_Bounds(-2, 17.75f, -2.5f, 2, 20.75f, -0.5f),
		BoxDesc_Rot(0, 20, -1)
	});
	BoxDesc_BuildBox(&leftGlute, &(struct BoxDesc) {
		BoxDesc_Tex(26, 28),
		BoxDesc_Dims(-4, 11, 2, 0, 14, 3),
		BoxDesc_Bounds(-4, 11, 1.5f, 0, 14, 2.5f)
	});
	BoxDesc_BuildBox(&rightGlute, &(struct BoxDesc) {
		BoxDesc_Tex(30, 27),
		BoxDesc_Dims(0, 11, 2, 4, 14, 3),
		BoxDesc_Bounds(0, 11, 1.5f, 4, 14, 2.5f)
	});
}

static void FemaleModel_GetTransform(struct Entity *e, Vec3 pos, struct Matrix *m) {
	pos.Y += ((float)Math_Sin(e->Anim.WalkTime * 2)) * e->Anim.Swing / 32 + 1 / 16.0f;
	Entity_GetTransform(e, pos, e->ModelScale, m);
}

static void FemaleModel_Draw(struct Entity *e) {
	static Vec3 pos;
	static struct Matrix m;
	
	Model_ApplyTexture(e);

	Model_DrawRotate(MATH_PI / 16, 0, 0, &middle, false);
	Model_DrawRotate(MATH_PI / 16, 0, 0, &leftBreast, false);
	Model_DrawRotate(MATH_PI / 16, 0, 0, &rightBreast, false);

	Model_DrawPart(&leftGlute);
	Model_DrawPart(&rightGlute);
	Model_UpdateVB();

	Model_SetupState(Models.Human, e);
	pos = e->Position;
	pos.Y += e->Anim.BobbingModel;
	Models.Human->GetTransform(e, pos, &e->Transform);
	Matrix_Mul(&m, &e->Transform, &Gfx.View);

	Gfx_LoadMatrix(MATRIX_VIEW, &m);
	Models.Human->Draw(e);
}

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 5];
static struct Model model;
struct Model* FemaleModel_GetInstance(void) {
	// copy everything from human model
	model = *Models.Human;

	model.inited = false;
	model.name = "female";
	model.vertices = vertices;
	model.MakeParts = FemaleModel_MakeParts;
	model.Draw = FemaleModel_Draw;
	model.GetTransform = FemaleModel_GetTransform;

	return &model;
}