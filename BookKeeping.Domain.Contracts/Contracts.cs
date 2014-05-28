using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedMember.Local
namespace BookKeeping.Domain.Contracts
{
    #region Generated by Lokad Code DSL
    [DataContract(Namespace = "BookKeeping")]
public partial class CreateProduct : ICommand<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public string Title { get; private set; }
        [DataMember(Order = 3)] public string ItemNo { get; private set; }
        [DataMember(Order = 4)] public CurrencyAmount Price { get; private set; }
        [DataMember(Order = 5)] public decimal Stock { get; private set; }
        [DataMember(Order = 6)] public string UnitOfMeasure { get; private set; }
        [DataMember(Order = 7)] public VatRate VatRate { get; private set; }
        [DataMember(Order = 8)] public Barcode Barcode { get; private set; }
        
        CreateProduct () {}
        public CreateProduct (ProductId id, string title, string itemNo, CurrencyAmount price, decimal stock, string unitOfMeasure, VatRate vatRate, Barcode barcode)
        {
            Id = id;
            Title = title;
            ItemNo = itemNo;
            Price = price;
            Stock = stock;
            UnitOfMeasure = unitOfMeasure;
            VatRate = vatRate;
            Barcode = barcode;
        }
        
        public override string ToString()
        {
            return string.Format(@"Create product - {0}, {1}, {2}, {3}, {4}, {5}, {6}", Title, ItemNo, Price, Stock, UnitOfMeasure, VatRate, Barcode);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ProductCreated : IEvent<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public string Title { get; private set; }
        [DataMember(Order = 3)] public string ItemNo { get; private set; }
        [DataMember(Order = 4)] public CurrencyAmount Price { get; private set; }
        [DataMember(Order = 5)] public decimal Stock { get; private set; }
        [DataMember(Order = 6)] public string UnitOfMeasure { get; private set; }
        [DataMember(Order = 7)] public VatRate VatRate { get; private set; }
        [DataMember(Order = 8)] public Barcode Barcode { get; private set; }
        [DataMember(Order = 9)] public DateTime Utc { get; private set; }
        
        ProductCreated () {}
        public ProductCreated (ProductId id, string title, string itemNo, CurrencyAmount price, decimal stock, string unitOfMeasure, VatRate vatRate, Barcode barcode, DateTime utc)
        {
            Id = id;
            Title = title;
            ItemNo = itemNo;
            Price = price;
            Stock = stock;
            UnitOfMeasure = unitOfMeasure;
            VatRate = vatRate;
            Barcode = barcode;
            Utc = utc;
        }
        
        public override string ToString()
        {
            return string.Format(@"Product {1} created, {0}, {2}, {3}, {4}, {5}, {6}, utc - {7}", Title, ItemNo, Price, Stock, UnitOfMeasure, VatRate, Barcode, Utc);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ChangeProductBarcode : ICommand<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public Barcode NewBarcode { get; private set; }
        
        ChangeProductBarcode () {}
        public ChangeProductBarcode (ProductId id, Barcode newBarcode)
        {
            Id = id;
            NewBarcode = newBarcode;
        }
        
        public override string ToString()
        {
            return string.Format(@"Change product barcode - {0}", NewBarcode);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ProductBarcodeChanged : IEvent<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public Barcode NewBarcode { get; private set; }
        [DataMember(Order = 3)] public DateTime Utc { get; private set; }
        
        ProductBarcodeChanged () {}
        public ProductBarcodeChanged (ProductId id, Barcode newBarcode, DateTime utc)
        {
            Id = id;
            NewBarcode = newBarcode;
            Utc = utc;
        }
        
        public override string ToString()
        {
            return string.Format(@"Product barcode changed on {0}, utc - {1}", NewBarcode, Utc);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ChangeProductItemNo : ICommand<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public string NewItemNo { get; private set; }
        
        ChangeProductItemNo () {}
        public ChangeProductItemNo (ProductId id, string newItemNo)
        {
            Id = id;
            NewItemNo = newItemNo;
        }
        
        public override string ToString()
        {
            return string.Format(@"Change product item no. - {0}", NewItemNo);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ProductItemNoChanged : IEvent<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public string NewItemNo { get; private set; }
        [DataMember(Order = 3)] public DateTime Utc { get; private set; }
        
        ProductItemNoChanged () {}
        public ProductItemNoChanged (ProductId id, string newItemNo, DateTime utc)
        {
            Id = id;
            NewItemNo = newItemNo;
            Utc = utc;
        }
        
        public override string ToString()
        {
            return string.Format(@"{0} item changed on {1}, utc - {2}", Id, NewItemNo, Utc);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class MakeProductOrderable : ICommand<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        
        MakeProductOrderable () {}
        public MakeProductOrderable (ProductId id)
        {
            Id = id;
        }
        
        public override string ToString()
        {
            return string.Format(@"Make {0} orderable", Id);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ProductMakedOrderable : IEvent<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public DateTime Utc { get; private set; }
        
        ProductMakedOrderable () {}
        public ProductMakedOrderable (ProductId id, DateTime utc)
        {
            Id = id;
            Utc = utc;
        }
        
        public override string ToString()
        {
            return string.Format(@"{0} maked orderable", Id);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class MakeProductNonOrderable : ICommand<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public string Reason { get; private set; }
        
        MakeProductNonOrderable () {}
        public MakeProductNonOrderable (ProductId id, string reason)
        {
            Id = id;
            Reason = reason;
        }
        
        public override string ToString()
        {
            return string.Format(@"Make {0} non-orderable, reason - {1}", Id, Reason);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ProductMakedNonOrderable : IEvent<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public string Reason { get; private set; }
        [DataMember(Order = 3)] public DateTime Utc { get; private set; }
        
        ProductMakedNonOrderable () {}
        public ProductMakedNonOrderable (ProductId id, string reason, DateTime utc)
        {
            Id = id;
            Reason = reason;
            Utc = utc;
        }
        
        public override string ToString()
        {
            return string.Format(@"{0} maked non-orderable, reason - {1}, utc - {2}", Id, Reason, Utc);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class UpdateProductStock : ICommand<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public decimal Quantity { get; private set; }
        [DataMember(Order = 3)] public string Reason { get; private set; }
        
        UpdateProductStock () {}
        public UpdateProductStock (ProductId id, decimal quantity, string reason)
        {
            Id = id;
            Quantity = quantity;
            Reason = reason;
        }
        
        public override string ToString()
        {
            return string.Format(@"update {0} stock {qunatity}, reason - {1}", Id, Reason);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ProductStockUpdated : IEvent<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public decimal Quantity { get; private set; }
        [DataMember(Order = 3)] public string Reason { get; private set; }
        [DataMember(Order = 4)] public DateTime Utc { get; private set; }
        
        ProductStockUpdated () {}
        public ProductStockUpdated (ProductId id, decimal quantity, string reason, DateTime utc)
        {
            Id = id;
            Quantity = quantity;
            Reason = reason;
            Utc = utc;
        }
        
        public override string ToString()
        {
            return string.Format(@"{0} stock updated {1}, reason - {2}, utc - {3}", Id, Quantity, Reason, Utc);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class RenameProduct : ICommand<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public string NewTitle { get; private set; }
        
        RenameProduct () {}
        public RenameProduct (ProductId id, string newTitle)
        {
            Id = id;
            NewTitle = newTitle;
        }
        
        public override string ToString()
        {
            return string.Format(@"rename {0}", Id);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ProductRenamed : IEvent<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public string NewTitle { get; private set; }
        [DataMember(Order = 3)] public DateTime Utc { get; private set; }
        
        ProductRenamed () {}
        public ProductRenamed (ProductId id, string newTitle, DateTime utc)
        {
            Id = id;
            NewTitle = newTitle;
            Utc = utc;
        }
        
        public override string ToString()
        {
            return string.Format(@"{0} renamed, utc - {1}", Id, Utc);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ChangeProductVatRate : ICommand<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public VatRate NewVatRate { get; private set; }
        
        ChangeProductVatRate () {}
        public ChangeProductVatRate (ProductId id, VatRate newVatRate)
        {
            Id = id;
            NewVatRate = newVatRate;
        }
        
        public override string ToString()
        {
            return string.Format(@"Change product vat rate - {0}", NewVatRate);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ProductVatRateChanged : IEvent<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public VatRate NewVatRate { get; private set; }
        [DataMember(Order = 3)] public DateTime Utc { get; private set; }
        
        ProductVatRateChanged () {}
        public ProductVatRateChanged (ProductId id, VatRate newVatRate, DateTime utc)
        {
            Id = id;
            NewVatRate = newVatRate;
            Utc = utc;
        }
        
        public override string ToString()
        {
            return string.Format(@"{0} item changed on {1}, utc - {2}", Id, NewVatRate, Utc);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ChangeProductUnitOfMeasure : ICommand<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public string NewUnitOfMeasure { get; private set; }
        
        ChangeProductUnitOfMeasure () {}
        public ChangeProductUnitOfMeasure (ProductId id, string newUnitOfMeasure)
        {
            Id = id;
            NewUnitOfMeasure = newUnitOfMeasure;
        }
        
        public override string ToString()
        {
            return string.Format(@"Change product unit of measure - {newItemNo}");
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ProductUnitOfMeasureChanged : IEvent<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public string NewUnitOfMeasure { get; private set; }
        [DataMember(Order = 3)] public DateTime Utc { get; private set; }
        
        ProductUnitOfMeasureChanged () {}
        public ProductUnitOfMeasureChanged (ProductId id, string newUnitOfMeasure, DateTime utc)
        {
            Id = id;
            NewUnitOfMeasure = newUnitOfMeasure;
            Utc = utc;
        }
        
        public override string ToString()
        {
            return string.Format(@"{0} item changed on {1}, utc - {2}", Id, NewUnitOfMeasure, Utc);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ChangeProductPrice : ICommand<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public CurrencyAmount NewPrice { get; private set; }
        
        ChangeProductPrice () {}
        public ChangeProductPrice (ProductId id, CurrencyAmount newPrice)
        {
            Id = id;
            NewPrice = newPrice;
        }
        
        public override string ToString()
        {
            return string.Format(@"Change product price - {0}", NewPrice);
        }
    }
    [DataContract(Namespace = "BookKeeping")]
public partial class ProductPriceChanged : IEvent<ProductId>
    {
        [DataMember(Order = 1)] public ProductId Id { get; private set; }
        [DataMember(Order = 2)] public CurrencyAmount NewPrice { get; private set; }
        [DataMember(Order = 3)] public DateTime Utc { get; private set; }
        
        ProductPriceChanged () {}
        public ProductPriceChanged (ProductId id, CurrencyAmount newPrice, DateTime utc)
        {
            Id = id;
            NewPrice = newPrice;
            Utc = utc;
        }
        
        public override string ToString()
        {
            return string.Format(@"{0} item changed on {1}, utc - {2}", Id, NewPrice, Utc);
        }
    }
    
    public interface IProductApplicationService
    {
        void When(CreateProduct c);
        void When(ChangeProductBarcode c);
        void When(ChangeProductItemNo c);
        void When(MakeProductOrderable c);
        void When(MakeProductNonOrderable c);
        void When(UpdateProductStock c);
        void When(RenameProduct c);
        void When(ChangeProductVatRate c);
        void When(ChangeProductUnitOfMeasure c);
        void When(ChangeProductPrice c);
    }
    
    public interface IProductState
    {
        void When(ProductCreated e);
        void When(ProductBarcodeChanged e);
        void When(ProductItemNoChanged e);
        void When(ProductMakedOrderable e);
        void When(ProductMakedNonOrderable e);
        void When(ProductStockUpdated e);
        void When(ProductRenamed e);
        void When(ProductVatRateChanged e);
        void When(ProductUnitOfMeasureChanged e);
        void When(ProductPriceChanged e);
    }
    #endregion
}
