using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebPortal.Data.Entities;
using WebPortal.ViewModels;

namespace WebPortal.Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Banner, BannerRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Cart, CartRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Category, CategoryRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Customer, CustomerRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Language, LanguageRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<MailBox, MailBoxRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Order, OrderRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<OrderItem, OrderItemRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PageView, PageViewRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Phrase, PhraseRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Product, ProductRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProductComment, ProductCommentRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProductFile, ProductFileRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProductType, ProductTypeRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProductVote, ProductVoteRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Promotion, PromotionRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Transaction, TransactionRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Website, WebsiteRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AppUser, AppUserRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AppRole, AppRoleRequest>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Banner, BannerView>();
            CreateMap<Cart, CartView>();
            CreateMap<Category, CategoryView>();
            CreateMap<Customer, CustomerView>();
            CreateMap<Language, LanguageView>();
            CreateMap<MailBox, MailBoxView>();
            CreateMap<Order, OrderView>()
                .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.FullName))
                .ForMember(d => d.SaleName, opt => opt.MapFrom(s => s.AppUser.FullName));
            CreateMap<OrderItem, OrderItemView>();
            CreateMap<PageView, PageViewView>();
            CreateMap<Phrase, PhraseView>();
            CreateMap<Product, ProductView>();
            CreateMap<ProductComment, ProductCommentView>();
            CreateMap<ProductFile, ProductFileView>();
            CreateMap<ProductType, ProductTypeView>();
            CreateMap<ProductVote, ProductVoteView>();
            CreateMap<Promotion, PromotionView>();
            CreateMap<Transaction, TransactionView>();
            CreateMap<AppRole, AppRoleView>();
            CreateMap<AppUser, AppUserView>();
        }
    }
}
