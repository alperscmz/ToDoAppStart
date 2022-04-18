using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDolist.Models;

namespace ToDolist.Infrastructure
{
    public class ToDoContext :DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext>options)
            :base(options)
        {
        }
        public DbSet<Todolist> ToDolist { get; set; }
    }
}
