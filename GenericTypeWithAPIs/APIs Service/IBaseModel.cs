

namespace Vehicle_Business.Interface
{
    public interface IBaseModel
    {
        /// <summary>
        /// Add=0, Update=1
        /// </summary>
        byte Mode { get; set; }
        int? ID { get; set; }
        List<object> GetAll();
        object GetByID(int id);
        object GetByName(string name);
        /// <summary>
        /// Include Add,Update Operation 
        /// </summary>
        /// <returns>Result of operation[Add Or Update]</returns>
        bool Save();
        bool Delete(int id);
        bool Exists(int id);
    }
}
