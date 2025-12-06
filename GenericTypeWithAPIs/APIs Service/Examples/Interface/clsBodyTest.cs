using DTO;
using Vehicle_Access;
using System.Text.Json.Serialization;
using Vehicle_Business.Interface;

namespace Vehicle_Business
{
    public class clsBodyTest : IBaseModel
    {
        public enum enMode:byte { Add, Update }
        public int? BodyID { get; set; }
        public string BodyName { get; set; }
        [JsonIgnore]
        public BodyDTO DTO { get { return new BodyDTO(BodyID, BodyName); } }
        public clsBodyTest(BodyDTO bodyDTO, enMode mode = enMode.Add)
        {
            BodyID = bodyDTO.BodyID.Value;
            BodyName = bodyDTO.BodyName;
            Mode = (byte)mode;
        }
        public clsBodyTest()
        {
            Mode = 0;
        }

        [JsonIgnore]
        public int? ID { get => BodyID;
            set => BodyID = value; }
        [JsonIgnore]
       public byte Mode { get; set; }
        public bool Delete(int id)
        {
            return clsBodies.DeleteBody(id);
        }

        public List<object> GetAll()
        {
            var all = clsBodies.GetAllBodies();
            return all.Cast<object>().ToList();
        }

        public object GetByID(int id)
        {
            BodyDTO bDTO = clsBodies.GetBody(id);
            if (bDTO != null)
                return new clsBodyTest(bDTO, enMode.Update);
            return null;
        }
        public object GetByName(string name)
        {
            BodyDTO bDTO = clsBodies.GetBody(name);
            if (bDTO != null)
                return new clsBodyTest(bDTO, enMode.Update);
            return null;

        }
        private bool AddNewBody()
        {
            this.BodyID = clsBodies.AddNewBody((BodyDTO)DTO).Value;
            return this.BodyID.HasValue;
        }
        private bool UpdateBody()
            => clsBodies.UpdatedBody((BodyDTO)DTO);

        public bool SaveBody()
        {
            switch (Mode)
            {
                case (byte)enMode.Add:
                    {
                        if (AddNewBody())
                        {
                            Mode = (byte)enMode.Update;
                            return true;
                        }
                        break;
                    }
                case (byte)enMode.Update:
                    {
                        return UpdateBody();
                    }

            }
            return false;
        }
        public bool Save()
        {
            return this.SaveBody();
        }

        public bool Exists(int id)
        {
            return clsBodies.IsBodyExistsBy(id);
        }
    }
}
