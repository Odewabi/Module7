using System;
using System.Collections.Generic;
using System.IO;

class GradeProcessor
{
    static void Main(string[] args)
    {
        string filePath = "grades.txt";

        try
        {
            Console.WriteLine("Processing grades...");
            List<double> grades = ReadGradesFromFile(filePath);
            double average = CalculateAverage(grades);

            Console.WriteLine("\nSummary:");
            Console.WriteLine($"Total grades processed: {grades.Count + _invalidCount}");
            Console.WriteLine($"Valid grades: {grades.Count}");
            Console.WriteLine($"Average grade: {average:F2}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: File not found.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error: An I/O error occurred. {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

    private static int _invalidCount = 0;

    /// <summary>
    /// Reads grades from a file and validates each line.
    /// </summary>
    /// <param name="filePath">Path to the file containing grades.</param>
    /// <returns>List of valid grades.</returns>
    private static List<double> ReadGradesFromFile(string filePath)
    {
        List<double> grades = new List<double>();
        int lineNumber = 0;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                try
                {
                    double grade = ValidateAndConvertGrade(line);
                    grades.Add(grade);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Error on line {lineNumber}: Invalid grade format '{line}'");
                    _invalidCount++;
                }
            }
        }

        return grades;
    }

    /// <summary>
    /// Validates and converts a grade from string to double.
    /// </summary>
    /// <param name="gradeStr">The grade as a string.</param>
    /// <returns>The grade as a double.</returns>
    private static double ValidateAndConvertGrade(string gradeStr)
    {
        if (double.TryParse(gradeStr, out double grade))
        {
            if (grade >= 0 && grade <= 100)
            {
                return grade;
            }
            throw new FormatException("Grade out of range.");
        }
        throw new FormatException("Invalid grade format.");
    }

    /// <summary>
    /// Calculates the average of a list of grades.
    /// </summary>
    /// <param name="grades">The list of grades.</param>
    /// <returns>The average grade.</returns>
    private static double CalculateAverage(List<double> grades)
    {
        if (grades.Count == 0)
        {
            Console.WriteLine("No valid grades to calculate an average.");
            return 0;
        }

        double total = 0;
        foreach (double grade in grades)
        {
            total += grade;
        }

        return total / grades.Count;
    }
}