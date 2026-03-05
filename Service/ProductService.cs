using AutoMapper;
using DTOs;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;


        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            return await _productRepository.AddProductAsync(product);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<PageResponseDTO<ProductDTO>> GetProducts(int position, int skip, ProductSearchParams parameters)
        {
            (List<Product>, int) response = await _productRepository.GetProducts(position, skip, parameters);

            PageResponseDTO<ProductDTO> pResponse = new();
            List<ProductDTO> products = _mapper.Map<List<ProductDTO>>(response.Item1);
            pResponse.Data = products;
            pResponse.TotalItems = response.Item2;
            pResponse.CurrentPage = position;
            pResponse.SizeOfPage = skip;
            pResponse.HasPreviousPage = position > 1;
            int numOfPages = (pResponse.TotalItems + pResponse.SizeOfPage - 1) / pResponse.SizeOfPage;
            pResponse.HasNextPage = position < numOfPages;
            return pResponse;
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            if (await _productRepository.ProductHasOrdersAsync(id))
                return false;

            await _productRepository.DeleteProductAsync(id);
            return true;
        }

    }
}
