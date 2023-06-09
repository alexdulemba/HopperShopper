﻿using System.ComponentModel.DataAnnotations;

namespace HopperShopper.Entities
{
  public class ProductCategory
  {
    [Required]
    public int Id { get; set; }

    [Key]
    [Required]
    public Guid ObjectID { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public List<Product> Products { get; set; }
  }
}
