using Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.controller;

[Route("api/[controller]/[Action]")]
[ApiController]
public class StoreController : ControllerBase
{
    protected readonly AppDbContext dbContext;
    public StoreController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}
