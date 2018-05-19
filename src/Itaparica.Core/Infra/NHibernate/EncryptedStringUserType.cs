using System;
using System.Data.Common;
using Itaparica.Core.Infra.Services;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Itaparica.Core.Infra.NHibernate
{
    /// <summary>
    /// NHibernate User Type para string criptografada
    /// </summary>
    public class EncryptedStringUserType : AbstractStringType
    {
        private readonly TripleDESEncryptionService encryptionService;

        public EncryptedStringUserType() : base(new StringSqlType())
        {
            encryptionService = new TripleDESEncryptionService();
        }

        public override string Name => "EncryptedString";

        public override void Set(DbCommand dbCommand, object value, int index, ISessionImplementor session)
        {
            string encrypted = encryptionService.Encrypt(Convert.ToString(value));

            base.Set(dbCommand, encrypted, index, session);
        }

        public override object Get(DbDataReader dbDataReader, int index, ISessionImplementor session)
        {
            string value = Convert.ToString(base.Get(dbDataReader, index, session));

            return encryptionService.Decrypt(value);
        }

    }
}