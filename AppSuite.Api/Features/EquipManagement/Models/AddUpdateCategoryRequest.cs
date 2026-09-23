namespace AppSuite.Api.Features.EquipManagement.Models
{
    public class AddUpdateCategoryRequest
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }
}
