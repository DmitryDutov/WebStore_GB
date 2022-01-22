using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //Создаём словарь со строками (Examlpe)
        private static readonly Dictionary<int, string> _Values = Enumerable.Range(1, 10)
            .Select(i => (Id: i, Value: $"Value: {i}"))
            .ToDictionary(v => v.Id, v => v.Value);

        //Объявляем поле логгер
        private readonly ILogger<ValuesController> _logger;

        //Объявляем конструктор и передаём в него логгер
        public ValuesController(ILogger<ValuesController> logger) => _logger = logger;


        #region Набор действий со словарём

        //Получение всех данных
        [HttpGet] // GET-> http://localhost:50101/api/values
        public IActionResult Get() => Ok(_Values.Values);

        //Получение данных по  Id
        [HttpGet("{Id}")] // GET-> http://localhost:50101/api/values/5
        public IActionResult GetById(int Id)
        {
            if (!_Values.ContainsKey(Id))
                return NotFound();
            return Ok(_Values[Id]);
        }

        //Получаем количество записей
        [HttpGet("count")] // GET-> http://localhost:50101/api/values/count
        public IActionResult Count() => Ok(_Values.Count());

        //Добавляем новую запись
        //[HttpPost] // POST -> api/values
        [HttpPost("add")] // POST -> api/values/add
        public IActionResult Add([FromBody] string Value)
        {
            var id = _Values.Count == 0 ? 1 : _Values.Keys.Max() + 1;
            _Values[id] = Value;

            return CreatedAtAction(nameof(GetById), new { id });
        }

        //Редактируем значения
        [HttpPut("{Id}")] // PUT -> api/values/5
        public IActionResult Replace(int Id, [FromBody] string Value)
        {
            if (!_Values.ContainsKey(Id))
                return NotFound();

            _Values[Id] = Value;

            return Ok();
        }

        //Удаляем запись
        [HttpDelete("{Id}")] // DELETE -> api/values/5
        public IActionResult Delete(int Id)
        {
            if (!_Values.ContainsKey(Id))
                return NotFound();

            _Values.Remove(Id);

            return Ok();
        }

        #endregion
    }
}

