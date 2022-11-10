using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DemoWebAPI.Models
{
    public sealed class Order
    {
        [FromRoute(Name ="oid")]
        [Required]
        public int Id { get; set; }
        [FromQuery(Name ="name")]
        public string Name { get; set; }
        [FromQuery(Name = "price")]
        [Range(0,99)]
        public int Price { get; set; }
        [FromQuery(Name = "pid")]
        public int ProductId { get; set; }
    }
}
