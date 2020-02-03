using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MYT.Data.Entity
{
    public class TodoItemList : BaseManger
    {
        public TodoItemList()
        {
            TodoItems = new HashSet<TodoItem>( );
        }
        public int Id { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }

        [ForeignKey("Priority")]
        public int PriorityId { get; set; }

        public virtual Priority Priority { get; set; }
        public IEnumerable<TodoItem> TodoItems { get; set; }
    }
}
