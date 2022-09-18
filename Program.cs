//Written by Andy Mahoney
//Last Updated 9/17/2022
//please modify connect strings, database names, and collection names before use
using MongoDB.Bson;
using MongoDB.Driver;

//Connect to DB with source Data
var FromSettings = MongoClientSettings.FromConnectionString("mongodb+srv://guest:defaultPass@mangadb.hrhudi3.mongodb.net/?retryWrites=true&w=majority");
FromSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
var FromClient = new MongoClient(FromSettings);
var FromDatabase = FromClient.GetDatabase("MangaDB");
IMongoCollection<BsonDocument> FromCollection = null;
Console.WriteLine("checking for user");
FromCollection = FromDatabase.GetCollection<BsonDocument>("sht135@gmail.com");
Console.WriteLine("got collection");
var documents = FromCollection.Find(new BsonDocument()).ToList();

//Connect to DB to insert data into
Console.WriteLine("Connecting to DB to send docs to");
var ToSettings = MongoClientSettings.FromConnectionString("mongodb+srv://guest:defaultPass@serverlessinstance.izekv.mongodb.net/?retryWrites=true&w=majority");
ToSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
var ToClient = new MongoClient(ToSettings);
var ToDatabase = ToClient.GetDatabase("MangaDB");
IMongoCollection<BsonDocument> ToCollection = null;
ToCollection = ToDatabase.GetCollection<BsonDocument>("sht135@gmail.com");
Console.WriteLine("Begininng Transfer...");
//Insert docs
foreach(var document in documents)
{
    Console.WriteLine(document);
    ToCollection.InsertOne(document);
}
Console.WriteLine("done");