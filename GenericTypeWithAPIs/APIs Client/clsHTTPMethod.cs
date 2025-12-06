using System.Net;
using System.Net.Http.Json;

namespace VehiclesApp.GlobalClasses
{
    /// <summary>
    /// This class is Client side.
    /// </summary>
    /// <typeparam name="T">Type of Model</typeparam>
    public class clsHTTPMethod<T>
    {
        /// <summary>
        /// It expresses Data transfer object for specific model
        /// </summary>
        static T _DTO;
        /// <summary>
        /// It expresses list of DTO for specific model
        /// </summary>
        static List<T> _DTOList;
        public static async Task<(List<T> DTOList, HttpStatusCode StatusCode)> GetAllDTOs(HttpClient httpClient)
        {
            HttpStatusCode StatusCode= HttpStatusCode.BadRequest;
            try
            {
                var response = await httpClient.GetAsync("");
                if (response.IsSuccessStatusCode)
                    _DTOList = await response.Content.ReadFromJsonAsync<List<T>>();
                StatusCode = response.StatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Occure an erro: {ex.Message}.", "Failed Process",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (_DTOList, StatusCode);
        }
        public static async Task<(T DTO, HttpStatusCode StatusCode)> AddNewDTO(HttpClient httpClient, T NewDTO)
        {
            HttpStatusCode StatusCode = HttpStatusCode.BadRequest;
            try
            {
                var response = await httpClient.PostAsJsonAsync("", NewDTO);
                if (response.IsSuccessStatusCode) 
                    _DTO= await response.Content.ReadFromJsonAsync<T>();
                StatusCode = response.StatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Occure an erro: {ex.Message}.", "Failed Process",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (_DTO, StatusCode);
        }
        public static async Task<HttpStatusCode> UpdateDTO(HttpClient httpClient, int ID,T UpdatedDTO)
        {
            HttpStatusCode StatusCode=HttpStatusCode.BadRequest;
            try
            {
                //string URL = httpClient.BaseAddress + "/" + ID.ToString();
                var response = await httpClient.PutAsJsonAsync(ID.ToString(), UpdatedDTO);
                StatusCode = response.StatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Occure an erro: {ex.Message}.", "Failed Process",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return StatusCode;
        }
        public static async Task<HttpStatusCode> DeleteDTO(HttpClient httpClient, int ID)
        {
            HttpStatusCode StatusCode=HttpStatusCode.BadRequest;
            try
            {
                var response = await httpClient.DeleteAsync($"{ID}");
                StatusCode = response.StatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Occure an erro: {ex.Message}.", "Failed Process",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return StatusCode;

        }
        public static async Task<(T DTO, HttpStatusCode StatusCode)> GetDTOBy(HttpClient httpClient, int ID)
        {
            HttpStatusCode Status= HttpStatusCode.BadRequest;
            try
            {
                var response = await httpClient.GetAsync($"{ID}");
                if (response.IsSuccessStatusCode)
                    _DTO = await httpClient.GetFromJsonAsync<T>($"{ID}");
                Status= response.StatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Occure an error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (_DTO, Status);
        }
        public static async Task<(T DTO, HttpStatusCode StatusCode)> GetDTOBy(HttpClient httpClient, string Name)
        {
            HttpStatusCode Status = HttpStatusCode.BadRequest;
            string URL = httpClient.BaseAddress+"/"+Name;
            try
            {
                var response = await httpClient.GetAsync($"'{Name}'");
                if (response.IsSuccessStatusCode)
                    _DTO = await httpClient.GetFromJsonAsync<T>($"'{Name}'");

                Status = response.StatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Occure an error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (_DTO, Status);
        }
    }
}
