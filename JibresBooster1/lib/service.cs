﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace JibresBooster1.lib
{
    class service
    {
        public static void start(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = 0;
                TimeSpan timeout;

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec1));
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);

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
