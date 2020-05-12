using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace JibresBooster1.lib
{
    internal class service
    {
        public static void start(string serviceName, int timeoutMilliseconds)
        {

            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = 0;
                TimeSpan timeout;
                //this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalService;

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec1));
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);

                //service.WaitForStatus(ServiceControllerStatus.Running);
                //service.Stop();
                //service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
        public static void stop(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = 0;
                TimeSpan timeout;
                if (service.Status == ServiceControllerStatus.Running)
                {
                    millisec1 = Environment.TickCount;
                    timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }


            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
        public static void restart(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = 0;
                TimeSpan timeout;
                if (service.Status == ServiceControllerStatus.Running)
                {
                    millisec1 = Environment.TickCount;
                    timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);

            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}
