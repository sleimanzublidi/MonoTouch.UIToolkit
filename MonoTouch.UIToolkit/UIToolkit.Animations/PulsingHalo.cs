using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.CoreAnimation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MonoTouch.UIToolkit.Animations
{
    /// <summary>
    /// Original Objective-C code by Shuichi Tsutsumi
    /// https://github.com/shu223/PulsingHalo
    /// </summary>
    public class PulsingHalo : CALayer
    {        
        private float fromValueForRadius = 0.45f;
        private float fromValueForAlpha = 0.45f;
        private float keyTimeForHalfOpacity = 0.2f; // (range: 0 < keyTime < 1)        
        private float repeatCount = float.MaxValue;
        private CAAnimationGroup animationGroup;

        public PulsingHalo()
            : this(float.MaxValue)
        {}

        public PulsingHalo(float repeatCount)
        {
            this.repeatCount = repeatCount;
            Init();
        }

        private void Init()
        {
            // Default Values
            Opacity = 0;
            ContentsScale = UIScreen.MainScreen.Scale;
            BackgroundColor = UIColor.FromRGBA(0f, 0.478f, 1f, 1f).CGColor;
            Duration = 3f;
            PulseInterval = 0;
            SetupAnimationGroup();           
        }

        private float radius = 60.0f;
        public float Radius
        {
            get { return radius; }
            set { SetRadius(value); }
        }

        public float Duration { get; set; }
        public float PulseInterval { get; set; }
        
        public void SetRadius(float radius)
        {
            this.radius = radius;
            PointF position = this.Position;
            float diameter = radius * 2;
            this.Bounds = new RectangleF(0, 0, diameter, diameter);
            this.CornerRadius = this.radius;
            this.Position = position;
        }

        public void Start()
        {
            Stop();
            SetupAnimationGroup();
            this.AddAnimation(animationGroup, "pulsingHalo");
        }

        public void Stop()
        {
            this.RemoveAnimation("pulsingHalo");
        }

        private void SetupAnimationGroup()
        {
            if (animationGroup != null) return;

            var defaultCurve = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Default);
            
            animationGroup = new CAAnimationGroup();
            animationGroup.RemovedOnCompletion = true;
            animationGroup.Duration = this.Duration + this.PulseInterval;
            animationGroup.RepeatCount = this.repeatCount;            
            animationGroup.TimingFunction = defaultCurve;

            var scaleAnimation = CABasicAnimation.FromKeyPath("transform.scale.xy");
            scaleAnimation.RemovedOnCompletion = false;
            scaleAnimation.Duration = this.Duration;
            scaleAnimation.From = NSObject.FromObject(fromValueForRadius);
            scaleAnimation.To = NSObject.FromObject(1.0f);            

            var opacityAnimation = CAKeyFrameAnimation.GetFromKeyPath("opacity");
            opacityAnimation.RemovedOnCompletion = false;
            opacityAnimation.Duration = this.Duration;            
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
