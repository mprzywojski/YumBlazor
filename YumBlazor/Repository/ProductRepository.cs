using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment? _webHostEnvironment;
    public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }

    public Product Create(Product obj)
    {
        _db.Products.Add(obj);
        _db.SaveChanges();
        return obj;
    }

    public bool Delete(int id)
    {
        var obj = _db.Products.FirstOrDefault(x => x.Id == id);
        if (obj != null)
        {
            _db.Products.Remove(obj);
             return _db.SaveChanges() > 0;
        }
        return false;
    }

    public Product Get(int id)
    {
        var obj = _db.Products.FirstOrDefault(x => x.Id == id);
        if (obj == null)
        {
            return new Product();
        }
        return obj;
    }

    public IEnumerable<Product> GetAll()
    {
        return [.. _db.Products];
    }

    public Product Update(Product obj)
    {
        var objFromDb = _db.Products.FirstOrDefault(x => x.Id == obj.Id);
        if (objFromDb is not null)
        {
            objFromDb.Name = obj.Name;
            objFromDb.Description = obj.Description;
            objFromDb.ImageUrl = obj.ImageUrl;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.Price = obj.Price;
            _db.Products.Update(objFromDb);
            _db.SaveChanges();
            return objFromDb;
        }
        return obj;
    }

    public async Task<Product> CreateAsync(Product obj)
    {
        await _db.Products.AddAsync(obj);
        await _db.SaveChangesAsync();
        return obj;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var obj = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('/'));
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
        if (obj != null)
        {
            _db.Products.Remove(obj);
             return (await _db.SaveChangesAsync()) > 0;
        }
        return false;
    }

    public async Task<Product> GetAsync(int id)
    {
        var obj = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (obj == null)
        {
            return new Product();
        }
        return obj;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _db.Products.Include(u => u.Category).ToListAsync();
    }

    public async Task<Product> UpdateAsync(Product obj)
    {
        var objFromDb = await _db.Products.FirstOrDefaultAsync(x => x.Id == obj.Id);
        if (objFromDb is not null)
        {
            objFromDb.Name = obj.Name;
            objFromDb.Description = obj.Description;
            objFromDb.ImageUrl = obj.ImageUrl;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.Price = obj.Price;
            _db.Products.Update(objFromDb);
            await _db.SaveChangesAsync();
            return objFromDb;
        }
        return obj;
    }
}
