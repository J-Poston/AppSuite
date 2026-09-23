using AppSuite.Api.Features.EquipManagement.Models;
using AppSuite.Data;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

namespace AppSuite.Api.Features.EquipManagement
{
    public class EquipService
    {

        private readonly AppSuiteDbContext _equipDb;
        public EquipService(AppSuiteDbContext equipDb)
        {
            _equipDb = equipDb;
        }

        public GetEquipmentsResponse GetAllEquip()
        {
            GetEquipmentsResponse response = new GetEquipmentsResponse();

            response.Equipments = _equipDb.Equipments
                .Include(e => e.EquipCategory)
                .Include(e => e.EquipSubcategory)
                .Include(e => e.EquipManuf)
                .Select(r => new Models.EquipmentDto()
                {
                    EquipmentId = r.Id,
                    ManufacturerId = r.MakeId,
                    ManufacturerName = r.Make,
                    ModelNumber = r.ModelNum,
                    Description = r.Description,
                    CategoryId = r.EquipCategoryId,
                    CategoryName = (r.EquipCategory ?? new EquipCategory()).Name,
                    SubcategoryId = r.EquipSubcategoryId,
                    SubcategoryName = (r.EquipSubcategory ?? new EquipSubcategory()).Name,
                    SerialNumber = r.SerialNum,
                    LotNumber = r.LotNum,
                }).ToList();

            return response;
        }

        public GetMakesResponse GetAllMakes()
        {
            GetMakesResponse response = new GetMakesResponse();

            response.ManufacturerDtos = _equipDb.EquipManufs
                .Select(r => new ManufacturerDto()
                {
                    MakeId = r.Id,
                    MakeName = r.Name,
                }).ToList();

            return response;
        }

        public AddUpdateMakeResponse AddUpdateMake(AddUpdateMakeRequest request)
        {
            AddUpdateMakeResponse response = new AddUpdateMakeResponse();

            try
            {
                EquipManuf? manuf = GetEquipManuf(request.ManufacturerId, request.ManufacturerName);

                if(manuf != null)
                {
                    manuf.Name = request.ManufacturerName;
                    _equipDb.SaveChanges();

                    response.ManufacturerId = manuf.Id; 
                    response.ManufacturerName = manuf.Name;
                }
                else
                {
                    EquipManuf newManuf = new EquipManuf()
                    {

                        Name = request.ManufacturerName
                    };
                    _equipDb.EquipManufs.Add(newManuf);
                    _equipDb.SaveChanges();
                    response.ManufacturerId = newManuf.Id;
                    response.ManufacturerName = newManuf.Name;
                }
            }

            catch (Exception ex)
            {
                throw new Exception("Error processing request.", ex);
            }

            return response;
        }

        public AddUpdateModelResponse AddUpdateModel(AddUpdateModelRequest request)
        {
            AddUpdateModelResponse response = new AddUpdateModelResponse();

            try
            {
                EquipManuf? equipManuf = GetEquipManuf(request.ManufacturerId, request.ManufacturerName);

                if(equipManuf == null)
                {
                    throw new Exception($"Could not find EquipManuf with ManufacturerId {request.ManufacturerId} or ManufacturerName {request.ManufacturerName}.");
                }

                EquipModel? existingModel = GetEquipModel(modelId: null, modelNumber: request.ModelNumber);

                if((request.ModelId == null || request.ModelId == 0) && existingModel != null)
                {
                    throw new Exception(message: "Model Number already exists.");
                }               

                EquipCategory? equipCategory = GetEquipCategory(request.CategoryId, request.CategoryName);

                EquipSubcategory? equipSubcategory = GetEquipSubcategory(request.SubcategoryId, request.SubcategoryName);

                if(existingModel != null)
                {
                    existingModel.MakeId = equipManuf.Id;
                    existingModel.Description = request.ModelDescription;
                    existingModel.EquipCategoryId = equipCategory.Id;
                    existingModel.EquipSubcategoryId = equipSubcategory.Id;
                    existingModel.Serialized = request.Serialized;
                    existingModel.LotTracked = request.LotTracked;

                    _equipDb.SaveChanges();

                    response.ModelId = existingModel.Id;
                    response.ModelNumber = existingModel.ModelNum;
                    response.ModelDescription = existingModel.Description;
                    response.ManufacturerId = existingModel.MakeId;
                    response.ManufacturerName = equipManuf.Name;
                    response.CategoryId = existingModel.EquipCategoryId;
                    response.CategoryName = equipCategory?.Name;
                    response.SubcategoryId = existingModel.EquipSubcategoryId;
                    response.SubcategoryName = equipSubcategory?.Name;
                    response.Serialized = existingModel.Serialized;
                    response.LotTracked = existingModel.LotTracked;
                    
                }

                else
                {
                    EquipModel newModel = new EquipModel()
                    {
                        MakeId = equipManuf.Id,
                        ModelNum = request.ModelNumber,
                        Description = request.ModelDescription,
                        EquipCategoryId = equipCategory?.Id,
                        EquipSubcategoryId = equipSubcategory?.Id,
                        Serialized = request.Serialized,
                        LotTracked = request.LotTracked
                    };

                    _equipDb.EquipModels.Add(newModel);
                    _equipDb.SaveChanges();

                    response.ModelId = newModel.Id;
                    response.ModelNumber = newModel.ModelNum;
                    response.ModelDescription = newModel.Description;
                    response.ManufacturerId = newModel.MakeId;
                    response.ManufacturerName = equipManuf.Name;
                    response.CategoryId = newModel.EquipCategoryId;
                    response.CategoryName = equipCategory?.Name;
                    response.SubcategoryId = newModel.EquipSubcategoryId;
                    response.SubcategoryName = equipSubcategory?.Name;
                    response.Serialized = newModel.Serialized;
                    response.LotTracked = newModel.LotTracked;
                }
                
            }

            catch(Exception ex)
            {
                throw new Exception("Error processing request.", ex);
            }

            return response;

        }

        public GetModelResponse GetModelById(int modelId)
        {
            GetModelResponse response = new GetModelResponse();

            EquipModel modelEntity = GetEquipModel(modelId: modelId, modelNumber: null);

            response.ModelDto.ModelId = modelEntity.Id;
            response.ModelDto.ModelNum = modelEntity.ModelNum;
            response.ModelDto.MakeId = modelEntity.MakeId;
            response.ModelDto.Make = modelEntity.EquipManuf.Name;
            response.ModelDto.CategoryId = modelEntity.EquipCategoryId;
            response.ModelDto.CategoryName = modelEntity.EquipCategory.Name;
            response.ModelDto.SubcategoryId = modelEntity.EquipSubcategoryId;
            response.ModelDto.SubcategoryName = modelEntity.EquipSubcategory.Name;
            response.ModelDto.Serialized = modelEntity.Serialized;
            response.ModelDto.LotTracked = modelEntity.LotTracked;

            return response;
        }


        public GetModelResponse GetModelByModelNum(string modelNum)
        {
            GetModelResponse response = new GetModelResponse();

            EquipModel modelEntity = GetEquipModel(modelId: null, modelNumber: modelNum);

            response.ModelDto.ModelId = modelEntity.Id;
            response.ModelDto.ModelNum = modelEntity.ModelNum;
            response.ModelDto.MakeId = modelEntity.MakeId;
            response.ModelDto.Make = modelEntity.EquipManuf.Name;
            response.ModelDto.CategoryId = modelEntity.EquipCategoryId;
            response.ModelDto.CategoryName = modelEntity.EquipCategory.Name;
            response.ModelDto.SubcategoryId = modelEntity.EquipSubcategoryId;
            response.ModelDto.SubcategoryName = modelEntity.EquipSubcategory.Name;
            response.ModelDto.Serialized = modelEntity.Serialized;
            response.ModelDto.LotTracked = modelEntity.LotTracked;

            return response;
        }

        // helper methods
        private EquipManuf? GetEquipManuf(int? makeId, string? makeName)
        {
            EquipManuf? make = _equipDb.EquipManufs
                .Where(m => m.Id == makeId || m.Name == makeName)
                .FirstOrDefault();

            if(makeId != null && makeId != 0 && make == null)
            {
                throw new Exception($"Could not find EquipManuf with ID {makeId}.");
            }

            return make;
        }
        private EquipModel? GetEquipModel(int? modelId, string? modelNumber)
        {
            EquipModel? model = _equipDb.EquipModels
                .Include(m => m.EquipManuf)
                .Include(m => m.EquipCategory)
                .Include(m => m.EquipSubcategory)
                .Where(m => m.Id == modelId || m.ModelNum == modelNumber)
                .FirstOrDefault();

            if(modelId != null && modelId != 0 && model == null)
            {
                throw new Exception($"Could not find EquipModel with modelId {modelId}.");
            }

            return model;
        }

        private EquipCategory? GetEquipCategory(int? categoryId, string? categoryName)
        {
            EquipCategory? category = _equipDb.EquipCategories
                .Where(c => c.Id == categoryId || c.Name == categoryName)
                .FirstOrDefault();
            
            if (categoryId != null && categoryId != 0 && category == null)
            {
                throw new Exception($"Could not find EquipModel with modelId {categoryId}.");
            }

            return category;
        }

        private EquipSubcategory? GetEquipSubcategory(int? subcategoryId, string? subcategoryName)
        {
            EquipSubcategory? subcategory = _equipDb.EquipSubcategories
                .Where(s => s.Id == subcategoryId || s.Name == subcategoryName)
                .FirstOrDefault();

            if (subcategoryId != null && subcategoryId != 0 && subcategory == null)
            {
                throw new Exception($"Could not find EquipModel with modelId {subcategoryId}.");
            }

            return subcategory;
        }
        public GetCategoriesResponse GetCategories()
        {
            GetCategoriesResponse response = new GetCategoriesResponse();

            try
            {
                response.CategoryDtos = _equipDb.EquipCategories
                    .Select(c => new CategoryDto()
                    {
                        CategoryId = c.Id,
                        CategoryName = c.Name
                    }).ToList();
            }

            catch(Exception ex)
            {
                throw new Exception("Error processing request.", ex);
            }

            return response;
        }

        public GetCategoryResponse GetCategoryById(int id)
        {
            GetCategoryResponse response = new GetCategoryResponse();

            try
            {

                EquipCategory? categoryEntity = GetEquipCategory(categoryId: id, categoryName: null);

                response.Category.CategoryId = categoryEntity.Id;
                response.Category.CategoryName = categoryEntity.Name;

                return response;
            }

            catch (Exception ex)
            {
                throw new Exception($"Could not find Category with Id {id}.", ex);
            }
        }

        public GetCategoryResponse GetCategoryByName(string name)
        {
            GetCategoryResponse response = new GetCategoryResponse();

            try
            {

                EquipCategory? categoryEntity = GetEquipCategory(categoryId: null, categoryName: name);

                response.Category.CategoryId = categoryEntity.Id;
                response.Category.CategoryName = categoryEntity.Name;

                return response;
            }

            catch (Exception ex)
            {
                throw new Exception($"Could not find Category with Name {name}.", ex);
            }

        }
        public AddUpdateCategoryResponse AddUpdateCategory(AddUpdateCategoryRequest request)
        {

            AddUpdateCategoryResponse response = new AddUpdateCategoryResponse();

            try
            {

                EquipCategory? existingCategory = GetEquipCategory(request.CategoryId, request.CategoryName);

                if(existingCategory != null)
                {
                    existingCategory.Description = request.CategoryDescription;
                    _equipDb.SaveChanges();
                    response.CategoryId = existingCategory.Id;
                    response.CategoryName = existingCategory.Name;
                    response.CategoryDescription = existingCategory.Description;
                }

                else
                {
                    EquipCategory newCategory = new EquipCategory()
                    {
                        Name = request.CategoryName,
                        Description = request.CategoryDescription
                    };

                    _equipDb.EquipCategories.Add(newCategory);
                    _equipDb.SaveChanges();
                    response.CategoryId = newCategory.Id;
                    response.CategoryName = newCategory.Name;
                    response.CategoryDescription = newCategory.Description;
                }
            }

            catch(Exception ex)
            {
                throw new Exception("Error processing request.", ex);
            }

            return response;
        }

        public GetSubcategoriesResponse GetSubcategories()
        {
            GetSubcategoriesResponse response = new GetSubcategoriesResponse();

            try
            {
                response.Subcategories = _equipDb.EquipSubcategories
                    .Select(c => new SubcategoryDto()
                    {
                        SubcategoryId = c.Id,
                        SubcategoryName = c.Name
                    }).ToList();
            }

            catch (Exception ex)
            {
                throw new Exception("Error processing request.", ex);
            }
            
            return response;
        }
        public AddUpdateSubcategoryResponse AddUpdateSubcategory(AddUpdateSubcategoryRequest request)
        {
            AddUpdateSubcategoryResponse response = new AddUpdateSubcategoryResponse();

            try
            {
                EquipCategory? equipCategory = GetEquipCategory(request.CategoryId, request.CategoryName);

                if (equipCategory == null) { throw new Exception(message: "Invalid Category."); }
                
                EquipSubcategory? existingSubcat = GetEquipSubcategory(request.SubcategoryId, request.SubcategoryName);

                if(existingSubcat!= null)
                {
                    existingSubcat.EquipCategoryId = equipCategory.Id;
                    existingSubcat.Name = request.SubcategoryName;
                    existingSubcat.Description = request.SubcategoryDescription;
                    
                    _equipDb.SaveChanges();

                    response.CategoryId = equipCategory.Id;
                    response.CategoryName = equipCategory.Name;
                    response.SubcategoryId = existingSubcat.Id;
                    response.SubcategoryName = existingSubcat.Name;
                    response.SubcategoryDescription = existingSubcat.Description;
                }

                else
                {
                    EquipSubcategory newSubcat = new EquipSubcategory()
                    {
                        EquipCategoryId = equipCategory.Id,
                        Name = request.SubcategoryName,
                        Description = request.SubcategoryDescription,
                    };
                    
                    _equipDb.EquipSubcategories.Add(newSubcat);
                    _equipDb.SaveChanges();

                    response.CategoryId = equipCategory.Id;
                    response.CategoryName = equipCategory.Name;
                    response.SubcategoryId = newSubcat.Id;
                    response.SubcategoryName = newSubcat.Name;
                    response.SubcategoryDescription = newSubcat.Description;
                }

            }

            catch(Exception ex)
            {
                throw new Exception("Error processing request.", ex);
            }

            return response;

        }

        public GetModelsResponse GetAllModels()
        {
            GetModelsResponse response = new GetModelsResponse();

            try
            {

                List<ModelDto> models = _equipDb.EquipModels
                    .Include(m => m.EquipManuf)
                    .Include(m => m.EquipCategory)
                    .Include(m => m.EquipSubcategory)
                    .Select(m => new ModelDto()
                    {
                        ModelId = m.Id,
                        ModelNum = m.ModelNum,
                        ModelDescription = m.Description,
                        MakeId = m.MakeId,
                        Make = m.EquipManuf.Name,
                        CategoryId = m.EquipCategoryId,
                        CategoryName = m.EquipCategory.Name,
                        SubcategoryId = m.EquipSubcategoryId,
                        SubcategoryName = m.EquipSubcategory.Name,
                    }).ToList();

                response.ModelDtos = models;
            }

            catch(Exception ex)
            {
                throw new Exception("Error processing request.", ex);
            }

            return response;
        }

        public GetEquipmentResponse GetEquipmentById(int id)
        {
            GetEquipmentResponse response = new GetEquipmentResponse();

            Models.EquipmentDto? equipment = _equipDb.Equipments
                .Where(e => e.Id == id)
                .Include(e => e.EquipSubcategory)
                .Include(e => e.EquipCategory)
                .Include(e => e.EquipManuf)
                .Select(e => new Models.EquipmentDto()
                {
                    EquipmentId = e.Id,
                    ManufacturerId = e.MakeId,
                    ManufacturerName = e.EquipManuf.Name,
                    ModelId = e.ModelId,
                    ModelNumber = e.ModelNum,
                    Description = e.Description,
                    CategoryId = e.EquipCategoryId,
                    CategoryName = e.EquipCategory.Name,
                    SubcategoryId = e.EquipSubcategoryId,
                    SubcategoryName = e.EquipSubcategory.Name,
                    SerialNumber = e.SerialNum,
                    LotNumber = e.LotNum

                }).FirstOrDefault();

            response.Equipment = equipment;
            return response;
        }

        public AddUpdateEquipmentResponse AddUpdateEquipment(AddUpdateEquipmentRequest request)
        {
            AddUpdateEquipmentResponse response = new AddUpdateEquipmentResponse();

            try
            {
                EquipManuf? equipManuf = GetEquipManuf(request.ManufacturerId, request.ManufacturerName);

                if (equipManuf == null)
                {
                    throw new Exception("Invalid EquipManuf.");
                }

                EquipCategory? equipCategory = GetEquipCategory(request.CategoryId, request.CategoryName);

                if(equipCategory == null)
                {
                    throw new Exception("Invalid EquipCategory.");
                }

                EquipSubcategory? equipSubcategory = GetEquipSubcategory(request.SubcategoryId, request.SubcategoryName);

                if(equipSubcategory == null)
                {
                    throw new Exception("Invalid EquipSubcategory.");
                }

                EquipModel? equipModel = GetEquipModel(request.ModelId, request.ModelNumber);

                if(equipModel == null)
                {
                    throw new Exception("Invalid EquipModel.");
                }

                if(request.EquipmentId != null)
                {
                    AppSuite.Data.Equipment? existingEquip = GetEquipment((int)request.EquipmentId);
                    existingEquip.EquipCategoryId = equipCategory.Id;
                    existingEquip.EquipSubcategoryId = equipSubcategory.Id;
                    existingEquip.MakeId = equipManuf.Id;
                    existingEquip.ModelId = equipModel.Id;
                    existingEquip.Description = request.Description;
                    existingEquip.LotNum = request.LotNumber ?? "";
                    existingEquip.SerialNum = request.SerialNumber ?? "";
                    _equipDb.SaveChanges();

                    response.EquipmentId = existingEquip.Id;
                    response.CategoryId = equipCategory.Id;
                    response.CategoryName = equipCategory.Name;
                    response.SubcategoryId = equipSubcategory.Id;
                    response.SubcategoryName = equipSubcategory.Name;
                    response.ManufacturerId = equipManuf.Id;
                    response.ManufacturerName = equipManuf.Name;
                    response.ModelId = equipModel.Id;
                    response.ModelNumber = equipModel.ModelNum;
                    response.LotNumber = request.LotNumber;
                    response.SerialNumber = request.SerialNumber;
                }

                else
                {
                    AppSuite.Data.Equipment newEquip = new AppSuite.Data.Equipment()
                    {
                        EquipCategoryId = equipCategory.Id,
                        EquipSubcategoryId = equipSubcategory.Id,
                        MakeId = equipManuf.Id,
                        Make = equipManuf.Name,
                        ModelId = equipModel.Id,
                        ModelNum = equipModel.ModelNum,
                        Description = request.Description,
                        LotNum = request.LotNumber ?? "",
                        SerialNum = request.SerialNumber ?? "",
                    };
                    _equipDb.Equipments.Add(newEquip);
                    _equipDb.SaveChanges();

                    response.EquipmentId = newEquip.Id;
                    response.CategoryId = equipCategory.Id;
                    response.CategoryName = equipCategory.Name;
                    response.SubcategoryId = equipSubcategory.Id;
                    response.SubcategoryName = equipSubcategory.Name;
                    response.ManufacturerId = equipManuf.Id;
                    response.ManufacturerName = equipManuf.Name;
                    response.ModelId = equipModel.Id;
                    response.ModelNumber = equipModel.ModelNum;
                    response.LotNumber = request.LotNumber;
                    response.SerialNumber = request.SerialNumber;
                }
            }

            catch(Exception ex)
            {
                throw new Exception("Error processing request.", ex);
            }

            return response;
        }

        private AppSuite.Data.Equipment GetEquipment(int equipId)
        {
            AppSuite.Data.Equipment? equip = _equipDb.Equipments.Find(equipId);

            if(equip == null)
            {
                throw new Exception($"Could not find Equipment with EquipmentId {equipId}.");
            }

            return equip;

        }

        private void ValidateEquipManuf(EquipManuf equipManuf)
        {
            
        }

    }
}
