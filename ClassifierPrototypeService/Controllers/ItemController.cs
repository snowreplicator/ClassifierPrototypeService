using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Prototype.ClassifierPrototypeService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private class Item
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    
    private readonly List<Item> _items = new List<Item>
    {
        new Item { Id = 1, Name = "Item 1", Description = "Description for Item 1" },
        new Item { Id = 2, Name = "Item 2", Description = "Description for Item 2" }
    };
    
    /// <summary>
    /// Get all items
    /// </summary>
    [HttpGet("api/items")]
    public IActionResult GetItems()
    {
        return Ok(_items);
    }

    /// <summary>
    /// Get item by id
    /// </summary>
    [HttpGet("api/items/{id}")]
    public IActionResult GetItem(int id)
    {
        var item = _items.Find(i => i.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }
}

