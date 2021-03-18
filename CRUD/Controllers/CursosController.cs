using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Common.Data;
using Common.Models;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class CursosController : Controller
    {
        private readonly PruebaDBContext _context;

        public CursosController(PruebaDBContext context)
        {
            _context = context;
        }

        // GET: Cursos
        public async Task<IActionResult> Index()
        {
            dynamic cursosDTo = await _context.Curso.Select(x =>
                new CursoDTO()
                {
                    Id = x.Id,
                    CodigoCurso = x.CodigoCurso,
                    NombreCurso = x.NombreCurso,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin
                }).ToListAsync();

            return View(cursosDTo);
        }

        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            dynamic cursoDTO = await _context.Curso.Select(x =>
                new CursoDTO()
                {
                    Id = x.Id,
                    CodigoCurso = x.CodigoCurso,
                    NombreCurso = x.NombreCurso,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin
                }).FirstOrDefaultAsync(m => m.Id == id);
            if (cursoDTO == null)
            {
                return NotFound();
            }

            return View(cursoDTO);
        }

        // GET: Cursos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoCurso,NombreCurso,FechaInicio,FechaFin")] CursoDTO cursoDTO)
        {
            if (ModelState.IsValid)
            {
                dynamic curso = new Curso();

                curso.CodigoCurso = cursoDTO.CodigoCurso;
                curso.NombreCurso = cursoDTO.NombreCurso;
                curso.FechaInicio = cursoDTO.FechaInicio;
                curso.FechaFin = cursoDTO.FechaFin;
                curso.FechaCreacion = DateTime.Today;

                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cursoDTO);
        }

        // GET: Cursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            dynamic cursoDTO = await _context.Curso.Select(x =>
                new CursoDTO()
                {
                    Id = x.Id,
                    CodigoCurso = x.CodigoCurso,
                    NombreCurso = x.NombreCurso,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin
                }).FirstOrDefaultAsync(m => m.Id == id);
            if (cursoDTO == null)
            {
                return NotFound();
            }
            return View(cursoDTO);
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoCurso,NombreCurso,FechaInicio,FechaFin")] CursoDTO cursoDTO)
        {
            if (id != cursoDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dynamic curso = new Curso();

                    curso.Id = cursoDTO.Id;
                    curso.CodigoCurso = cursoDTO.CodigoCurso;
                    curso.NombreCurso = cursoDTO.NombreCurso;
                    curso.FechaInicio = cursoDTO.FechaInicio;
                    curso.FechaFin = cursoDTO.FechaFin;
                    curso.FechaActualizacion = DateTime.Today;

                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(cursoDTO.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cursoDTO);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cursoDTO = await _context.Curso.Select(x =>
                new CursoDTO()
                {
                    Id = x.Id,
                    CodigoCurso = x.CodigoCurso,
                    NombreCurso = x.NombreCurso,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursoDTO == null)
            {
                return NotFound();
            }

            return View(cursoDTO);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            dynamic curso = new Curso();

            curso.Id = id;
            curso.EstaBorrado = true;
            curso.FechaActualizacion = DateTime.Today;

            _context.Curso.Update(curso);
            await _context.SaveChangesAsync();

            await _context.Database.ExecuteSqlRawAsync("delete from Curso where id =" + id);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
            return _context.Curso.Any(e => e.Id == id);
        }
    }
}
