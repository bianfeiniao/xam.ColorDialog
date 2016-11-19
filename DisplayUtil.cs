using System;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Content.Res;
using Android.App;

namespace xam.colorDialog
{
    public class DisplayUtil
    {

        /**
   * 根据分辨率从 dp 的单位 转成为 px(像素)
   */
        public static int dp2px(Context context, float dpValue)
        {
            float scale = context.Resources.DisplayMetrics.Density;
            return (int)(dpValue * scale + 0.5f);
        }

        /**
         * 根据分辨率从 px(像素) 的单位 转成为 dp
         */
        public static int px2dp(Context context, float pxValue)
        {
            float scale = context.Resources.DisplayMetrics.Density; 
            return (int)(pxValue / scale + 0.5f);
        }

        /**
         * 获得屏幕尺寸
         * @param context
         * @return
         */
        public static Point getScreenSize(Context _context)
        {
            Point point = new Point();
            Activity context = _context as Activity;
            context.WindowManager.DefaultDisplay.GetSize(point);
            return point;
        }

    }

}
