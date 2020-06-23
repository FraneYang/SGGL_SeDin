using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.Win32;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    public static class SoftRegeditService
    {
        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        private static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"d:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        /// <summary>
        /// 获得CPU的序列号
        /// </summary>
        /// <returns></returns>
        private static string getCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            try
            {
                ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
                foreach (ManagementObject myObject in myCpuConnection)
                {
                    strCpu = myObject.Properties["Processorid"].Value.ToString();
                    break;
                }
            }
            catch(Exception ex)
            {
                strCpu = ex.ToString();
            }

            if (System.Web.HttpContext.Current.Application["CPUID"] == null)
            {
                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["CPUID"] = strCpu;
                System.Web.HttpContext.Current.Application.UnLock();
            }
            return strCpu;
        }

        /// <summary>
        /// 获取序列号

        /// </summary>
        /// <returns></returns>
        public static string SerialId()
        {
            string serialId = getCpu() + GetDiskVolumeSerialNumber();//获得24位Cpu和硬盘序列号
            string lastStr = String.Empty;
            for (int i = 0; i < serialId.Length; i = i + 6)
            {
                lastStr = lastStr + serialId.Substring(i, 6) + "-";
            }
            return lastStr.Substring(0, lastStr.Length - 1);
        }

        /// <summary>
        /// 获取注册码

        /// </summary>
        /// <returns>注册码</returns>
        public static string GetRegeditCode()
        { 
          RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("SOFTWARE");
            RegistryKey choseKey = softwareKey.OpenSubKey("SOW");
            if (choseKey != null)
            {
                RegistryKey SowKey = choseKey.OpenSubKey("GOL");
                if (SowKey != null)               //防止在"SOW"键下有多键值时产生错误
                {
                    string valueId = SowKey.GetValue("RegisterCode").ToString();
                    return valueId;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 注册信息写入函数
        /// </summary>
        /// <param name="serialId">序列号</param>
        /// <param name="registerCode">注册码</param>
        public static void WriteRegedit(string serialId, string registerCode)
        {
            // 以可写的方式打开SOFTWARE子键
            RegistryKey key = Registry.LocalMachine;
            RegistryKey softwareKey = key.OpenSubKey("SOFTWARE", true);
            // 依次创建Chose子键和Value子键
            RegistryKey choseKey = softwareKey.CreateSubKey("SOW");
            RegistryKey ValueKey = choseKey.CreateSubKey("GOL");
            // 设置Value键的SetValue方法可能引发异常
            ValueKey.SetValue("SerialId", serialId);
            ValueKey.SetValue("RegisterCode", registerCode);
        }

        /// <summary>
        /// 是否注册
        /// </summary>
        /// <returns></returns>
        public static bool IsRegedit()
        {
            RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("SOFTWARE");
            RegistryKey choseKey = softwareKey.OpenSubKey("SOW");
            if (choseKey != null)
            {
                RegistryKey SowKey = choseKey.OpenSubKey("GOL");
                if (SowKey != null)               //防止在"SOW"键下有多键值时产生错误
                {
                    string keyId = SowKey.GetValue("SerialId").ToString();
                    string valueId = SowKey.GetValue("RegisterCode").ToString();
                    
                    //string str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(keyId, "MD5");
                   
                    string str = Md532(keyId);
                    string substr = str.Substring(8, 24);
                    string lastStr = String.Empty;
                    for (int i = 0; i < substr.Length; i = i + 6)
                    {
                        lastStr = lastStr + substr.Substring(i, 6) + "-";
                    }
                    string regeditCode = lastStr.Substring(0, lastStr.Length - 1);

                    if (regeditCode == valueId && keyId == SerialId())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private static string Md532(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i]);

            }
            return sb.ToString();
        }
    }
}