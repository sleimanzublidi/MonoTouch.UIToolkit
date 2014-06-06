using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.CoreAnimation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MonoTouch.UIToolkit.Animations
{
    /// <summary>
    /// Original Objective-C by shu223
    /// https://github.com/shu223/PulsingHalo
    /// </summary>
    public class PulsingHalo : CALayer
    {
        private float radius = 60.0f;
        private float fromValueForRadius = 0.45f;
        private float fromValueForAlpha = 0.45f;
        private float keyTimeForHalfOpacity = 0.2f; // (range: 0 < keyTime < 1)        
        private float animationDuration = 3f;
        private float pulseInterval = 0;
        private float repeatCount = float.MaxValue;
        private CAAnimationGroup animationGroup;

        public PulsingHalo()
        {
            Init();
        }

        public PulsingHalo(float repeatCount)
        {
            this.repeatCount = repeatCount;
            Init();
        }

        protected async virtual void Init()
        {
            ContentsScale = UIScreen.MainScreen.Scale;
            Opacity = 0;
            BackgroundColor = UIColor.FromRGBA(0f, 0.478f, 1f, 1f).CGColor;

            await Task.Run(delegate
            {
                SetupAnimationGroup();
                AddAnimation(animationGroup, "pulse");
            });

            //dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^(void) {            
            //    [self setupAnimationGroup];            
            //    if(self.pulseInterval != INFINITY) {                
            //        dispatch_async(dispatch_get_main_queue(), ^(void) {                    
            //            [self addAnimation:self.animationGroup forKey:@"pulse"];
            //        });
            //    }
            //});            
        }
        
        public void SetRadius(float radius)
        {
            this.radius = radius;
            PointF position = this.Position;
            float diameter = radius * 2;
            this.Bounds = new RectangleF(0, 0, diameter, diameter);
            this.CornerRadius = this.radius;
            this.Position = position;
        }

        private void SetupAnimationGroup()
        {
            if (animationGroup != null) return;

            var defaultCurve = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Default);
            
            animationGroup = new CAAnimationGroup();
            animationGroup.Duration = this.animationDuration + this.pulseInterval;
            animationGroup.RepeatCount = this.repeatCount;
            animationGroup.RemovedOnCompletion = false;
            animationGroup.TimingFunction = defaultCurve;

            var scaleAnimation = CABasicAnimation.FromKeyPath("transform.scale.xy");
            scaleAnimation.Duration = this.animationDuration;
            scaleAnimation.From = NSObject.FromObject(fromValueForRadius);
            scaleAnimation.To = NSObject.FromObject(1.0f);            

            var opacityAnimation = CAKeyFrameAnimation.GetFromKeyPath("opacity");
            opacityAnimation.Duration = this.animationDuration;
            opacityAnimation.RemovedOnCompletion = false;
            opacityAnimation.Values = new NSNumber[] {
                NSNumber.FromFloat(fromValueForAlpha),
                NSNumber.FromFloat(0.45f),
                NSNumber.FromFloat(0f),
            };
            opacityAnimation.KeyTimes = new NSNumber[] {
                NSNumber.FromFloat(0f),
                NSNumber.FromFloat(keyTimeForHalfOpacity),
                NSNumber.FromFloat(1f),
            };            

            animationGroup.Animations = new CAAnimation[] { scaleAnimation, opacityAnimation };
        }
    }
}
