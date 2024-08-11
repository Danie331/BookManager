using AutoMapper;
using BookManager.CQRS.Queries.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Dto = BookManager.DtoModels;
using Domain = DomainModels;
using BookManager.CQRS.Commands.UseCases;

namespace BookManager.Controllers
{
    [ApiController, Route("api/[controller]")]
    [Produces("application/json")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public BooksController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of books as a paginated response
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Successfully return list of books</response>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        [HttpGet]
        [Produces(typeof(Dto.PaginatedList<Dto.Book>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int? pageIndex = null, int? pageSize = null)
        {
            var data = await _mediator.Send(new GetBooksQuery(pageIndex, pageSize));
            var response = _mapper.Map<Dto.PaginatedList<Dto.Book>>(data);
            return Ok(response);
        }

        /// <summary>
        /// Gets a specific book by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns the book with matching ID</response>
        /// <response code="404">Item with specified ID not found</response>
        [HttpGet("{id}")]
        [Produces(typeof(Dto.Book))]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _mediator.Send(new GetBookQuery(id));
            var response = _mapper.Map<Dto.Book>(data);
            return Ok(response);
        }

        /// <summary>
        /// Adds a new book to the collection
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <response code="200">Book successfully added</response>
        /// <response code="400">One or more validation errors occurred</response>
        [HttpPost]
        [Consumes(typeof(Dto.Book), "application/json")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Dto.Book book)
        {
            var data = _mapper.Map<Domain.Book>(book);
            await _mediator.Send(new AddBookCommand(data));
            return Ok();
        }

        /// <summary>
        /// Updates an existing book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <response code="200">Book successfully updated</response>
        /// <response code="400">One or more validation errors occurred</response>
        /// <response code="404">Item with specified ID not found</response>
        [HttpPut("{id}")]
        [Consumes(typeof(Dto.Book), "application/json")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Dto.Book book)
        {
            var data = _mapper.Map<Domain.Book>(book);
            await _mediator.Send(new UpdateBookCommand(id, data));
            return Ok();
        }

        /// <summary>
        /// Deletes a book with specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Book successfully deleted</response>
        /// <response code="404">Item with specified ID not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteBookCommand(id));
            return Ok();
        }

        /// <summary>
        /// Gets books matching filter criteria
        /// </summary>
        /// <param name="filterDto">Search filter on author and title</param>
        /// <returns></returns>
        [HttpGet("search")]
        //[Consumes(typeof(Dto.SearchFilter), "application/json")]
        [Produces(typeof(List<Dto.Book>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery]Dto.SearchFilter filterDto)
        {
            var filter = _mapper.Map<Domain.SearchFilter>(filterDto);
            var data = await _mediator.Send(new GetBooksFilterQuery(filter));
            var response = _mapper.Map<List<Dto.Book>>(data);
            return Ok(response);
        }
    }
}
