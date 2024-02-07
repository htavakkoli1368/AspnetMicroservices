using Discount.grpc.Protos;

namespace Basket.Api.GrpcServices
{
    public class DiscountGrpcServices
    {
        public readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcServices(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClients)
        {
            _discountProtoService = discountProtoServiceClients;
        }

        public async Task<CouponModel> Getdiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}
