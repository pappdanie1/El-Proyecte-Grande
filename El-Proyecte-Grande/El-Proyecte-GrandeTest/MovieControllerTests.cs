using El_Proyecte_Grande.Controllers;
using El_Proyecte_Grande.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace El_Proyecte_GrandeTest;


[TestFixture]
public class MovieControllerTests
{
    private Mock<IMovieRepository> _mockMovieRepository;
    private MovieController _movieController;
    
    [SetUp]
    public void Setup()
    {
        _mockMovieRepository = new Mock<IMovieRepository>();
        _movieController = new MovieController(_mockMovieRepository.Object);
    }

    [Test]
    public void GetAllTestNotNull()
    {
        _mockMovieRepository.Setup(x => x.GetAll());

        var result = _movieController.GetAll();
        
        Assert.That(result, Is.Not.Null);
    }

    /*[Test]
    public  void GetAllFailsWithStatusCode()
    {
        // Arrange
        
        _mockMovieRepository.Setup(x => x.GetAll()).Throws(new Exception("Test Exception"));
        var movieController = new MovieController(_mockMovieRepository.Object);

        // Act
        var result = movieController.GetAll() as ObjectResult;
        
        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        Assert.AreEqual("Test Exception", result.Value);
    }*/

    [Test]
    public void GetMovieByIdFound()
    {
        int id = 1011985;
        _mockMovieRepository.Setup(x => x.GetById(id));

        var result = _movieController.GetMovieById(id);

        if (result.Value != null) Assert.That(result.Value.Id, Is.EqualTo(id));
    }

    [Test]
    public void GetMovieByIdFailed()
    {
        int id = 0;
        _mockMovieRepository.Setup(x => x.GetById(id));

        var result = _movieController.GetMovieById(id);
        
        //Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);
        Assert.That(result.Value, Is.Null);
    }
}