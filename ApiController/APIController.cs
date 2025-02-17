using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ECommerceWeb.ApiController
{
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly DemoProjectContext _context;
        public APIController(DemoProjectContext context)
        {
            _context = context;
        }

        [Route("Api/GetStates")]
        public IActionResult GetStates(int countryId)
        {
            var states = _context.States.Where(s => s.CountryId == countryId).ToList();
            return Ok(states);
        }

        [Route("Api/GetAllSubCat")]
        public IActionResult GetAllSubCat(int CategoryId)
        {
            var subCategories = _context.SubCategories.Where(s => s.CategoryId == CategoryId).ToList();
            return Ok(subCategories);
        }

        [Route("Api/GetAllCities")]
        public IActionResult GetAllCities(int CountryId)
        {
            var states = _context.States.Where(s => s.CountryId == CountryId).ToList();
            return Ok(states);
        }

        [Route("Api/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }

        [Route("Api/Dashboard")]
        public IActionResult Dashboard()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }

        [Route("Api/GetOrderDetails")]
        public IActionResult GetOrderDetails(int OrderId)
        {
            var result = _context.Orders.Where(s => s.Id == OrderId).Select(x => new OrderDetailsVM
            {
                CustomerId = x.CustomerId,
                Id = x.Id,
                OrderDate = x.OrderDate,
                OrderNumber = x.OrderNumber,
                OrderStatus = x.OrderStatus,
                orderItems = x.OrderItems.Select(y => new OrderItemsList
                {
                    Id = y.Id,
                    Price = y.Product.Price,
                    ProductId = y.ProductId,
                    ProductName = y.Product.ProductName,
                    Quantity = y.ItemQuantity
                }).ToList()
            }).FirstOrDefault();

            return Ok(result);
        }

        [Route("Api/GetOrderDetailsForPopup")]
        public IActionResult GetOrderDetailsForPopup(int OrderId)
        {
            var result = _context.Orders.Where(s => s.Id == OrderId).Select(x => new OrderDetailsVM
            {
                CustomerId = x.CustomerId,
                Id = x.Id,
                OrderDate = x.OrderDate,
                OrderNumber = x.OrderNumber,
                OrderStatus = x.OrderStatus,
                orderItems = x.OrderItems.Select(y => new OrderItemsList
                {
                    Id = y.Id,
                    Price = y.Product.Price,
                    ProductId = y.ProductId,
                    ProductName = y.Product.ProductName,
                    Quantity = y.ItemQuantity
                }).ToList()
            }).FirstOrDefault();

            return Ok(result);
        }


        [Route("Api/GetOrderItemsListForPopup")]
        public IActionResult GetOrderItemsListForPopup(int OrderId)
        {
            var result = _context.Orders.Where(s => s.Id == OrderId).Select(x => new OrderDetailsVM
            {
                CustomerId = x.CustomerId,
                Id = x.Id,
                OrderDate = x.OrderDate, 
                OrderNumber = x.OrderNumber,
                OrderStatus = x.OrderStatus,
                orderItems = x.OrderItems.Select(y => new OrderItemsList
                {
                    Id = y.Id,
                    Price = y.Product.Price,
                    ProductId = y.ProductId,
                    ProductName = y.Product.ProductName,
                    Quantity = y.ItemQuantity
                }).ToList()
            }).FirstOrDefault();

            return Ok(result);
        }




        [Route("Api/SignOut")]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }


        //------------YourOrders API---------

        [Route("Api/GetYourOrdersForPopup")]
        public IActionResult GetYourOrdersForPopup(int YourOrderId)
        {
            var result = _context.Orders
                .Where(x => x.Id == YourOrderId)
                .Select(x => new OrderDetailsVM
                {
                    CustomerId = x.CustomerId,
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    OrderNumber = x.OrderNumber,
                    OrderStatus = x.OrderStatus,
                    orderItems = x.OrderItems.Select(y => new OrderItemsList
                    {
                        Id = y.Id,
                        Price = y.Product.Price,
                        ProductId = y.ProductId,
                        ProductName = y.Product.ProductName,
                        Quantity = y.ItemQuantity
                    }).ToList()
                })
                .FirstOrDefault();

            return Ok(result);


        }

        //[Route("Api/GetAppInfo")]
        //public IActionResult GetInfoApps(int AppInfoId)
        //{
        //    // Query to find AppInfo by Id
        //    var result = _context.AppInfos
        //        .Where(s => s.Id == AppInfoId)
        //        .Select(x => new
        //        {
        //            Id = x.Id,
        //            LogoBackGroundImage = x.LogoBackGroundImage,
        //            FavIcon = x.FavIcon,
        //            Logo = x.Logo
        //        })
        //        .FirstOrDefault();

        //    if (result == null)
        //    {
        //        return NotFound(new { Message = "AppInfo not found" });
        //    }

        //    return Ok(result);
        //}

        //[Route("Api/GetProductReview")]
        //public IActionResult GetProductReviews (int ProductReviewId) 
        //{
        //  var result = _context.ProductReviews.Where(p => p.Id == ProductReviewId).Select(x => new
        //  {
        //      Id = x.Id,
        //      Product = x.Product,
        //      Registration = x.Registration.CreatedDate,
        //      img1 = x.ImageOne,
        //      img2 = x.ImageTwo,


        //  }).FirstOrDefault();
        //    if(result == null)
        //    {
        //        return NotFound(new { Message = "Product Review not found" });
        //    }
        //    return Ok(result);
        //}










        //[Route("api/AppInfo")]
        //[ApiController]
        //public class AppInfoApiController : ControllerBase
        //{
        //    [HttpPost("UploadImages")]
        //    public IActionResult UploadImages([FromForm] IFormFile file)
        //    {
        //        // Logic to save the image
        //        return Ok("Image Uploaded Successfully");
        //    }
        //}



    //    [ApiController]
    //    [Route("api/[]")]
    //    public class CallController : ControllerBase
    //    {
    //        [HttpPost("make-call")]
    //        public IActionResult MakeCall([FromBody] CallRequest callRequest)
    //        {
    //            if (string.IsNullOrEmpty(callRequest.PhoneNumber))
    //            {
    //                return BadRequest("Phone number is required.");
    //            }

    //            // Here you would implement the logic to make the call.
    //            // This is usually done via a telephony API (like Twilio, Nexmo, etc.).
    //            // For this example, we'll just simulate success.

    //            return Ok(new { message = $"Calling {callRequest.PhoneNumber}..." });
    //        }
    //    }

    //    public class CallRequest
    //    {
    //        public string PhoneNumber { get; set; }
    //    }
    //}








}

}  
























