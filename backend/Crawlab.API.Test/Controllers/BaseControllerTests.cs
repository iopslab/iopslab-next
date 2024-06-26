using Microsoft.Extensions.Logging;
using Moq;
using Crawlab.API.Controllers;
using Crawlab.DB;
using Crawlab.Model;
using Crawlab.Model.Models;

namespace Crawlab.API.Test.Controllers;

public class BaseControllerTests
{
    private ILogger<NodeController> _logger;
    private NodeController _controller;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<NodeController>>().Object;
        var factory = new CrawlabDbContextFactory();
        var dbContext = factory.CreateDbContext(Array.Empty<string>());
        _controller = new NodeController(_logger, dbContext);
    }

    [Test, Order(1)]
    public void Create()
    {
        var result = _controller.Create(new Node());

        Assert.That(result, Is.Not.Null);
    }

    [Test, Order(2)]
    public void GetList()
    {
        var result = _controller.GetList();
        var enumerable = result as Node[] ?? result.ToArray();

        Assert.That(enumerable, Is.Not.Null);
        Assert.That(enumerable, Is.InstanceOf<Node[]>());
        Assert.That(enumerable, Is.Not.Empty);
    }

    [Test, Order(2)]
    public void Get()
    {
        var result = _controller.GetList();
        var enumerable = result as Node[] ?? result.ToArray();
        var node = enumerable.First();

        var res = _controller.Get(node.Id);

        Assert.That(res, Is.EqualTo(node));
    }

    [Test, Order(3)]
    public void Update()
    {
        var result = _controller.GetList();
        var enumerable = result as Node[] ?? result.ToArray();
        var node = enumerable.First();

        node.Enabled = !node.Enabled;
        var res = _controller.Update(node);

        Assert.That(res, Is.EqualTo(node));
    }

    [Test, Order(4)]
    public void Delete()
    {
        var result = _controller.GetList();
        var enumerable = result as Node[] ?? result.ToArray();

        foreach (var node in enumerable)
        {
            _controller.Delete(node.Id);
        }

        result = _controller.GetList();
        enumerable = result as Node[] ?? result.ToArray();

        Assert.That(enumerable, Is.Empty);
    }
}