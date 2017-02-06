using System;
using Foundation;
using UIKit;
using N2Demo.Core;
using Nukepayload2.N2Engine.iOS;
using Microsoft.Xna.Framework.Input.Touch;
using Nukepayload2.N2Engine.IO.FileSystem;

namespace N2Demo.iOS
{
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        static MonoGameHandler gameHandler;
        static SparksView sparks;
        static GameCanvasRenderer sparksRenderer;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        public override async void FinishedLaunching(UIApplication app)
        {
            if (gameHandler == null)
            {
                // 注册 N2 引擎的 Mono 实现
                MonoImplRegistration.Register(typeof(XamarinApiContract).Assembly);
                gameHandler = new MonoGameHandler();

                sparks = new SparksView();
                await sparks.LoadSceneAsync();
                sparksRenderer = new GameCanvasRenderer(sparks, gameHandler);
                gameHandler.Updating += GameHandler_Updating;
                gameHandler.Run();
            }
        }

        private void GameHandler_Updating(Microsoft.Xna.Framework.Game sender, MonogameUpdateEventArgs args)
        {
            var touchState = TouchPanel.GetState();
            foreach (var touchPoint in touchState)
            {
                if (touchPoint.State == TouchLocationState.Pressed)
                {
                    sparks.OnTappedAsync(new System.Numerics.Vector2(touchPoint.Position.X, touchPoint.Position.Y));
                    break;
                }
            }
        }
    }
}
