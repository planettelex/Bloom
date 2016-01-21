using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloom.Data.Interfaces;

namespace Bloom.State.Data.Tables
{
    public class SuiteStateTable : ISqlTable
    {
        public string CreateSql
        {
            get
            {
                return "CREATE TABLE \"suite_state\" (" +
                       "\"last_process_access\" VARCHAR PRIMARY KEY NOT NULL UNIQUE) ";
            }
        }
    }
}
