using App.Repository.Products;

namespace App.Repository.Categories;

public class Category
{
    public int Id { get; set; }
    
    public string Name { get; set; }  

    /// <summary>
    /// Null geçilebilir işaretledik, çünkü ilk defa kategori tanımını yapıyoruz bu durumda ürün olmayabilir.
    /// </summary>
    public List<Product>? Products { get; set; }
}