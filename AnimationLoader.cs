using Android.Content;
using Android.Views.Animations;

namespace xam.colorDialog
{
    public class AnimationLoader
    {

        public static AnimationSet getInAnimation(Context context)
        {
            AnimationSet _in = new AnimationSet(context, null);
            AlphaAnimation alpha = new AlphaAnimation(0.0f, 1.0f);
            alpha.Duration = 90;

            ScaleAnimation scale1 = new ScaleAnimation(0.8f, 1.05f, 0.8f, 1.05f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
            scale1.Duration = 135;

            ScaleAnimation scale2 = new ScaleAnimation(1.05f, 0.95f, 1.05f, 0.95f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
            scale2.Duration = 105;
            scale2.StartOffset = 135;

            ScaleAnimation scale3 = new ScaleAnimation(0.95f, 1f, 0.95f, 1.0f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
            scale3.Duration = 60;
            scale3.StartOffset = 240;
            _in.AddAnimation(alpha);
            _in.AddAnimation(scale1);
            _in.AddAnimation(scale2);
            _in.AddAnimation(scale3);
            return _in;
        }

        public static AnimationSet getOutAnimation(Context context)
        {
            AnimationSet _out = new AnimationSet(context, null);
            AlphaAnimation alpha = new AlphaAnimation(1.0f, 0.0f);
            alpha.Duration = 150;
            ScaleAnimation scale = new ScaleAnimation(1.0f, 0.6f, 1.0f, 0.6f, Dimension.RelativeToSelf, 0.5f, Dimension.RelativeToSelf, 0.5f);
            scale.Duration = 150;
            _out.AddAnimation(alpha);
            _out.AddAnimation(scale);
            return _out;
        }

    }
}