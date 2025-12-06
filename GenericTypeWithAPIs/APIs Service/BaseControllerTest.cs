using Microsoft.AspNetCore.Mvc;
using Vehicle_Business.Interface;

namespace VehicleAPIsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BaseControllerTest<T> : ControllerBase where T : IBaseModel, new()
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<T>> GetAll()
        {
            T model = new T();
            var items = model.GetAll();
            var List = items.ToList();
            if (List.Count == 0)
                return NotFound("There isn`t any data.");
            else
                return Ok(List);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<T> GetById(int id)
        {
            if (id <= 0)
                return BadRequest($"Bad request: Id[{id}] isn`t correct.");
            T model = new T();
            var item = model.GetByID(id);
            if (item == null)
                return NotFound($"Not Found: This Id[{id}] isn`t found.");
            return Ok((T)item);
        }
        [HttpGet("'{Name}'")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<T> GetByName(string Name)
        {
            if (string.IsNullOrEmpty(Name) || Name.ToLower() == "string")
                return BadRequest($"Bad request: This Name[{Name}] is not valid.");
            T model = new T();
            var item = model.GetByName(Name);
            if (item == null)
                return NotFound($"Not Found: This Name[{Name}] isn`t found.");
            return Ok((T)item);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<T> Create(T item)
        {
            if (item.Save())
                return CreatedAtAction(nameof(GetById), new { id = item.ID }, item);
            else
                return BadRequest("Cannot added this data");
        }
        [HttpPut("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<T> Update(int ID,T item)
        {
            if (ID <= 0)
                return BadRequest($"Bad request: This id[{ID}] isn`t valid.");
            T model = new T();
            if (!model.Exists(ID))
                return NotFound($"Record with this id[{ID}] isn`t found.");
            item.ID = ID;
            item.Mode = 1;//Update
            if (item.Save())
                return Ok(item);
            else
                return BadRequest("Failed to update record");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest($"Bad request: Id[{id}] isn`t correct.");
            T model = new T();
            if(!model.Exists(id))
                return NotFound($"Not Found: Id[{id}] isn`t found");
            if (model.Delete(id))
                return Ok($"This id[{id}] is deleted.");
            return StatusCode(500, new { Message = "Error deleting"});
        }
    }
}
