namespace AppSuite.Api.Features.EquipManagement.Models
{
    public class AddUpdateModelRequest
    {
        public int? ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public int? ModelId { get; set; }
        public string ModelNumber { get; set;  }        
        public string ModelDescription { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? SubcategoryId { get; set; }
        public string? SubcategoryName { get; set; }
        public bool? Serialized { get; set; }
        public bool? LotTracked { get; set; }
    }
}
