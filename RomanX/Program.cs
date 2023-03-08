using RomanCalculator;

var roman = new Calculator();
var testString1 = "(MMMDCCXXIV - MMCCXXIX) * II";
var testString2 = "(MMMDCCLXXIV - MMCCLXXIX) * ILI"; //line with exception
var testString3 = "(MMDCCXXIV - MMCCXXI) * VII";
var testString4 = "VVV / "; //line with exception
var testString5 = "I /III";
Console.WriteLine(roman.Evaluate(testString1));
Console.WriteLine(roman.Evaluate(testString3));
Console.WriteLine(roman.Evaluate(testString5));
try
{
    Console.WriteLine(roman.Evaluate(testString2));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
try
{
    Console.WriteLine(roman.Evaluate(testString4));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
