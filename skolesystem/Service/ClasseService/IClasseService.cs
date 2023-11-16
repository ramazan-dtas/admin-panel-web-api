using System;
using skolesystem.DTOs.Classe.Request;
using skolesystem.DTOs.Classe.Response;

namespace skolesystem.Service.ClasseService
{
	public interface IClasseService
	{
        Task<List<ClasseResponse>> GetAll();
        Task<ClasseResponse> GetById(int KlasseId);
        Task<ClasseResponse> Create(NewClasse newKlasse);
        Task<ClasseResponse> Update(int KlasseId, UpdateClasse updateKlasse);
        Task<bool> Delete(int KlasseId);
    }
}

