#include "Common.h"
#include "Chat.h"
#include "Server.h"

/*
  TO DO:
	Add the rest of the models.
	Add more boxes to stray
	Magmacube needs more boxes.
*/

/* === MODELS LIST COMMAND === */

static void ListModelsCommand_Execute(const String* args, int argsCount) {
	args; argsCount;
	char lineBuffer[STRING_SIZE + 4];
	struct Model* model;

	String line = String_FromArray(lineBuffer);
	String_AppendConst(&line, "&eLoaded models: &7");

	for (model = Models.Human; model; model = model->next) {
		int nameLen = String_CalcLen(model->name, 1000);

		/* lame word wrapping */
		if (line.length + (nameLen + 2) > 64) {
			Chat_Add(&line);
			line.length = 0;
			String_AppendConst(&line, "> &7");
		}
		String_AppendConst(&line, model->name);
		String_AppendConst(&line, ", ");
	}

	if (line.length > 4) Chat_Add(&line);
}

static struct ChatCommand ListModelsCommand = {
	"ListModels", ListModelsCommand_Execute, false,
	{
		"&a/client ListModels",
		"&eShows all the models",
	}
};

/* === PLUGIN FUNCTIONALITY === */

static struct VertexTextured large_vertices[32 * 32]; 
static void MoreModels_Init(void) {
	Model_RegisterTexture(&boat_tex);
	Model_RegisterTexture(&cape_tex);
	Model_RegisterTexture(&cape2011_tex);
	Model_RegisterTexture(&cape2012_tex);
	Model_RegisterTexture(&cape2013_tex);
	Model_RegisterTexture(&cape2015_tex);
	Model_RegisterTexture(&cape2016_tex);
	Model_RegisterTexture(&car_tex);
	Model_RegisterTexture(&carSiren_tex);
	Model_RegisterTexture(&caveSpider_tex);
	Model_RegisterTexture(&copter_tex);
	Model_RegisterTexture(&cow_tex);
	Model_RegisterTexture(&croc_tex);
	Model_RegisterTexture(&enderman_tex);
	Model_RegisterTexture(&endermanEyes_tex);
	Model_RegisterTexture(&husk_tex);
	Model_RegisterTexture(&magmaCube_tex);
//	Model_RegisterTexture(&char_tex);
	Model_RegisterTexture(&printer_tex);
	Model_RegisterTexture(&slime_tex);
	Model_RegisterTexture(&spiderEyes_tex);
	Model_RegisterTexture(&stray_tex);
	Model_RegisterTexture(&truck_tex);
	Model_RegisterTexture(&truckSiren_tex);
	Model_RegisterTexture(&tv_tex);
	Model_RegisterTexture(&sailBoat_tex);
	Model_RegisterTexture(&villager_tex);
	Model_RegisterTexture(&witherSkeleton_tex);
	Model_RegisterTexture(&wood_tex);
	Model_RegisterTexture(&zombiePigman_tex);
	Model_RegisterTexture(&zombieVillager_tex);

	Model_Register(BoatModel_GetInstance());
	Model_Register(CapeModel_GetInstance());
	Model_Register(Cape2011Model_GetInstance());
	Model_Register(Cape2012Model_GetInstance());
	Model_Register(Cape2013Model_GetInstance());
	Model_Register(Cape2015Model_GetInstance());
	Model_Register(Cape2016Model_GetInstance());
	Model_Register(CarModel_GetInstance());
	Model_Register(CarSirenModel_GetInstance());
	Model_Register(CaveSpiderModel_GetInstance());
	Model_Register(ChairModel_GetInstance());
	Model_Register(ChibiSitModel_GetInstance());
	Model_Register(CopterModel_GetInstance());
	Model_Register(CowModel_GetInstance());
	Model_Register(CrocModel_GetInstance());
	Model_Register(DabModel_GetInstance());
	Model_Register(EndermanModel_GetInstance());
	Model_Register(FemaleModel_GetInstance());
	Model_Register(FlyModel_GetInstance());
	Model_Register(HeadlessModel_GetInstance());
	Model_Register(HoldModel_GetInstance());
	Model_Register(Human2Model_GetInstance());
	Model_Register(HuskModel_GetInstance());
	Model_Register(MagmaCubeModel_GetInstance());
	Model_Register(PrinterModel_GetInstance());
	Model_Register(SailBoatModel_GetInstance());	
	Model_Register(SlimeModel_GetInstance());
	Model_Register(StrayModel_GetInstance());
	Model_Register(TModel_GetInstance());
	Model_Register(TableModel_GetInstance());
	Model_Register(TruckModel_GetInstance());
	Model_Register(TruckSirenModel_GetInstance());
	Model_Register(TVModel_GetInstance());
	Model_Register(VillagerModel_GetInstance());
	Model_Register(WitherSkeletonModel_GetInstance());
	Model_Register(ZombiePigmanModel_GetInstance());
	Model_Register(ZombieVillagerModel_GetInstance());

	// Modify existing models
	pig = Model_Get(&(String)String_FromConst("pig"));
	NewPigModel_Init();

	// Recreate the modelcache VB to be bigger
	Gfx_DeleteVb(&Models.Vb);
	Models.Vertices    = large_vertices;
	Models.MaxVertices = 32 * 32;
	Models.Vb = Gfx_CreateDynamicVb(VERTEX_FORMAT_TEXTURED, Models.MaxVertices);

	String_AppendConst(&Server.AppName, " + MM 1.2.5");
	Commands_Register(&ListModelsCommand);
}

/* === API IMPLEMENTATION === */

#ifdef CC_BUILD_WIN
// special attribute to get symbols exported with Visual Studio
#define PLUGIN_EXPORT __declspec(dllexport)
#else
// public symbols already exported when compiling shared lib with GCC
#define PLUGIN_EXPORT
#endif

PLUGIN_EXPORT int Plugin_ApiVersion = GAME_API_VER;
PLUGIN_EXPORT struct IGameComponent Plugin_Component = { MoreModels_Init };

/* === TEXTURES === */

struct ModelTex
	boat_tex		   = { "boat.png" },
	cape_tex           = { "cape.png"},
	cape2011_tex       = { "cape_2011.png"},
	cape2012_tex       = { "cape_2012.png"},
	cape2013_tex       = { "cape_2013.png"},
	cape2015_tex       = { "cape_2015.png"},
	cape2016_tex       = { "cape_2016.png"},
	car_tex            = { "car.png" },
	carSiren_tex       = { "car_siren.png" },
	caveSpider_tex     = { "cave_spider.png" },
	char_tex           = { "char.png" },
	copter_tex		       = { "copter.png" },
	cow_tex            = { "cow.png" },
	croc_tex           = { "croc.png" },
	enderman_tex       = { "enderman.png" },
	endermanEyes_tex   = { "enderman_eyes.png" },
	husk_tex           = { "husk.png" },
	magmaCube_tex      = { "magmacube.png" },
	printer_tex        = { "printer.png" },
	sailBoat_tex	   = { "sail_boat.png" },
	slime_tex          = { "slime.png" },
	spiderEyes_tex     = { "spider_eyes.png" },
	stray_tex          = { "stray.png" },
	truck_tex		   = { "truck.png" },
	truckSiren_tex     = { "truck_siren.png" },
	tv_tex             = { "tv.png" },
	villager_tex       = { "villager.png" },
	witherSkeleton_tex = { "wither_skeleton.png" },
	wood_tex           = { "wood.png" },
	zombiePigman_tex   = { "zombie_pigman.png" },
	zombieVillager_tex = { "zombie_villager.png" };


/* === MISCELLANEOUS === */

void BoxDesc_BuildBendyBox(struct ModelPart *partUpper, struct ModelPart *partLower, const struct BoxDesc *desc, float rotOffset) {
	int sidesW = desc->sizeZ, bodyW = desc->sizeX, bodyH = desc->sizeY >> 1;
	float x1 = desc->x1, y1 = desc->y1, z1 = desc->z1;
	float x2 = desc->x2, y2 = desc->y2, z2 = desc->z2;
	float yInter = (desc->y1 + desc->y2) / 2;
	int x = desc->texX, y = desc->texY;
	struct Model *m = Models.Active;

	BoxDesc_YQuad2(m, x1, x2, z2, z1, y2,     /* upper top */
		x + sidesW + bodyW,                  y,
		x + sidesW,                          y + sidesW);
	BoxDesc_YQuad2(m, x2, x1, z2, z1, yInter, /* upper bottom */
		x + sidesW - bodyW,                  y,
		x + sidesW,                          y + sidesW);
	BoxDesc_ZQuad2(m, x1, x2, yInter, y2, z1, /* upper front */
		x + sidesW + bodyW,                  y + sidesW,
		x + sidesW,                          y + sidesW + bodyH);
	BoxDesc_ZQuad2(m, x2, x1, yInter, y2, z2, /* upper back */
		x + sidesW + bodyW + sidesW + bodyW, y + sidesW,
		x + sidesW + bodyW + sidesW,         y + sidesW + bodyH);
	BoxDesc_XQuad2(m, z1, z2, yInter, y2, x2, /* upper left */
		x + sidesW,                          y + sidesW,
		x,                                   y + sidesW + bodyH);
	BoxDesc_XQuad2(m, z2, z1, yInter, y2, x1, /* upper right */
		x + sidesW + bodyW + sidesW,         y + sidesW,
		x + sidesW + bodyW,                  y + sidesW + bodyH);
	ModelPart_Init(partUpper, m->index - MODEL_BOX_VERTICES, MODEL_BOX_VERTICES,
		desc->rotX, desc->rotY, desc->rotZ);

	BoxDesc_YQuad2(m, x1, x2, z2, z1, yInter, /* lower top */
		x + sidesW + bodyW + bodyW + bodyW,  y,
		x + sidesW + bodyW + bodyW,          y + sidesW);
	BoxDesc_YQuad2(m, x2, x1, z2, z1, y1,     /* lower bottom */
		x + sidesW + bodyW,                  y,
		x + sidesW + bodyW + bodyW,          y + sidesW);
	BoxDesc_ZQuad2(m, x1, x2, y1, yInter, z1, /* lower front */
		x + sidesW + bodyW,                  y + sidesW + bodyH,
		x + sidesW,                          y + sidesW + bodyH + bodyH);
	BoxDesc_ZQuad2(m, x2, x1, y1, yInter, z2, /* lower back */
		x + sidesW + bodyW + sidesW + bodyW, y + sidesW + bodyH,
		x + sidesW + bodyW + sidesW,         y + sidesW + bodyH + bodyH);
	BoxDesc_XQuad2(m, z1, z2, y1, yInter, x2, /* lower left */
		x + sidesW,                          y + sidesW + bodyH,
		x,                                   y + sidesW + bodyH + bodyH);
	BoxDesc_XQuad2(m, z2, z1, y1, yInter, x1, /* lower right */
		x + sidesW + bodyW + sidesW, y + sidesW + bodyH,
		x + sidesW + bodyW,          y + sidesW + bodyH + bodyH);
	ModelPart_Init(partLower, m->index - MODEL_BOX_VERTICES, MODEL_BOX_VERTICES,
		desc->rotX, desc->rotY + rotOffset - (float)desc->sizeY / 32, desc->rotZ);
}

void nullfunc(void) { }

int _fltused;