using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MYT.Data.Entity
{
    public class TodoItem : BaseManger
    {
        public int Id { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }

        [ForeignKey("Priority")]
        public int PriorityID { get; set; }

        [ForeignKey("TodoItemList")]
        public int? ListId { get; set; }

        public virtual Priority Priority { get; set; }
        public TodoItemList TodoItemList { get; set; }
    }
}
