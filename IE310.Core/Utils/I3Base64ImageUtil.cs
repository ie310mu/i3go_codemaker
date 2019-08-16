using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using System.Text;

namespace IE310.Core.Utils
{
    public static class I3Base64ImageUtil
    {
        public static I3MsgInfo SaveBase64Image(string source, string fileName, ImageFormat imageFormat)
        {
            try
            {
                string strbase64 = source.Substring(source.IndexOf(',') + 1);
                strbase64 = strbase64.Trim('\0');

                byte[] arr = Convert.FromBase64String(strbase64);
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    using (Bitmap bmp = new Bitmap(ms))
                    {
                        bmp.Save(fileName, imageFormat);
                    }
                    ms.Close();
                }

                return I3MsgInfo.Default;
            }
            catch (Exception e)
            {
                return new I3MsgInfo(false, "转换错误\r\n" + e.Message);
            }
        }


        public static I3MsgInfo GetBytesBase64String(string source)
        {
            try
            {
                string strbase64 = source.Substring(source.IndexOf(',') + 1);
                strbase64 = strbase64.Trim('\0');
                byte[] arr = Convert.FromBase64String(strbase64);

                I3MsgInfo result = I3MsgInfo.Default;
                result.UserData = arr;
                return result;
            }
            catch (Exception e)
            {
                return new I3MsgInfo(false, "转换错误\r\n" + e.Message);
            }
        }
    }
}
