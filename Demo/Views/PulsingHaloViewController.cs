using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MonoTouch.UIToolkit.Demo
{
    public class PulsingHaloViewController : DialogViewController
    {
        public PulsingHaloViewController()
            : base(CreateRootElement())
        {}

        private static RootElement CreateRootElement()
        {
            var root = new RootElement("Pulsing Halo");

            var pulsinHalo = new PulsingHalo();

            UIImageView imgView = new UIImageView(new RectangleF(0, 0, 320, 320));
            imgView.ContentMode = UIViewContentMode.Center;
            imgView.Image = UIImage.FromBundle("iPhone.png").Scale(new SizeF(76, 160));
            imgView.Layer.AddSublayer(pulsinHalo);
            
            var animationSection = new Section()
            {
                new UIViewElement(String.Empty, imgView, true)
            };
            root.Add(animationSection);

            var configurationSection = new Section() 
            {
                //new FloatElement(null, null, 60f)
            };
            root.Add(configurationSection);
            return root;
        }
    }
}