using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.Shipping;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.service
{
    public class ShippingBL : IShippingService
    {
        private readonly IShipping _shippingRepo;

        public ShippingBL(IShipping shippingRepo)
        {
            _shippingRepo = shippingRepo;
        }

        public Task<ResponseBody<bool>> AddAddressAsync(ShippingCreateDto shippingDto)
        {
            return _shippingRepo.AddAddressAsync(shippingDto);
        }

        public Task<ResponseBody<bool>> DeleteAddressAsync(int shippingId)
        {
            return _shippingRepo.DeleteAddressAsync(shippingId);
        }

        public Task<ResponseBody<Shipping>> GetAddressByIDAsync(int shippingId)
        {
            return _shippingRepo.GetAddressByIDAsync(shippingId);
        }

        public Task<ResponseBody<IEnumerable<Shipping>>> getAllAddressByUserId()
        {
            return _shippingRepo.getAllAddressByUserId();
        }

        public Task<ResponseBody<bool>> updateAddress(ShippingUpdateDto shippingDto)
        {
            return _shippingRepo.updateAddress(shippingDto);
        }
    }
}
