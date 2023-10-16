using System;
using GeoApi.Domain.Entities;

namespace GeoApi.Domain.Models
{
    public enum RankingType
    {
        top_ranking,
        user_ranking,
        iteration_ranking,
        killmeplease_ranking
    }

    public class RankingModel
	{
        public int Value { get; set; }
        public string Title { get; set; }
        public RankingType Type { get; set; }
        public Survey Survey { get; set; }
    }
}

