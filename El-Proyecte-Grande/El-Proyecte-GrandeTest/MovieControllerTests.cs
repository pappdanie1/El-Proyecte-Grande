using El_Proyecte_Grande.Controllers;
using El_Proyecte_Grande.Services;

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
    
    
}