namespace AppSuite.Api.Features.EquipManagement.Models
{
    public class ModelDto
    {
        public int ModelId { get; set; }
        public string ModelNum { get; set; }
        public string ModelDescription { get; set; }
        public int MakeId { get; set; }
        public string Make { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? SubcategoryId { get; set; }
        public string SubcategoryName { get; set; } 
        public bool? Serialized { get; set; }
        public bool? LotTracked { get; set; }
    }
}
