using MiniEngines.Engines;
using MiniEngines.UI;
using RTCV.Common;
using RTCV.NetCore;
using RTCV.PluginHost;
using RTCV.UI;
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace MiniEngines
{
    [Export(typeof(IPlugin))]
    public class PluginCore : IPlugin, IDisposable
    {
        public string Name => "MiniEngines";
        public string Description => "A";

        public string Author => "None";

        public Version Version => new Version(1, 0, 0);

        /// <summary>
        /// Defines which sides will call Start
        /// </summary>
        public RTCSide SupportedSide => RTCSide.Both;
        internal static RTCSide CurrentSide = RTCSide.Both;

        internal static PluginConnectorEMU connectorEMU = null;
        internal static PluginConnectorRTC connectorRTC = null;
        public void Dispose()
        {
        }

        public bool Start(RTCSide side)
        {
            if (side == RTCSide.Client)
            {
                connectorEMU = new PluginConnectorEMU();
            }
            else if (side == RTCSide.Server)
            {
                connectorRTC = new PluginConnectorRTC();

                var form = new DynaVectorSettings();
                S.SET<DynaVectorSettings>(form);
                form.TopLevel = false;

                S.GET<CorruptionEngineForm>().RegisterPluginEngine(new DynaVectorEngine(form));

            }
            CurrentSide = side;
            return true;
        }

        internal static string GetOtherSide()
        {
            return (PluginCore.CurrentSide == RTCV.PluginHost.RTCSide.Server ? PluginRouting.Endpoints.EMU_SIDE : PluginRouting.Endpoints.RTC_SIDE);
        }

        public bool StopPlugin()
        {
            return true;
        }
    }
}
