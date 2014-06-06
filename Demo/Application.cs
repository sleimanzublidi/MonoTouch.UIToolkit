using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MonoTouch.UIToolkit.Demo
{
    [Register("AppDelegate")]
    public partial class Application : UIApplicationDelegate
    {
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        UIWindow window;
        RootViewController viewController;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);
            window.RootViewController = viewController = new RootViewController();
            window.MakeKeyAndVisible();
            return true;
        }
    }
}

