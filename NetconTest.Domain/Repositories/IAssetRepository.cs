using NetconTest.Domain.Entities;

namespace NetconTest.Domain.Repositories
{
    public interface IAssetRepository
    {
        List<Asset> GetAll();
    }
}
