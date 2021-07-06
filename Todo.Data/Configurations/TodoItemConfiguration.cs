using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Business.Entities;

namespace Todo.Data.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(i => i.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(i => i.Description)
                .HasColumnType("nvarchar(max)");

            builder
                .Property(i => i.Status)
                .HasDefaultValue(TodoItemStatus.Todo);
        }
    }
}
