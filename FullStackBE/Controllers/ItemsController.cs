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
    [ApiExplorerSettings(GroupName = "FullStackOpenAPISpecItems")]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemsController(IItemRepository itemRepository, IMapper mapper)
        {
            this._itemRepository = itemRepository;
            this._mapper = mapper;
        }

        //Get All Items
        [HttpGet(Name = "GetItems")]
        public IActionResult GetItems()
        {
            var itemList = _itemRepository.GetItems();

            var itemDtoList = new List<ItemDto>();

            foreach (var itm in itemList)
            {
                itemDtoList.Add(_mapper.Map<ItemDto>(itm));
            }

            return Ok(itemDtoList);
        }

        //Get Individual item by id
        [HttpGet("{id:int}", Name = "GetItem")]
        public IActionResult GetItem(int id)
        {
            var exists = _itemRepository.ItemExists(id);
            if (exists)
            {
                var itemObj = _itemRepository.GetItem(id);

                var itemDto = _mapper.Map<ItemDto>(itemObj);

                return Ok(itemDto);
            }
            else
            {
                return NotFound("Not Found...");
            }
        }

        //Get Items In a specific category
        [HttpGet("[action]/{categoryId:int}", Name = "GetItemsInCategory")]
        public IActionResult GetItemsInCategory(int categoryId)
        {
            var objList = _itemRepository.GetItemsInCategory(categoryId);
            if (objList == null)
            {
                return NotFound();
            }

            var objDto = new List<ItemDto>();
            foreach (var itm in objList)
            {
                objDto.Add(_mapper.Map<ItemDto>(itm));
            }
            return Ok(objDto);
        }

        //Create(Post) Item
        [HttpPost(Name = "CreateItem")]
        public IActionResult CreateItem([FromBody] ItemCreateDto itemCreateDto)
        {

            if (itemCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_itemRepository.ItemExists(itemCreateDto.Name))
            {
                ModelState.AddModelError("", "Item Exists!");
                return StatusCode(404, ModelState);
            }

            var itemObj = _mapper.Map<Item>(itemCreateDto);

            if (!_itemRepository.CreateItem(itemObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {itemObj.Name}");
                return StatusCode(500, ModelState);
            }


            itemObj = _itemRepository.GetItem(itemObj.Id);
            var itemDto = _mapper.Map<ItemDto>(itemObj);

            return StatusCode(StatusCodes.Status201Created, itemDto);
        }

        //Update(Patch) Item
        [HttpPatch("{id:int}", Name = "UpdateItem")]
        public IActionResult UpdateItem(int id, [FromBody] ItemUpdateDto itemDto)
        {
            if (itemDto == null || id != itemDto.Id)
            {
                return BadRequest(ModelState);
            }

            var itemObj = _mapper.Map<Item>(itemDto);

            if (!_itemRepository.UpdateItem(itemObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {itemObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        //Delete Item
        [HttpDelete("{id:int}", Name = "DeleteItem")]
        public IActionResult DeleteItem(int id)
        {
            bool exists = _itemRepository.ItemExists(id);
            if (exists)
            {
                var itemObj = _itemRepository.GetItem(id);
                bool deleted = _itemRepository.DeleteItem(itemObj);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    ModelState.AddModelError("", $"Something went wrong when deleting the record {itemObj.Name}");
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
