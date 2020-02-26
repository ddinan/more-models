// Dab model made by Fam0r
#include "Common.h"
static void DabModel_Draw(struct Entity *e) {
  e->Anim.LeftArmX = 0.45f;
  e->Anim.RightArmX = 0.85f;
  e->Anim.LeftArmZ = -1.86f; e->Anim.RightArmZ = -1.86f;

  Models.Human->Draw(e);
}

static struct Model model;
struct Model* DabModel_GetInstance(void) {
	// copy everything from human model
	model = *Models.Human;
	
	model.name = "dab";
	model.MakeParts = nullfunc;
	model.Draw = DabModel_Draw;
	return &model;
}