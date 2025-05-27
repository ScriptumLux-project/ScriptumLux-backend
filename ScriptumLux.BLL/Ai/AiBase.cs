using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.InMemory;

namespace ScriptumLux.BLL.Ai;

public abstract class AiBase
{
    protected readonly List<VectorizedMovie> ItemList;
    protected readonly IVectorStoreRecordCollection<int, VectorizedMovie> Items;
    protected readonly IEmbeddingGenerator<string, Embedding<float>> Generator;

    protected AiBase(List<VectorizedMovie> itemList, Uri uri, string model)
    {
        ItemList = itemList;

        Generator = new OllamaEmbeddingGenerator(uri, model);
        InMemoryVectorStore vectorStore = new();
        Items = vectorStore.GetCollection<int, VectorizedMovie>("movies");
    }

    protected async Task GenerateEmbeddings(List<Func<VectorizedMovie, string>> propertySelectors)
    {
        await Items.CreateCollectionIfNotExistsAsync();
        
        foreach (VectorizedMovie movie in ItemList)
        {
            List<string> combinedTexts = propertySelectors.Select(selector => selector(movie)).ToList();
            string combinedText = string.Join(" ", combinedTexts);
        
            movie.Vector = await Generator.GenerateEmbeddingVectorAsync(combinedText);
            await Items.UpsertAsync(movie);
        }
    }
}