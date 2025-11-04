using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _db;

    public CategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public Category Create(Category obj)
    {
        _db.Categories.Add(obj);
        _db.SaveChanges();
        return obj;
    }

    public bool Delete(int id)
    {
        var obj = _db.Categories.FirstOrDefault(x => x.Id == id);
        if (obj != null)
        {
            _db.Categories.Remove(obj);
             return _db.SaveChanges() > 0;
        }
        return false;
    }

    public Category Get(int id)
    {
        var obj = _db.Categories.FirstOrDefault(x => x.Id == id);
        if (obj == null)
        {
            return new Category();
        }
        return obj;
    }

    public IEnumerable<Category> GetAll()
    {
        return [.. _db.Categories];
    }

    public Category Update(Category obj)
    {
        var objFromDb = _db.Categories.FirstOrDefault(x => x.Id == obj.Id);
        if (objFromDb is not null)
        {
            objFromDb.Name = obj.Name;
            _db.Categories.Update(objFromDb);
            _db.SaveChanges();
            return objFromDb;
        }
        return obj;
    }

    public async Task<Category> CreateAsync(Category obj)
    {
        await _db.Categories.AddAsync(obj);
        await _db.SaveChangesAsync();
        return obj;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var obj = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (obj != null)
        {
            _db.Categories.Remove(obj);
             return (await _db.SaveChangesAsync()) > 0;
        }
        return false;
    }

    public async Task<Category> GetAsync(int id)
    {
        var obj = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (obj == null)
        {
            return new Category();
        }
        return obj;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _db.Categories.ToListAsync();
    }

    public async Task<Category> UpdateAsync(Category obj)
    {
        var objFromDb = await _db.Categories.FirstOrDefaultAsync(x => x.Id == obj.Id);
        if (objFromDb is not null)
        {
            objFromDb.Name = obj.Name;
            _db.Categories.Update(objFromDb);
            await _db.SaveChangesAsync();
            return objFromDb;
        }
        return obj;
    }
}
