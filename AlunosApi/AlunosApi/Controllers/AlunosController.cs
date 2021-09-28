using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                //return BadRequest("Request inválido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos.");
            }
        }

        [HttpGet("AlunosPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByName([FromQuery] string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByName(nome);
                if (alunos.Count() == 0) return NotFound($"Não existem alunos com o critério: {nome}.");
                return Ok(alunos);
            }
            catch
            {
                //return BadRequest("Request inválido");
                 return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos.");
            }
        }

        [HttpGet("{id:int}", Name="GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if (aluno == null) return NotFound($"Não existe aluno para o código {id}.");
                return Ok(aluno);
            }
            catch
            {
                //return BadRequest("Request inválido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter aluno.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> CreateAluno(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id}, aluno);
            }
            catch
            {
                //return BadRequest("Request inválido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar aluno.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Aluno>> UpdateAluno(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if (aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return Ok($"Aluno de Código {id} atualizado com sucesso.");
                }
                else
                {
                    return BadRequest("Código do aluno não encontrado.");
                }
            }
            catch
            {
                //return BadRequest("Request inválido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao alterar aluno.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Aluno>> DeleteAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if(aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"Aluno de Código {id} excluído com sucesso.");
                }
                else
                {
                    return NotFound($"Aluno de Código {id} não encontrado.");
                }
            }
            catch
            {
                //return BadRequest("Request inválido");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar aluno.");
            }
        }
    }
}
