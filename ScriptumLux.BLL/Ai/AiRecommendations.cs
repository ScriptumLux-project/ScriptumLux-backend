using Microsoft.Extensions.VectorData;

namespace ScriptumLux.BLL.Ai;

public class AiRecommendations : AiBase
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
            //vectorizedMovie => vectorizedMovie.Genre,
            vectorizedMovie => vectorizedMovie.Description
        });
        
        VectorizedMovie? targetMovie = ItemList.Find(m => m.MovieId == movie.MovieId);

        VectorSearchOptions searchOptions = new()
        {
            Top = Convert.ToInt32(maxResults), 
            VectorPropertyName = "Vector"
        };

        if (targetMovie != null)
        {
            VectorSearchResults<VectorizedMovie> results = await Items.VectorizedSearchAsync(targetMovie.Vector, searchOptions);
        
            List<VectorizedMovie> similarMovies = new();

            await foreach (VectorSearchResult<VectorizedMovie> result in results.Results)
            {
                if (result.Record != targetMovie)
                {
                    similarMovies.Add(result.Record);
                }
            }
        
            return similarMovies;
        }
        else
        {
            return new List<VectorizedMovie>();
        }
        
    }
    
}