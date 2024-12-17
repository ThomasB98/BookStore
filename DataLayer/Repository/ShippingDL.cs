using AutoMapper;
using DataLayer.Constants.DBContext;
using DataLayer.Exceptions;
using DataLayer.Interfaces;
using DataLayer.Utilities.ResponseBody;
using Microsoft.AspNetCore.Http;
using ModelLayer.DTO.Shipping;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
    public class ShippingDL : IShipping
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShippingDL(DataContext dataContext, IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseBody<bool>> AddAddressAsync(ShippingCreateDto shippingDto)
        {
            var userIdContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdContext == null)
            {
                throw new UserNotLoggedInException("User Not logged in");
            }
            var userId = int.Parse(userIdContext);

            if(userId != shippingDto.UserId)
            {
                throw new Exception("Invalid user");
            }

            var shipping=_mapper.Map<Shipping>(shippingDto);
            shipping.OrderId = null;

            _dataContext.Shipping.Add(shipping);

            await _dataContext.SaveChangesAsync();

            return new ResponseBody<bool>
            {
                Data=true,
                Success=true,
                Message = "address add",
                StatusCode = HttpStatusCode.Created,
            };
        }

        public async Task<ResponseBody<bool>> DeleteAddressAsync(int shippingId)
        {
            var userIdContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdContext == null)
            {
                throw new UserNotLoggedInException("User Not logged in");
            }
            var userId = int.Parse(userIdContext);

            var shipping =_dataContext.Shipping.FirstOrDefault(s=>s.ShippingId == shippingId);
            if (shipping == null)
            {
                throw new ShippingAddressNotFoundException("address not found invalid id");
            }

            _dataContext.Shipping.Remove(shipping);
            await _dataContext.SaveChangesAsync();

            return new ResponseBody<bool>
            {
                Data = true,
                Success = true,
                Message = "address Deleted",
                StatusCode = HttpStatusCode.Created,
            };
        }

        public async Task<ResponseBody<Shipping>> GetAddressByIDAsync(int shippingId)
        {
            var userIdContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdContext == null)
            {
                throw new UserNotLoggedInException("User Not logged in");
            }
            var userId = int.Parse(userIdContext);

            var shipping = _dataContext.Shipping.FirstOrDefault(s => s.ShippingId == shippingId);
            if (shipping == null)
            {
                throw new ShippingAddressNotFoundException("address not found invalid id");
            }

            return new ResponseBody<Shipping>
            {
                Data = shipping,
                Success = true,
                Message = "address ",
                StatusCode = HttpStatusCode.OK,
            };
        
        }

        public async Task<ResponseBody<IEnumerable<Shipping>>> getAllAddressByUserId()
        {
            var userIdContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdContext == null)
            {
                throw new UserNotLoggedInException("User Not logged in");
            }
            var userId = int.Parse(userIdContext);

            var shipping = _dataContext.Shipping.Where(u=>u.UserId==userId).ToList();

            if (shipping == null)
            {
                throw new ShippingAddressNotFoundException("address not found invalid id");
            }

            return new ResponseBody<IEnumerable<Shipping>>
            {
                Data = shipping,
                Success = true,
                Message = "User address List",
                StatusCode = HttpStatusCode.OK,
            };

        }

        public async Task<ResponseBody<bool>> updateAddress(ShippingUpdateDto shippingDto)
        {
            var userIdContext = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdContext == null)
            {
                throw new UserNotLoggedInException("User Not logged in");
            }
            var userId = int.Parse(userIdContext);

            if(userId != shippingDto.UserId)
            {
                throw new Exception("Invalid user id");
            }

            var shipping = await _dataContext.Shipping.FirstOrDefaultAsync(s=>s.ShippingId==shippingDto.ShippingId); 

            if (shipping == null)
            {
                throw new ShippingAddressNotFoundException("Invalid shipping id" );
            }

            var mapedShipping= _mapper.Map<Shipping>(shippingDto);

            mapedShipping.OrderId = null;
            _dataContext.Shipping.Update(mapedShipping);

            await _dataContext.SaveChangesAsync();

            return new ResponseBody<bool>
            {
                Data = true,
                Success = true,
                Message = "User address updated",
                StatusCode = HttpStatusCode.OK,
            };
        }
    }
}
