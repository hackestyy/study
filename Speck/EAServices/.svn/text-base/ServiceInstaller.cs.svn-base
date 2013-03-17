using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ZteApp.ProductService.EAServices
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : System.Configuration.Install.Installer
    {
        private ServiceProcessInstaller mProcess;
        private System.ServiceProcess.ServiceInstaller mService;

        public ServiceInstaller()
        {
            InitializeComponent();
            mProcess = new ServiceProcessInstaller();
            mProcess.Account = ServiceAccount.LocalSystem;
            mService = new System.ServiceProcess.ServiceInstaller();
            mService.Description = Resource1.EngineeringServiceDescription;
            mService.ServiceName = "EngineeringService";
            Installers.Add(mProcess);
            Installers.Add(mService);
        }
    }
}
