/*
 * 
算法名称    算法类（抽象）   有效密钥大小(Bit)   默认密钥大小(Bit)  IV长度   默认实现类 
DES        DES             64                 64                          DESCryptoServiceProvider 
TripleDES  TripleDES       128, 192           192                         TripleDESCryptoServiceProvider 
RC2        RC2             40-128             128                         RC2CryptoServiceProvider 
Rijndael   RijnDael        128, 192, 256      256                16*8     RijnDaelManaged 
 * 
 * DES mCrypt = new SymmetricAlgorithm.Create("DES"); 
 * SymmetricAlgorithm mCrypt = new SymmetricAlgorithm.Create("DES"); 
    TripleDES  RC2  Rijndael  Aes
 * 
*/


using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;
using IE310.Core.Utils;

namespace IE310.Core.IO.Compression
{

    public class I3Cry : I3RijnDaelCry
    {
    }

    public class I3RijnDaelCry
    {
        /// <summary>
        /// 压缩字符串  
        /// 被压缩的结果，在转换时，使用了Convert.ToBase64String，不能使用Encoding.Unicode.GetBytes，因为如果不是
        /// 合法的Unicode字符，将会变换掉
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string CryString(string source, string key)
        {
            //MD5值经ASCII转码，正好为32*8=256位
            string tmpStr = I3MD5.MD5String(key);
            byte[] keyBytes = Encoding.ASCII.GetBytes(tmpStr);  //32*8
            byte[] ivBytes = Encoding.ASCII.GetBytes(I3StringUtil.SubString(tmpStr, 0, 16));  //16*8
            
            return I3CryWeb.Encrypt(source, "Rijndael", keyBytes, ivBytes);
        }

        /// <summary>
        /// 解压缩字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string UnCryString(string source, string key)
        {
            string tmpStr = I3MD5.MD5String(key);
            byte[] keyBytes = Encoding.ASCII.GetBytes(tmpStr);  //32*8
            byte[] ivBytes = Encoding.ASCII.GetBytes(I3StringUtil.SubString(tmpStr, 0, 16));  //16*8
            
            return I3CryWeb.Decrypt(source, "Rijndael", keyBytes, ivBytes);
        }

        /// <summary>
        /// 加密流
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static I3MsgInfo CryStream(Stream source, Stream dest, string key)
        {
            try
            {
                string tmpStr = I3MD5.MD5String(key);
                byte[] keyBytes = Encoding.ASCII.GetBytes(tmpStr);  //32*8
                byte[] ivBytes = Encoding.ASCII.GetBytes(I3StringUtil.SubString(tmpStr, 0, 16));  //16*8                
                SymmetricAlgorithm sym = SymmetricAlgorithm.Create("Rijndael");
                sym.Key = keyBytes;
                sym.IV = ivBytes;

                I3CryWeb cry = new I3CryWeb();
                cry.EncryptData(source, dest, sym, null, null, null);

                return I3MsgInfo.Default;
            }
            catch (Exception ex)
            {
                return new I3MsgInfo(false, ex.Message, ex);
            }
        }

        /// <summary>
        /// 解密流
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static I3MsgInfo UnCryStream(Stream source, Stream dest, string key)
        {
            try
            {
                string tmpStr = I3MD5.MD5String(key);
                byte[] keyBytes = Encoding.ASCII.GetBytes(tmpStr);  //32*8
                byte[] ivBytes = Encoding.ASCII.GetBytes(I3StringUtil.SubString(tmpStr, 0, 16));  //16*8                
                SymmetricAlgorithm sym = SymmetricAlgorithm.Create("Rijndael");
                sym.Key = keyBytes;
                sym.IV = ivBytes;

                I3CryWeb cry = new I3CryWeb();
                cry.DecryptData(source, dest, sym, null, null, null);

                return I3MsgInfo.Default;
            }
            catch (Exception ex)
            {
                return new I3MsgInfo(false, ex.Message, ex);
            }
        }

        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static I3MsgInfo CryFile(string source, string dest, string key)
        {
            if (!File.Exists(source))
            {
                return new I3MsgInfo(false, "源文件不存在!");
            }
            if (!I3FileUtil.CheckFileNotExists(dest))
            {
                return new I3MsgInfo(false, "目标文件无法删除");
            }

            using (FileStream sourceStream = File.OpenRead(source))
            {
                sourceStream.Position = 0;
                using (FileStream destStream = new FileStream(dest, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                {
                    return I3RijnDaelCry.CryStream(sourceStream, destStream, key);
                }
            }
        }

        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static I3MsgInfo UnCryFile(string source, string dest, string key)
        {
            if (!File.Exists(source))
            {
                return new I3MsgInfo(false, "源文件不存在!");
            }
            if (!I3FileUtil.CheckFileNotExists(dest))
            {
                return new I3MsgInfo(false, "目标文件无法删除");
            }

            using (FileStream sourceStream = File.OpenRead(source))
            {
                sourceStream.Position = 0;
                using (FileStream destStream = new FileStream(dest, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                {
                    return I3RijnDaelCry.UnCryStream(sourceStream, destStream, key);
                }
            }
        }
    }

    internal class I3CryWeb
    {
        /// <summary>   
        /// 按指定对称算法、键和向量加密字符串   
        /// </summary>   
        public static string Encrypt(string source, string algName, byte[] rgbKey, byte[] rgbIv)
        {
            var alg = SymmetricAlgorithm.Create(algName);
            var transform = alg.CreateEncryptor(rgbKey, rgbIv);
            var ms = new MemoryStream();
            var encStream = new CryptoStream(ms, transform, CryptoStreamMode.Write);

            byte[] data = Encoding.Unicode.GetBytes(source);
            encStream.Write(data, 0, data.Length);
            encStream.FlushFinalBlock();

            byte[] resultData = ms.ToArray();
            return Convert.ToBase64String(resultData);
        }

        /// <summary>   
        /// 按指定对称算法、键和向量解密数据   
        /// </summary>   
        public static string Decrypt(string source, string algName, byte[] rgbKey, byte[] rgbIv)
        {
            var alg = SymmetricAlgorithm.Create(algName);
            var transform = alg.CreateDecryptor(rgbKey, rgbIv);
            var ms = new MemoryStream();
            var encStream = new CryptoStream(ms, transform, CryptoStreamMode.Write);

            byte[] data = Convert.FromBase64String(source);
            encStream.Write(data, 0, data.Length);
            encStream.FlushFinalBlock();

            byte[] resultData = ms.ToArray();
            return Encoding.Unicode.GetString(resultData);
        }


        //------下面为对文件/流加密解密的需要实例化的部分------   

        /// <summary>   
        /// 完成计算的回调   
        /// </summary>   
        public delegate void EndCallback(I3EndState endState);

        /// <summary>   
        /// 进行计算时的回调   
        /// </summary>   
        public delegate void ProgressCallback(I3ProgressState progressState);

        private bool _cancel; //取消计算   

        /// <summary>   
        /// 停止计算   
        /// </summary>   
        public void Stop()
        {
            _cancel = true;
        }

        /// <summary>   
        /// 按指定对称算法、键和向量将输入文件加密结果保存为输出文件   
        /// </summary>   
        /// <param name="inFileName">输入文件</param>   
        /// <param name="outFileName">输出文件</param>   
        /// <param name="algName">对称算法名称</param>   
        /// <param name="rgbKey">键</param>   
        /// <param name="rgbIv">向量</param>   
        /// <param name="progressCallback">进度回调函数</param>   
        /// <param name="endCallback">结束计算的回调函数</param>   
        /// <param name="state">传递给回调的参数</param>   
        public void EncryptData(string inFileName, string outFileName, string algName, byte[] rgbKey, byte[] rgbIv, ProgressCallback progressCallback, EndCallback endCallback, object state)
        {
            var alg = SymmetricAlgorithm.Create(algName);
            alg.Key = rgbKey;
            alg.IV = rgbIv;
            EncryptData(inFileName, outFileName, alg, progressCallback, endCallback, state);
        }

        /// <summary>   
        /// 按指定对称密钥将输入文件加密结果保存为输出文件   
        /// </summary>   
        /// <param name="inFileName">输入文件</param>   
        /// <param name="outFileName">输出文件</param>   
        /// <param name="alg">对称密钥</param>   
        /// <param name="progressCallback">进度回调函数</param>   
        /// <param name="endCallback">结束计算的回调函数</param>   
        /// <param name="state">传递给回调的参数</param>   
        public void EncryptData(string inFileName, string outFileName, SymmetricAlgorithm alg, ProgressCallback progressCallback, EndCallback endCallback, object state)
        {
            using (var infs = new FileStream(inFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var outfs = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                {
                    EncryptData(infs, outfs, alg, progressCallback, endCallback, state);
                }
            }
        }

        /// <summary>   
        /// 按指定对称密钥将输入流加密结果保存为输出流   
        /// </summary>   
        /// <param name="inStream">输入流（可读）</param>   
        /// <param name="outStream">输出流（可写）</param>   
        /// <param name="alg">对称密钥</param>   
        /// <param name="progressCallback">进度回调函数</param>   
        /// <param name="endCallback">结束计算的回调函数</param>   
        /// <param name="state">传递给回调的参数</param>   
        public void EncryptData(Stream inStream, Stream outStream, SymmetricAlgorithm alg, ProgressCallback progressCallback, EndCallback endCallback, object state)
        {
            const int bufferSize = 100;
            _cancel = false;

            long bytesRead = 0L;
            long totalBytesLength = inStream.Length;
            var buffer = new byte[bufferSize];

            using (var encStream = new CryptoStream(outStream, alg.CreateEncryptor(), CryptoStreamMode.Write))
            {
                int num;
                do
                {
                    num = inStream.Read(buffer, 0, bufferSize);
                    encStream.Write(buffer, 0, num);
                    if (progressCallback == null)
                        continue;
                    bytesRead += num;
                    progressCallback(new I3ProgressState(bytesRead, totalBytesLength, state)); //进度回调   
                } while (num > 0 && !_cancel);
                if (endCallback != null)
                    endCallback(new I3EndState(_cancel, state)); //计算结束回调   
            }
        }

        /// <summary>   
        /// 按指定对称算法、键和向量将输入文件解密结果保存为输出文件   
        /// </summary>   
        /// <param name="inFileName">输入文件</param>   
        /// <param name="outFileName">输出文件</param>   
        /// <param name="algName">对称算法名称</param>   
        /// <param name="rgbKey">键</param>   
        /// <param name="rgbIv">向量</param>   
        /// <param name="progressCallback">进度回调函数</param>   
        /// <param name="endCallback">结束计算的回调函数</param>   
        /// <param name="state">传递给回调的参数</param>   
        public void DecryptData(string inFileName, string outFileName, string algName, byte[] rgbKey, byte[] rgbIv, ProgressCallback progressCallback, EndCallback endCallback, object state)
        {
            var alg = SymmetricAlgorithm.Create(algName);
            alg.Key = rgbKey;
            alg.IV = rgbIv;
            DecryptData(inFileName, outFileName, alg, progressCallback, endCallback, state);
        }

        /// <summary>   
        /// 按指定对称密钥将输入文件解密结果保存为输出文件   
        /// </summary>   
        /// <param name="inFileName">输入文件</param>   
        /// <param name="outFileName">输出文件</param>   
        /// <param name="alg">对称密钥</param>   
        /// <param name="progressCallback">进度回调函数</param>   
        /// <param name="endCallback">结束计算的回调函数</param>   
        /// <param name="state">传递给回调的参数</param>   
        public void DecryptData(string inFileName, string outFileName, SymmetricAlgorithm alg, ProgressCallback progressCallback, EndCallback endCallback, object state)
        {
            using (var infs = new FileStream(inFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var outfs = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                {
                    DecryptData(infs, outfs, alg, progressCallback, endCallback, state);
                }
            }
        }

        /// <summary>   
        /// 按指定对称密钥将输入流解密结果保存为输出流   
        /// </summary>   
        /// <param name="inStream">输入流（可读）</param>   
        /// <param name="outStream">输出流（可写）</param>   
        /// <param name="alg">对称密钥</param>   
        /// <param name="progressCallback">进度回调函数</param>   
        /// <param name="endCallback">结束计算的回调函数</param>   
        /// <param name="state">传递给回调的参数</param>   
        public void DecryptData(Stream inStream, Stream outStream, SymmetricAlgorithm alg, ProgressCallback progressCallback, EndCallback endCallback, object state)
        {
            const int bufferSize = 100;
            _cancel = false;

            long bytesRead = 0L;
            long totalBytesLength = inStream.Length;
            var buffer = new byte[bufferSize];

            using (var encStream = new CryptoStream(inStream, alg.CreateDecryptor(), CryptoStreamMode.Read))
            {
                int num;
                do
                {
                    num = encStream.Read(buffer, 0, bufferSize);
                    outStream.Write(buffer, 0, num);
                    if (progressCallback == null)
                        continue;
                    bytesRead += num;
                    progressCallback(new I3ProgressState(bytesRead, totalBytesLength, state)); //进度回调   
                } while (num > 0 && !_cancel);
                if (endCallback != null)
                    endCallback(new I3EndState(_cancel, state)); //计算结束回调   
            }
        }


        /// <summary>   
        /// 为EndCallback提供数据   
        /// </summary>   
        public class I3EndState
        {
            internal I3EndState(bool isCancel, object state)
            {
                IsCancel = isCancel;
                State = state;
            }

            /// <summary>   
            /// 是否取消退出的   
            /// </summary>   
            public bool IsCancel
            {
                get;
                private set;
            }

            /// <summary>   
            /// 获得传递的参数   
            /// </summary>   
            public object State
            {
                get;
                private set;
            }
        }


        /// <summary>   
        /// 为ProgressCallback提供数据   
        /// </summary>   
        public class I3ProgressState
        {
            internal I3ProgressState(long byteRead, long totalBytesLength, object state)
            {
                BytesRead = byteRead;
                TotalBytesLength = totalBytesLength;
                State = state;
            }

            /// <summary>   
            /// 获得已经计算完成的字节数   
            /// </summary>   
            public long BytesRead
            {
                get;
                private set;
            }

            /// <summary>   
            /// 获得总字节数   
            /// </summary>   
            public long TotalBytesLength
            {
                get;
                private set;
            }

            /// <summary>   
            /// 获得传递的参数   
            /// </summary>   
            public object State
            {
                get;
                private set;
            }
        }
    }
}

