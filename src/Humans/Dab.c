// Dab model made by Fam0r
#include "Common.h"
static void DabModel_Draw(struct Entity* e) {
  e->Anim.LeftArmX = 0.45;
  e->Anim.RightArmX = 0.85;
  e->Anim.LeftArmZ = -1.86; e->Anim.RightArmZ = -1.86;

  Models.Human->Draw(e);
}

static struct Model model;
struct Model* DabModel_GetInstance(void) {
	// copy everything from human model
	model = *Models.Human;
	
	model.Name = "dab";
	model.MakeParts = nullfunc;
	model.Draw = DabModel_Draw;
	return &model;
}
