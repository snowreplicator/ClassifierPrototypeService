using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prototype.ClassifierPrototypeService.Bll.Models;

public class Movie
{
    #region Fields and Properties
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    
    #endregion
    
    #region CTORs
    
    private Movie(string title, string genre, DateTime releaseDate)
    {
        Title = title;
        Genre = genre;
        ReleaseDate = releaseDate;
    }

    #endregion
    
    #region Create methods
    
    public static Movie CreateMovie(string title, string genre, DateTime releaseDate) 
        => new(title, genre,releaseDate);

    #endregion

}
