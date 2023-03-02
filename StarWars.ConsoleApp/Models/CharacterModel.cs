using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StarWars.ConsoleApp.Models
{
    /// <summary>
    /// Represents the possible allegiances.
    /// </summary>
    /// <remarks>
    /// The value is de/serialized from/to a JSON string value.
    /// </remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Allegiance
    {
        /// <summary>
        /// Represents no allegiance.
        /// </summary>
        [EnumMember(Value = "None")]
        None = 0,

        /// <summary>
        /// Represents allegiance to the Alliance to Restore the Republic, commonly known as the
        /// Rebel Alliance, its formative resistance groups, or its successor state, the New
        /// Republic.
        /// </summary>
        [EnumMember(Value = "Rebellion")]
        Rebellion,

        /// <summary>
        /// Represents allegiance to the Galactic Empire, the Imperial Remnant, or its successor
        /// military junta, the First Order.
        /// </summary>
        [EnumMember(Value = "Empire")]
        Empire,
    }

    /// <summary>
    /// Represents the possible trilogies.
    /// </summary>
    /// <remarks>
    /// The value is de/serialized from/to a JSON string value.
    /// </remarks>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Trilogy
    {
        /// <summary>
        /// Represents no trilogy.
        /// </summary>
        [EnumMember(Value = "None")]
        None = 0,

        /// <summary>
        /// Represents the original trilogy: Episodes IV "A New Hope", V "The Empire Strikes
        /// Back", and VI "Return of the Jedi".
        /// </summary>
        [EnumMember(Value = "Original")]
        Original,

        /// <summary>
        /// Represents the prequel trilogy: Episodes I "The Phantom Menace", II "Attack of the
        /// Clones", and III "Revenge of the Sith".
        /// </summary>
        [EnumMember(Value = "Prequel")]
        Prequel,

        /// <summary>
        /// Represents the sequel trilogy: Episodes VII "The Force Awakens", VIII "The Last
        /// Jedi", and IX "The Rise of Skywalker".
        /// </summary>
        [EnumMember(Value = "Sequel")]
        Sequel,
    }

    /// <summary>
    /// Represents a character in the Star Wars universe.
    /// </summary>
    public class CharacterModel
    {
        /// <summary>
        /// Gets or sets the character's allegiance.
        /// </summary>
        /// <value>
        /// An <see cref="Allegiance">allegiance</see> from the set of options.
        /// </value>
        public Allegiance Allegiance { get; set; }

        /// <summary>
        /// Gets or sets the character's ID.
        /// </summary>
        /// <value>
        /// A unique <see cref="int">integer</see> assigned by the database.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets whether is a Jedi.
        /// </summary>
        /// <value>
        /// A <see cref="bool">boolean</see> value indicating if the character is a Jedi.
        /// </value>
        public bool IsJedi { get; set; }

        /// <summary>
        /// Gets or sets the character's name.</summary>
        /// <value>
        /// A unique unicode <see cref="string">string</see>, maximum 850 characters.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the trilogy the character was introduced in.
        /// </summary>
        /// <value>
        /// An <see cref="Trilogy">trilogy</see> from the set of options.
        /// </value>
        public Trilogy TrilogyIntroducedIn { get; set; }
    }
}
