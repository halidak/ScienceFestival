using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ScienceFestival.Async.DTOs;
using ScienceFestival.Async.Models;
using ScienceFestival.Async.Persistance;
using System.IdentityModel.Tokens.Jwt;

namespace ScienceFestival.Async.Services
{
    public class ReviewService
    {
        private readonly AppDbContext context;
        private readonly IMessageBroker messageBroker;

        public ReviewService(AppDbContext context, IMessageBroker messageBroker)
        {
            this.context = context;
            this.messageBroker = messageBroker;
        }

        public async Task<Review> CreateReview(ReviewDTO reviewDTO)
        {
            var review = new Review
            {
                ShowId = reviewDTO.ShowId,
                JuryId = reviewDTO.JuryId,
                Rating = reviewDTO.Rating,
                Comment = reviewDTO.Comment
            };

            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();

            if (review.Rating > 0 &&
                context.Reviews.Where(r => r.ShowId == review.ShowId && r.Rating > 0 && r.JuryId != review.JuryId).Count() > 0)
            {
                messageBroker.Publish(review);
            }
            return review;

        }

        public async Task<List<Review>> GetAllReviews()
        {
            return await context.Reviews.ToListAsync();
        }

        //get reviews from a specific jury
        public async Task<List<Review>> GetReviewsForJury(string juryId)
        {
            return await context.Reviews.Where(r => r.JuryId == juryId).ToListAsync();
        }

        public async Task<List<Review>> GetReviewsForShow(string showId)
        {
            return await context.Reviews.Where(r => r.ShowId == showId).ToListAsync();
        }

       
    }
}
