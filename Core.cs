using ClassicalSharp;
using ClassicalSharp.GraphicsAPI;
using System;

namespace Dab
{
    public sealed class Core : Plugin, IGameComponent, IDisposable
    {

        public string ClientVersion {
            get {
                return "0.99.9.92";
            }
        }

        public void Dispose()
        {
        }

        public void Init(Game game)
        {
            game.ModelCache.Register("cow", "cow.png", new CowModel(game));
            game.Graphics.DeleteVb(ref game.ModelCache.vb);
            game.ModelCache.vertices = new VertexP3fT2fC4b[480];
            game.ModelCache.vb = game.Graphics.CreateDynamicVb(VertexFormat.P3fT2fC4b, game.ModelCache.vertices.Length);
        }

        public void Ready(Game game)
        {
        }

        public void Reset(Game game)
        {
        }

        public void OnNewMap(Game game)
        {
        }

        public void OnNewMapLoaded(Game game)
        {
        }
    }
}
