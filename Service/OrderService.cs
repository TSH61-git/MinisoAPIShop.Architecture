using AutoMapper;
using DTOs;
using Entities;
using NuGet.Protocol.Core.Types;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }
        public async Task<IEnumerable<OrderReadDTO>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderReadDTO>>(orders);
        }

        public async Task<OrderReadDTO> addOrder(OrderCreateDTO orderDto)
        {
            Order order = _mapper.Map<Order>(orderDto);
            order.OrderDate = DateOnly.FromDateTime(DateTime.Now);
            order.Status = "Accepted";
            Order newOrder = await _orderRepository.AddOrder(order);
            return _mapper.Map<OrderReadDTO>(newOrder);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, ChangeOrderStatusDto dto)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) return false;

            // נעילה אחרי Received
            if (order.Status == "Received")
                throw new InvalidOperationException("אי אפשר לשנות אחרי Received");

            // אם לקוח לחץ קיבלתי
            if (dto.Received)
            {
                if (order.Status != "Delivered")
                    throw new InvalidOperationException("אפשר לסמן קיבלתי רק אחרי Delivered");

                return await _orderRepository.UpdateOrderStatusAsync(orderId, "Received");
            }

            // שינוי רגיל
            if (string.IsNullOrWhiteSpace(dto.Status))
                throw new ArgumentException("Status חסר");

            var allowed = new[] { "Accepted", "Processing", "Shipped", "Delivered" };

            if (!allowed.Contains(dto.Status))
                throw new ArgumentException("סטטוס לא חוקי");

            return await _orderRepository.UpdateOrderStatusAsync(orderId, dto.Status);
        }
    }
}
