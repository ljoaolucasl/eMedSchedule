using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.Serialization;

namespace eMedSchedule.Tests.Common
{
    public class TestBase
    {
        public static DbUpdateException CreateDbUpdateException(string message)
        {
            var sqlException = (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));
            FieldInfo messageField = typeof(SqlException).GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic)!;

            messageField?.SetValue(sqlException, message);

            DbUpdateException dbUpdateException = (DbUpdateException)FormatterServices.GetUninitializedObject(typeof(DbUpdateException));
            FieldInfo innerExceptionField = typeof(Exception).GetField("_innerException", BindingFlags.Instance | BindingFlags.NonPublic);

            innerExceptionField?.SetValue(dbUpdateException, sqlException);
            return dbUpdateException;
        }

        public static Guid CadastrarUsuario()
        {
            Guid usuarioId = Guid.Parse("A8BC593B-5945-417B-3C2A-08DBE8775234");

            var sql =
                        @$"INSERT INTO ASPNETUSERS
				(
					ID,
					NAME,
					USERNAME,
					NORMALIZEDUSERNAME,
					EMAIL,
					NORMALIZEDEMAIL,
					EMAILCONFIRMED,
					PASSWORDHASH,
					SECURITYSTAMP,
					CONCURRENCYSTAMP,
					PHONENUMBER,
					PHONENUMBERCONFIRMED,
					TWOFACTORENABLED,
					LOCKOUTEND,
					LOCKOUTENABLED,
					ACCESSFAILEDCOUNT
				)
				VALUES
				(
					'{usuarioId}',
					'CAROLINA RECH',
					'CAROLINA',
					'CAROLINA',
					'CAROLINA@GMAIL.COM',
					'CAROLINA@GMAIL.COM',
					1,
					'AQAAAAEAACCQAAAAEL/NE00SPXPMU7SRDGSENWX7TKLQNMKI9AEYIDFGYKLGT1V6YFH+QEGZJMF5HVBN8G==',
					'FSNVOM5DIYV67KMJWQBDDIE3OSR57XTN',
					'CAB37435-2315-4C44-AB99-12EF2C7D91A4',
					NULL,
					0,
					0,
					NULL,
					1,
					0
				)";

            SqlConnection conexao = new SqlConnection(@"Data Source=(LocalDb)\MSSqlLocalDb;Initial Catalog=eMedScheduleDb-Tests;Integrated Security=True");
            conexao.Open();
            SqlCommand comando = new SqlCommand(sql, conexao);
            comando.ExecuteNonQuery();
            conexao.Close();
            return usuarioId;
        }

        public static void DeleteUser()
        {
            var sql = @$"DELETE FROM FK_TBDoctorActivity_TBDoctor;";

            sql = @$"DELETE FROM TBDoctorActivity;";

            sql += @$"DELETE FROM TBDoctor;";

            sql += @$"DELETE FROM ASPNETUSERS";

            SqlConnection conexao = new SqlConnection(@"Data Source=(LocalDb)\MSSqlLocalDb;Initial Catalog=eMedScheduleDb-Tests;Integrated Security=True");
            conexao.Open();
            SqlCommand comando = new SqlCommand(sql, conexao);
            comando.ExecuteNonQuery();
            conexao.Close();
        }
    }
}