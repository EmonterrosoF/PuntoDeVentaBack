using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace PuntoDeVenta.Common;

public class JwtGeneradorToken : IJwtGeneratorToken
{
    private readonly JwtAjustes _jwtAjustes;

    public JwtGeneradorToken(IOptions<JwtAjustes> jwtOpciones)
    {
        _jwtAjustes = jwtOpciones.Value;
    }

    public string GenerarToken(string username, string email)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAjustes.Secreto)),

        SecurityAlgorithms.HmacSha256
        );

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Name, username),
            new Claim(JwtRegisteredClaimNames.Email, email)
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtAjustes.Issuer,
            expires: DateTime.Now.AddMinutes(_jwtAjustes.ExpiracionEnMinutos),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}