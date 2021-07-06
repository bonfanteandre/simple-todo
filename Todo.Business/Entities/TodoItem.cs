namespace Todo.Business.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoItemStatus Status { get; set; }

        public TodoItem()
        {
            SetTodo();
        }

        public void SetTodo()
        {
            Status = TodoItemStatus.Todo;
        }

        public void SetDoing()
        {
            Status = TodoItemStatus.Doing;
        }

        public void SetDone()
        {
            Status = TodoItemStatus.Done;
        }
    }
}
