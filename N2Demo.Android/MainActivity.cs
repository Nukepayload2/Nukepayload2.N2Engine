using Android.App;
using Android.OS;
using Nukepayload2.N2Engine.Android;
using N2Demo.Core;
using Microsoft.Xna.Framework.Input.Touch;
using Nukepayload2.N2Engine.IO.FileSystem;

namespace N2Demo.Android
{
    [Activity(Label = "N2Demo.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Microsoft.Xna.Framework.AndroidGameActivity
    {
        MonoGameHandler gameHandler;
        SparksView sparks;
        GameCanvasRenderer sparksRenderer;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // 注册 N2 引擎的 Mono 实现
            MonoImplRegistration.Register(typeof(XamarinApiContract).Assembly);

            gameHandler = new MonoGameHandler();
            // 要从 "main.axml" 载入布局，取消以下注释
            // SetContentView(Resource.Layout.Main);
            SetContentView(gameHandler.OpenGLView);

            sparks = new SparksView();
            sparksRenderer = new GameCanvasRenderer(sparks, gameHandler);

            gameHandler.Updating += GameHandler_Updating;
            gameHandler.Run();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            gameHandler.Exit();
            gameHandler.Dispose();
        }

        private void GameHandler_Updating(Microsoft.Xna.Framework.Game sender, MonogameUpdateEventArgs args)
        {
            var touchState = TouchPanel.GetState();
            foreach (var touchPoint in touchState)
            {
                if(touchPoint.State== TouchLocationState.Pressed)
                {
                    sparks.OnTapped(new System.Numerics.Vector2(touchPoint.Position.X, touchPoint.Position.Y));
                    break;
                }
            }
        }
    }
}

