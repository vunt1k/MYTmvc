using MYT.Data.AppContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MYT.Data.Entity
{
    public class UserProfile
    {
        public UserProfile()
        {
            TodoItems = new HashSet<TodoItem>();
            TodoItemLists = new HashSet<TodoItemList>();
        }

        [Key,ForeignKey("User")]
        public string Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
            
        public DateTime RegistrationDate { get; set; }

        public virtual DbUser User { get; set; }
        public virtual IEnumerable<TodoItem> TodoItems { get; set; }
        public virtual IEnumerable<TodoItemList> TodoItemLists { get; set; }
    }
}
