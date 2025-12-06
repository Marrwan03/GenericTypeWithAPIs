using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehiclesApp.GlobalClasses;
using VehiclesApp.GlobalControls;

namespace VehiclesApp.VehicleBodies
{
    public partial class frmAllVehicleBodies : Form
    {
        clsEnum.enFilter _FilterType;
        DataTable _dtAllBodies;
        public frmAllVehicleBodies()
        {
            InitializeComponent();
        }

        public async void LoadData()
        {
            _dtAllBodies = clsConvert.ToDataTable(await GetAllBodies());
            dgvAllBodies.DataSource = _dtAllBodies;
            if (dgvAllBodies.Rows.Count > 0)
                dgvAllBodies.Columns[0].Width = 90;

            lblEmptyResult.Visible = dgvAllBodies.Rows.Count <= 0;
            lblNumberOfRecord.Text = dgvAllBodies.Rows.Count.ToString();
            dgvAllBodies.EnableHeadersVisualStyles = false;
            dgvAllBodies.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
        }
        public async Task<bool> DeleteBodyBy(int ID)
        {
            bool Success = false;
            var response = await clsHTTPMethod<BodyDTO>.DeleteDTO(clsHttpClients.Body, ID);
            if (response == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show($"Body ID[{ID}] is deleted successful.", "Success Process",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Success = true;
            }
            else if (response == System.Net.HttpStatusCode.BadRequest)
                MessageBox.Show($"Bad request: This BodyID[{ID}] isn`t valid.", "Fail Process",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (response == System.Net.HttpStatusCode.NotFound)
                MessageBox.Show($"Not Found: This BodyID isn`t exists.", "Fail Process",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            return Success;
        }
        public async Task<List<BodyDTO>> GetAllBodies()
        {
            lblTitlePage.Text = "Get Data";
            ctrlDownloadDot1.Start();
            ctrlDownloadDot1.Visible = true;
            //BodiesTest
            //GetAllBodies
            var AllBodies = await clsHTTPMethod<BodyDTO>.GetAllDTOs(clsHttpClients.Body);

            if (AllBodies.StatusCode == System.Net.HttpStatusCode.NotFound)
                MessageBox.Show("There isn`t any bodies", "Empty Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

            lblTitlePage.Text = "All Body Vehicles";
            ctrlDownloadDot1.Stop();
            ctrlDownloadDot1.Visible = false;
            return AllBodies.DTOList;
        }
        string _GetFilterStringBy(clsEnum.enFilter enFilter, string Text)
        {
            switch (enFilter)
            {
                case clsEnum.enFilter.ID:
                    return $"[bodyID] = {Text}";
                case clsEnum.enFilter.Name:
                    return $"[bodyName] LIKE '{Text}%'";
                default:
                    return null;
            }
        }
        private void frmAllVehicleBodies_Load(object sender, EventArgs e)
        {
            clsHttpClients.Defining(clsEnum.enClients.Body);
            this.Region = clsFormat.CornerForm(Width, Height);
            ctrlBasicSearch1.ctrlBasicSearch_Load();
            LoadData();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ctrlBasicSearch1_OnFilterName(string obj)
        {
            if(!string.IsNullOrEmpty(obj))
                _dtAllBodies.DefaultView.RowFilter = _GetFilterStringBy(_FilterType, obj);
            else
                _dtAllBodies.DefaultView.RowFilter = "";
            lblNumberOfRecord.Text = dgvAllBodies.Rows.Count.ToString();
            lblEmptyResult.Visible = dgvAllBodies.Rows.Count <= 0;
        }
        private void ctrlBasicSearch1_OnFilterType(clsEnum.enFilter obj)
        {
            _FilterType = obj;
        }
        private void ctrlBasicSearch1_OnAdd()
        {
            frmAddUpdateBody frmAddBody = new frmAddUpdateBody();
            frmAddBody.ShowDialog();
            LoadData();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBodyDetail frmBodyDetail = new frmBodyDetail((int)dgvAllBodies.CurrentRow.Cells[0].Value);
            frmBodyDetail.ShowDialog();
        }

        private void btnClose_MouseMove(object sender, MouseEventArgs e)
        {
            clsEvents.GunaCircleButton_MouseMove(sender, null);
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            clsEvents.GunaCircleButton_MouseLeave(sender, null);
        }
        void ToolStripMenuItem_MouseLeave(object sender, EventArgs e)
            => clsEvents.ToolStripMenuItem_MouseLeave(sender);
        void ToolStripMenuItem_MouseMove(object sender, MouseEventArgs e)
            => clsEvents.ToolStripMenuItem_MouseMove(sender);

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateBody updateBody = new frmAddUpdateBody((int)dgvAllBodies.CurrentRow.Cells[0].Value);
            updateBody.ShowDialog();
            LoadData();
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure do you want delete This Body[{(string)dgvAllBodies.CurrentRow.Cells[1].Value}]?", "Continue!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
           if(await DeleteBodyBy((int)dgvAllBodies.CurrentRow.Cells[0].Value))
                LoadData();

        }
    }
}
