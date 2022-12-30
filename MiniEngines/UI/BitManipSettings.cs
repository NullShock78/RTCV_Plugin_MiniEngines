using MiniEngines.Engines.BitManip;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Modular;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MiniEngines.UI
{
    public partial class BitManipSettings : ComponentForm, IColorize
    {
        public BitManipSettings()
        {
            InitializeComponent();

            this.Shown += DynaVectorSettings_Shown;
        }

        private void DynaVectorSettings_Shown(object sender, EventArgs e)
        {

            cbMode.SuspendLayout();
            var engModes = Enum.GetNames(typeof(ManipMode));
            foreach (var engMode in engModes)
            {
                cbMode.Items.Add(engMode);
            }
            cbMode.ResumeLayout();
            cbMode.SelectedIndexChanged += CbMode_SelectedIndexChanged;
            cbMode.SelectedIndex = 0;
            Resync();
        }

        public void Resync()
        {
            EngineSpec.Suspend();
            BitManipEngine.EngineMode = (ManipMode)System.Enum.Parse(typeof(ManipMode), (string)cbMode.SelectedItem);

            EngineSpec.ResumeAndPush();
        }

        private void CbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BitManipEngine.EngineMode = (ManipMode)System.Enum.Parse(typeof(ManipMode), (string)cbMode.SelectedItem);
        }

        private void bResync_Click(object sender, EventArgs e)
        {
            Resync();
        }
    }
}
