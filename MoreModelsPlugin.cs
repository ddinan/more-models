using System;
using System.Collections.Generic;
using ClassicalSharp;
using ClassicalSharp.GraphicsAPI;
using ClassicalSharp.Commands;

namespace MoreModels {
	public sealed class Core : Plugin {

		public string ClientVersion { get { return "0.99.*"; } }

        public int APIVersion { get { return 2; } }

		public void Dispose() { }
		public static Game game;
		public void Init(Game g) {
			game = g;
			game.ModelCache.RegisterTextures("cape.png");
			game.ModelCache.RegisterTextures("cape_2011.png");
			game.ModelCache.RegisterTextures("cape_2012.png");
			game.ModelCache.RegisterTextures("cape_2013.png");
			game.ModelCache.RegisterTextures("cape_2015.png");
			game.ModelCache.RegisterTextures("cape_2016.png");
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
			game.ModelCache.RegisterTextures("tv.png");
			game.ModelCache.RegisterTextures("villager.png");
			game.ModelCache.RegisterTextures("wither_skeleton.png");
			game.ModelCache.RegisterTextures("wood.png");
			game.ModelCache.RegisterTextures("zombie_pigman.png");

			game.ModelCache.Register("cape", "cape.png", new CapeModel(game));
			game.ModelCache.Register("cape_2011", "cape_2011.png", new CapeModel(game));
			game.ModelCache.Register("cape_2012", "cape_2012.png", new CapeModel(game));
			game.ModelCache.Register("cape_2013", "cape_2013.png", new CapeModel(game));
			game.ModelCache.Register("cape_2015", "cape_2015.png", new CapeModel(game));
			game.ModelCache.Register("cape_2016", "cape_2016.png", new CapeModel(game));
			game.ModelCache.Register("car", "car.png", new CarModel(game));
			game.ModelCache.Register("cavespider", "cave_spider.png", new CaveSpiderModel(game));
			game.ModelCache.Register("chair", "wood.png", new ChairModel(game));
			game.ModelCache.Register("chibisit", "char.png", new ChibiSittingModel(game));
			game.ModelCache.Register("cow", "cow.png", new CowModel(game));
			game.ModelCache.Register("croc", "croc.png", new CrocModel(game));
			game.ModelCache.Register("enderman", "enderman.png", new EndermanModel(game));
			game.ModelCache.Register("female", "char.png", new FemaleModel(game));
			game.ModelCache.Register("flying", "char.png", new FlyingModel(game));
			game.ModelCache.Register("headless", "char.png", new HeadlessModel(game));
			game.ModelCache.Register("husk", "husk.png", new HuskModel(game));
			game.ModelCache.Register("holding", "char.png", new HoldingModel(game));
			game.ModelCache.Register("male", "male.png", new MaleModel(game));
			game.ModelCache.Register("magmacube", "magmacube.png", new MagmaCubeModel(game));
			game.ModelCache.Register("printer", "printer.png", new PrinterModel(game));
			game.ModelCache.Register("slime", "slime.png", new SlimeModel(game));
			game.ModelCache.Register("stray", "stray.png", new StrayModel(game));
			game.ModelCache.Register("t", "char.png", new TModel(game));
			game.ModelCache.Register("table", "wood.png", new TableModel(game));
			game.ModelCache.Register("tv", "tv.png", new TVModel(game));
			game.ModelCache.Register("witherskeleton", "wither_skeleton.png", new WitherSkeletonModel(game));
			game.ModelCache.Register("zombiepigman", "zombie_pigman.png", new ZombiePigmanModel(game));

			// Recreate the modelcache VB to be bigger
			game.Graphics.DeleteVb(ref game.ModelCache.vb);
			game.ModelCache.vertices = new VertexP3fT2fC4b[32 * 32];
			game.ModelCache.vb = game.Graphics.CreateDynamicVb(VertexFormat.P3fT2fC4b, game.ModelCache.vertices.Length);
			game.Server.AppName += " + More Models v1.2.1";

			game.CommandList.Register(new ListModelsCommand());
		}

		public void Ready(Game game) { }

		public void Reset(Game game) { }

		public void OnNewMap(Game game) {
			//Increase holding model size limit if inf id is supported
			HoldingModel model = (HoldingModel)game.ModelCache.Get("holding");
			model.resetMaxScale();
		}

		public void OnNewMapLoaded(Game game) { }
	}


	public sealed class ListModelsCommand : Command {
		public ListModelsCommand() {
			Name = "ListModels";
			Help = new string[]
			{
				"&a/client ListModels",
				"&eShows all the models",
			};
		}

		public override void Execute(string[] args) {
			List<String> chat = WordWrap("&eLoaded models: &7" + string.Join(", ", GetModels()), 65);
			for (int i = 0; i < chat.Count; i++) {
				if (i == 0) game.Chat.Add(chat[i]);
				else game.Chat.Add("> &7" + chat[i]);
			}
		}

		public string[] GetModels() {
			string[] models = new string[Core.game.ModelCache.Models.Count];
			for (int i = 0; i < Core.game.ModelCache.Models.Count; i++) {
				models[i] = Core.game.ModelCache.Models[i].Name;
			}
			return models;
		}

		public static List<string> WordWrap(string text, int maxLineLength) {
			List<String> list = new List<string>();

			int currentIndex, lastWrap = 0;
			char[] whitespace = new[] { ' ', '\r', '\n', '\t' };
			do {
				currentIndex = lastWrap + maxLineLength > text.Length ? text.Length : (text.LastIndexOfAny(new[] { ' ', ',', '.', '?', '!', ':', ';', '-', '\n', '\r', '\t' }, Math.Min(text.Length - 1, lastWrap + maxLineLength)) + 1);
				if (currentIndex <= lastWrap)
					currentIndex = Math.Min(lastWrap + maxLineLength, text.Length);
				list.Add(text.Substring(lastWrap, currentIndex - lastWrap).Trim(whitespace));
				lastWrap = currentIndex;
			} while (currentIndex < text.Length);
			return list;
		}
	}
}