using Android.App;
using Android.OS;
using Nukepayload2.N2Engine.Android;
using N2Demo.Core;
using Microsoft.Xna.Framework.Input.Touch;
using Nukepayload2.N2Engine.Resources;
using Android.Content.Res;
using Android.Util;
using System.Numerics;
using Nukepayload2.N2Engine.Foundation;
using Nukepayload2.N2Engine.Information;

namespace N2Demo.Android
{
    [Activity(Label = "N2Demo.Android", MainLauncher = true, Icon = "@drawable/icon",
        ConfigurationChanges = global::Android.Content.PM.ConfigChanges.Orientation |
                               global::Android.Content.PM.ConfigChanges.Keyboard |
                               global::Android.Content.PM.ConfigChanges.KeyboardHidden |
                               global::Android.Content.PM.ConfigChanges.ScreenSize,
        ScreenOrientation = global::Android.Content.PM.ScreenOrientation.Landscape |
                            global::Android.Content.PM.ScreenOrientation.ReverseLandscape)]
    public class MainActivity : Microsoft.Xna.Framework.AndroidGameActivity
    {
        MonoGameHandler gameHandler;
        MainCanvas sparks;
        GameCanvasRenderer sparksRenderer;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // 注册 N2 引擎的 Mono 实现
            MonoImplRegistration.Register();

            var dm = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(dm);
            var size = new SizeInInteger(dm.WidthPixels, dm.HeightPixels);
            BackBufferInformation.ScreenSize = size;
            BackBufferInformation.ViewPortSize = size;
            BackBufferInformation.Dpi = (float)dm.DensityDpi;

            ResourceLoader.AddAndroidRawResourceMapping("theme1.ogg", Resource.Raw.Theme1);
            ResourceLoader.AddAndroidRawResourceMapping("explosion4.ogg", Resource.Raw.Explosion4);

            gameHandler = new MonoGameHandler();

            // 载入布局
            SetContentView(gameHandler.OpenGLView);

            // 载入游戏画布
            sparks = new MainCanvas();
            await sparks.LoadSceneAsync();
            sparksRenderer = new GameCanvasRenderer(sparks, gameHandler);
            
            gameHandler.Run();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            gameHandler.Exit();
            gameHandler.Dispose();
        }

    }
}

