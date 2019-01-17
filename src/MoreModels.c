#include "Common.h"

/* === MODELS LIST COMMAND === */
void ListModelsCommand_Execute(const String* args, int argsCount) {
	char lineBuffer[64 + 4];
	String line = String_FromArray(lineBuffer);
	String_AppendConst(&line, "&eLoaded models: &7");
	
	struct Model* model;

	for (model = Models.Human; model != NULL; model = model->Next) {
		int nameLen = String_CalcLen(model->Name, 1000);

		/* lame word wrapping */
		if (line.length + (nameLen + 2) > 64) {
			Chat_Add(&line);
			line.length = 0;
			String_AppendConst(&line, "> &7");
		}
		String_AppendConst(&line, model->Name);
	}

	if (line.length > 4) Chat_Add(&line);
}

struct ChatCommand ListModelsCommand = {
	"ListModels", ListModelsCommand_Execute, false,
	{
		"&a/client ListModels",
		"&eShows all the models",
	}
};

/* === PLUGIN FUNCTIONALITY === */

VertexP3fT2fC4b large_vertices[32 * 32];
void MoreModels_Init(void) {
	Model_RegisterTexture(&cape_tex);
	Model_RegisterTexture(&cape2011_tex);
	Model_RegisterTexture(&cape2012_tex);
	Model_RegisterTexture(&cape2013_tex);
	Model_RegisterTexture(&cape2015_tex);
	Model_RegisterTexture(&cape2016_tex);
	Model_RegisterTexture(&car_tex);
	Model_RegisterTexture(&caveSpider_tex);
	Model_RegisterTexture(&cow_tex);
	Model_RegisterTexture(&croc_tex);
	Model_RegisterTexture(&enderman_tex);
	Model_RegisterTexture(&endermanEyes_tex);
	Model_RegisterTexture(&husk_tex);
	Model_RegisterTexture(&magmaCube_tex);
	Model_RegisterTexture(&male_tex);
	Model_RegisterTexture(&printer_tex);
	Model_RegisterTexture(&slime_tex);
	Model_RegisterTexture(&stray_tex);
	Model_RegisterTexture(&tv_tex);
	Model_RegisterTexture(&villager_tex);
	Model_RegisterTexture(&witherSkeleton_tex);
	Model_RegisterTexture(&wood_tex);
	Model_RegisterTexture(&zombiePigman_tex);

	Model_Register(CapeModel_GetInstance());
	Model_Register(Cape2011Model_GetInstance());
	Model_Register(Cape2012Model_GetInstance());
	Model_Register(Cape2013Model_GetInstance());
	Model_Register(Cape2015Model_GetInstance());
	Model_Register(Cape2016Model_GetInstance());
	Model_Register(CarModel_GetInstance());
	Model_Register(CaveSpiderModel_GetInstance());
	Model_Register(ChairModel_GetInstance());
	Model_Register(ChibiSitModel_GetInstance());
	Model_Register(CowModel_GetInstance());
	//game.ModelCache.Register("croc", "croc.png", new CrocModel(game));
	//game.ModelCache.Register("enderman", "enderman.png", new EndermanModel(game));
	//game.ModelCache.Register("female", "char.png", new FemaleModel(game));
	//game.ModelCache.Register("flying", "char.png", new FlyingModel(game));
	//game.ModelCache.Register("headless", "char.png", new HeadlessModel(game));
	Model_Register(HuskModel_GetInstance());
	//game.ModelCache.Register("holding", "char.png", new HoldingModel(game));
	//game.ModelCache.Register("male", "male.png", new MaleModel(game));
	Model_Register(MagmaCubeModel_GetInstance());
	//game.ModelCache.Register("printer", "printer.png", new PrinterModel(game));
	Model_Register(SlimeModel_GetInstance());
	Model_Register(StrayModel_GetInstance());
	Model_Register(TModel_GetInstance());
	Model_Register(TableModel_GetInstance());
	Model_Register(TVModel_GetInstance());
	Model_Register(VillagerModel_GetInstance());
	Model_Register(WitherSkeletonModel_GetInstance());
	Model_Register(ZombiePigmanModel_GetInstance());
	Model_Register(ZombieVillagerModel_GetInstance());

	// Recreate the modelcache VB to be bigger
	Gfx_DeleteVb(&Models.Vb);
	Models.Vertices    = large_vertices;
	Models.MaxVertices = 32 * 32;
	Models.Vb = Gfx_CreateDynamicVb(VERTEX_FORMAT_P3FT2FC4B, Models.MaxVertices);

	String_AppendConst(&Server.AppName, " + More Models v1.2.1");
	Commands_Register(&ListModelsCommand);
}

void MoreModels_OnNewMap(void) {
	// Increase holding model size limit if inf id is supported 
	//HoldingModel model = (HoldingModel)game.ModelCache.Get("holding");
	//model.resetMaxScale();
}

/* === API IMPLEMENTATION === */
__declspec(dllexport) int Plugin_ApiVersion = GAME_API_VER;

__declspec(dllexport) struct IGameComponent Plugin_Component = {
	MoreModels_Init,     /* Init */
	NULL,                /* Free */
	NULL,                /* Reset */
	MoreModels_OnNewMap, /* OnNewMap */
};

/* === TEXTURES === */
// avoid repeating 'struct ModelTex' over and over
struct ModelTex
	cape_tex           = { "cape.png"},
	cape2011_tex       = { "cape_2011.png"},
	cape2012_tex       = { "cape_2012.png"},
	cape2013_tex       = { "cape_2013.png"},
	cape2015_tex       = { "cape_2015.png"},
	cape2016_tex       = { "cape_2016.png"},
	car_tex            = { "car.png" },
	caveSpider_tex     = { "cave_spider.png" },
	cow_tex            = { "cow.png" },
	croc_tex           = { "croc.png" },
	enderman_tex       = { "enderman.png" },
	endermanEyes_tex   = { "enderman_eyes.png" },
	husk_tex           = { "husk.png" },
	magmaCube_tex      = { "magmacube.png" },
	male_tex           = { "male.png" },
	printer_tex        = { "printer.png" },
	slime_tex          = { "slime.png" },
	stray_tex          = { "stray.png" },
	tv_tex             = { "tv.png" },
	villager_tex       = { "villager.png" },
	witherSkeleton_tex = { "wither_skeleton.png" },
	wood_tex           = { "wood.png" },
	zombiePigman_tex   = { "zombie_pigman.png" },
	zombieVillager_tex = { "zombie_villager.png" };