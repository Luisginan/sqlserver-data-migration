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
    public partial class FrmViewer : Form
    {

        NgFileMaker ngFileMaker = new NgFileMaker();
        List<ScriptHistoryData> listScriptHistoryData;
        List<ScriptHistoryData> result;
        public FrmViewer()
        {
            InitializeComponent();
        }

        private void FrmViewer_Load(object sender, EventArgs e)
        {
            var xmlPath = $@"{Environment.CurrentDirectory}\SqlExecutorHistory.xml";
            txtXmlLocation.Text = xmlPath;

            listScriptHistoryData = ngFileMaker.ReadConfig<List<ScriptHistoryData>>(txtXmlLocation.Text);
            FillCboVersion(listScriptHistoryData);

            RefreshUI(listScriptHistoryData);

        }

        private void FillCboVersion(List<ScriptHistoryData> listScriptHistoryData)
        {
            var versionList = listScriptHistoryData.Select(x => x.DeployVersion).Distinct().ToList();
            cboVersion.Items.Clear();
            versionList.ForEach(x => cboVersion.Items.Add(x));
            cboVersion.Items.Add("");
        }

        private void RefreshUI(List<ScriptHistoryData> listScriptHistoryData)
        {

            result = listScriptHistoryData.Where(x => x.DeployVersion.ToLower().Contains(cboVersion.Text.ToLower()) &&
                                                          x.FileName.ToLower().Contains(txtCriteria.Text.ToLower()))
                                                          .ToList();
            dgFileSql.DataSource = null;
            dgFileSql.AutoGenerateColumns = false;
            dgFileSql.DataSource = result;
            dgFileSql.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(*.xml)|*xml";
            openFileDialog1.FileName = "SqlExecutorHistory.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtXmlLocation.Text = openFileDialog1.FileName;
                listScriptHistoryData = ngFileMaker.ReadConfig<List<ScriptHistoryData>>(txtXmlLocation.Text);
                FillCboVersion(listScriptHistoryData);
                RefreshUI(listScriptHistoryData);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RefreshUI(listScriptHistoryData);
        }

        private void dgFileSql_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgFileSql.Columns[e.ColumnIndex].Name.ToLower() == "cOPenContent".ToLower())
            {
                FrmContentViewer frmContentViewer = new FrmContentViewer(result[e.RowIndex]);
                frmContentViewer.ShowDialog();
            }
        }
    }
}
