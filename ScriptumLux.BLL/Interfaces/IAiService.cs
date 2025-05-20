using ScriptumLux.BLL.Ai;

namespace ScriptumLux.BLL.Interfaces;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAiService
{
    //Task<List<VectorizedMovie>> GetVectorizedMovies();
    Task<List<VectorizedMovie>> GetRecommendedMovies(string query, uint maxResults);
    Task<List<VectorizedMovie>> GetSimilarMovies(int movieId, uint maxResults);
}