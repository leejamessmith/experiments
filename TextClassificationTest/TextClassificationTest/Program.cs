// See https://aka.ms/new-console-template for more information
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.Data.Analysis;
using Microsoft.ML.TorchSharp;
using TextClassificationTest;

var mlContext = new MLContext();


var yelpReviews = DataLocator.EnsureDataSetDownloaded("yelp_labelled.txt");

var columnNames = new [] {"Text", "Sentiment"};

//var df = DataFrame.LoadCsvFromString(yelp_reviews, separator:'\t',header:false, columnNames:columnNames);

string data = File.ReadAllText(yelpReviews);

DataFrame? df = DataFrame.LoadCsvFromString(data, '\t', false, columnNames);

foreach (var r in df.Head(3).Rows)
{
    Console.WriteLine(r[0]);    
}

DataOperationsCatalog.TrainTestData trainTestSplit = mlContext.Data.TrainTestSplit(df, testFraction:0.2);

var pipeline =
    mlContext.Transforms.Conversion.MapValueToKey("Label","Sentiment")
        .Append(mlContext.MulticlassClassification.Trainers.TextClassification(1, sentence1ColumnName: "Text"))
        .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

var model = pipeline.Fit(trainTestSplit.TrainSet);

var predictionIDV = model.Transform(trainTestSplit.TestSet);

var columnsToSelect = new [] {"Text", "Sentiment", "PredictedLabel"};

var predictions = predictionIDV.ToDataFrame(columnsToSelect);

foreach (var r in predictions.Tail(3).Rows)
{
    Console.WriteLine(r[0]);    
}
