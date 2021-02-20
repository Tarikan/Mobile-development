using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSPLabWork.Models
{
    public class CachedRequest
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; }

        public string Request { get; set; }

        public string ResponseBody { get; set; }
    }
}
