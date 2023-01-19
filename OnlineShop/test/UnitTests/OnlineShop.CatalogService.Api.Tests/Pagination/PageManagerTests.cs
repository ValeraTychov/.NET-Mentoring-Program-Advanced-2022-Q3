using OnlineShop.CatalogService.Api.Pagination;

namespace OnlineShop.CatalogService.Api.Tests.Pagination;

[TestClass]
public class PageManagerTests
{
    [TestMethod]
    public void CretePage_DataCountExceedsThePageSize()
    {
        var data = Enumerable.Range(1, 12);
        var pageSize = 5;
        var pageManager = new PageManager<int>(data, pageSize);

        var pageOne = pageManager.CreatePage(1);
        Assert.AreEqual(1, pageOne.Number);
        Assert.AreEqual(3, pageOne.Total);
        CollectionAssert.AreEqual(Enumerable.Range(1, 5).ToList(), pageOne.Content);

        var pageTwo = pageManager.CreatePage(2);
        Assert.AreEqual(2, pageTwo.Number);
        Assert.AreEqual(3, pageTwo.Total);
        CollectionAssert.AreEqual(Enumerable.Range(6, 5).ToList(), pageTwo.Content);

        var pageThree = pageManager.CreatePage(3);
        Assert.AreEqual(3, pageThree.Number);
        Assert.AreEqual(3, pageThree.Total);
        CollectionAssert.AreEqual(Enumerable.Range(11, 2).ToList(), pageThree.Content);

        var pageFour = pageManager.CreatePage(4);
        Assert.AreEqual(4, pageFour.Number);
        Assert.AreEqual(3, pageFour.Total);
        CollectionAssert.AreEqual(Enumerable.Empty<int>().ToList(), pageFour.Content);
    }
}
