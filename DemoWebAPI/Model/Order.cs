using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Model;

public sealed class Order
{
    [FromRoute(Name = "oid")]
    [Required]
    public int Id { get; set; }

    [FromQuery(Name = "name")]
    public string Name { get; set; }
    
    [FromQuery(Name = "price")]
    [Range(0,99)]
    public int Price { get; set; }
    
    [FromQuery(Name = "pid")]
    public int ProductId { get; set; }
}