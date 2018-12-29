using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Toolkit
{
    [TemplatePart(Name = PartPaintArea, Type = typeof(Shape))]
    [TemplatePart(Name = PartMainContent, Type = typeof(ContentPresenter))]
    public class TransitionContentControl : ContentControl
    {
        private const string PartPaintArea = "PART_PaintArea";
        private const string PartMainContent = "PART_MainContent";

        private Shape _partPaintArea;
        private ContentPresenter _partMainContent;
        private AnimationTimeline _currentAnimation;



        public PropertyPath IndexPath
        {
            get { return (PropertyPath)GetValue(IndexPathProperty); }
            set { SetValue(IndexPathProperty, value); }
        }

        public static readonly DependencyProperty IndexPathProperty =
            DependencyProperty.Register("IndexPath", typeof(PropertyPath), typeof(TransitionContentControl), new PropertyMetadata(null));


        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(TransitionContentControl), new PropertyMetadata(null));



        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Duration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(TransitionContentControl), new PropertyMetadata(TimeSpan.FromSeconds(0.5)));






        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if ((_partPaintArea != null) && (_partMainContent != null))
            {
                var oldIndex = IndexPath.Evaluate<int>(oldContent);
                var newIndex = IndexPath.Evaluate<int>(newContent);

                _partPaintArea.Fill = CreateBrushFromVisual(_partMainContent);
                BeginAnimateContentReplacement(oldIndex <= newIndex);

            }

            base.OnContentChanged(oldContent, newContent);
        }

        private void BeginAnimateContentReplacement(bool forward)
        {
            var newContentTransform = new TranslateTransform();
            var oldContentTransform = new TranslateTransform();

            _partPaintArea.RenderTransform = oldContentTransform;
            _partMainContent.RenderTransform = newContentTransform;
            _partPaintArea.Visibility = Visibility.Visible;

            double newFrom, newTo, oldFrom, oldTo;

            if (forward)
            {
                newFrom = this.ActualWidth;
                newTo = 0;
                oldFrom = 0;
                oldTo = -this.ActualWidth;
            }
            else
            {
                newFrom = -this.ActualWidth;
                newTo = 0;
                oldFrom = 0;
                oldTo = this.ActualWidth;
            }

            var newAnim = CreateAnimation(newFrom, newTo);
            _currentAnimation = CreateAnimation(oldFrom, oldTo, (s, e) =>
            {
                var ac = s as AnimationClock;
                var anim = ac.Timeline;

                if (_currentAnimation == anim)
                {
                    _partPaintArea.Visibility = Visibility.Hidden;
                }
            });

            newContentTransform.BeginAnimation(TranslateTransform.XProperty, newAnim);
            oldContentTransform.BeginAnimation(TranslateTransform.XProperty, _currentAnimation);

        }

        private AnimationTimeline CreateAnimation(double from, double to, EventHandler whenDone = null)
        {
            var anim = new DoubleAnimation(from, to, Duration) { EasingFunction = EasingFunction };

            if (whenDone != null)
                anim.Completed += whenDone;
            anim.Freeze();
            return anim;
        }


        private Brush CreateBrushFromVisual(Visual v)
        {
            if (v == null)
            {
                throw new ArgumentNullException(nameof(v));
            }

            var target = new RenderTargetBitmap((int)this.ActualWidth, (int)this.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            target.Render(v);
            var brush = new ImageBrush(target);
            brush.Freeze();
            return brush;
        }

        public override void OnApplyTemplate()
        {
            _partMainContent = GetTemplateChild(PartMainContent) as ContentPresenter;
            _partPaintArea = GetTemplateChild(PartPaintArea) as Shape;

            base.OnApplyTemplate();
        }

        static TransitionContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransitionContentControl), new FrameworkPropertyMetadata(typeof(TransitionContentControl)));
        }
    }
}
