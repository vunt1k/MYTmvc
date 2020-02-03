using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYT.Data.Entity
{
    public class Priority
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<TodoItem> TodoItems { get; set; }
        public IEnumerable<TodoItemList> TodoItemLists { get; set; }
    }
}
