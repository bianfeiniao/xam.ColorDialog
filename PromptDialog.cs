using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Views.Animations;
using Android.Graphics.Drawables.Shapes;
using Java.Lang;
using Android.Graphics.Drawables;
using Android.Content.Res;

namespace xam.colorDialog
{
    public class PromptDialog : Dialog
    {

        private static Bitmap.Config BITMAP_CONFIG = Bitmap.Config.Argb8888;
        private static int DEFAULT_RADIUS = 6;
        public static int DIALOG_TYPE_INFO = 0;
        public static int DIALOG_TYPE_HELP = 1;
        public static int DIALOG_TYPE_WRONG = 2;
        public static int DIALOG_TYPE_SUCCESS = 3;
        public static int DIALOG_TYPE_WARNING = 4;
        public static int DIALOG_TYPE_DEFAULT = DIALOG_TYPE_INFO;

        private AnimationSet mAnimIn, mAnimOut;
        private View mDialogView;
        private TextView mTitleTv, mContentTv, mPositiveBtn;
        private Prom_ClickListener mOnPositiveListener;

        private int mDialogType;
        private bool mIsShowAnim;
        private string mTitle, mContent, mBtnText;
        Context _context;
        public PromptDialog(Context context) : base(context, Resource.Style.color_dialog)
        {
            init();
            _context = context;
        }

        private void init()
        {
            mAnimIn = AnimationLoader.getInAnimation(Context);
            mAnimOut = AnimationLoader.getOutAnimation(Context);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            initView();
            initListener();
        }

        private void initView()
        {
            View contentView = View.Inflate(Context, Resource.Layout.layout_promptdialog, null);
            SetContentView(contentView);
            resizeDialog();

            mDialogView = Window.DecorView.FindViewById(Android.Resource.Id.Content);
            mTitleTv = (TextView)contentView.FindViewById(Resource.Id.tvTitle);
            mContentTv = (TextView)contentView.FindViewById(Resource.Id.tvContent);
            mPositiveBtn = (TextView)contentView.FindViewById(Resource.Id.btnPositive);

            View llBtnGroup = FindViewById(Resource.Id.llBtnGroup);
            ImageView logoIv = (ImageView)contentView.FindViewById(Resource.Id.logoIv);
            logoIv.SetBackgroundResource(getLogoResId(mDialogType));

            LinearLayout topLayout = (LinearLayout)contentView.FindViewById(Resource.Id.topLayout);
            ImageView triangleIv = new ImageView(Context);
            triangleIv.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, DisplayUtil.dp2px(Context, 10));
                        
            triangleIv.SetImageBitmap(createTriangel((int)(DisplayUtil.getScreenSize(_context).X * 0.7), DisplayUtil.dp2px(_context, 10)));

            topLayout.AddView(triangleIv);

            setBtnBackground(mPositiveBtn);
            setBottomCorners(llBtnGroup);


            int radius = DisplayUtil.dp2px(Context, DEFAULT_RADIUS);
            float[] outerRadii = new float[] { radius, radius, radius, radius, 0, 0, 0, 0 };
            RoundRectShape roundRectShape = new RoundRectShape(outerRadii, null, null);
            ShapeDrawable shapeDrawable = new ShapeDrawable(roundRectShape);

            shapeDrawable.Paint.SetStyle(Paint.Style.Fill);
            shapeDrawable.Paint.Color = Context.Resources.GetColor(getColorResId(mDialogType));
            LinearLayout llTop = (LinearLayout)FindViewById(Resource.Id.llTop);
            llTop.Background = shapeDrawable;

            mTitleTv.Text = mTitle;
            mContentTv.Text = mContent;
            mPositiveBtn.Text = mBtnText;
        }

        private void resizeDialog()
        {
            WindowManagerLayoutParams _params = Window.Attributes;
            _params.Width = (int)(DisplayUtil.getScreenSize(_context).X * 0.7);
            Window.Attributes = _params;
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

        private int getLogoResId(int mDialogType)
        {
            if (DIALOG_TYPE_DEFAULT == mDialogType)
            {
                return Resource.Mipmap.ic_info;
            }
            if (DIALOG_TYPE_INFO == mDialogType)
            {
                return Resource.Mipmap.ic_info;
            }
            if (DIALOG_TYPE_HELP == mDialogType)
            {
                return Resource.Mipmap.ic_help;
            }
            if (DIALOG_TYPE_WRONG == mDialogType)
            {
                return Resource.Mipmap.ic_wrong;
            }
            if (DIALOG_TYPE_SUCCESS == mDialogType)
            {
                return Resource.Mipmap.ic_success;
            }
            if (DIALOG_TYPE_WARNING == mDialogType)
            {
                return Resource.Mipmap.icon_warning;
            }
            return Resource.Mipmap.ic_info;
        }

        private int getColorResId(int mDialogType)
        {
            if (DIALOG_TYPE_DEFAULT == mDialogType)
            {
                return Resource.Color.color_type_info;
            }
            if (DIALOG_TYPE_INFO == mDialogType)
            {
                return Resource.Color.color_type_info;
            }
            if (DIALOG_TYPE_HELP == mDialogType)
            {
                return Resource.Color.color_type_help;
            }
            if (DIALOG_TYPE_WRONG == mDialogType)
            {
                return Resource.Color.color_type_wrong;
            }
            if (DIALOG_TYPE_SUCCESS == mDialogType)
            {
                return Resource.Color.color_type_success;
            }
            if (DIALOG_TYPE_WARNING == mDialogType)
            {
                return Resource.Color.color_type_warning;
            }
            return Resource.Color.color_type_info;
        }

        private int getSelBtn(int mDialogType)
        {
            if (DIALOG_TYPE_DEFAULT == mDialogType)
            {
                return Resource.Drawable.sel_btn;
            }
            if (DIALOG_TYPE_INFO == mDialogType)
            {
                return Resource.Drawable.sel_btn_info;
            }
            if (DIALOG_TYPE_HELP == mDialogType)
            {
                return Resource.Drawable.sel_btn_help;
            }
            if (DIALOG_TYPE_WRONG == mDialogType)
            {
                return Resource.Drawable.sel_btn_wrong;
            }
            if (DIALOG_TYPE_SUCCESS == mDialogType)
            {
                return Resource.Drawable.sel_btn_success;
            }
            if (DIALOG_TYPE_WARNING == mDialogType)
            {
                return Resource.Drawable.sel_btn_warning;
            }
            return Resource.Drawable.sel_btn;
        }

        private void initAnimListener()
        {
            mAnimOut.SetAnimationListener(new AniListeners()
            {
                AnimationEnd = (animation) =>
                 {
                     mDialogView.Post(new Runnable(() =>
                     {
                         callDismiss();
                     }));

                 }
            });
        }

        private void initListener()
        {
            mPositiveBtn.SetOnClickListener(new Prom_ClickListener()
            {
                Click = (v) =>
                {
                    if (mOnPositiveListener != null)
                    {
                        mOnPositiveListener.Click(v);
                    }
                } 
            });
            initAnimListener();
        }

        private void callDismiss()
        {
            base.Dismiss();
        }

        private Bitmap createTriangel(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                return null;
            }
            return getBitmap(width, height, Context.Resources.GetColor(getColorResId(mDialogType)));
        }

        private Bitmap getBitmap(int width, int height, Color backgroundColor)
        {
            Bitmap bitmap = Bitmap.CreateBitmap(width, height, BITMAP_CONFIG);
            Canvas canvas = new Canvas(bitmap);

            Paint paint = new Paint(PaintFlags.AntiAlias);
            paint.Color = backgroundColor;
            Path path = new Path();
            path.MoveTo(0, 0);
            path.LineTo(width, 0);
            path.LineTo(width / 2, height);
            path.Close();

            canvas.DrawPath(path, paint);
            return bitmap;
        }


        private void setBtnBackground(TextView btnOk)
        {
            btnOk.SetTextColor(createColorStateList(Context.Resources.GetColor(getColorResId(mDialogType)),
                    Context.Resources.GetColor(Resource.Color.color_dialog_gray)));
            btnOk.Background = Context.Resources.GetDrawable(getSelBtn(mDialogType));
        }

        private void setBottomCorners(View llBtnGroup)
        {
            int radius = DisplayUtil.dp2px(Context, DEFAULT_RADIUS);
            float[] outerRadii = new float[] { 0, 0, 0, 0, radius, radius, radius, radius };
            RoundRectShape roundRectShape = new RoundRectShape(outerRadii, null, null);
            ShapeDrawable shapeDrawable = new ShapeDrawable(roundRectShape);
            shapeDrawable.Paint.Color = Color.White;
            shapeDrawable.Paint.SetStyle(Paint.Style.Fill);
            llBtnGroup.Background = shapeDrawable;
        }

        private ColorStateList createColorStateList(int normal, int pressed)
        {
            return createColorStateList(normal, pressed, Color.Black, Color.Black);
        }

        private ColorStateList createColorStateList(int normal, int pressed, int focused, int unable)
        {
            int[] colors = new int[] { pressed, focused, normal, focused, unable, normal };
            int[][] states = new int[6][];
            states[0] = new int[] { Android.Resource.Attribute.StatePressed, Android.Resource.Attribute.StateEnabled };
            states[1] = new int[] { Android.Resource.Attribute.StateEnabled, Android.Resource.Attribute.StateFocused };
            states[2] = new int[] { Android.Resource.Attribute.StateEnabled };
            states[3] = new int[] { Android.Resource.Attribute.StateFocused };
            states[4] = new int[] { Android.Resource.Attribute.StateWindowFocused };
            states[5] = new int[] { };
            ColorStateList colorList = new ColorStateList(states, colors);
            return colorList;
        }

        public PromptDialog setAnimationEnable(bool enable)
        {
            mIsShowAnim = enable;
            return this;
        }

        public PromptDialog setTitleText(string title)
        {
            mTitle = title;
            return this;
        }

        public PromptDialog setTitleText(int resId)
        {
            return setTitleText(Context.GetString(resId));
        }

        public PromptDialog setContentText(string content)
        {
            mContent = content;
            return this;
        }

        public PromptDialog setContentText(int resId)
        {
            return setContentText(Context.GetString(resId));
        }

        public TextView getTitleTextView()
        {
            return mTitleTv;
        }

        public TextView getContentTextView()
        {
            return mContentTv;
        }

        public PromptDialog setDialogType(int type)
        {
            mDialogType = type;
            return this;
        }

        public int getDialogType()
        {
            return mDialogType;
        }

        public PromptDialog setPositiveListener(string btnText, Prom_ClickListener l)
        {
            mBtnText = btnText;
            return setPositiveListener(l);
        }

        public PromptDialog setPositiveListener(int stringResId, Prom_ClickListener l)
        {
            return setPositiveListener(Context.GetString(stringResId), l);
        }

        public PromptDialog setPositiveListener(Prom_ClickListener l)
        {
            mOnPositiveListener = l;
            return this;
        }

        public PromptDialog setAnimationIn(AnimationSet animIn)
        {
            mAnimIn = animIn;
            return this;
        }

        public PromptDialog setAnimationOut(AnimationSet animOut)
        {
            mAnimOut = animOut;
            initAnimListener();
            return this;
        }

        public interface OnPositiveListener_Prom
        {
            void OnClick(PromptDialog dialog);
        }

    }


}