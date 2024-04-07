using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entites;
using Talabat.Core.IRepositories;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : BaseAPIsController
    {
        private readonly Iunitofwork _Iunitofwork;
        private readonly IMapper _mapper;

        public ProductsController(Iunitofwork Iunitofwork,IMapper mapper)
        {
           
            _Iunitofwork = Iunitofwork;
            _mapper = mapper;
        }

        [CachedAttribute(600)]
        [HttpGet] 
        public async Task<ActionResult<Pagination<ProductToReturneDto>>> GetProducts([FromQuery]ProduectSpecParams produectParams )
        {
            var spec = new ProductWithBrandandTypeSpecifications(produectParams);

            var Products = await _Iunitofwork.Repository<Product>().GetallwithSpecAsync(spec);

            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturneDto>>(Products);

            var countSpec = new ProductwithFiltersforCountSpecification(produectParams);

            var count = await _Iunitofwork.Repository<Product>().GetCountAsync(countSpec);

            return Ok(new Pagination<ProductToReturneDto>(produectParams.PageIndex,produectParams.PageSize ,count, Data));
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<ProductToReturneDto>> GetProductById(int id)
        {
            var spec = new ProductWithBrandandTypeSpecifications(id);

            var Products = await _Iunitofwork.Repository<Product>().GetIdwithSpecAsync(spec);

            if(Products == null) return NotFound(new ApiResponese(404,"no Product have this id"));

            return Ok(_mapper.Map<Product , ProductToReturneDto>(Products));
        }

        [CachedAttribute(600)]
        [HttpGet("brands")] 
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrand()
        {
            var Brands = await _Iunitofwork.Repository<ProductBrand>().GetAllAsync();

            return Ok(Brands);
        }

        [CachedAttribute(600)]
        [HttpGet("types")] 
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _Iunitofwork.Repository<ProductType>().GetAllAsync();

            return Ok(types);
        }




    }
}
