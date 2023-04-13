using _310NutritionAPI.Models;
using _310NutritionAPI.Models.DTO;
using _310NutritionAPI.Repository.IRepository;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace _310NutritionAPI.Controllers
{
    [Route("api/CollectionAPI")]
    [ApiController]
    public class CollectionAPIController:ControllerBase
    {
        protected APIResponse _response;
        private readonly ICollectionRepository _dbCollections;
        private readonly IMapper _mapper;

        public CollectionAPIController(ICollectionRepository dbCollection, IMapper mapper) {
            this._response = new();
            _dbCollections = dbCollection;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowAnyOrigin")]
        public async Task<ActionResult<APIResponse>> GetCollections()
        {
            try
            {
                IEnumerable<Collection> collections = await _dbCollections.GetAllAsync();
                var collectionDTOs = _mapper.Map<List<CollectionDTO>>(collections);
                foreach (var collectionDTO in collectionDTOs) {
                    var productDTOs = _mapper.Map<List<ProductDTO>>(collectionDTO.CollectionProducts);
                    collectionDTO.CollectionProducts = productDTOs;
                }
                _response.Result = collectionDTOs;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return _response;
            }
        }
        [HttpGet("{id:int}", Name ="GetCollectionByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableCors("AllowAnyOrigin")]
        public async Task<ActionResult<APIResponse>> GetCollectionByID(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var collection = await _dbCollections.GetAsync(c => c.Id == id, includeProperties: "CollectionProducts");
                if (collection == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                var collectionDTO = _mapper.Map<CollectionDTO>(collection);
                collectionDTO.CollectionProducts = _mapper.Map<List<ProductDTO>>(collection.CollectionProducts);
                _response.Result = collectionDTO;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return _response;
            }
        }
        [HttpGet("{handle}",Name ="GetCollectionByHandle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCollectionByHandle(string handle)
        {
            try
            {
                if (handle == "")
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var collection = await _dbCollections.GetAsync(c => c.Handle == handle, includeProperties: "CollectionProducts");
                if (collection == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                var collectionDTO = _mapper.Map<CollectionDTO>(collection);
                collectionDTO.CollectionProducts = _mapper.Map<List<ProductDTO>>(collection.CollectionProducts);
                _response.Result = collectionDTO;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return _response;
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateCollection([FromBody] CollectionCreateDTO collectionDTO) {
            try {
                if (await _dbCollections.GetAsync(col => col.Handle.ToLower() == collectionDTO.Handle.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Collection Already Exists");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (collectionDTO == null)
                {
                    return BadRequest(collectionDTO);
                }
                Collection collection = _mapper.Map<Collection>(collectionDTO);
                await _dbCollections.CreateAsync(collection);
                _response.Result = _mapper.Map<CollectionDTO>(collection);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetCollectionByID", new { id = collection.Id }, _response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return _response;
            }
        }
        [HttpPut("{id:int}", Name = "UpdateCollection")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateCollection(int id, [FromBody] CollectionUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest(ModelState);
                }
                //if handle already exists in db
                Collection col = _mapper.Map<Collection>(updateDTO);
                await _dbCollections.UpdateAsync(col);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return _response;
            }
        }
        [HttpDelete("{id:int}", Name = "DeleteCollection")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteCollection(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var collection = await _dbCollections.GetAsync(u => u.Id == id);
                if (collection == null)
                {
                    return NotFound();
                }
                await _dbCollections.RemoveAsync(collection);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return _response;
            }
        }
    }
}
