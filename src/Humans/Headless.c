#include "Common.h"

static struct ModelVertex vertices[MODEL_BOX_VERTICES * (7 + 7 + 4)];
static struct Model model;
struct Model* HeadlessModel_GetInstance(void) {
	int i;
	/* Copy from human model. */
	model = *Models.Human;
	model.name = "headless";

	for (i = MODEL_BOX_VERTICES * 1; i != MODEL_BOX_VERTICES * 2;           i++) vertices[i] = model.vertices[i];
	for (i = MODEL_BOX_VERTICES * 3; i != MODEL_BOX_VERTICES * (7 + 7 + 4); i++) vertices[i] = model.vertices[i];
	model.vertices = vertices;
	return &model;
}