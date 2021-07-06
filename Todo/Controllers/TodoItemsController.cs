using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Business.Entities;
using Todo.Business.Repositories;
using Todo.ViewModel;

namespace Todo.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/todo-items")]
    [Authorize]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemsController(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] TodoItemViewModel todoItemViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
                });
            }

            var item = new TodoItem();
            item.Title = todoItemViewModel.Title;
            item.Description = todoItemViewModel.Description;

            await _todoItemRepository.AddAsync(item);

            return Created("todo-items", item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] TodoItemViewModel todoItemViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()
                });
            }

            var item = await _todoItemRepository.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.Title = todoItemViewModel.Title;
            item.Description = todoItemViewModel.Description;

            _todoItemRepository.Update(item);

            return Ok(item);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var item = await _todoItemRepository.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _todoItemRepository.Remove(item);

            return NoContent();
        }

        [HttpGet("status/{status:int}")]
        public async Task<IActionResult> GetByStatusAsync([FromRoute] TodoItemStatus status)
        {
            var items = await _todoItemRepository.ByStatusAsync(status);
            return Ok(items);
        }

        [HttpPatch("todo/{id}")]
        public async Task<IActionResult> TodoAsync([FromRoute] int id)
        {
            var item = await _todoItemRepository.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.SetTodo();

            _todoItemRepository.Update(item);

            return NoContent();
        }

        [HttpPatch("doing/{id}")]
        public async Task<IActionResult> DoingAsync([FromRoute] int id)
        {
            var item = await _todoItemRepository.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.SetDoing();

            _todoItemRepository.Update(item);

            return NoContent();
        }

        [HttpPatch("done/{id}")]
        public async Task<IActionResult> DoneAsync([FromRoute] int id)
        {
            var item = await _todoItemRepository.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.SetDone();

            _todoItemRepository.Update(item);

            return NoContent();
        }
    }
}
