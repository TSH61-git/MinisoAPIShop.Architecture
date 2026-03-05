using AutoMapper;
using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class AutoMapping : Profile
    {
        public AutoMapping()
        {
            // מיפוי עבור קטגוריות
            CreateMap<Category, CategoryDTO>().ReverseMap();

            // מיפוי עבור מוצרים
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductShortDTO>().ReverseMap();

            // מיפוי עבור משתמשים
            CreateMap<User, UserReadDTO>();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<UserLoginDTO, User>();
            CreateMap<UserUpdateDTO, User>();

            // מיפוי עבור הזמנות
            CreateMap<OrderCreateDTO, Order>();
            CreateMap<Order, OrderReadDTO>();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

            // מיפוי עבור סניפים
            CreateMap<Branch, BranchDTO>().ReverseMap();
        }
    }
}
