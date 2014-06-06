using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoTouch.CoreAnimation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MonoTouch.UIToolkit.Animations
{
    /// <summary>
    /// Original Objective-C code by Philip Vasilchenko
    /// https://github.com/ArtFeel/AFViewShaker
    /// </summary>
    public class ViewShaker
    {
        private UIView[] views;

        public ViewShaker(UIView view)
            : this(new UIView[] { view })
        {}

        public ViewShaker(UIView[] views)
        {
            this.views = views;
            this.Duration = 0.5f;
        }

        public float Duration { get; set; }

        public void Shake()
        {
            Shake(Duration, null);
        }

        public void Shake(float duration, NSAction completion)
        {
            int count = views.Length;
            NSAction onAnimationStopped = delegate
            {
                count--;
                if (count == 0 && completion != null)
                {
                    completion();
                }
            };

            foreach (var view in views)
            {
                AddShakeAnimation(view, onAnimationStopped);
            }    
        }

        private void AddShakeAnimation(UIView view, NSAction onAnimationStopped)
        {
            var curve = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);

            var shakeAnimation = CAKeyFrameAnimation.GetFromKeyPath("transform.translation.x");
            shakeAnimation.RemovedOnCompletion = false;
            shakeAnimation.TimingFunction = curve;
            shakeAnimation.Duration = Duration;
            shakeAnimation.Values = new NSNumber[] {
                NSNumber.FromFloat(0),
                NSNumber.FromFloat(10),
                NSNumber.FromFloat(-8),
                NSNumber.FromFloat(8),
                NSNumber.FromFloat(-5),
                NSNumber.FromFloat(5),
                NSNumber.FromFloat(0)
            };
            shakeAnimation.KeyTimes = new NSNumber[] {
                NSNumber.FromFloat(0f),
                NSNumber.FromFloat(0.225f),
                NSNumber.FromFloat(0.425f),
                NSNumber.FromFloat(0.6f),
                NSNumber.FromFloat(0.75f),
                NSNumber.FromFloat(0.875f),
                NSNumber.FromFloat(1f),
            };

            if (onAnimationStopped != null)
            {
                shakeAnimation.AnimationStopped += (o,e) => onAnimationStopped();
            }

            view.Layer.AddAnimation(shakeAnimation, "shake");
        }
    }
}