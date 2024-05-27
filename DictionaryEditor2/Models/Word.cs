using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DictionaryEditor.Models
{
    public class Word
    {
        [JsonProperty("Осетинское слово")]
        public string OssetianWord { get; set; }

        [JsonProperty("Значения")]
        public List<Meaning> Meanings { get; set; } = new List<Meaning>();

        [JsonProperty("Uniparsed")]
        public List<UniparsedItem> UniparsedItems { get; set; } = new List<UniparsedItem>();

    }
    public class Meaning
    {
        [JsonProperty("Переводы")]
        public List<string> Translations { get; set; } = new List<string>();

        [JsonProperty("Примеры")]
        public List<ExampleJSON> Examples { get; set; } = new List<ExampleJSON>();
    }

    public class ExampleJSON
    {
        [JsonProperty("Item1")]
        public string Item1 { get; set; }

        [JsonProperty("Item2")]
        public string Item2 { get; set; }
    }

    public class UniparsedItem
    {
        [JsonProperty("Лемма")]
        public string Lemma { get; set; }

        [JsonProperty("Граммматтические теги")]
        public string GrammaticalTags { get; set; }

        [JsonProperty("Gloss")]
        public string Gloss { get; set; }

        [JsonProperty("Английский перевод")]
        public string EnglishTranslation { get; set; }

        [JsonProperty("Русский перевод")]
        public string RussianTranslation { get; set; }
    }
}
