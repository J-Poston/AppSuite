namespace AppSuite.Api.Features.EquipManagement.Models
{
    public class AddUpdateSubcategoryRequest
    {
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string SubcategoryDescription { get; set; }
        internal bool IsCategoryNull => CategoryId == null && (CategoryName == null || CategoryName == String.Empty) ; 
    }
}
