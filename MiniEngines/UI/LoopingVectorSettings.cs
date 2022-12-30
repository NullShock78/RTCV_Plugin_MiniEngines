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
using MiniEngines.Engines.LoopingVector;
using System.Windows.Forms;

namespace MiniEngines.UI
{
    public partial class LoopingVectorSettings : ComponentForm, IColorize
    {
        public LoopingVectorSettings()
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
            
            Resync();
        }

        public void Resync()
        {
            EngineSpec.Suspend();
            ComboBoxItem<string> item2 = (ComboBoxItem<string>)cbDynaVectorLimiterList.SelectedItem;
            if (item2 != null)
            {
                LoopingVectorEngine.LimiterListHash = item2.Value;
            }
            ComboBoxItem<string> item = (ComboBoxItem<string>)cbDynaVectorValueList.SelectedItem;
            if (item != null)
            {
                LoopingVectorEngine.ValueListHash = item.Value;
            }
            LoopingVectorEngine.NumExtraUnits = (int)nmBleedBack.Value;
            LoopingVectorEngine.IntervalBetweenUnitToggle = (int)nmBleedForward.Value;

            EngineSpec.ResumeAndPush();
        }

        private void CbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private void CbDynaVectorValueList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem<string> item = (ComboBoxItem<string>)((ComboBox)sender).SelectedItem;
            if (item != null)
            {
                LoopingVectorEngine.ValueListHash = item.Value;
            }
        }

        private void CbDynaVectorLimiterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem<string> item = (ComboBoxItem<string>)((ComboBox)sender).SelectedItem;
            if (item != null)
            {
                LoopingVectorEngine.LimiterListHash = item.Value;
            }
        }

        private void nmBleedBack_ValueChanged(object sender, EventArgs e)
        {
            LoopingVectorEngine.NumExtraUnits = (int)nmBleedBack.Value;
        }

        private void nmBleedForward_ValueChanged(object sender, EventArgs e)
        {
            LoopingVectorEngine.IntervalBetweenUnitToggle = (int)nmBleedForward.Value;
        }

        private void cbInclZero_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void bResync_Click(object sender, EventArgs e)
        {
            Resync();
        }
    }
}
