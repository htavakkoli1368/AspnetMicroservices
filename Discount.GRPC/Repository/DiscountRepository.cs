using Dapper;
using Discount.GRPC.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
namespace Discount.GRPC.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        public readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration _config)
        {
            _configuration = _config ?? throw new ArgumentNullException(nameof(_config));
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var con = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var sqlquery = $@"SELECT * FROM coupon WHERE ProductName = @ProductName";
            var coupon = await con.QueryFirstOrDefaultAsync<Coupon>(sqlquery, new { ProductName = productName});
            if (coupon == null) 
            {
              return new Coupon() {ProductName="No Discount",Description="no coupon found",Amount =0 };
            }
            return coupon;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var con = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var result = await con.ExecuteAsync("insert into Coupon (ProductName,Description,Amount) values(@ProductName,@Description,@Amount)", new { ProductName = coupon.ProductName, Description=coupon.Description ,coupon.Amount});
            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var con = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var result = await con.ExecuteAsync("delete from Coupon where ProductName = @ProductName", new { ProductName = productName });
            if (result == 0)
            {
                return false;
            }
            return true;
        }    

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var con = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var result = await con.ExecuteAsync("update Coupon set ProductName= @ProductName,Description=@Description,Amount = @Amount where id =@id", new { ProductName = coupon.ProductName, Description = coupon.Description, coupon.Amount,Id= coupon.Id});
            if (result == 0)
            {
                return false;
            }
            return true;
        }
    }
}
