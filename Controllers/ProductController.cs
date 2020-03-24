using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Models;

namespace RegaloParaTiAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private  readonly  ApiDbContext _context;

        public ProductController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public object Get()
        {
            var data = _context.Products; 
            return data.ToList();
            
        }


    }


}