using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiDeneme.Model;


namespace ApiDeneme.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        Product product = new Product();

        private ProductContext _productContext;


        public ValuesController(ProductContext context)
        {
            _productContext = context;

            if (_productContext.products.Count() == 0)
            {
                _productContext.products.Add(new Product { ID = 1, ProductName = "Fındık", UnitPrice = 45 });
                _productContext.products.Add(new Product { ID = 2, ProductName = "Fıstık", UnitPrice = 55 });
                _productContext.products.Add(new Product { ID = 3, ProductName = "Kaju", UnitPrice = 35 });
                _productContext.products.Add(new Product { ID = 4, ProductName = "Çekirdek", UnitPrice = 24 });

                _productContext.SaveChanges();
               
            }
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productContext.products.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _productContext.products.Find(id);
            return new ObjectResult(result);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]Product product)
        {
            //_productContext.products.Add(new Product { ID = 5, ProductName = "Fındık", UnitPrice = 50 });
            //_productContext.SaveChanges();
            _productContext.Set<Product>().Add(new Product { ID = product.ID , ProductName = product.ProductName , UnitPrice = product.UnitPrice });
            _productContext.SaveChanges();
            return Ok("Success");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Product product)
        {
            //var query = _productContext.products.Find(id);
            //query.ID = id;
            //query.ProductName = product.ProductName;
            //query.UnitPrice = product.UnitPrice;
            //_productContext.Set<Product>().Update(query);
            //_productContext.SaveChanges();
            //return Ok("Success");

            _productContext.Set<Product>().Update(new Product { ID = product.ID , ProductName = product.ProductName, UnitPrice = product.UnitPrice });
            _productContext.SaveChanges();
            return Ok("Updated successful..!!");

        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if(id < 0)
            {
                return BadRequest("Geçersiz Id");
            }
            using (var _productContext = new ProductContext())
            {

                var query = _productContext.products.Where(x => x.ID == id).FirstOrDefault();
                _productContext.Entry(query).State = EntityState.Deleted;
                _productContext.SaveChanges();
            }
                
            return Ok();
        }
    }
}
