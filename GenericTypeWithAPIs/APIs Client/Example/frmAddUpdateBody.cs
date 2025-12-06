using DTO;
using System.ComponentModel;
using System.Net.Http.Json;
using VehiclesApp.GlobalClasses;

namespace VehiclesApp.VehicleBodies
{
    public partial class frmAddUpdateBody : Form
    {
        public frmAddUpdateBody()
        {
            InitializeComponent();
            _Mode = clsEnum.enMode.Add;
        }
        public frmAddUpdateBody(int BodyID)
        {
            InitializeComponent();
            _ID = BodyID;
            _Mode = clsEnum.enMode.Update;
        }
        clsEnum.enMode _Mode;
        int _ID;
        public event Action<int> DataBack;
        private void txtBodyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        async Task _UpdateBody(int ID, BodyDTO BodyDTO)
        {
            lblTitlePage.Text = "Wait for Updated";
            ctrlDownloadDot1.Location = new Point(376, 12);
            ctrlDownloadDot1.Visible = true;
            ctrlDownloadDot1.Start();
            var StatusCode = await clsHTTPMethod<BodyDTO>.UpdateDTO(clsHttpClients.Body, ID, BodyDTO);
            if(StatusCode == System.Net.HttpStatusCode.OK)
                MessageBox.Show($"Body with ID[{ID}] updated successful.",
                        "Successed Process.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (StatusCode == System.Net.HttpStatusCode.BadRequest)
                MessageBox.Show($"Body with ID[{ID}] updated failed.",
                   "Failed Process.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (StatusCode == System.Net.HttpStatusCode.NotFound)
                MessageBox.Show($"Body ID[{ID}] is not found.",
                   "Failed Process.", MessageBoxButtons.OK, MessageBoxIcon.Error);

            lblTitlePage.Text = $"Update BodyID[{lblBodyID.Text}]";
            ctrlDownloadDot1.Visible = false;
            ctrlDownloadDot1.Stop();
        }
        async Task _AddNewBody(BodyDTO NewBodyDTO)
        {
                lblTitlePage.Text = "Wait for Added";
                ctrlDownloadDot1.Location = new Point(348, 12);
                ctrlDownloadDot1.Visible = true;
                ctrlDownloadDot1.Start();
            var AddNewDTO = await clsHTTPMethod<BodyDTO>.AddNewDTO(clsHttpClients.Body,NewBodyDTO);
            if(AddNewDTO.StatusCode == System.Net.HttpStatusCode.Created)
            {
                if (MessageBox.Show($"Added body was successful, BodyID[{AddNewDTO.DTO.BodyID.ToString()}]", "Successed Process",
                              MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    _ID = AddNewDTO.DTO.BodyID.Value;
                    DataBack?.Invoke(AddNewDTO.DTO.BodyID.Value);
                    lblBodyID.Text = _ID.ToString();
                    lblTitlePage.Text = $"Update BodyID[{lblBodyID.Text}]";
                    _Mode = clsEnum.enMode.Update;
                }
            }
            else if (AddNewDTO.StatusCode == System.Net.HttpStatusCode.BadRequest)
                MessageBox.Show("Bad request: Please set correct data, Or change the body name.", "Failed Process",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            ctrlDownloadDot1.Visible = false;
            ctrlDownloadDot1.Stop();
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            BodyDTO bDTO = new BodyDTO(null, txtBodyName.Text.Trim());
            if (_Mode == clsEnum.enMode.Add)
                await _AddNewBody(bDTO);
            else
            {
                if(txtBodyName.Text.Trim() == _BodyName)
                    MessageBox.Show("There isn`t any different to update.", "Same Name!",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    bDTO.BodyID = _ID;
                    await _UpdateBody(_ID, bDTO);
                }     
            }
            _BodyName = txtBodyName.Text;
        }
        private void txtBodyName_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = !string.IsNullOrEmpty(txtBodyName.Text.Trim());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string _BodyName="";
        private async void frmAddUpdateBody_Load(object sender, EventArgs e)
        {
            this.Region = clsFormat.CornerForm(Width, Height);
            clsHttpClients.Defining(clsEnum.enClients.Body);
            if (_Mode == clsEnum.enMode.Add)
                lblTitlePage.Text = "Add New Body";
            else
            {
                lblTitlePage.Text = $"Update BodyID[{_ID}]";
                lblBodyID.Text = _ID.ToString();
                var BodyDetails = await clsHTTPMethod<BodyDTO>.GetDTOBy(clsHttpClients.Body, _ID);
                if (BodyDetails.StatusCode == System.Net.HttpStatusCode.OK)
                    _BodyName = BodyDetails.DTO.BodyName;
                txtBodyName.Text = _BodyName;
            }
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            clsEvents.GunaCircleButton_MouseLeave(sender);
        }

        private void btnClose_MouseMove(object sender, MouseEventArgs e)
        {
            clsEvents.GunaCircleButton_MouseMove(sender);
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            clsEvents.GunaButton_MouseLeave(sender);
        }

        private void btnSave_MouseMove(object sender, MouseEventArgs e)
        {
            clsEvents.GunaButton_MouseMove(sender);
        }
    }
}
