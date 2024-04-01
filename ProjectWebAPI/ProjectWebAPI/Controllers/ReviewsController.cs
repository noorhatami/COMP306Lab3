using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Models;
using AutoMapper;
using ProjectWebAPI.Services;
using ProjectWebAPI.DTOs;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewsRepository;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewRepository reviewsRepository, IMapper mapper)
        {
            _reviewsRepository = reviewsRepository;
            _mapper = mapper;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewsCreateDTO>>> GetReviews()
        {
            var reviews = await _reviewsRepository.GetReviewsAsync();
            var reviewsViewModel = _mapper.Map<IEnumerable<ReviewsCreateDTO>>(reviews);
            return Ok(reviewsViewModel);
        }
      
        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewsCreateDTO>> GetReview(string id)
        {
            var review = await _reviewsRepository.GetReviewAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReviews(string id, ReviewsUpdateDTO reviewsUpdateDTO)
        {
            await _reviewsRepository.UpdateAsync(id, _mapper.Map<Reviews>(reviewsUpdateDTO));
            return NoContent();
        }

        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<ReviewsCreateDTO>> PostReviews(ReviewsCreateDTO reviewsCreateDTO)
        {
            var review = _mapper.Map<Reviews>(reviewsCreateDTO);
            await _reviewsRepository.AddAsync(review);
            return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, reviewsCreateDTO);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviews(string id)
        {
            await _reviewsRepository.DeleteAsync(id);
            return NoContent();
        }

        // PATCH: api/Reviews/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<ReviewsInfoDTO>> PatchReviews(string id, ReviewsUpdateDTO reviewsUpdateDTO)
        {
            var review = await _reviewsRepository.GetReviewAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _mapper.Map(reviewsUpdateDTO, review);
            await _reviewsRepository.UpdateAsync(id, review);

            return Ok(_mapper.Map<ReviewsInfoDTO>(review));
        }
    }
}
