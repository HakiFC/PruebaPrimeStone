using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Common.Data;
using CRUD.Models;
using System.Web.Http.Description;
using Common.Models;

namespace CRUD.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly PruebaDBContext _context;

        public EstudiantesController(PruebaDBContext context)
        {
            _context = context;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            dynamic estudiantesDTO = await _context.Estudiante.Select(x =>
                        new EstudianteDTO()
                        {
                            Id = x.Id,
                            Nombres = x.Nombres,
                            Apellidos = x.Apellidos,
                            FechaNacimento = x.FechaNacimento,
                            Genero = x.Genero
                        }).ToListAsync();

            return View(estudiantesDTO);
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            dynamic estudianteDTO = await _context.Estudiante.Select(x =>
                new EstudianteDTO()
                {
                    Id = x.Id,
                    Nombres = x.Nombres,
                    Apellidos = x.Apellidos,
                    FechaNacimento = x.FechaNacimento,
                    Genero = x.Genero
                }).FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteDTO == null)
            {
                return NotFound();
            }

            return View(estudianteDTO);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombres,Apellidos,FechaNacimento,Genero")] EstudianteDTO estudianteDTO)
        {
            if (ModelState.IsValid)
            {
                dynamic estudiante = new Estudiante();

                estudiante.Nombres = estudianteDTO.Nombres;
                estudiante.Apellidos = estudianteDTO.Apellidos;
                estudiante.Genero = estudianteDTO.Genero;
                estudiante.FechaNacimento = estudianteDTO.FechaNacimento;
                estudiante.FechaCreacion = DateTime.Today;

                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estudianteDTO);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            dynamic estudianteDTO = await _context.Estudiante.Select(x =>
                new EstudianteDTO()
                {
                    Id = x.Id,
                    Nombres = x.Nombres,
                    Apellidos = x.Apellidos,
                    FechaNacimento = x.FechaNacimento,
                    Genero = x.Genero
                }).FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteDTO == null)
            {
                return NotFound();
            }
            return View(estudianteDTO);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,Apellidos,FechaNacimento,Genero")] EstudianteDTO estudianteDTO)
        {
            if (id != estudianteDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                dynamic estudiante = new Estudiante();

                estudiante.Id = estudianteDTO.Id;
                estudiante.Nombres = estudianteDTO.Nombres;
                estudiante.Apellidos = estudianteDTO.Apellidos;
                estudiante.Genero = estudianteDTO.Genero;
                estudiante.FechaNacimento = estudianteDTO.FechaNacimento;
                estudiante.FechaActualizacion = DateTime.Today;

                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.Id))
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
            return View(estudianteDTO);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            dynamic estudianteDTO = await _context.Estudiante.Select(x =>
                new EstudianteDTO()
                {
                    Id = x.Id,
                    Nombres = x.Nombres,
                    Apellidos = x.Apellidos,
                    FechaNacimento = x.FechaNacimento,
                    Genero = x.Genero
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudianteDTO == null)
            {
                return NotFound();
            }

            return View(estudianteDTO);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            dynamic estudiante = new Estudiante();

            estudiante.Id = id;
            estudiante.FechaBorrado = DateTime.Today;
            estudiante.EstaBorrado = true;

            _context.Estudiante.Update(estudiante);
            await _context.SaveChangesAsync();

            await _context.Database.ExecuteSqlRawAsync("delete from Estudiante where id =" + id);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiante.Any(e => e.Id == id);
        }
    }
}
