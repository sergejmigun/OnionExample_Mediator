using OnionExample.Areas.Products.ApiModels;
using OnionExample.Domain.Models.Common.Products;
using OnionExample.Domain.Services.Contracts.Products.Commands;

namespace OnionExample.Areas.Products.Mappers
{
    public static class ProductsMapper
    {
        public static ProductModel ToProductModel(this Product product)
        {
            return new ProductModel
            {
                 Id = product.Id,
                 Title = product.Title,
                 Description = product.Description,
                 Price = product.Price
            };
        }

        public static CreateProductCommand ToCreateProductCommand(this ProductSubmitModel productSubmitModel)
        {
            return new CreateProductCommand(
               productSubmitModel.Title,
               productSubmitModel.Description,
               productSubmitModel.Price);
        }

        public static UpdateProductCommand ToUpdateProductCommand(this ProductSubmitModel productSubmitModel)
        {
            return new UpdateProductCommand(
               productSubmitModel.Id.GetValueOrDefault(),
               productSubmitModel.Title,
               productSubmitModel.Description,
               productSubmitModel.Price);
        }
    }
}