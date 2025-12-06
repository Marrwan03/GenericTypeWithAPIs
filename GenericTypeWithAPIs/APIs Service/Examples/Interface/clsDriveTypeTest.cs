using DTO;
using System.Text.Json.Serialization;
using Vehicle_Access;
using Vehicle_Business.Interface;

namespace Vehicle_Business
{
    public class clsDriveTypeTest : IBaseModel
    {
        public enum enMode : byte { Add, Update }
        public int? DriveTypeID { get; set; }
        public string DriveTypeName { get; set; }
        [JsonIgnore]
        public byte Mode { get; set; }
        [JsonIgnore]
        public int? ID { get => DriveTypeID; set => DriveTypeID=value; }
        [JsonIgnore]
        public DriveTypeDTO dDTO { get { return new DriveTypeDTO(DriveTypeID, DriveTypeName); } }
        public clsDriveTypeTest(DriveTypeDTO dDTO, enMode mode = enMode.Add)
        {
            DriveTypeID = dDTO.DriveTypeID;
            DriveTypeName = dDTO.DriveTypeName;
            Mode = (byte)mode;
        }
        public clsDriveTypeTest() 
        {
            Mode = 0;
        }
        private bool AddNewDriveType()
        {
            this.DriveTypeID = clsDriveTypes.AddNewDriveType(dDTO);
            return this.DriveTypeID.HasValue;
        }
        private bool UpdateDriveType()
            => clsDriveTypes.UpdatedDriveType(this.dDTO);
        public bool Delete(int id)
            => clsDriveTypes.DeleteDriveType(id);

        public bool Exists(int id)
            => clsDriveTypes.IsDriveTypeExistsBy(id);

        public List<object> GetAll()
        {
            var all = clsDriveTypes.GetAllDriveTypes();
            return all.Cast<object>().ToList();
        }

        public object GetByID(int id)
        {
            DriveTypeDTO dDTO = clsDriveTypes.GetDriveType(id);
            if (dDTO != null)
                return new clsDriveTypeTest(dDTO, enMode.Update);
            return null;
        }

        public object GetByName(string name)
        {
            DriveTypeDTO dDTO = clsDriveTypes.GetDriveType(name);
            if (dDTO != null)
                return new clsDriveTypeTest(dDTO, enMode.Update);
            return null;
        }
        public bool SaveDriveType()
        {
            switch (Mode)
            {
                case (byte)enMode.Add:
                    {
                        if (AddNewDriveType())
                        {
                            Mode = (byte)enMode.Update;
                            return true;
                        }
                        break;
                    }
                case (byte)enMode.Update:
                    {
                        return UpdateDriveType();
                    }

            }
            return false;
        }
        public bool Save()
            => SaveDriveType();
    }
}
