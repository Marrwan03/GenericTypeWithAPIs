using DTO;
using VehiclesApp.GlobalClasses;

namespace VehiclesApp.VehicleBodies.Controls
{
    public partial class ctrlBodyDetail : UserControl
    {
        public ctrlBodyDetail()
        {
            InitializeComponent();
        }
        void _FillDataBy(BodyDTO bodyDTO)
        {
            lblBodyID.Text= bodyDTO.BodyID.ToString();
            lblBodyName.Text= bodyDTO.BodyName.ToString();   
        }
        public async void ctrlBodyDetail_Load(int BodyID)
        {
            clsHttpClients.Defining(clsEnum.enClients.Body);
            var Body = await clsHTTPMethod<BodyDTO>.GetDTOBy(clsHttpClients.Body,BodyID);
            if (Body.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _FillDataBy(Body.DTO);
                return;
            }
            else if (Body.StatusCode == System.Net.HttpStatusCode.BadRequest)
                MessageBox.Show("Bad request: Please set ID upper than 0.", "Bad Request",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show($"Not found: This bodyID[{Body.DTO.BodyID}] isn`t found.", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            string Empty = "[???]";
            lblBodyID.Text = Empty;
            lblBodyName.Text = Empty;
        }
        public async Task ctrlBodyDetail_Load(string BodyName)
        {
            clsHttpClients.Defining(clsEnum.enClients.Body);
            var bDTO = await clsHTTPMethod<BodyDTO>.GetDTOBy(clsHttpClients.Body, BodyName);
            if (bDTO.StatusCode == System.Net.HttpStatusCode.OK)
                _FillDataBy(bDTO.DTO);
        }
    }
}
