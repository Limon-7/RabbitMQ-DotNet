using ELK.Service.Domain.Services;
using Nest;

namespace ELK.Service.Infrastructure.Services;

public class ElasticSearchService<T> : IElasticSearchService<T> where T : class
{
    private readonly ElasticClient _elasticClient;

    public ElasticSearchService(ElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<bool> CreateDocumentAsync(T document)
    {
        try
        {
            var response = await _elasticClient.IndexDocumentAsync(document);
            return response.IsValid;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<T> GetDocumentByIdAsync(int id)
    {
        var response = await _elasticClient.GetAsync(new DocumentPath<T>(id));
        return response.Source;
    }

    public async Task<IEnumerable<T>> GetAllDocumentsAsync(string keyword)
    {
        var response = await _elasticClient.SearchAsync<T>(s =>
            s.Query(q => q.QueryString(d => d.Query('*' + keyword + '*')))
                .Size(10000));
        return response.Documents;
    }

    public async Task<bool> UpdateDocumentAsync(T document)
    {
        var response = await _elasticClient.UpdateAsync(new DocumentPath<T>(document), u =>
            u.Doc((document)).RetryOnConflict(3));
        return response.IsValid;
    }

    public async Task<bool> DeleteDocumentAsync(int id)
    {
        try
        {
            var response = await _elasticClient.DeleteAsync(new DocumentPath<T>(id));
            return response.IsValid;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}