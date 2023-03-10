using System.Collections.Generic;
using System.Threading.Tasks;

using StarWars.WebApi.Proxy.Models;

namespace StarWars.ConsoleApp.Business
{
    /// <summary>
    /// A business logic layer for <see cref="CharacterModel">character</see>s.
    /// </summary>
    internal interface ICharacterBL
    {
        /// <summary>
        /// Gets all <see cref="CharacterModel">character</see>s.
        /// </summary>
        /// <returns>
        /// The <see cref="Task{IEnumerable{CharacterModel}}">task object</see> representing the
        /// asynchronous operation, returning a <see
        /// cref="IEnumerable{CharacterModel}">collection</see> of all <see
        /// cref="CharacterModel">character</see>s.
        /// </returns>
        Task<IEnumerable<CharacterModel>> GetAllAsync();

        /// <summary>
        /// Gets all <see cref="CharacterModel">character</see>s with the specified <paramref
        /// name="allegiance">allegiance</paramref>.
        /// </summary>
        /// <param name="allegiance">
        /// The <see cref="CharacterModel.Allegiance">allegiance</see> to filter for.
        /// </param>
        /// <returns>
        /// The <see cref="Task{IEnumerable{CharacterModel}}">task object</see> representing the
        /// asynchronous operation, returning a <see
        /// cref="IEnumerable{CharacterModel}">collection</see> of all <see
        /// cref="CharacterModel">character</see>s with the specified <paramref
        /// name="allegiance">allegiance</paramref>.
        /// </returns>
        Task<IEnumerable<CharacterModel>> GetAllByAllegianceAsync(Allegiance allegiance);

        /// <summary>
        /// Gets all <see cref="CharacterModel">character</see>s that are introduced in the
        /// specified <paramref name="trilogy">trilogy</paramref>.
        /// </summary>
        /// <param name="trilogy">
        /// The <see cref="CharacterModel.TrilogyIntroducedIn">trilogy</see> to filter for.
        /// </param>
        /// <returns>
        /// The <see cref="Task{IEnumerable{CharacterModel}}">task object</see> representing the
        /// asynchronous operation, returning a <see
        /// cref="IEnumerable{CharacterModel}">collection</see> of all <see
        /// cref="CharacterModel">character</see>s that are introduced in the specified
        /// <paramref name="trilogy">trilogy</paramref>.
        /// </returns>
        Task<IEnumerable<CharacterModel>> GetAllByTrilogyAsync(Trilogy trilogy);

        /*
        /// <summary>
        /// Gets all <see cref="CharacterModel">character</see>s that are Jedi.
        /// </summary>
        /// <returns>
        /// The <see cref="Task{IEnumerable{CharacterModel}}">task object</see> representing the
        /// asynchronous operation, returning a <see
        /// cref="IEnumerable{CharacterModel}">collection</see> of all <see
        /// cref="CharacterModel">character</see>s that are Jedi.
        /// </returns>
        Task<IEnumerable<CharacterModel>> GetAllJediAsync();
        */

        /*
        /// <summary>
        /// Gets the <see cref="CharacterModel">character</see> with the specified <paramref
        /// name="id">ID</paramref>.
        /// </summary>
        /// <param name="id">
        /// The <see cref="CharacterModel.Id">ID</see> to filter for.
        /// </param>
        /// <returns>
        /// The <see cref="Task{CharacterModel}">task object</see> representing the asynchronous
        /// operation, returning the <see cref="CharacterModel">character</see> with the
        /// specified <paramref name="id">ID</paramref>, or <c><see
        /// langword="null">null</see></c> if none exists.
        /// </returns>
        Task<CharacterModel> GetByIdAsync(int id);
        */

        /// <summary>
        /// Gets the first <see cref="CharacterModel">character</see> with the specified
        /// <paramref name="name">name</paramref>.
        /// </summary>
        /// <param name="name">
        /// The <see cref="CharacterModel.Name">name</see> to filter for.
        /// </param>
        /// <returns>
        /// The <see cref="Task{CharacterModel}">task object</see> representing the asynchronous
        /// operation, returning the first <see cref="CharacterModel">character</see> with the
        /// specified <paramref name="name">name</paramref>, or <c><see
        /// langword="null">null</see></c> if none is found.
        /// </returns>
        Task<CharacterModel> GetOneByNameAsync(string name);
    }
}
