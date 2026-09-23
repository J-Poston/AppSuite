using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppSuite.Data
{
    [Table(nameof(Equipment))]
    public class Equipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MakeId { get; set; }
        [ForeignKey(nameof(MakeId))]
        public EquipManuf EquipManuf { get; set; }
        [MaxLength(30)]
        public string Make { get; set; }
        public int ModelId { get; set; }
        [ForeignKey(nameof(ModelId))]
        public EquipModel EquipModel { get; set; }
        [MaxLength(30)]
        public string ModelNum { get; set; }
        [MaxLength(30)]
        public string LotNum { get; set; }
        [MaxLength(30)]
        public string SerialNum { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        public int? EquipCategoryId { get; set; }
        public EquipCategory? EquipCategory { get; set; }
        public int? EquipSubcategoryId { get; set; }
        public EquipSubcategory? EquipSubcategory { get; set; }
    }
    [Table(nameof(EquipManuf))]
    public class EquipManuf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(40)]
        public string Name { get; set; }
        public ICollection<EquipModel> EquipModels { get; set; } = new List<EquipModel>();
    }
    [Table(nameof(EquipModel))]
    public class EquipModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MakeId { get; set; }
        [ForeignKey(nameof(MakeId))]
        public EquipManuf EquipManuf { get; set; }
        [MaxLength(30)]
        public string ModelNum { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        public int? EquipCategoryId { get; set; }
        [ForeignKey(nameof(EquipCategoryId))]
        public EquipCategory? EquipCategory { get; set; } = new();
        public int? EquipSubcategoryId { get; set; }
        [ForeignKey(nameof(EquipSubcategoryId))]
        public EquipSubcategory? EquipSubcategory { get; set; } = new();
        public bool? LotTracked { get; set; }
        public bool? Serialized { get; set; }
    }

    [Table(nameof(EquipCategory))]
    [Index(nameof(Name), IsUnique = true)]
    public class EquipCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        public ICollection<EquipSubcategory> EquipSubcategories { get; set; } = new List<EquipSubcategory>();
    }

    [Table(nameof(EquipSubcategory))]
    [Index(nameof(EquipCategoryId),nameof(Name),IsUnique = true)]
    public class EquipSubcategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        public int EquipCategoryId { get; set; }
        [ForeignKey(nameof(EquipCategoryId))]
        public EquipCategory EquipCategory { get; set; } 
    }

}
