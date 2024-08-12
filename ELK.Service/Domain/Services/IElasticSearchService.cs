namespace ELK.Service.Domain.Services;

public interface IElasticSearchService<T>
{
    Task<bool> CreateDocumentAsync(T document);
    Task<T> GetDocumentByIdAsync(int id);
    Task<IEnumerable<T>> GetAllDocumentsAsync(string keyword);
    Task<bool> UpdateDocumentAsync(T document);
    Task<bool> DeleteDocumentAsync(int id);
}