#include "Common.h"
// Cape textures are flipped so you will need to flip both of your cape textures.
struct ModelPart cape;

void CapeModel_MakeParts(void) {
	BoxDesc_BuildBox(&cape, &(struct BoxDesc) {
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-5,8,3, 5,24,2),
		BoxDesc_Rot(0, 23, 0)
	});
}

void CapeModel_Draw(struct Entity* e) {
	Model_ApplyTexture(e);
	Models.uScale = 1/64.0f;
	Models.vScale = 1/32.0f;

	Model_DrawRotate(-e->Anim.Swing * (MATH_PI / 3) + e->Anim.LeftArmZ * (1 - e->Anim.Swing), 0, 0, &cape, false);
	Model_UpdateVB();

	Model_SetupState(Models.Human, e);
	Models.Human->Draw(e);
}

void CapeModel_DrawArm(struct Entity* e) {
	Model_SetupState(Models.Human, e);
	Models.Human->DrawArm(e);
}

static struct ModelVertex vertices[MODEL_BOX_VERTICES];
static struct Model model;
struct Model* CapeModel_GetInstance(void) {
	// copy everything from human model
	model = *Models.Human;
	
	model.Name       = "cape";
	model.defaultTex = &cape_tex;
	model.vertices   = vertices;
	model.MakeParts  = CapeModel_MakeParts;
	model.Draw       = CapeModel_Draw;
	model.DrawArm    = CapeModel_DrawArm;
	return &model;
}

static struct Model model_2011;
struct Model* Cape2011Model_GetInstance(void) {
	model_2011 = *CapeModel_GetInstance();
	model_2011.Name       = "cape_2011";
	model_2011.defaultTex = &cape2011_tex;
	return &model_2011;
}

static struct Model model_2012;
struct Model* Cape2012Model_GetInstance(void) {
	model_2012 = *CapeModel_GetInstance();
	model_2012.Name       = "cape_2012";
	model_2012.defaultTex = &cape2012_tex;
	return &model_2012;
}

static struct Model model_2013;
struct Model* Cape2013Model_GetInstance(void) {
	model_2013 = *CapeModel_GetInstance();
	model_2013.Name       = "cape_2013";
	model_2013.defaultTex = &cape2013_tex;
	return &model_2013;
}

static struct Model model_2015;
struct Model* Cape2015Model_GetInstance(void) {
	model_2015 = *CapeModel_GetInstance();
	model_2015.Name       = "cape_2015";
	model_2015.defaultTex = &cape2015_tex;
	return &model_2015;
}

static struct Model model_2016;
struct Model* Cape2016Model_GetInstance(void) {
	model_2016 = *CapeModel_GetInstance();
	model_2016.Name       = "cape_2016";
	model_2016.defaultTex = &cape2016_tex;
	return &model_2016;
}