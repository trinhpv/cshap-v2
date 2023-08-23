using AutoMapper;
using Microsoft.Extensions.Hosting;
using Shoping.Model.Models;
using Shoping.Model.Types.Category;
using Shoping.Model.Types.Order;
using Shoping.Model.Types.Product;
using Shopping.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shopping.Service.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //source mapping to destination
            CreateMap<UserEntity, User>().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<CategoryEntity, Category>().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<Category, CategoryEntity>().ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<Product, ProductEntity>().ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<CategoryEntity, GetListCategoryResponse > ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<ProductEntity, ProductResponseItem> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<ProductEntity, Product> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<UserEntity, SimpleUser> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<CategoryEntity, SimpleCategory> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<CommentEntity, SimpleComment> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<CommentEntity, Comment> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<VoteEntity, SimpleVote> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<CartEntity, Cart> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<OrderEntity, Order> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<OrderEntity, OrderResponseItem> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<OrderProductEntity, OrderProduct> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<ProductEntity, SimpleProduct> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<OrderProductEntity, SimpleOrderProduct> ().ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
