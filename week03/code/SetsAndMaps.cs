using System.Text.Json;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE

        //1. create a string vairable that will hold the string to be tested.
        HashSet<string> seenWords = new HashSet<string>();

        //2. Hashset for faster lookups
        HashSet<string> wordSet = new HashSet<string>(words); 

        //3. a list to store the found pairs
        List<string> pairs = new List<string>();

        //4. for loop to iterate though each word in the words array
        foreach (string word in words)
        {
            string reversedWord = new string(word.Reverse().ToArray());//create revesedWord by reversing the current word

            //5. checking for symmetry
            if(wordSet.Contains(reversedWord) && word != reversedWord) //if reversedWord is present in words array, and not same as original word, 
            {   
                //then check if neither word nor reversedWord has been processed before.
                if (!seenWords.Contains(word) && !seenWords.Contains(reversedWord))
                {
                    pairs.Add($"{word} & {reversedWord}");
                    seenWords.Add(word);
                    seenWords.Add(reversedWord);
                }
            }
        }
        //6. return pairs list containing the found symmetric pairs
        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            //1. ensure there are at least 4 fields.
            if (fields.Length >= 4)
            {
                string degree = fields[3].Trim();//2. this extracts degree from 4th field

                if (!degrees.ContainsKey(degree))
                {
                    degrees[degree] = 1; //3. if degree is not in dictionary, add it with count 1.
                }
                else 
                {
                    degrees[degree]++; //4. increment the count for existing degrees
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE
        // 1. cleaning the input strings
        word1 = word1.ToLower().Replace(" ", "");
        word2 = word2.ToLower().Replace(" ","");

        // 2. check for lenght mismatch
        if (word1.Length != word2.Length)
        {
            return false; //anagrams must have the same length.
        }

        // 3. Create a dictionary to count character frequencies in word1
        Dictionary<char, int> charCounts1 = new Dictionary<char, int>();
        foreach (char c in word1)
        {
            if (charCounts1.ContainsKey(c))
            {
                charCounts1[c]++;
            }
            else
            {
                charCounts1[c] = 1;
            }
        }

        // 4. Check if word2 has the same character frequencies
        foreach (char c in word2)
        {
            if (!charCounts1.ContainsKey(c)) 
            {
                return false; // Character not found in word1
            }
            charCounts1[c]--; 
            if (charCounts1[c] < 0) 
            {
                return false; // More occurrences of the character in word2 than in word1
            }
        }

        // 5. Check character frequency counts
        foreach (var kvp in charCounts1)
        {
            if (kvp.Value != 0)
            {
                return false;
            }
        }
        return true; 
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        var summaries = featureCollection.Features
            .Select(f => $"Location: {f.Properties.Place} - Mag {f.Properties.Mag}")
            .ToArray();
        // 3. Return an array of these string descriptions.
        return summaries;
    }
}