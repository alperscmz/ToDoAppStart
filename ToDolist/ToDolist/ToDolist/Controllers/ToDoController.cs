using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDolist.Infrastructure;
using ToDolist.Models;

namespace ToDolist.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoContext context;
        public ToDoController(ToDoContext context)
        {
            this.context = context;
        }
        //GET/
        public async Task<ActionResult> Index()
        {
            IQueryable<Todolist> items = from i in context.ToDolist orderby i.Id select i;

            List<Todolist> todolist = await items.ToListAsync();
            return View(todolist);
        }
        //GET/todo/create
        public IActionResult Create() => View();
        // POST /todo/create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(Todolist item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been added.";

                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET/todo/edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Todolist item = await context.ToDolist.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        // POST/todo/edit/5
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Edit(Todolist item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been updated.";

                return RedirectToAction("Index");
            }
            return View(item);
        }
        //GET/todo/delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Todolist item = await context.ToDolist.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exist!.";
            }
            else
            {
                context.ToDolist.Remove(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been deleted.";
            }

            return RedirectToAction("Index");
        }
    }
}
