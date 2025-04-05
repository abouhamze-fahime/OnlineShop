﻿using API.CustomAttributes;
using Application.CQRS.ProductCommandQuery.Command;
using Application.CQRS.ProductCommandQuery.Query;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductCQRSController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;
        public ProductCQRSController(IMediator mediator  , IMapper mapper)
        {
            this.mediator = mediator;
            _mapper = mapper;
        }

        public IMapper Mapper { get; }

        [HttpPost]
       
        public async Task<IActionResult> Create(SaveProductCommand product)
        {
          var result = await mediator.Send(product);
            return Ok(result);
        }

        [HttpGet("id")]
        [AccessControl(Permission = "get-by-id")]
        public async Task <IActionResult> GetProduct([FromQuery] GetProductQuery getProductQuery)
        {
          
            var result = await mediator.Send(getProductQuery);

            return Ok(result);
        }
    }
}
