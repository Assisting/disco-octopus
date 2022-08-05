using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disco_octopus.HttpClients
{
    public class HawkingResponse
    {
        public List<DateGroup>? DateGroups { get; set; }
        public List<ParserOutput>? ParserOutputs { get; set;}
    }

    public class DateGroup
    {
        public string? SequenceType { get; set; }
        public string? Expression { get; set; }
        public string? RecurrentPeriod { get; set; }
        public string? RecurrentCount { get; set; }
    }

    public class ParserOutput
    {
        public int Id { get; set; }
        public HawkingDateRange? DateRange { get; set; }
        public string? ParserLabel { get; set; }
        public int? ParserStartIndex { get; set; }
        public string? Text { get; set; }
        public bool IsTimeZonePresent { get; set; }
        public bool IsExactTimePresent { get; set; }
        public string? TimezoneOffset { get; set; }
        public int ParserEndIndex { get; set; }
        public List<RecognizerOutput>? RecognizerOutputs { get; set; }
    }

    public class HawkingDateRange
    {
        public string? MatchType { get; set; }
        public string? Start { get; set; }
        public string? End { get; set; }
        public string? StartDateFormat { get; set; }
        public string? EndDateFormat { get; set; }
    }

    public class RecognizerOutput
    {
        public string? RecognizerLabel { get; set; }
        public int RecognizerStartIndex { get; set; }
        public int RecognizerEndIndex { get; set; }
        public string? Text { get; set; }
    }
}
