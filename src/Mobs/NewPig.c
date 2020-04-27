#include "Common.h"

struct Model *pig;
static struct ModelPart snout;

void (*oldDraw)(struct Entity *e);

static struct ModelVertex vertices[MODEL_BOX_VERTICES * 1];
static struct Model newPig = { NULL, vertices };

void NewPigModel_MakeParts(void) {
	struct Model *active = Models.Active;

	Models.Active = &newPig;

	BoxDesc_BuildBox(&snout, &(struct BoxDesc) { BoxDesc_Tex(16, 16), BoxDesc_Box(-2, 9, -15, 2, 12, -14), BoxDesc_Rot(0, 12, -6) });

	newPig.index = 0;
	Models.Active = active;
}

static void NewPigModel_Draw(struct Entity *e) {
	oldDraw(e);

	newPig.index = 0;
	Models.Active = &newPig;	
	Model_DrawRotate(-e->Pitch * MATH_DEG2RAD, 0, 0, &snout, true);
	Model_UpdateVB();
}

void NewPigModel_Init(void) {
	oldDraw = pig->Draw;
	pig->Draw = NewPigModel_Draw;

	NewPigModel_MakeParts();
}