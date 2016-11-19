using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.Lang;
using System;

namespace xam.colorDialog
{
    public class ColorDialog : Dialog, View.IOnClickListener
    {
        private ImageView mContentIv;
        private Bitmap mContentBitmap;
        private View mBtnGroupView, mDividerView, mBkgView, mDialogView;
        private TextView mTitleTv, mContentTv, mPositiveBtn, mNegativeBtn;
        private Drawable mDrawable;
        private AnimationSet mAnimIn, mAnimOut;
        private Color mBackgroundColor, mTitleTextColor, mContentTextColor;
        private int mResId=0;
        private OnPositiveListener mPositiveListener;
        private OnNegativeListener mNegativeListener;
        private string mTitleText="", mContentText="", mPositiveText="", mNegativeText="";
        private bool mIsShowAnim=true;

        //public ColorDialog(Context context) : base(context, 0) {

        //}

        public ColorDialog(Context context) : base(context,Resource.Style.color_dialog)
        {
            init();
        }

        private void callDismiss()
        {
            base.Dismiss();
        }

        private void init()
        {
            mAnimIn = AnimationLoader.getInAnimation(this.Context);
            mAnimOut = AnimationLoader.getOutAnimation(this.Context);
            initAnimListener();
        }

        private void initAnimListener()
        {
            mAnimOut.SetAnimationListener(new AniListeners()
            {
                AnimationEnd = (a) =>
                {
                    mDialogView.Post(new Runnable(() =>
                    {
                        callDismiss();
                    }));
                }
            });
        }

        public override void SetTitle(ICharSequence title)
        {
            mTitleText = title.ToString();
        }
        public override void SetTitle(int titleId)
        {
            SetTitle(this.Context.GetText(titleId));
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            View contentView = View.Inflate(this.Context, Resource.Layout.layout_colordialog, null);
            SetContentView(contentView);

            mDialogView = Window.DecorView.FindViewById(Android.Resource.Id.Content);
            mBkgView = contentView.FindViewById(Resource.Id.llBkg);
            mTitleTv = (TextView)contentView.FindViewById(Resource.Id.tvTitle);
            mContentTv = (TextView)contentView.FindViewById(Resource.Id.tvContent);
            mContentIv = (ImageView)contentView.FindViewById(Resource.Id.ivContent);

            mPositiveBtn = (TextView)contentView.FindViewById(Resource.Id.btnPositive);
            mNegativeBtn = (TextView)contentView.FindViewById(Resource.Id.btnNegative);

            mDividerView = contentView.FindViewById(Resource.Id.divider);
            mBtnGroupView = contentView.FindViewById(Resource.Id.llBtnGroup);

            mPositiveBtn.SetOnClickListener(this);
            mNegativeBtn.SetOnClickListener(this);

            mTitleTv.Text = mTitleText;
            mContentTv.Text = mContentText;
            mPositiveBtn.Text = mPositiveText;
            mNegativeBtn.Text = mNegativeText;

            if (null == mPositiveListener && null == mNegativeListener)
            {
                mBtnGroupView.Visibility = ViewStates.Gone;
            }
            else if (null == mPositiveListener && null != mNegativeListener)
            {
                mPositiveBtn.Visibility = ViewStates.Gone;
                mDividerView.Visibility = ViewStates.Gone;
                mNegativeBtn.Background = Context.Resources.GetDrawable(Resource.Drawable.sel_def_gray);
            }
            else if (null != mPositiveListener && null == mNegativeListener)
            {
                mNegativeBtn.Visibility = ViewStates.Gone;
                mDividerView.Visibility = ViewStates.Gone;
                mPositiveBtn.Background = this.Context.Resources.GetDrawable(Resource.Drawable.sel_def_gray);
            }

            if (null != mDrawable)
            {
                mContentIv.Background = mDrawable;
            }

            if (null != mContentBitmap)
            {
                mContentIv.SetImageBitmap(mContentBitmap);
            }

            if (0 != mResId)
            {
                mContentIv.SetBackgroundResource(mResId);
            }

            setTextColor();
            setBackgroundColor();
            setContentMode();
        }

        protected override void OnStart()
        {
            base.OnStart();
            startWithAnimation(mIsShowAnim);
        }

        public override void Dismiss()
        {           
            dismissWithAnimation(mIsShowAnim);
        }

        private void setTextColor()
        {
            if (0 != mTitleTextColor)
            {
                mTitleTv.SetTextColor(mTitleTextColor);               
            }
            if (0 != mContentTextColor)
            {
                mContentTv.SetTextColor(mContentTextColor);
            }
        }

        private void setBackgroundColor()
        {
            if (0 == mBackgroundColor)
            {
                return;
            }

            int radius = DisplayUtil.dp2px(Context, 6);
            float[] outerRadii = new float[] { radius, radius, radius, radius, 0, 0, 0, 0 };
            RoundRectShape roundRectShape = new RoundRectShape(outerRadii, null, null);
            ShapeDrawable shapeDrawable = new ShapeDrawable(roundRectShape);
            shapeDrawable.Paint.Color=mBackgroundColor;

            shapeDrawable.Paint.SetStyle(Paint.Style.Fill);
            mBkgView.Background=shapeDrawable;
        }

        private void setContentMode()
        {
            bool isImageMode = (null != mDrawable | null != mContentBitmap | 0 != mResId);
            bool isTextMode = (!TextUtils.IsEmpty(mContentText));

            if (isImageMode && isTextMode)
            {
                FrameLayout.LayoutParams _params = (FrameLayout.LayoutParams)mContentTv.LayoutParameters;
                _params.Gravity = GravityFlags.Bottom;
                mContentTv.LayoutParameters = _params;
                mContentTv.SetBackgroundColor(Color.Black);
                mContentTv.Background.SetAlpha(0x28);
                mContentTv.Visibility = ViewStates.Visible;
                mContentIv.Visibility = ViewStates.Visible;
                return;
            }
            if (isTextMode)
            {
                FrameLayout.LayoutParams _params = (FrameLayout.LayoutParams)mContentTv.LayoutParameters;
                _params.Gravity = GravityFlags.Bottom;
                mContentTv.LayoutParameters=_params;
                mContentIv.Visibility = ViewStates.Gone;
                mContentTv.Visibility = ViewStates.Visible;
                return;
            }

            if (isImageMode)
            {
                mContentTv.Visibility = ViewStates.Gone;
                mContentIv.Visibility = ViewStates.Visible;
                return;
            }
        }
        
        private void startWithAnimation(bool showInAnimation)
        {
            if (showInAnimation)
            {
                mDialogView.StartAnimation(mAnimIn);
            }
        }

        private void dismissWithAnimation(bool showOutAnimation)
        {
            if (showOutAnimation)
            {
                mDialogView.StartAnimation(mAnimOut);
            }
            else
            {
                base.Dismiss();
            }
        }

        public void OnClick(View v)
        {
            int id = v.Id;

            if (Resource.Id.btnPositive == id)
            {
                mPositiveListener.onClick(v);
            }
            else if (Resource.Id.btnNegative == id)
            {
                mNegativeListener.onClick(v);
            }
        }


        public interface OnPositiveListener
        {
            void onClick(View dialog);
        }

        public interface OnNegativeListener
        {
            void onClick(View dialog);
        }

        #region Set


        public ColorDialog setAnimationEnable(bool enable)
        {
            mIsShowAnim = enable;
            return this;
        }

        public ColorDialog setAnimationIn(AnimationSet animIn)
        {
            mAnimIn = animIn;
            return this;
        }

        public ColorDialog setAnimationOut(AnimationSet animOut)
        {
            mAnimOut = animOut;
            initAnimListener();
            return this;
        }

        public ColorDialog setColor(Color color)
        {
            mBackgroundColor = color;
            return this;
        }

        public ColorDialog setColor(string color)
        {
            try
            {
                setColor(Color.ParseColor(color));
            }
            catch (IllegalArgumentException e)
            {
                e.PrintStackTrace();
            }
            return this;
        }

        public ColorDialog setTitleTextColor(Color color)
        {
            mTitleTextColor = color;
            return this;
        }

        public ColorDialog setTitleTextColor(string color)
        {
            try
            {
                setTitleTextColor(Color.ParseColor(color));
            }
            catch (IllegalArgumentException e)
            {
                e.PrintStackTrace();
            }
            return this;
        }

        public ColorDialog setContentTextColor(Color color)
        {
            mContentTextColor = color;
            return this;
        }

        public ColorDialog setContentTextColor(string color)
        {
            try
            {
                setContentTextColor(Color.ParseColor(color));
            }
            catch (IllegalArgumentException e)
            {
                e.PrintStackTrace();
            }
            return this;
        }


        public ColorDialog setPositiveListener(string text, OnPositiveListener l)
        {
            mPositiveText = text;
            mPositiveListener = l;
            return this;
        }

        public ColorDialog setPositiveListener(int textId, OnPositiveListener l)
        {
            return setPositiveListener(Context.GetText(textId), l);
        }

        public ColorDialog setNegativeListener(string text, OnNegativeListener l)
        {
            mNegativeText = text;
            mNegativeListener = l;
            return this;
        }

        public ColorDialog setNegativeListener(int textId, OnNegativeListener l)
        {
            return setNegativeListener(Context.GetText(textId), l);
        }

        public ColorDialog setContentText(string text)
        {
            mContentText = text;
            return this;
        }

        public ColorDialog setContentText(int textId)
        {
            return setContentText(Context.GetText(textId));
        }

        public ColorDialog setContentImage(Drawable drawable)
        {
            mDrawable = drawable;
            return this;
        }

        public ColorDialog setContentImage(Bitmap bitmap)
        {
            mContentBitmap = bitmap;
            return this;
        }

        public ColorDialog setContentImage(int resId)
        {
            mResId = resId;
            return this;
        }


        #endregion


    }
}