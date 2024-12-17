using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.Shipping;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IShipping
    {
        Task<ResponseBody<bool>> AddAddressAsync(ShippingCreateDto shippingDto);

        Task<ResponseBody<bool>> DeleteAddressAsync(int shippingId);

        Task<ResponseBody<Shipping>> GetAddressByIDAsync(int shippingId);

        Task<ResponseBody<IEnumerable<Shipping>>> getAllAddressByUserId();

        Task<ResponseBody<bool>> updateAddress(ShippingUpdateDto shippingDto);
    }
}
