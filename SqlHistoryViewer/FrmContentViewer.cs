using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlHistoryViewer
{
    public partial class FrmContentViewer : Form
    {
        NgFileMaker ngFileMaker = new NgFileMaker();
        public FrmContentViewer(ScriptHistoryData scriptHistoryData)
        {
            InitializeComponent();
            ScriptHistoryData = scriptHistoryData;
        }

        public ScriptHistoryData ScriptHistoryData { get; }

        private void FrmContentViewer_Load(object sender, EventArgs e)
        {
            txtContent.Text = ScriptHistoryData.QueryData;
            Text = ScriptHistoryData.DeployVersion + " - " + ScriptHistoryData.FileName;
        }

        private void btnExportToFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = ScriptHistoryData.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ngFileMaker.WriteToFile(saveFileDialog1.FileName, ScriptHistoryData.QueryData);
                MessageBox.Show("Export file successfull", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
