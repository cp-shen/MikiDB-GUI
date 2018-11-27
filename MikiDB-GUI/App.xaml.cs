using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MikiDB.Core;

namespace MikiDB_GUI {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            DbManager.Instance.Deserialize();
        }

        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);

            DbManager.Instance.Serialize();
        }
    }
}
