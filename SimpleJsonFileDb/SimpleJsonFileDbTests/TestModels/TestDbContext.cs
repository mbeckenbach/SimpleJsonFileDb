﻿using SimpleJsonFileDb;
using SimpleJsonFileDbContext.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJsonFileDbTests.TestModels
{
    public class TestDbContext : SimpleDbContext
    {
        public TestDbContext()
            :base(JsonFileTests._testDataFileDir)
        {
        }

        public JsonFile<TestModel> SomeTestData { get; set; }
        public JsonFile<TestModel2> MoreTestData { get; set; }

        public override void InitiateJsonFiles()
        {
            this.SomeTestData = new JsonFile<TestModel>(this.dataDirectory);
            this.MoreTestData = new JsonFile<TestModel2>(this.dataDirectory);
        }
    }
}
