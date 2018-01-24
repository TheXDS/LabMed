using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART.PluginSupport;

namespace Sinergia.SLM.Modules
{
    public abstract class Modulo : Plugin
    {
        protected MainWindow App { get; }
        public Modulo() { App = MainWindow.Instance; }
    }



    [AttributeUsage(AttributeTargets.Class)]
    sealed class MultiInstanceAttribute : Attribute { }
}
