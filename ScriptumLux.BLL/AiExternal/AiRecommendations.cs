using Microsoft.Extensions.VectorData;
using ScriptumLux.BLL.DTOs.Movie;

namespace ScriptumLux.BLL.AiExternal;

public class AiRecommendations: AiBase
{
    public AiRecommendations(List<VectorizedMovie> itemList, Uri uri, string model)
        : base(itemList, uri, model)
    {
        
    }

    public async Task<List<VectorizedMovie>> GetSimilarItems(VectorizedMovie movie, uint maxResults)
    {
        await GenerateEmbeddings(new List<Func<VectorizedMovie, string>>
        {
            vectorizedMovie => vectorizedMovie.Title,
            //vectorizedMovie => vectorizedMovie.Genre, // не используешь
            vectorizedMovie => vectorizedMovie.Description
        });

        VectorizedMovie? targetMovie = ItemList.Find(m => m.MovieId == movie.MovieId);

        if (targetMovie == null)
        {
            return new List<VectorizedMovie>();
        }

        var searchOptions = new VectorSearchOptions<VectorizedMovie>
        {
            VectorPropertyName = nameof(VectorizedMovie.Vector),
            Skip = 0
        };

        IAsyncEnumerable<VectorSearchResult<VectorizedMovie>> results =
            Items.VectorizedSearchAsync(
                targetMovie.Vector, // здесь Embedding
                top: (int)maxResults,
                options: searchOptions);

        List<VectorizedMovie> similarMovies = new();

        await foreach (VectorSearchResult<VectorizedMovie> result in results)
        {
            if (result.Record.MovieId != targetMovie.MovieId)
            {
                similarMovies.Add(result.Record);
            }
        }

        return similarMovies;
    }
  
    
}