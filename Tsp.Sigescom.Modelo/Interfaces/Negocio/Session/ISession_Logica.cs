using Tsp.Sigescom.Modelo.ClasesNegocio.Core.TipoCambio;
using Tsp.Sigescom.Modelo.Entidades.Sesion;

namespace Tsp.Sigescom.Modelo.Interfaces.Logica
{
    public interface ISession_Logica
    {
        UserProfileSessionData GenerarSesionUsuario(string userId, string userName);
        UserProfileSessionData GenerarSesionUsuario();
        UserProfileSessionData ResolverSession(UserProfileSessionData profileData, TipoCambio tipoCambio, int idCentroDeAtencionInicioSesion);


    }
}

