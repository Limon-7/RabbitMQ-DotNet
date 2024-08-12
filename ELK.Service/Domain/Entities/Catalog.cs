namespace ELK.Service.Domain.Entities;

public class Catalog
{
    public Catalog(int catalogId, string name, string brandName, int price)
    {
        CatalogId = catalogId;
        Name = name;
        BrandName = brandName;
        Price = price;
    }

    public int CatalogId { get; set; }
    public string Name { get; set; }
    public string BrandName { get; set; }
    public int Price { get; set; }
}