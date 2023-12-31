using api.models.client;
using api.repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers;

[ApiController]
[Route("api/v1/game")]
public class BoardController : ControllerBase
{
    private readonly GameRepository _repo;

    public BoardController(GameRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("startGame")]
    [SwaggerOperation(
        Summary = "Start new game",
        Description = "Builds new board and returns game ID"
    )]
    public GameStart StartGame(bool isWhite)
    {
        return _repo.StartGame(isWhite);
    }

    [HttpGet("{gameID}/compMove")]
    [SwaggerOperation(
        Summary = "Gets computer move",
        Description = "Runs a MinMax algorithm on the board and returns the best move"
    )]
    public BoardDisplay CompMove(Guid gameID)
    {
        return _repo.CompMove(gameID);
    }

    [HttpPost("{gameID}/click")]
    [SwaggerOperation(
        Summary = "HandlesClick",
        Description = "Handles board click and moves piece if applicable, returns board."
    )]
    public ClickReturn HandleClick(Guid gameID, [FromQuery] int row, [FromQuery] int col)
    {
        return _repo.HandleClick(gameID, row, col);
    }

    [HttpPost("{gameID}/ping")]
    [SwaggerOperation(
        Summary = "Pings Server to Keep Game Alive",
        Description = "Pings Server to Keep Game Alive"
    )]
    public bool Ping(Guid gameID)
    {
        return _repo.Ping(gameID);
    }
}
