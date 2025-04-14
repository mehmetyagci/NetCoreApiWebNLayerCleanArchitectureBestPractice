using App.Domain.Entities.Common;

namespace App.Domain.Entities;

public class Category : BaseEntity<int>, IAuditEntity
{
    public string Name { get; set; }   = default!;

    /// <summary>
    /// Null geçilebilir işaretledik, çünkü ilk defa kategori tanımını yapıyoruz bu durumda ürün olmayabilir.
    /// </summary>
    public List<Product>? Products { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}