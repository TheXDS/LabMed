using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART.PluginSupport;
using TheXDS.MCART.Attributes;

namespace Sinergia.SLM.Modules
{
    [MinMCARTVersion(0,8,1,0)]
    [TargetMCARTVersion(0,8,1,0)]
    public class ConceptModule : Modulo
    {

        [InteractionItem]
        public void ShowTestPage(object sender, EventArgs e)
        {
            this.App.AddPage(new Pages.TestPage());
        }
    }
}
