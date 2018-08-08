using ClassicalSharp;
using ClassicalSharp.GraphicsAPI;
using ClassicalSharp.Model;

namespace MoreModels
{
	public sealed class Core : Plugin {
		
		public string ClientVersion { get { return "0.99.*"; } }
		
		public void Dispose() { }
		
		public void Init(Game game)
        {
            game.ModelCache.RegisterTextures("car.png");
            game.ModelCache.RegisterTextures("cave_spider.png");
            game.ModelCache.RegisterTextures("cow.png");
            game.ModelCache.RegisterTextures("croc.png");
            game.ModelCache.RegisterTextures("enderman.png");
            game.ModelCache.RegisterTextures("enderman_eyes.png");
            game.ModelCache.RegisterTextures("husk.png");
            game.ModelCache.RegisterTextures("magmacube.png");
            game.ModelCache.RegisterTextures("male.png");
            game.ModelCache.RegisterTextures("printer.png");
            game.ModelCache.RegisterTextures("slime.png");
            game.ModelCache.RegisterTextures("stray.png");
            game.ModelCache.RegisterTextures("villager.png");
            game.ModelCache.RegisterTextures("wither_skeleton.png");
            game.ModelCache.RegisterTextures("zombie_pigman.png");

            game.ModelCache.Register("car", "car.png", new CarModel(game));
            game.ModelCache.Register("cavespider", "cave_spider.png", new CaveSpiderModel(game));
            game.ModelCache.Register("chibisit", "char.png", new ChibiSittingModel(game));
			game.ModelCache.Register("cow", "cow.png", new CowModel(game));
            game.ModelCache.Register("croc", "croc.png", new CrocModel(game));
            game.ModelCache.Register("enderman", "enderman.png", new EndermanModel(game));
			game.ModelCache.Register("flying", "char.png", new FlyingModel(game));
	        game.ModelCache.Register("headless", "char.png", new HeadlessModel(game));
			game.ModelCache.Register("husk", "husk.png", new HuskModel(game));
			game.ModelCache.Register("holding", "char.png", new HoldingModel(game));
            game.ModelCache.Register("male", "male.png", new MaleModel(game));
            game.ModelCache.Register("magmacube", "magmacube.png", new MagmaCubeModel(game));
            game.ModelCache.Register("printer", "printer.png", new PrinterModel(game));
            game.ModelCache.Register("slime", "slime.png", new SlimeModel(game));
            game.ModelCache.Register("stray", "stray.png", new StrayModel(game));
            game.ModelCache.Register("witherskeleton", "wither_skeleton.png", new WitherSkeletonModel(game));
            game.ModelCache.Register("zombiepigman", "zombie_pigman.png", new ZombiePigmanModel(game));
			
			// Recreate the modelcache VB to be bigger
			game.Graphics.DeleteVb(ref game.ModelCache.vb);
			game.ModelCache.vertices = new VertexP3fT2fC4b[24 * 20];
			game.ModelCache.vb = game.Graphics.CreateDynamicVb(VertexFormat.P3fT2fC4b, game.ModelCache.vertices.Length);
			game.Server.AppName += " + More Models v1.1";
		}
		
		public void Ready(Game game) { }
		
		public void Reset(Game game) { }
		
		public void OnNewMap(Game game)
        {
            //Increase holding model size limit if inf id is supported
            HoldingModel model = (HoldingModel)game.ModelCache.Get("holding");
            model.resetMaxScale();
        }
		
		public void OnNewMapLoaded(Game game) { }
	}
}