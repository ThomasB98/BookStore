using AutoMapper;
using DataLayer.Constants.DBContext;
using DataLayer.Exceptions;
using DataLayer.Interfaces;
using DataLayer.Utilities.Logger;
using DataLayer.Utilities.ResponseBody;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Model.Entity;
using ModelLayer.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using MailKit;
using ModelLayer.DTO.Order;
using ModelLayer.DTO.Book;

namespace DataLayer.Repository
{
    public class OrderDL : IOrder
    {
        private readonly IUser _user;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly ILoggerService _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICart _cart;

        public OrderDL(IUser user,IMapper mapper,DataContext dataContext,
                        ILoggerService logger,IHttpContextAccessor httpContextAccessor,
                        ICart cart)
        {
            _context = dataContext; 
            _user = user;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _cart = cart;
        }
        public async Task<ResponseBody<Order>> PlaceOrder(int ShippingId)
        {
            var userContext =_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userContext == null )
            {
                throw new UserNotLoggedInException("User not Logged in");
            }

            var userId = int.Parse(userContext);

            var cartItems=_context.Cart.Include(ci=>ci.cartItems).Where(ci=>ci.userId == userId).ToList();  

            if (cartItems==null)
            {
                throw new CartListEmptyException();
            }

            var orderItems = cartItems.SelectMany(oi => oi.cartItems).
                Select(cartItem => new OrderItem
                {
                    quantity = cartItem.quantity,
                    price = cartItem.Price,
                    BookId = cartItem.bookId
                }).ToList();

            var totalAmount = orderItems.Sum(oi => oi.quantity * oi.price);

            Order order = new Order()
            {
                orderDate = DateTime.Now,
                userId = userId,
                orderStatus = OrderStatus.CONFIRM,
                totalAmount = totalAmount,
                Items = orderItems
            };

            var shippingAddress = _context.Shipping
                .FirstOrDefault(sa => sa.UserId == userId && sa.ShippingId == ShippingId);

            if (shippingAddress == null)
            {
                throw new ShippingAddressNotFoundException("No shipping address found for the user.");
            }

            order.Shipping = shippingAddress;
            _context.Order.Add(order);

            _context.Cart.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Order placed successfully for user {userId}.");

            return new ResponseBody<Order>
            {
                Data = order,
                Message = "Order placed successfully",
                StatusCode = HttpStatusCode.OK,
                Success = true,
            };
        }


        public async Task<ResponseBody<Order>> GetOrderByIdAsync(int orderId)
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userContext == null)
            {
                throw new UserNotLoggedInException("User not Logged in");
            }

            var userId = int.Parse(userContext);


            //var order = await _context.Order.
            //    Include(or => or.Items).
            //    ThenInclude(oi => oi.Book).
            //    Include(or => or.Shipping)
            //    .FirstOrDefaultAsync(or => or.Id == orderId
            //                        && or.userId == userId);

            //var order = await _context.Order.Include(or => or.Items)
            //    .ThenInclude(oi => oi.Book).FirstOrDefaultAsync(or => or.Id == orderId
            //    && or.userId == userId);

            var order = await _context.Order
                .Where(or => or.Id == orderId && or.userId == userId)
                .Include(or => or.Items)
                .ThenInclude(oi => oi.Book)   
                .FirstOrDefaultAsync();



            if (order == null)
            {
                throw new OrderNotFoundException($"Order with ID {orderId} not found for the current user.");
            }

            _logger.LogInformation($"Order with ID {orderId} retrieved successfully for user {userId}.");

            return new ResponseBody<Order>
            {
                Data = order,
                Message = "Order retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Success = true,
            };
        }


        public async Task<ResponseBody<IEnumerable<OrderResponseDto>>> GetOrdersByUserIdAsync(int userId)
        {
            var user=await _context.User.FirstOrDefaultAsync(us=>us.Id == userId);

            if (user == null)
            {
                throw new UserNotFoundException($"user with id {userId} not found");
            }

            var orders = await _context.Order
                .Where(o => o.userId == userId)  
                .Select(o => new OrderResponseDto
                {
                    Id = o.Id,
                    orderDate = o.orderDate,
                    orderStatus = o.orderStatus,
                    totalAmount = o.totalAmount,
                    Items = o.Items.Select(i => new OrderItemResponseDto
                    {
                        Id = i.Id,
                        quantity = i.quantity,
                        price = i.price,
                        Book = new BookResponseDto
                        {
                            Id = i.Book.Id,
                            Title = i.Book.Title,
                            Author = i.Book.Author,
                            Price = i.Book.price,
                            Publisher = i.Book.Publisher,
                            Description = i.Book.descrption,
                            Stock = i.Book.stock,
                            Img = i.Book.img
                        }
                    }).ToList()
                })
                .ToListAsync();

            //var orderEntity = await _context.Order
            //              .Include(o => o.Items)        
            //              .ThenInclude(oi => oi.Book)  
            //               .ToListAsync();

            //.Include(o => o.Shipping)
            //              .Where(o => o.userId == userId)

            //var orders = _mapper.Map<IEnumerable<OrderResponseDto>>(orderEntity);
            if (!orders.Any())
            {
                throw new OrderNotFoundException($"No orders found for user with ID {userId}");
            }

            _logger.LogInformation($"Retrieved {orders} orders for user with ID {userId}.");

            return new ResponseBody<IEnumerable<OrderResponseDto>>
            {
                Data = orders,
                Message = "Orders retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Success = true,
            };

        }

        public async Task<ResponseBody<bool>> DeleteOrderAsync(int orderId)
        {
            var userContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userContext == null)
            {
                throw new UserNotLoggedInException("User not Logged in");
            }

            var userId = int.Parse(userContext);

            var order= await _context.Order.FirstOrDefaultAsync(o => o.Id == userId && o.userId ==userId);

            if(order == null)
            {
                throw new OrderNotFoundException("order not found");
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();


            return new ResponseBody<bool>
            {
                Data=true,
                Message="Order deleted",
                StatusCode = HttpStatusCode.OK,
                Success = true,
            };
        }


    }



}
