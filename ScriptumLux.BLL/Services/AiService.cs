using ScriptumLux.BLL.Ai;
using ScriptumLux.BLL.Interfaces;

namespace ScriptumLux.BLL.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


    public class AiService : IAiService
    {
        private readonly AiQueryRecommendations _queryRecommender;
        private readonly AiRecommendations _similarRecommender;
        private readonly IMovieService _movieService;

        public AiService(IConfiguration configuration, IMovieService movieService)
        {
            _movieService = movieService;

            //Uri aiUri = new Uri("http://192.168.62.86:44444");
            Uri aiUri = new Uri("http://127.0.0.1:11434");

            string model = "nomic-embed-text:v1.5";

            List<VectorizedMovie> movies = GetVectorizedMovies().Result;

            _queryRecommender = new AiQueryRecommendations(movies, aiUri, model);
            _similarRecommender = new AiRecommendations(movies, aiUri, model);
        }

        private async Task<List<VectorizedMovie>> GetVectorizedMovies()
        {
            var movies = await _movieService.GetAllAsync();
            List<VectorizedMovie> vectorizedMovies = new();

            foreach (var movie in movies)
            {
                vectorizedMovies.Add(new VectorizedMovie(movie));
            }

            return vectorizedMovies;
        }
        public async Task<List<VectorizedMovie>> GetRecommendedMovies(string query, uint maxResults)
        {
            return await _queryRecommender.GetRecommendationsByQuery(query, maxResults);
        }

        public async Task<List<VectorizedMovie>> GetSimilarMovies(int movieId, uint maxResults)
        {
            var movieDto = await _movieService.GetByIdAsync(movieId);
            if (movieDto == null)
                return new List<VectorizedMovie>();

            var vectorizedMovie = new VectorizedMovie(movieDto);
            var similarMovies = await _similarRecommender.GetSimilarItems(vectorizedMovie, maxResults);

            foreach (var movie in similarMovies)
            {
                var movieDtoSimilar = await _movieService.GetByIdAsync(movieId);
            }

            return similarMovies;
        }

    }
