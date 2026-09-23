namespace AppSuite.Api.Features.EquipManagement.Models
{
    public class EquipmentDto
    {
        public int EquipmentId { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public int ModelId { get; set; }
        public string ModelNumber { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? SubcategoryId { get; set; }
        public string? SubcategoryName { get; set; }
        public string? SerialNumber { get; set; }
        public string? LotNumber { get; set; }
    }
}
