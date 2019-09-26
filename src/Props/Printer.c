#include "Common.h"
static struct ModelPart bottom, back, tray, front, center, topLeft, topRight, lineLeft, lineRight, left, right, top;

static void PrinterModel_MakeParts(void) {
	BoxDesc_BuildRotatedBox(&bottom, &(struct BoxDesc){
		BoxDesc_Tex(0, 0),
		BoxDesc_Box(-8, 0, -8, 8, 1, 8)
	});
	BoxDesc_BuildRotatedBox(&left, &(struct BoxDesc){
		BoxDesc_Tex(34, 14),
		BoxDesc_Box(-6, 1, -7, -7, 6, 7)
	});
	BoxDesc_BuildRotatedBox(&right, &(struct BoxDesc){
		BoxDesc_Tex(34, 14),
		BoxDesc_Box(6, 1, -7, 7, 6, 7)
	});
	BoxDesc_BuildRotatedBox(&center, &(struct BoxDesc){
		BoxDesc_Tex(0, 17),
		BoxDesc_Box(-6, 2, -6.5f, 6, 7, 4.5f)
	});
	BoxDesc_BuildRotatedBox(&top, &(struct BoxDesc){
		BoxDesc_Tex(46, 14),
		BoxDesc_Box(-2, 6.5f, -3, 2, 7.5f, 3)
	});
	BoxDesc_BuildRotatedBox(&topLeft, &(struct BoxDesc){
		BoxDesc_Tex(56, 14),
		BoxDesc_Box(-2, 7, 0, -5, 8, 3)
	});
	BoxDesc_BuildRotatedBox(&topRight, &(struct BoxDesc){
		BoxDesc_Tex(56, 14),
		BoxDesc_Box(2, 7, 0, 5, 8, 3)
	});
	BoxDesc_BuildRotatedBox(&lineLeft, &(struct BoxDesc){
		BoxDesc_Tex(60, 0),
		BoxDesc_Box(-3, 6.5f, -7, -4, 7.5f, 0)
	});
	BoxDesc_BuildRotatedBox(&lineRight, &(struct BoxDesc){
		BoxDesc_Tex(60, 0),
		BoxDesc_Box(3, 6.5f, -7, 4, 7.5f, 0)
	});

	BoxDesc_BuildBox(&back, &(struct BoxDesc){
		BoxDesc_Tex(34, 9),
		BoxDesc_Box(-6, 1, 5.75f, 6, 5, 6.75f)
	});
	BoxDesc_BuildBox(&front, &(struct BoxDesc){
		BoxDesc_Tex(46, 21),
		BoxDesc_Box(-4, 2.5f, -7, 4, 6.5f, -6)
	});

	BoxDesc_BuildBox(&tray, &(struct BoxDesc){
		BoxDesc_Tex(34, 0),
		BoxDesc_Box(-6, 4.5f, 4.75f, 6, 12.5f, 5.75f),
		BoxDesc_Rot(0, 4, 4)
	});
}
static float PrinterModel_GetEyeY(struct Entity* e) { e; return 0.25f; }
static float PrinterModel_GetNameY(struct Entity* e) { e; return 0.75f; }

static void PrinterModel_GetSize(struct Entity* e) { e; _SetSize(15, 12, 15); }
static void PrinterModel_GetBounds(struct Entity* e) { e; _SetBounds(-8, 0, -8, 8, 8, 8); }

void PrinterModel_Draw(struct Entity* e) {
	Model_ApplyTexture(e);
	Models.vScale = 1/64.0f;

	Model_DrawPart(&bottom);
	Model_DrawPart(&left);
	Model_DrawPart(&right);
	Model_DrawPart(&topLeft);
	Model_DrawPart(&topRight);
	Model_DrawPart(&back);
	Model_DrawPart(&front);
	Model_DrawPart(&center);
	Model_DrawPart(&top);
	Model_DrawPart(&lineLeft);
	Model_DrawPart(&lineRight);

	Model_DrawRotate(MATH_PI/8, 0, 0, &tray, false);

	Model_UpdateVB();
}
static struct ModelVertex vertices[MODEL_BOX_VERTICES * 12];
static struct Model model = {
	"printer", vertices, &printer_tex,
	PrinterModel_MakeParts, PrinterModel_Draw,
	PrinterModel_GetNameY,  PrinterModel_GetEyeY,
	PrinterModel_GetSize,   PrinterModel_GetBounds
};

struct Model* PrinterModel_GetInstance(void) {
	Model_Init(&model);
	return &model;
}