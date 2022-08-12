using Entities;
namespace IRepository
{
    public interface IRepositoryManager
    {
        IProductRepository Product{ get; }

        //Any other repository can come here
        void Save();
    }
}