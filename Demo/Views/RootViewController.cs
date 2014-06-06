using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Dialog;
using MonoTouch.UIKit;

namespace MonoTouch.UIToolkit.Demo
{
    public class RootViewController : DialogViewController
    {
        public RootViewController()
            : base(CreateRootElement())
        {}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            PushViewController = NavigationController.PushViewController;
        }

        private static RootElement CreateRootElement()
        {
            var root = new RootElement("Demo");

            var animations = new List<StringElement>();
            animations.Add(new StringElement("Pulsing Halo", delegate { Navigate<PulsingHaloViewController>(); }));

            var animationsSection = new Section("Animations") { animations.OrderBy(e => e.Caption) };
            root.Add(animationsSection);
            return root;
        }

        private static readonly Dictionary<Type, UIViewController> cache = new Dictionary<Type, UIViewController>();        
        
        private static void Navigate<T>()
            where T : UIViewController, new()
        {
            UIViewController controller = null;
            if (cache.TryGetValue(typeof(T), out controller) == false)
            {
                controller = new T();
                cache[typeof(T)] = controller;
            }

            PushViewController(controller, true);
        }

        private static Action<UIViewController, bool> PushViewController;
    }
}

