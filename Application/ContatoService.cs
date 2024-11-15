using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VimaV2.DTOs;
using VimaV2.Models;
using VimaV2.Repositories;

namespace VimaV2.Services
{
    public class ContatoService
    {
        private readonly ContatoRepository _contatoRepository;

        public ContatoService(ContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        //[GET ALL]
        public List<ContatoDTO> getAll()
        {
            var contatos = _contatoRepository.GetAll();

            var contatoDTOs = contatos.Select(c => new ContatoDTO
            {
                Id = c.Id,
                Name = c.Name,
                Sobrenome = c.Sobrenome,
                Email = c.Email,
                Assunto = c.Assunto,
                Description = c.Description

            }).ToList();

            return contatoDTOs;
        }

        //[POST]
        public async Task<ContatoDTO> CreateContatoAsync(ContatoDTO contatoDTO)
        {
            var contato = new Contato
            {
                Name = contatoDTO.Name,
                Sobrenome = contatoDTO.Sobrenome,
                Email = contatoDTO.Email,
                Assunto= contatoDTO.Assunto,
                Description = contatoDTO.Description
            };

            var contatoCriado = await _contatoRepository.AddAsync(contato);

            return new ContatoDTO
            {
                Id = contatoCriado.Id,
                Name = contatoCriado.Name,
                Sobrenome = contatoCriado.Sobrenome,
                Email = contatoCriado.Email,
                Assunto = contatoCriado.Assunto,
                Description = contatoCriado.Description
            };


        }

        internal async Task GetContatoByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
