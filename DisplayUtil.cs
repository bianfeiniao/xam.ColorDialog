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
   * ���ݷֱ��ʴ� dp �ĵ�λ ת��Ϊ px(����)
   */
        public static int dp2px(Context context, float dpValue)
        {
            float scale = context.Resources.DisplayMetrics.Density;
            return (int)(dpValue * scale + 0.5f);
        }

        /**
         * ���ݷֱ��ʴ� px(����) �ĵ�λ ת��Ϊ dp
         */
        public static int px2dp(Context context, float pxValue)
        {
            float scale = context.Resources.DisplayMetrics.Density; 
            return (int)(pxValue / scale + 0.5f);
        }

        /**
         * �����Ļ�ߴ�
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
