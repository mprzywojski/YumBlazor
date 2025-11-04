using YumBlazor.Data;

namespace YumBlazor.Repository.IRepository;

public interface ICategoryRepository
{
    public Category Create(Category obj);
    public Category Update(Category obj);
    public bool Delete(int id);
    public Category Get(int id);
    public IEnumerable<Category> GetAll();

    public Task<Category> CreateAsync(Category obj);
    public Task<Category> UpdateAsync(Category obj);
    public Task<bool> DeleteAsync(int id);
    public Task<Category> GetAsync(int id);
    public Task<IEnumerable<Category>> GetAllAsync();
}
