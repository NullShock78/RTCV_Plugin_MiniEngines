using MiniEngines.UI;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MiniEngines
{
    /// <summary>
    /// This lies on the Emulator(Client) side
    /// </summary>
    internal class PluginConnectorEMU : IRoutable
    {
        public PluginConnectorEMU()
        {
            LocalNetCoreRouter.registerEndpoint(this, PluginRouting.Endpoints.EMU_SIDE);
        }

        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            NetCoreAdvancedMessage message = e.message as NetCoreAdvancedMessage;

            switch (message.Type)
            {
                case PluginRouting.Commands.SYNC:
                    var data = message.objectValue as Dictionary<string, object>;
                    EngineSpec.Sync(data);
                    break;
                default:
                    break;
            }
            return e.returnMessage;
        }
    }
}
