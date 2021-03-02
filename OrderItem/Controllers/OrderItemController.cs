using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderItem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderItem.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderItemController : ControllerBase
    {
        private CartContext _context;

        public OrderItemController()
        {
            _context = new CartContext();
        }
        
        
        // POST api/<OrderItemController>
        
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id)
        {
            
            var jwtToken = "";
            string request1 = this.Request.Headers["Authorization"];
            //bool isToken = request1.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues value);
            jwtToken = request1.Remove(0,7);
            //return new OkObjectResult(jwtToken);
            if (jwtToken !="")
            {
                //jwtToken = value;
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://menuitemlistingservicesvc/api/MenuItem/" + id);
                    client.DefaultRequestHeaders.Accept.Clear();
                    var request = new HttpRequestMessage(HttpMethod.Get, client.BaseAddress);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                    HttpResponseMessage result = await client.SendAsync(request);
                    if (result.IsSuccessStatusCode)
                    {
                        var object1 = new { Id = 1, Name = "", FreeDelivery = true, Price = 100.00, DateOfLaunch = DateTime.Parse("2020-10-10"), Active = true };
                        var jsonstring = result.Content.ReadAsStringAsync();
                        jsonstring.Wait();
                        var obj = JsonConvert.DeserializeAnonymousType(jsonstring.Result, object1);
                        Cart c = new Cart()
                        {
                            Id = 1,
                            UserId = 1,
                            MenuItemId = id,
                            MenuItemName = (string)obj.Name
                        };
                        return new OkObjectResult(c);

                    }
                    else
                    {
                        return new OkObjectResult("No cart build");
                    }

                }
                
                

            }
            return new BadRequestObjectResult("Un Authorized");

            
        }



    }
}
