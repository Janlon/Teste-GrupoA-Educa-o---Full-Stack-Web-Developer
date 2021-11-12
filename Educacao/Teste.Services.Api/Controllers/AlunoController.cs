using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Teste.Application.DTOs;
using Teste.Application.Interfaces;
using Teste.Domain.Entities;

namespace Teste.Services.Api.Controllers
{
    public class AlunoController : BaseController<Aluno, AlunoDTO>
    {
        public AlunoController(IAlunoApp app, ILogger<AlunoController> logger) : base(app, logger)
        {
            
        }

        // public override IActionResult Create([FromBody] AlunoDTO alunoDTO)
        // {
        //     try
        //     {
                
        //         // bool existe = app.FindCpf(alunoDTO.CPF);
        //         // if(existe)
        //         //     return BadRequest("Já existe esse CPF registrado no sistema");

        //         // existe = app.FindRa(alunoDTO.RA);
        //         // if(existe)
        //         //     return BadRequest("Já existe esse RA registrado no sistema");  

        //         return new OkObjectResult(app.Create(alunoDTO));
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }


        // public override IActionResult Edit([FromBody] AlunoDTO alunoDTO)
        // {
        //     try
        //     {
        //         bool existe = app.FindCpf(alunoDTO.CPF);
        //         if(existe)
        //             return BadRequest("Já existe esse CPF registrado no sistema");

        //         existe = app.FindRa(alunoDTO.RA);
        //         if(existe)
        //             return BadRequest("Já existe esse RA registrado no sistema");  

        //         app.Edit(alunoDTO);
        //         return new OkObjectResult(true);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }
    }
}
