using ECommerce.Contracts.IntegrationEvents.Inventory;
using ECommerce.Inventory.ApplicationUseCases.CommandsAndQueries;
using ECommerce.Inventory.Domain;
using ECommerce.SharedFramework;
using MassTransit;
using MediatR;

namespace ECommerce.Inventory.ApplicationUseCases;

public class ProductStockUseCases(
    IUnitOfWork unitOfWork,
    IProductStockRepository productStockRepository,
    IBus rabbitBus) :
    IRequestHandler<AddStockCommand, StockQueryResult>,
    IRequestHandler<EditStockCommand, StockQueryResult>,
    IRequestHandler<GetStockByIdQuery, StockQueryResult>,
    IRequestHandler<GetStockByProductIdQuery,StockQueryResult>
    
{
    public async Task<StockQueryResult> Handle(AddStockCommand request, CancellationToken cancellationToken)
    {
        var productStock = new ProductStock(request.ProductId,
            request.Count,
            request.Discount,
            request.SerialNumber,
            (ProductType)request.ProductType,
            request.Price,
            (Color)request.Color);

        var stock = productStockRepository.RegisterStock(productStock);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await rabbitBus.Publish(new ProductStockCreatedEvent(stock.Id,
                stock.ProductId,
                stock.Count,
                stock.Discount,
                stock.SerialNumber,
                (byte)stock.ProductType,
                stock.Price,
                (byte)stock.Color),
            cancellationToken);

        return MapToStockQueryResult(stock);
    }


    public async Task<StockQueryResult> Handle(EditStockCommand request, CancellationToken cancellationToken)
    {
        var existingProductStock = await productStockRepository.GetStockByIdAsync(request.Id);
        if (existingProductStock is null)
        {
            throw new Exception("Product stock not found");
        }

        existingProductStock.UpdateInventoryData(request.Count,
            request.Discount, 
            request.SerialNumber,
            (ProductType)request.ProductType, 
            request.Price,
            (Color)request.Color);
        
        productStockRepository.UpdateStock(existingProductStock);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        await rabbitBus.Publish(new ProductStockEditedEvent(
                existingProductStock.Id,
                existingProductStock.ProductId,
                existingProductStock.Count,
                existingProductStock.Discount,
                existingProductStock.SerialNumber,
                (byte)existingProductStock.ProductType,
                existingProductStock.Price,
                (byte)existingProductStock.Color),
            cancellationToken);

        return MapToStockQueryResult(existingProductStock);
    }

    public async Task<StockQueryResult> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
    {
        var productStock = await productStockRepository.GetStockByIdAsync(request.Id);
        if (productStock is null)
        {
            throw new Exception("Product stock not found");
        }
        return MapToStockQueryResult(productStock);
    }

    public async Task<StockQueryResult> Handle(GetStockByProductIdQuery request, CancellationToken cancellationToken)
    {
        var productStock = await productStockRepository.GetStockByProductIdAsync(request.ProductId);
        if (productStock is null)
        {
            throw new Exception("Product stock not found");
        }
        return MapToStockQueryResult(productStock);
    }
    
    private static StockQueryResult MapToStockQueryResult(ProductStock stock)
    {
        return new StockQueryResult()
        {
            Id = stock.Id,
            ProductId = stock.ProductId,
            Count = stock.Count,
            Discount = stock.Discount,
            SerialNumber = stock.SerialNumber,
            ProductType = (byte)stock.ProductType,
            Price = stock.Price,
            Color = (byte)stock.Color
        };
    }
}