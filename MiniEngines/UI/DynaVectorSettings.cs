using MiniEngines.Engines;
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
    public partial class DynaVectorSettings : ComponentForm, IColorize
    {
        public DynaVectorSettings()
        {
            InitializeComponent();

            cbDynaVectorValueList.DataSource = null;
            cbDynaVectorLimiterList.DataSource = null;

            this.Shown += DynaVectorSettings_Shown;
        }

        private void DynaVectorSettings_Shown(object sender, EventArgs e)
        {
            cbDynaVectorValueList.DisplayMember = "Name";
            cbDynaVectorLimiterList.DisplayMember = "Name";
            cbDynaVectorValueList.ValueMember = "Value";
            cbDynaVectorLimiterList.ValueMember = "Value";

            cbDynaVectorLimiterList.SelectedIndexChanged += CbDynaVectorLimiterList_SelectedIndexChanged;
            cbDynaVectorValueList.SelectedIndexChanged += CbDynaVectorValueList_SelectedIndexChanged;

            cbDynaVectorValueList.DataSource = RtcCore.ValueListBindingSource;
            cbDynaVectorLimiterList.DataSource = RtcCore.LimiterListBindingSource;

            cbMode.SuspendLayout();
            var engModes = Enum.GetNames(typeof(EngineMethod));
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
            DynaVectorEngine.Method = (EngineMethod)System.Enum.Parse(typeof(EngineMethod), (string)cbMode.SelectedItem);
            ComboBoxItem<string> item2 = (ComboBoxItem<string>)cbDynaVectorLimiterList.SelectedItem;
            if (item2 != null)
            {
                DynaVectorEngine.LimiterListHash = item2.Value;
            }
            ComboBoxItem<string> item = (ComboBoxItem<string>)cbDynaVectorValueList.SelectedItem;
            if (item != null)
            {
                DynaVectorEngine.ValueListHash = item.Value;
            }
            DynaVectorEngine.BleedBackwards = (int)nmBleedBack.Value;
            DynaVectorEngine.BleedForwards = (int)nmBleedForward.Value;
            DynaVectorEngine.IncludeNeighboringZeros = cbInclZero.Checked;

            EngineSpec.ResumeAndPush();
        }

        private void CbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DynaVectorEngine.Method = (EngineMethod)System.Enum.Parse(typeof(EngineMethod), (string)cbMode.SelectedItem);
        }


        private void CbDynaVectorValueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem<string> item = (ComboBoxItem<string>)((ComboBox)sender).SelectedItem;
            if (item != null)
            {
                DynaVectorEngine.ValueListHash = item.Value;
            }
        }

        private void CbDynaVectorLimiterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem<string> item = (ComboBoxItem<string>)((ComboBox)sender).SelectedItem;
            if (item != null)
            {
                DynaVectorEngine.LimiterListHash = item.Value;
            }
        }

        private void nmBleedBack_ValueChanged(object sender, EventArgs e)
        {
            DynaVectorEngine.BleedBackwards = (int)nmBleedBack.Value;
        }

        private void nmBleedForward_ValueChanged(object sender, EventArgs e)
        {
            DynaVectorEngine.BleedForwards = (int)nmBleedForward.Value;
        }

        private void cbInclZero_CheckedChanged(object sender, EventArgs e)
        {
            DynaVectorEngine.IncludeNeighboringZeros = cbInclZero.Checked;
        }

        private void bResync_Click(object sender, EventArgs e)
        {
            Resync();
        }
    }
}
