using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.ServiceProcess;
using System.Timers;
using System.IO;
using System.Threading;


namespace IE310.Core.WinService
{
    /// <summary>
    /// 服务控制器
    /// </summary>
    public class I3ServiceManager : IDisposable
    {
        /// <summary>
        /// 创建服务管理器
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="refreshSecond">服务状态刷新间隔</param>
        public I3ServiceManager(string serviceName, int refreshSecond)
        {
            this.serviceName = serviceName;
            this.refreshSecond = refreshSecond;

            InnerInit();
        }

        private void InnerInit()
        {
            ServiceController[] controllers = ServiceController.GetServices();
            foreach (ServiceController controller in controllers)
            {
                if (controller.ServiceName == serviceName)
                {
                    serviceController = controller;
                    break;
                }
            }

            timerRefreshState = new System.Timers.Timer(this.refreshSecond * 1000);
            timerRefreshState.Elapsed += new ElapsedEventHandler(timerRefreshState_Elapsed);
            timerRefreshState.Start();
        }

        private void InnerStop()
        {
            if (serviceController != null)
            {
                serviceController.Dispose();
            }
            serviceController = null;
            timerRefreshState.Elapsed -= new ElapsedEventHandler(timerRefreshState_Elapsed);
            timerRefreshState.Stop();
            timerRefreshState.Dispose();
            timerRefreshState = null;
        }

        /// <summary>
        /// 刷新
        /// waitMillSecond:等待多少毫秒
        /// </summary>
        public void Refresh(int waitMillSeconds)
        {
            InnerStop();
            Thread.Sleep(waitMillSeconds);
            InnerInit();
        }


        /// <summary>
        /// 控制的服务的名称
        /// </summary>
        private string serviceName;

        private int refreshSecond;
        public int RefreshSecond
        {
            get
            {
                return refreshSecond;
            }
            set
            {
                refreshSecond = value;
                if (this.timerRefreshState != null)
                {
                    this.timerRefreshState.Stop();
                    this.timerRefreshState.Interval = this.refreshSecond * 1000;
                    this.timerRefreshState.Start();
                }
            }
        }
        private ServiceController serviceController;

        /// <summary>
        /// 定时器，用于实时获取服务的状态值
        /// </summary>
        private System.Timers.Timer timerRefreshState;

        /// <summary>
        /// 服务是否存在
        /// </summary>
        public bool ServiceExists
        {
            get
            {
                return serviceController != null;
            }
        }


        /// <summary>
        /// 实时获取服务状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRefreshState_Elapsed(object sender, ElapsedEventArgs e)
        {
            ServiceControllerStatus status = GetServiceStatus();
            OnStateChange(status);

            //如果服务不存在，停止刷新状态  //服务不在也要刷新，可能重新安装了
            //if (!ServiceExists && timerRefreshState != null)
            //{
            //    timerRefreshState.Stop();
            //}
        }

        /// <summary>
        /// 状态改变事件
        /// </summary>
        public event ServiceStatusChangedEvent StateChanged;
        private void OnStateChange(ServiceControllerStatus status)
        {
            if (StateChanged != null)
            {
                StateChanged(status);
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public void StartService(int tryTimes, int waitMillseconds)
        {
            if (serviceController == null)
            {
                return;
            }

            while (tryTimes > 0)
            {
                try
                {
                    serviceController.Refresh();
                    if (serviceController.Status != ServiceControllerStatus.Running && serviceController.Status != ServiceControllerStatus.StartPending)
                    {
                        serviceController.Start();
                        Thread.Sleep(waitMillseconds);
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                }

                tryTimes--;
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void StopService(int tryTimes, int waitMillseconds)
        {
            if (serviceController == null)
            {
                return;
            }

            while (tryTimes > 0)
            {
                try
                {
                    serviceController.Refresh();
                    if (serviceController.Status != ServiceControllerStatus.Stopped)
                    {
                        serviceController.Stop();
                        Thread.Sleep(waitMillseconds);
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                }

                tryTimes--;
            }
        }

        /// <summary>
        /// 暂停服务
        /// </summary>
        public void PauseService(int tryTimes, int waitMillseconds)
        {
            if (serviceController == null)
            {
                return;
            }

            while (tryTimes > 0)
            {
                try
                {
                    serviceController.Refresh();
                    if (serviceController.Status == ServiceControllerStatus.Running)
                    {
                        serviceController.Pause();
                        Thread.Sleep(waitMillseconds);
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                }

                tryTimes--;
            }
        }

        /// <summary>
        /// 返回服务的当前状态
        /// </summary>
        /// <returns></returns>
        public ServiceControllerStatus GetServiceStatus()
        {
            if (serviceController == null)
            {
                return (ServiceControllerStatus)(-1);
            }

            try
            {
                serviceController.Refresh();
                return serviceController.Status;
            }
            catch
            {
                return (ServiceControllerStatus)(-1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (timerRefreshState != null)
            {
                timerRefreshState.Stop();
                timerRefreshState.Dispose();
            }
        }
    }

    /// <summary>
    /// 服务状态更改委托
    /// </summary>
    /// <param name="serviceSate"></param>
    public delegate void ServiceStatusChangedEvent(ServiceControllerStatus status);
}
