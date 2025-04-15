using API.CustomAttributes;
using Application.Interfaces;
using Infrastructure.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize]
public class ProductController : ControllerBase
{

    private readonly IProductService _service;
    public ProductController(IProductService productService)
    {
        _service = productService;
    }

   

    [HttpGet("{id}")]
   [AccessControl(Permission = "get-by-id")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.GetAsync(id);
      return Ok(result);
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll(int page , int size)
    {
        var result = await _service.GetAllAsync(page , size );
        return Ok(result);
    }


    [HttpPost]
    [SwaggerOperation(
        Summary =" Create a new Product " , 
        Description ="This is for test " ,
        OperationId ="Product.Get",
        Tags =new []{"Product controller "}
    )]
    public async Task<ActionResult> Create(ProductDto product)
    {
       var result =  await _service.AddAsync(product);
        return Ok(result);

    }

}