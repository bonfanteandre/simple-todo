using System.ComponentModel.DataAnnotations;

namespace Todo.ViewModel
{
    public class TodoItemViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
