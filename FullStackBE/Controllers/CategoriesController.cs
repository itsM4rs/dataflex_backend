using AutoMapper;
using FullStackBE.Models;
using FullStackBE.Models.Dtos;
using FullStackBE.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "FullStackOpenAPISpecCategories")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }

        //Get All Categories
        [HttpGet(Name = "GetCategories")]
        public IActionResult GetCategories()
        {
            var categoryList = _categoryRepository.GetCategories();

            var categoryDtoList = new List<CategoryDto>();

            foreach (var item in categoryList)
            {
                categoryDtoList.Add(_mapper.Map<CategoryDto>(item));
            }

            return Ok(categoryDtoList);
        }

        //Get Individual Category By Id
        [HttpGet("{id:int}", Name = "GetCategory")]
        public IActionResult GetCategory(int id)
        {
            var exists = _categoryRepository.CategoryExists(id);
            if (exists)
            {
                var categoryObj = _categoryRepository.GetCategory(id);

                var categoryDto = _mapper.Map<CategoryDto>(categoryObj);

                return Ok(categoryDto);
            }
            else
            {
                return NotFound("Not Found...");
            }
        }

        //Create(Post) Category
        [HttpPost(Name = "CreateCategory")]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {

            if (categoryDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepository.CategoryExists(categoryDto.Name))
            {
                ModelState.AddModelError("", "Category Exists!");
                return StatusCode(404, ModelState);
            }

            var categoryObj = _mapper.Map<Category>(categoryDto);

            if (!_categoryRepository.CreateCategory(categoryObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {categoryObj.Name}");
                return StatusCode(500, ModelState);
            }
            return StatusCode(StatusCodes.Status201Created, categoryObj);
        }

        //Update(Patch) Category
        [HttpPatch("{id:int}", Name = "UpdateCategory")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null || id != categoryDto.Id)
            {
                return BadRequest(ModelState);
            }

            var categoryObj = _mapper.Map<Category>(categoryDto);

            if (!_categoryRepository.UpdateCategory(categoryObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {categoryObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        //Delete Category
        [HttpDelete("{id:int}", Name = "DeleteCategory")]
        public IActionResult DeleteCategory(int id)
        {
            bool exists = _categoryRepository.CategoryExists(id);
            if (exists)
            {
                var categoryObj = _categoryRepository.GetCategory(id);
                bool deleted = _categoryRepository.DeleteCategory(categoryObj);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    ModelState.AddModelError("", $"Something went wrong when deleting the record {categoryObj.Name}");
                    return StatusCode(500, ModelState);
                }
            }
            else
            {
                return NotFound("Not Found...");
            }

        }
    }
}
