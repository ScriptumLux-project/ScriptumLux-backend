using Microsoft.Extensions.VectorData;
using ScriptumLux.BLL.DTOs.Movie;

namespace ScriptumLux.BLL.Ai;

public class VectorizedMovie
{
    [VectorStoreRecordData]
    public string Title { get; set; }

    [VectorStoreRecordData]
    public string Description { get; set; }

    [VectorStoreRecordVector(4096, DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float> Vector { get; set; }

    [VectorStoreRecordKey]
    public int MovieId { get; set; } 
    

    public VectorizedMovie(MovieDto dto)
    {
        MovieId = dto.MovieId;
        Title = dto.Title;
        Description = dto.Description;
    }
}