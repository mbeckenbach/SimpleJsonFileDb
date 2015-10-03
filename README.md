# SimpleJsonFileDb

A simple file based JSON database, that uses a IList like api. Converts your model classes into JSON arrays using JSON.Net. 
The arrays are stored in a single JSON file per model class. All JSON files are cached at startup.
After every add, remove or update the cache will be renewed.

## Installation
Add my myGet feed to Visual Studio. 
You can browse it 


For VS 2015+:
```
 https://www.myget.org/F/mbeckenbach/api/v3/index.json
```

For VS 2012+:
```
https://www.myget.org/F/mbeckenbach/api/v2
```

Then install the Nuget package:
```
PM> Install-Package SimpleJsonFileDb 
```

## Usage

Add a new class to your project, that you can use simliar to the Entity Framework dbContext. 
(Similar, but you will never need SaveChanges())

```c#
using SimpleJsonFileDb;
using MyProject.MyModels;

namespace MyProject.MyModels
{
    public class DemoDbContext : SimpleDbContext
    {
        // use "../../" to ovoid that your data files are stored in the /bin directory
        public static _dataDirectory = "../../MyDataDirectory";

        public DemoDbContext()
            :base(_dataDirectory) 
        {
        }

        // Add your entities similar to Entity Framework DbSet<>
        // TestModel is an example of your model class. Could be anything.
        public JsonFile<TestModel> SomeTestData { get; set; }
        public JsonFile<TestModel2> MoreTestData { get; set; }

        // Now init each JsonFile property
        // This is required to get existing data after application restart
        public override void InitiateJsonFiles()
        {
            this.SomeTestData = new JsonFile<TestModel>(this.dataDirectory);
            this.MoreTestData = new JsonFile<TestModel2>(this.dataDirectory);
        }
    }
}
```

Now anywhere in your application use this to access your db:

```c#

// Get instance of your db
var db = new TestDbContext();

// Add some data
db.SomeTestData.Add(new TestModel { MyTest = "Foo "});

// Get some data
var data = db.SomeTestData.FirstOrDefault(x => x.MyTest == "Foo");

// Delete some data
db.SomeTestData.Remove(data);

// Use most of your favorite IList<T> methods :-)

```