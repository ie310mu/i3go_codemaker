using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using IE310.Core.Utils;
using IE310.Core.Progressing;

namespace IE310.Core.IO
{
    public class I3Stream
    {
        /// <summary>
        /// 将一个流写入另一个流中，失败返回false
        /// 从destStream的当前位置，将sourceStream从当前位置写入
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="sourceStream"></param>
        /// <param name="zipOutPutStream"></param>
        /// <param name="BlockSize"></param>
        /// <param name="form"></param>
        public static I3MsgInfo WriteStreamToStream(string messageInfo, Stream sourceStream, Stream destStream,
            int BlockSize, IProgressReporter progressReporter)
        {
            byte[] buffer = new byte[BlockSize];

            long longsize = 0;
            int size = sourceStream.Read(buffer, 0, buffer.Length);
            if (size == 0)
            {
                return new I3MsgInfo(false, "源文件流大小为0！");
            }

            destStream.Write(buffer, 0, size);
            longsize = size;
            if (progressReporter != null)
            {
                progressReporter.ChangeProgress(new I3ProgressingEventArgs(0, sourceStream.Length, longsize, messageInfo));
            }
            try
            {
                while (longsize < sourceStream.Length)
                {
                    size = sourceStream.Read(buffer, 0, buffer.Length);
                    destStream.Write(buffer, 0, size);
                    longsize += size;
                    if (progressReporter != null)
                    {
                        progressReporter.ChangeProgress(new I3ProgressingEventArgs(0, sourceStream.Length, longsize, messageInfo));
                    }
                }

                return I3MsgInfo.Default;
            }
            catch (Exception ex)
            {
                return new I3MsgInfo(false, ex.Message, ex);
            }
        }

        /// <summary>
        /// 将流写入一个文件中，失败返回false
        /// 重新生成文件，将整个流从当前位置写入
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <param name="sourceStream"></param>
        /// <param name="destFileName"></param>
        /// <param name="BlockSize"></param>
        /// <param name="form"></param>
        public static I3MsgInfo WriteStreamToFile(string messageInfo, Stream sourceStream, string destFile,
            int BlockSize, IProgressReporter progressReporter)
        {
            //删除目的文件
            if (File.Exists(destFile))
            {
                File.Delete(destFile);
                if (File.Exists(destFile))
                {
                    return new I3MsgInfo(false, "错误：目的文件已经存在，且无法删除！文件名：" + destFile);
                }
            }

            //生成文件流
            FileStream destStream = null;
            try
            {
                destStream = File.Create(destFile);
            }
            catch (Exception ex)
            {
                return new I3MsgInfo(false, ex.Message, ex);
            }

            try
            {
                return WriteStreamToStream(messageInfo, sourceStream, destStream, BlockSize, progressReporter);
            }
            finally
            {
                destStream.Close();
            }
        }

        /// <summary>
        /// 将文件写入一个流中，失败返回false
        /// 从destStream的当前位置，写入整个文件
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <param name="sourceFile"></param>
        /// <param name="destStream"></param>
        /// <param name="BlockSize"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static I3MsgInfo WriteFileToStream(string messageInfo, string sourceFile, Stream destStream, int BlockSize,
            IProgressReporter progressReporter)
        {
            if (!File.Exists(sourceFile))
            {
                return new I3MsgInfo(false, "错误:源文件不存在！");
            }

            FileStream sourceStream;
            try
            {
                sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception ex)
            {
                return new I3MsgInfo(false, ex.Message, ex);
            }

            try
            {
                sourceStream.Position = 0;
                return WriteStreamToStream(messageInfo, sourceStream, destStream, BlockSize, progressReporter);
            }
            finally
            {
                sourceStream.Close();
            }
        }

        /// <summary>
        /// 将文件写入写入到另一个文件，失败返回false
        /// 删除destFile再重新写入
        /// 与File.Move的区别在于，此函数可以自己控制进度
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <param name="sourceFile"></param>
        /// <param name="destFile"></param>
        /// <param name="BlockSize"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static I3MsgInfo WriteFileToFile(string messageInfo, string sourceFile, string destFile, int BlockSize,
            IProgressReporter progressReporter)
        {
            //删除目的文件
            if (File.Exists(destFile))
            {
                File.Delete(destFile);
                if (File.Exists(destFile))
                {
                    return new I3MsgInfo(false, "错误：目的文件已经存在，且无法删除！文件名：" + destFile);
                }
            }

            //生成文件流
            FileStream destStream = null;
            try
            {
                destStream = File.Create(destFile);
            }
            catch (Exception ex)
            {
                return new I3MsgInfo(false, ex.Message, ex);
            }

            try
            {
                return WriteFileToStream(messageInfo, sourceFile, destStream, BlockSize, progressReporter);
            }
            finally
            {
                destStream.Close();
            }
        }

        /// <summary>
        /// 将整个字节数组写入到流的当前位置，失败返回false
        /// </summary>
        /// <param name="messageInfo"></param>
        /// <param name="bytes"></param>
        /// <param name="destStream"></param>
        /// <param name="BlockSize"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static I3MsgInfo WriteBytesToStream(string messageInfo, byte[] data, Stream destStream, int BlockSize,
            IProgressReporter progressReporter)
        {
            MemoryStream sourceStream;
            try
            {
                sourceStream = new MemoryStream(data);
            }
            catch (Exception ex)
            {
                return new I3MsgInfo(false, ex.Message, ex);
            }

            try
            {
                sourceStream.Position = 0;
                return WriteStreamToStream(messageInfo, sourceStream, destStream, BlockSize, progressReporter);
            }
            finally
            {
                sourceStream.Close();
            }
        }



        public static void WriteInt16ToStream(short aShort, Stream aStream)
        {
            byte[] intBytes = BitConverter.GetBytes(aShort);
            aStream.Write(intBytes, 0, intBytes.Length);
        }
        public static short ReadInt16FromStream(Stream aStream)
        {
            short temp = 1;
            byte[] intBytes = BitConverter.GetBytes(temp);
            aStream.Read(intBytes, 0, intBytes.Length);
            temp = BitConverter.ToInt16(intBytes, 0);
            return temp;
        }

        public static void WriteInt32ToStream(int aInt, Stream aStream)
        {
            byte[] intBytes = BitConverter.GetBytes(aInt);
            aStream.Write(intBytes, 0, intBytes.Length);
        }
        public static int ReadInt32FromStream(Stream aStream)
        {
            int temp = 1;
            byte[] intBytes = BitConverter.GetBytes(temp);
            aStream.Read(intBytes, 0, intBytes.Length);
            temp = BitConverter.ToInt32(intBytes, 0);
            return temp;
        }

        public static void WriteInt64ToStream(long aLong, Stream aStream)
        {
            byte[] intBytes = BitConverter.GetBytes(aLong);
            aStream.Write(intBytes, 0, intBytes.Length);
        }
        public static long ReadInt64FromStream(Stream aStream)
        {
            long temp = 1;
            byte[] intBytes = BitConverter.GetBytes(temp);
            aStream.Read(intBytes, 0, intBytes.Length);
            temp = BitConverter.ToInt64(intBytes, 0);
            return temp;
        }

        public static void WriteSingleToStream(Single aSingle, Stream aStream)
        {
            byte[] singleBytes = BitConverter.GetBytes(aSingle);
            aStream.Write(singleBytes, 0, singleBytes.Length);
        }
        public static Single ReadSingleFromStream(Stream aStream)
        {
            Single temp = 1;
            byte[] singleBytes = BitConverter.GetBytes(temp);
            aStream.Read(singleBytes, 0, singleBytes.Length);
            temp = BitConverter.ToSingle(singleBytes, 0);
            return temp;
        }

        public static void WriteDoubleToStream(double aDouble, Stream aStream)
        {
            byte[] doubleBytes = BitConverter.GetBytes(aDouble);
            aStream.Write(doubleBytes, 0, doubleBytes.Length);
        }
        public static double ReadDoubleFromStream(Stream aStream)
        {
            double temp = 1;
            byte[] doubleBytes = BitConverter.GetBytes(temp);
            aStream.Read(doubleBytes, 0, doubleBytes.Length);
            temp = BitConverter.ToDouble(doubleBytes, 0);
            return temp;
        }

        /// <summary>
        /// 此方法会丢失精度
        /// </summary>
        /// <param name="aDecimal"></param>
        /// <param name="aStream"></param>
        public static void WriteDecimalToStream(decimal aDecimal, Stream aStream)
        {
            BinaryWriter writer = new BinaryWriter(aStream);
            writer.Write(aDecimal);
        }

        public static decimal ReadDecimalFromStream(Stream aStream)
        {
            BinaryReader reader = new BinaryReader(aStream);
            return reader.ReadDecimal();
        }

        public static void WriteBoolToStream(bool aBool, Stream aStream)
        {
            byte[] boolBytes = BitConverter.GetBytes(aBool);
            aStream.Write(boolBytes, 0, boolBytes.Length);
        }
        public static bool ReadBoolFromStream(Stream aStream)
        {
            bool temp = false;
            byte[] boolBytes = BitConverter.GetBytes(temp);
            aStream.Read(boolBytes, 0, boolBytes.Length);
            temp = BitConverter.ToBoolean(boolBytes, 0);
            return temp;
        }

        public static void WriteStringToStream(string aStr, Stream aStream)
        {
            byte[] strBytes = Encoding.Unicode.GetBytes(aStr);
            long strBytesLength = strBytes.Length;
            WriteInt64ToStream(strBytesLength, aStream);
            aStream.Write(strBytes, 0, strBytes.Length);
        }
        public static string ReadStringFromStream(Stream aStream)
        {
            long strLength = I3Stream.ReadInt64FromStream(aStream);
            byte[] strBytes = new byte[strLength];
            aStream.Read(strBytes, 0, strBytes.Length);
            string str = Encoding.Unicode.GetString(strBytes);
            return str;
        }

        public static void WriteDateTimeToStream(DateTime aDateTime, Stream aStream)
        {
            string aStr = I3DateTimeUtil.ConvertDateTimeToDateTimeString(aDateTime);
            I3Stream.WriteStringToStream(aStr, aStream);
        }
        public static DateTime ReadDateTimeFromStream(Stream aStream)
        {
            string aStr = I3Stream.ReadStringFromStream(aStream);
            return I3DateTimeUtil.ConvertStringToDateTime(aStr);
        }
    }


}
