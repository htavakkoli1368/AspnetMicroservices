using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrdersVm>>
    {

        public readonly IOrderRepository _orderRepository;
        public readonly IMapper _mapper;

        public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrdersVm>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {

           var orderList = await _orderRepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<OrdersVm>>(orderList);
        }
    }
}
  