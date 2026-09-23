using AppSuite.Api.Features.EquipManagement.Models;
using AppSuite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppSuite.Api.Features.EquipManagement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EquipController : ControllerBase
    {
        private EquipService _equipSvc;
        public EquipController(AppSuiteDbContext equipDb)
        {
            _equipSvc = new EquipService(equipDb);
        }


        /*** EQUIPMENT ***/


        [HttpPost]
        public AddUpdateEquipmentResponse AddUpdateEquipment(AddUpdateEquipmentRequest request)
        {
            return _equipSvc.AddUpdateEquipment(request);
        }

        [HttpGet]
        public GetEquipmentsResponse GetEquipments()
        {
            return _equipSvc.GetAllEquip();
        }

        [HttpGet]
        public GetEquipmentResponse GetEquipmentById(int id)
        {
            return _equipSvc.GetEquipmentById(id);
        }


        /*** MAKES & MODELS ***/


        [Authorize]

        [HttpPost]
        public AddUpdateMakeResponse AddUpdateManufacturer(AddUpdateMakeRequest request)
        {
            return _equipSvc.AddUpdateMake(request);
        }

        [HttpGet]
        public GetMakesResponse GetManufacturers()
        {
            return _equipSvc.GetAllMakes();
        }

        [HttpPost]
        public AddUpdateModelResponse AddUpdateModel(AddUpdateModelRequest request)
        {
            return _equipSvc.AddUpdateModel(request);
        }

        [HttpGet]
        public GetModelsResponse GetModels()
        {
            return _equipSvc.GetAllModels();
        }

        [HttpGet]
        public GetModelResponse GetModelById(int id)
        {
            return _equipSvc.GetModelById(id);
        }

        [HttpGet]
        public GetModelResponse GetModelByModelNum(string modelNum)
        {
            return _equipSvc.GetModelByModelNum(modelNum);
        }




        /*** CATEGORIES ***/

        
        [HttpPost]
        public AddUpdateCategoryResponse AddUpdateCategory(AddUpdateCategoryRequest request)
        {
            return _equipSvc.AddUpdateCategory(request);
        }

        [HttpGet]
        public GetCategoriesResponse GetCategories()
        {
            return _equipSvc.GetCategories();
        }

        [HttpGet]
        public GetCategoryResponse GetCategoryById(int id)
        {
            return _equipSvc.GetCategoryById(id);
        }

        [HttpGet]
        public GetCategoryResponse GetCategoryByName(string name)
        {
            return _equipSvc.GetCategoryByName(name);
        }



        /*** SUBCATEGORIES ***/


        [HttpPost]
        public AddUpdateSubcategoryResponse AddUpdateSubcategory(AddUpdateSubcategoryRequest request)
        {
            return _equipSvc.AddUpdateSubcategory(request);
        }

        [HttpGet]
        public GetSubcategoriesResponse GetSubcategories()
        {
            return _equipSvc.GetSubcategories();
        }

    }
}
