using Microsoft.EntityFrameworkCore;
using MYT.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYT.Data.AppContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<TodoItem> TodoItems { get; set; }
        public virtual DbSet<TodoItemList> TodoItemLists { get; set; }
    }
}
