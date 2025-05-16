using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using ScriptumLux.BLL.DTOs.Movie;

namespace ScriptumLux.BLL.AiExternal;

public class VectorizedMovie
{
    [VectorStoreRecordData]
    public string Title { get; set; }
    
    [VectorStoreRecordData]
    public string Genre{get; set;}
    
    [VectorStoreRecordData]
    public string Description { get; set; }
    
    [VectorStoreRecordVector(Dimensions: 4096, DistanceFunction = DistanceFunction.CosineSimilarity)]
    public Embedding<float> Vector { get; set; }


    
    [VectorStoreRecordKey]
    public int MovieId { get; set; }

    public VectorizedMovie(MovieDto dto)
    {
        Title = dto.Title;
        Description = dto.Description;
        
        MovieId = dto.MovieId;
    }
    public VectorizedMovie()
    {
    }
    
    
}