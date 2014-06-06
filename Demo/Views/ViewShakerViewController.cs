using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.UIToolkit.Animations;

namespace MonoTouch.UIToolkit.Demo
{
    public class ViewShakerViewController : DialogViewController
    {
        public ViewShakerViewController()
            : base(CreateRootElement())
        {}

        private static RootElement CreateRootElement()
        {
            var root = new RootElement("Pulsing Halo");

            UIImageView imgView = new UIImageView(new RectangleF(0, 0, 320, 320));
            imgView.ContentMode = UIViewContentMode.Center;
            imgView.Image = UIImage.FromBundle("iPhone.png").Scale(new SizeF(76, 160));

            var viewShaker = new ViewShaker(imgView);
            
            var animationSection = new Section()
            {
                new UIViewElement(String.Empty, imgView, true)
            };
            root.Add(animationSection);

            var configurationSection = new Section() 
            {
                new StringElement("Shake", viewShaker.Shake)
            };
            root.Add(configurationSection);
            return root;
        }
    }
}