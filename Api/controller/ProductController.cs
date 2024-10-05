using Api.Data;
using Api.Model;
using Api.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Api.controller
{

    public class ProductController : StoreController
    {
        public ProductController(AppDbContext dbContext) : base(dbContext)
        {

        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {

            return Ok(new ResponceServer
            {
                StatusCode = HttpStatusCode.OK,
                Result = await dbContext.Products.ToListAsync()
            });
        }

        [HttpGet("{id}", Name = nameof(GetProductById))]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new ResponceServer
                {
                    IsSucsesful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "неверный id" }
                });
            }

            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound(new ResponceServer
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSucsesful = false,
                    ErrorMessages = { "по указанному id продукт не найден" }
                });
            }
            else
            {
                return Ok(new ResponceServer
                {
                    IsSucsesful = true,
                    StatusCode = HttpStatusCode.OK,
                    Result = product
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponceServer>> CreateProduct([FromBody] ProductCreateDto ProductCreateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ProductCreateDto.Image == null || ProductCreateDto.Image.Length == 0)
                    {
                        return BadRequest(new ResponceServer
                        {
                            IsSucsesful = false,
                            StatusCode = HttpStatusCode.BadRequest,
                            ErrorMessages = { "Image не может быть пустым" }
                        });
                    }
                    else
                    {
                        Product item = new()
                        {
                            Name = ProductCreateDto.Name,
                            Description = ProductCreateDto.Description,
                            SpetialTag = ProductCreateDto.SpetialTag,
                            Category = ProductCreateDto.Category,
                            Price = ProductCreateDto.Price,
                            Image = $"https://placehold.co/250"
                        };
                        await dbContext.Products.AddAsync(item);
                        await dbContext.SaveChangesAsync();
                        ResponceServer responce = new()
                        {
                            StatusCode = HttpStatusCode.Created,
                            Result = item
                        };


                        return CreatedAtRoute(nameof(GetProductById), new { id = item.Id }, responce);
                    }
                }
                else
                {
                    return BadRequest(new ResponceServer
                    {
                        IsSucsesful = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrorMessages = { "модель данных не подходит" }
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponceServer
                {
                    IsSucsesful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "что то сломалось" , ex.Message }
                });
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponceServer>> UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if(productUpdateDto == null ||  productUpdateDto.Id != id)
                    {
                        return BadRequest(new ResponceServer
                        {
                            IsSucsesful = false,
                            StatusCode = HttpStatusCode.BadRequest,
                            ErrorMessages = { "несоответствие модели данных" }
                        });
                    }
                    else
                    {
                        Product productFromDb = await dbContext
                            .Products
                            .FindAsync(id);
                        if(productFromDb == null)
                        {
                            return NotFound(new ResponceServer
                            {
                                IsSucsesful = false,
                                StatusCode = HttpStatusCode.NotFound,
                                ErrorMessages = { "id не найден" }
                            });
                        }
                        productFromDb.Name = productUpdateDto.Name;
                        productFromDb.Description = productUpdateDto.Description;
                        productFromDb.Category = productUpdateDto.Category;
                        productFromDb.SpetialTag = productUpdateDto.SpetialTag;
                        productFromDb.Price = productUpdateDto.Price;
                        if(productUpdateDto.Image != null && productUpdateDto.Image.Length > 0)
                        {
                            productFromDb.Image = $"https://placehold.co/350";
                        }
                        dbContext.Products.Update(productFromDb);
                        await dbContext.SaveChangesAsync();
                        return Ok(new ResponceServer
                        {
                            StatusCode = HttpStatusCode.OK,
                            Result = productFromDb
                        });
                    }
                }
                else
                {
                    return BadRequest(new ResponceServer
                    {
                        IsSucsesful = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrorMessages = { " модели не верна" }
                    });
                }
            }
            catch(Exception ex) 
            {
                return BadRequest(new ResponceServer
                {
                    IsSucsesful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { " что то не так", ex.Message }
                });
            }
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveProductById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ResponceServer
                    {
                        IsSucsesful = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrorMessages = { "неверный id" }
                    });
                }
                var product = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                {
                    return NotFound(new ResponceServer
                    {
                        IsSucsesful = false,
                        StatusCode = HttpStatusCode.NotFound,
                        ErrorMessages = { "по указанному id продукт не найден" }
                    });
                }
                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();
                return Ok(new ResponceServer
                {
                    IsSucsesful = true,
                    StatusCode = HttpStatusCode.NoContent
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponceServer
                {
                    IsSucsesful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "все плохо", ex.Message }
                });
            }
        }
    }
}
