using System;
using Android.Views.Animations;
using static xam.colorDialog.ColorDialog;
using static Android.Views.View;
using Android.Views;
using static xam.colorDialog.PromptDialog;

namespace xam.colorDialog
{

    public class AniListeners : Java.Lang.Object, Animation.IAnimationListener
    {
        public Action<Animation> AnimationEnd { get; set; }
        public Action<Animation> AnimationRepeat { get; set; }
        public Action<Animation> AnimationStart { get; set; }

        public void OnAnimationEnd(Animation animation)
        {
            AnimationEnd?.Invoke(animation);
        }

        public void OnAnimationRepeat(Animation animation)
        {
            AnimationRepeat?.Invoke(animation);
        }

        public void OnAnimationStart(Animation animation)
        {
            AnimationStart?.Invoke(animation);
        }         
    }

    public class PositiveListener : Java.Lang.Object, OnPositiveListener
    {
        public Action<View> Click { get; set; }
        public void onClick(View dialog)
        {
            Click?.Invoke(dialog);
        }
    }

    public class NegativeListener : Java.Lang.Object, OnNegativeListener
    {
        public Action<View> Click { get; set; }
        public void onClick(View dialog)
        {
            Click?.Invoke(dialog);
        }
    }


    public class Prom_ClickListener : Java.Lang.Object,IOnClickListener
    {
        public Action<View> Click { get; set; }

        public void OnClick(View v)
        {
            Click?.Invoke(v);
        }

        //public Action<PromptDialog> PClick { get; set; }
        //public void OnClick(PromptDialog v)
        //{
        //    PClick?.Invoke(v);
        //}
    }
     




}