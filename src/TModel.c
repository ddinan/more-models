#include "Common.h"

static void TModel_Draw(struct Entity* e) {
	// No animation for arms/legs
	e->Anim.LeftArmX = 0;            e->Anim.RightArmX = 0;
	e->Anim.LeftArmZ = -MATH_PI / 2; e->Anim.RightArmZ = MATH_PI / 2;

	e->Anim.LeftLegX = 0; e->Anim.RightLegX = 0;
	e->Anim.LeftLegZ = 0; e->Anim.RightLegZ = 0;

	Models.Human->Draw(e);
}

static struct Model model;
struct Model* TModel_GetInstance(void) {
	// copy everything from human model
	model = *Models.Human;
	
	model.Name = "t";
	model.Draw = TModel_Draw;
	return &model;
}