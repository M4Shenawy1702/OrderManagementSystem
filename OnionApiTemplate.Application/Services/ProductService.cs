using OrderManagementSystem.Application.DOTs;
using OrderManagementSystem.Application.DOTs.Product;

namespace OrderManagementSystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductDto> _validator;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<ProductDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<int> CreateAsync(ProductDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var repo = _unitOfWork.GetRepository<Product, int>();
            var existingProduct = await repo.GetAsync(new GetProductByNameSpecification(dto.Name));

            if (existingProduct != null)
                throw new ProductAlreadyExistException();

            var product = _mapper.Map<Product>(dto);
            await repo.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return product.Id;
        }

        public async Task<ProductDetailsDto> UpdateAsync(int id, ProductDto dto)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var product = await repo.GetByIdAsync(id) ?? throw new ProductNotFoundException(id);

            _mapper.Map(dto, product);
            repo.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductDetailsDto>(product);
        }

        public async Task DeleteAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var product = await repo.GetByIdAsync(id) ?? throw new ProductNotFoundException(id);

            repo.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProductDetailsDto> GetByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var product = await repo.GetByIdAsync(id);

            return product == null ? throw new ProductNotFoundException(id) : _mapper.Map<ProductDetailsDto>(product);
        }

        public async Task<PaginatedResult<ProductDetailsDto>> GetAllAsync(ProductQueryParameters productQuery)
        {
            var spec = new GetAllProductsSpecification(productQuery);
            var repo = _unitOfWork.GetRepository<Product, int>();

            var totalCount = await repo.GetCountAsync(spec);
            var products = await repo.GetAllAsync(spec);

            var productDtos = _mapper.Map<IEnumerable<ProductDetailsDto>>(products);

            return new PaginatedResult<ProductDetailsDto>(
                PageIndex: productQuery.PageIndex,
                PageSize: productQuery.PageSize,
                Count: totalCount,
                Data: productDtos
            );
        }

    }
}
