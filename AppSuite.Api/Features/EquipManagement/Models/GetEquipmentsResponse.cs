namespace AppSuite.Api.Features.EquipManagement.Models
{
    public class GetEquipmentsResponse
    {
        public List<EquipmentDto> Equipments { get; set; } = new List<EquipmentDto>();
    }
}
